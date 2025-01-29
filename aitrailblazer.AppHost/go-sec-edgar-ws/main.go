package main

import (
	"encoding/json"
	"fmt"
	"log"
	"net/http"
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

// secFilingsHandler serves SEC filings data
func secFilingsHandler(sf *filings.SECFilings) http.HandlerFunc {
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

		cik, err := companyInfo.GetCIKByTicker(ticker)
		if err != nil {
			log.Printf("Error fetching CIK for ticker %s: %v", ticker, err)
			http.Error(w, err.Error(), http.StatusNotFound)
			return
		}

		filings, err := sf.GetCompanyFilings(cik)
		if err != nil {
			log.Printf("Error fetching filings for CIK %s: %v", cik, err)
			http.Error(w, err.Error(), http.StatusInternalServerError)
			return
		}

		w.Header().Set("Content-Type", "application/json")
		if err := json.NewEncoder(w).Encode(filings); err != nil {
			log.Printf("Error encoding response: %v", err)
			http.Error(w, "Failed to encode response", http.StatusInternalServerError)
		}
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
			log.Printf("Error fetching CIK for ticker %s: %v", ticker, err)
			http.Error(w, err.Error(), http.StatusNotFound)
			return
		}

		filings, err := sf.GetCompanyFilings(cik)
		if err != nil {
			log.Printf("Error fetching filings for CIK %s: %v", cik, err)
			http.Error(w, err.Error(), http.StatusInternalServerError)
			return
		}

		uniqueForms := make(map[string]bool)
		for _, filing := range filings {
			uniqueForms[filing.Form] = true
		}

		forms := make([]string, 0, len(uniqueForms))
		for form := range uniqueForms {
			forms = append(forms, form)
		}

		response := map[string]interface{}{
			"ticker": ticker,
			"forms":  forms,
		}

		w.Header().Set("Content-Type", "application/json")
		if err := json.NewEncoder(w).Encode(response); err != nil {
			log.Printf("Error encoding response: %v", err)
			http.Error(w, "Failed to encode response", http.StatusInternalServerError)
		}
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
	http.HandleFunc("/sec-filings", measureSpeedMiddleware(secFilingsHandler(secFilings)))
	http.HandleFunc("/forms", measureSpeedMiddleware(getAvailableForms(companyInfo, secFilings)))

	port := "8001"
	fmt.Printf("Starting server on 0.0.0.0:%s...\n", port)
	log.Fatal(http.ListenAndServe("0.0.0.0:"+port, nil))
}
