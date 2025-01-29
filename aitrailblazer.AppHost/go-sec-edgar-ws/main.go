package main

import (
	"encoding/json"
	"fmt"
	"log"
	"net/http"
	"sort"
	"time"

	"github.com/aitrailblazer/aitrailblazer/go-sec-edgar-ws/company_info"
	"github.com/aitrailblazer/aitrailblazer/go-sec-edgar-ws/filings"
)

// measureSpeedMiddleware logs execution time for each request
func measureSpeedMiddleware(handler http.HandlerFunc) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		start := time.Now()
		handler(w, r)
		duration := time.Since(start)
		log.Printf("Request to %s took %v", r.URL.Path, duration)
	}
}

// rootHandler handles GET requests to "/"
func rootHandler(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodGet {
		http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	w.WriteHeader(http.StatusOK)
	if _, err := w.Write([]byte("Hello")); err != nil {
		log.Printf("Error writing response: %v", err)
		http.Error(w, "Internal server error", http.StatusInternalServerError)
	}
}

// companyInfoHandler serves company data
func companyInfoHandler(ci *company_info.CompanyInfo) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
			return
		}

		ticker := r.URL.Query().Get("ticker")

		// Return all companies if no ticker is provided
		if ticker == "" {
			df := ci.ToDataFrame()
			companies := make([]map[string]string, len(df["ticker"]))

			for i := range df["ticker"] {
				companies[i] = map[string]string{
					"cik":      fmt.Sprintf("%v", df["cik"][i]),
					"name":     fmt.Sprintf("%v", df["name"][i]),
					"ticker":   fmt.Sprintf("%v", df["ticker"][i]),
					"exchange": fmt.Sprintf("%v", df["exchange"][i]),
				}
			}

			w.Header().Set("Content-Type", "application/json")
			if err := json.NewEncoder(w).Encode(companies); err != nil {
				log.Printf("Error encoding response: %v", err)
				http.Error(w, "Failed to encode response", http.StatusInternalServerError)
			}
			return
		}

		// Fetch single company info
		cik, err := ci.GetCIKByTicker(ticker)
		if err != nil {
			log.Printf("Error fetching CIK for ticker %s: %v", ticker, err)
			http.Error(w, err.Error(), http.StatusNotFound)
			return
		}

		response := map[string]string{
			"ticker":   ticker,
			"cik":      cik,
			"name":     ci.GetCompanyNameByTicker(ticker),
			"exchange": ci.GetCompanyExchangeByTicker(ticker),
		}

		w.Header().Set("Content-Type", "application/json")
		if err := json.NewEncoder(w).Encode(response); err != nil {
			log.Printf("Error encoding response: %v", err)
			http.Error(w, "Failed to encode response", http.StatusInternalServerError)
		}
	}
}

// SEC Filings Handler
func secFilingsHandler(ci *company_info.CompanyInfo, sf *filings.SECFilings) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
			return
		}

		ticker := r.URL.Query().Get("ticker")
		if ticker == "" {
			http.Error(w, "Ticker is required", http.StatusBadRequest)
			return
		}

		// Fetch CIK
		cik, err := ci.GetCIKByTicker(ticker)
		if err != nil {
			log.Printf("[ERROR] Error fetching CIK for ticker %s: %v", ticker, err)
			http.Error(w, err.Error(), http.StatusNotFound)
			return
		}

		// Fetch filings from SEC
		filings, err := sf.GetCompanyFilings(cik)
		if err != nil {
			log.Printf("[ERROR] Error fetching filings for CIK %s: %v", cik, err)
			http.Error(w, err.Error(), http.StatusInternalServerError)
			return
		}

		// Convert filings to structured format
		dataFrame, err := sf.FilingsToDataFrame(filings)
		if err != nil {
			log.Printf("[ERROR] Error processing filings for CIK %s: %v", cik, err)
			http.Error(w, err.Error(), http.StatusInternalServerError)
			return
		}

		// Pretty-print JSON response
		responseJSON, err := json.MarshalIndent(dataFrame, "", "  ")
		if err != nil {
			log.Printf("[ERROR] Error encoding response: %v", err)
			http.Error(w, "Failed to encode response", http.StatusInternalServerError)
			return
		}

		// Send JSON response
		w.Header().Set("Content-Type", "application/json")
		w.Write(responseJSON)
	}
}

