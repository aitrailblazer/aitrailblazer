package main

import (
	"encoding/json"
	"fmt"
	"log"
	"net/http"
	"strings"

	"your-module-path/company_info"
)

// RequestBody defines the structure of the incoming JSON payload
type RequestBody struct {
	HTML string `json:"html"`
}

// rootHandler handles GET requests to the root "/" path
func rootHandler(w http.ResponseWriter, r *http.Request) {
	if r.Method == http.MethodGet {
		w.WriteHeader(http.StatusOK)
		_, err := w.Write([]byte("Hello"))
		if err != nil {
			http.Error(w, "Failed to write response", http.StatusInternalServerError)
		}
	} else {
		http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
	}
}

// companyInfoHandler handles requests for company information
func companyInfoHandler(ci *company_info.CompanyInfo) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		ticker := r.URL.Query().Get("ticker")
		if ticker == "" {
			http.Error(w, "Ticker is required", http.StatusBadRequest)
			return
		}

		cik, err := ci.GetCIKByTicker(ticker)
		if err != nil {
			http.Error(w, err.Error(), http.StatusNotFound)
			return
		}

		response := map[string]string{"ticker": ticker, "cik": cik}
		w.Header().Set("Content-Type", "application/json")
		json.NewEncoder(w).Encode(response)
	}
}

func main() {
	// Initialize CompanyInfo
	companyInfo, err := company_info.NewCompanyInfo("path/to/your/json/file.json")
	if err != nil {
		log.Fatalf("Failed to initialize CompanyInfo: %v", err)
	}

	// Register the root and /html-to-pdf handlers
	http.HandleFunc("/", rootHandler)
	http.HandleFunc("/company-info", companyInfoHandler(companyInfo))

	port := "0.0.0.0:8001"
	fmt.Printf("Starting server on %s...\n", port)
	log.Fatal(http.ListenAndServe(port, nil))
}