// getAvailableForms handles the route /forms/{ticker}
func getAvailableForms(ci *company_info.CompanyInfo, sf *filings.SECFilings) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
			return
		}

		ticker := r.URL.Query().Get("ticker")
		if ticker == "" {
			http.Error(w, "Ticker is required", http.StatusBadRequest)
			return
		}

		cik, err := ci.GetCIKByTicker(ticker)
		if err != nil {
			log.Printf("[ERROR] Error fetching CIK for ticker %s: %v", ticker, err)
			http.Error(w, err.Error(), http.StatusNotFound)
			return
		}

		filings, err := sf.GetCompanyFilings(cik)
		if err != nil {
			log.Printf("[ERROR] Error fetching filings for CIK %s: %v", cik, err)
			http.Error(w, err.Error(), http.StatusInternalServerError)
			return
		}

		// Debug Full Response
		responseJSON, _ := json.MarshalIndent(filings, "", "  ")
		log.Printf("[DEBUG] Full SEC Response for %s:\n%s", cik, string(responseJSON))

		// Validate "filings" key
		filingsData, filingsExists := filings["filings"].(map[string]interface{})
		if !filingsExists {
			log.Printf("[ERROR] 'filings' key missing or invalid for CIK %s", cik)
			http.Error(w, "Failed to parse filings data", http.StatusInternalServerError)
			return
		}

		// Validate "recent" key
		recentRaw, recentExists := filingsData["recent"]
		if !recentExists {
			log.Printf("[ERROR] 'recent' key missing for CIK %s", cik)
			http.Error(w, "Failed to parse filings data", http.StatusInternalServerError)
			return
		}

		// Ensure "recent" is a map
		recentMap, ok := recentRaw.(map[string]interface{})
		if !ok {
			log.Printf("[ERROR] 'recent' key is not a map for CIK %s", cik)
			http.Error(w, "Unexpected response format", http.StatusInternalServerError)
			return
		}

		// Extract forms from recent
		formList, formExists := recentMap["form"].([]interface{})
		if !formExists {
			log.Printf("[ERROR] 'form' key missing or invalid in 'recent' for CIK %s", cik)
			http.Error(w, "Failed to parse filings data", http.StatusInternalServerError)
			return
		}

		uniqueForms := make(map[string]bool)
		for _, form := range formList {
			if formStr, ok := form.(string); ok {
				uniqueForms[formStr] = true
			} else {
				log.Printf("[WARNING] Unexpected type for form: %T", form)
			}
		}

		// Convert to sorted slice
		forms := make([]string, 0, len(uniqueForms))
		for form := range uniqueForms {
			forms = append(forms, form)
		}
		sort.Strings(forms) // 🔥 Sort forms alphabetically

		// Log extracted forms
		if len(forms) == 0 {
			log.Printf("[WARNING] No valid form types found for CIK %s", cik)
		} else {
			log.Printf("[INFO] Extracted & Sorted Form Types for CIK %s: %+v", cik, forms)
		}

		// Return prettified JSON response
		response := map[string]interface{}{
			"ticker": ticker,
			"forms":  forms,
		}
		prettyJSON, _ := json.MarshalIndent(response, "", "  ")
		w.Header().Set("Content-Type", "application/json")
		w.Write(prettyJSON)
	}
}

var companyInfo *company_info.CompanyInfo

func main() {
	var err error
	companyInfo, err = company_info.NewCompanyInfo("./data/company_tickers_exchange.json")
	if err != nil {
		log.Fatalf("Failed to initialize CompanyInfo: %v", err)
	}

	secFilings := filings.NewSECFilings("FinanceDataCorp your-email@example.com")

	// Register handlers with middleware
	http.HandleFunc("/", measureSpeedMiddleware(rootHandler))
	http.HandleFunc("/company-info", measureSpeedMiddleware(companyInfoHandler(companyInfo)))
	http.HandleFunc("/sec-filings", measureSpeedMiddleware(secFilingsHandler(companyInfo, secFilings)))
	http.HandleFunc("/forms", measureSpeedMiddleware(getAvailableForms(companyInfo, secFilings)))

	port := "8001"
	fmt.Printf("Starting server on 0.0.0.0:%s...\n", port)
	log.Fatal(http.ListenAndServe("0.0.0.0:"+port, nil))
}
