package filings

import (
	"encoding/json"
	"fmt"
	"io"
	"log"
	"net/http"
	"strings"

	"github.com/shopspring/decimal"
)

// SECFilings represents the SEC filings handler
type SECFilings struct {
	Email   string
	Headers map[string]string
	Client  *http.Client
}

// NewSECFilings initializes SECFilings with a user email and HTTP client
func NewSECFilings(email string) *SECFilings {
	headers := map[string]string{
		"User-Agent": email,
		"Accept":     "application/json",
	}
	return &SECFilings{
		Email:   email,
		Headers: headers,
		Client:  &http.Client{},
	}
}

// GetCompanyFilings fetches SEC filings and logs request/response details
func (s *SECFilings) GetCompanyFilings(cik string) (map[string]interface{}, error) {
	url := fmt.Sprintf("https://data.sec.gov/submissions/CIK%s.json", strings.Repeat("0", 10-len(cik))+cik)

	req, err := http.NewRequest("GET", url, nil)
	if err != nil {
		return nil, fmt.Errorf("failed to create request for CIK %s: %v", cik, err)
	}
	s.setHeaders(req)

	log.Printf("Fetching SEC filings for CIK: %s", cik)
	log.Printf("Request URL: %s", url)
	log.Printf("Request Headers: %v", req.Header)

	resp, err := s.Client.Do(req)
	if err != nil {
		return nil, fmt.Errorf("failed to fetch data for CIK %s: %v", cik, err)
	}
	defer resp.Body.Close()

	log.Printf("Response Status: %d", resp.StatusCode)
	if resp.StatusCode != http.StatusOK {
		return nil, fmt.Errorf("failed to fetch data for CIK %s: %s", cik, resp.Status)
	}

	body, err := io.ReadAll(resp.Body)
	if err != nil {
		return nil, fmt.Errorf("failed to read response body for CIK %s: %v", cik, err)
	}

	//log.Printf("Response Body (First 500 chars): %s", truncateString(string(body), 500))

	var filings map[string]interface{}
	if err := json.Unmarshal(body, &filings); err != nil {
		return nil, fmt.Errorf("failed to decode response for CIK %s: %v", cik, err)
	}

	return filings, nil
}

// FilingsToDataFrame converts SEC filings data into a structured format
func (s *SECFilings) FilingsToDataFrame(filings map[string]interface{}) ([]map[string]interface{}, error) {
	recentFilings, ok := filings["filings"].(map[string]interface{})["recent"].([]interface{})
	if !ok {
		return nil, fmt.Errorf("failed to parse recent filings")
	}

	var dataFrame []map[string]interface{}
	for _, filing := range recentFilings {
		if filingMap, ok := filing.(map[string]interface{}); ok {
			dataFrame = append(dataFrame, filingMap)
		} else {
			return nil, fmt.Errorf("failed to parse filing")
		}
	}

	return dataFrame, nil
}

// DownloadHTMLContent fetches the raw HTML content of a filing
func (s *SECFilings) DownloadHTMLContent(cik, accessionNumber, fileName string) (string, error) {
	url := fmt.Sprintf("https://www.sec.gov/Archives/edgar/data/%s/%s/%s", cik, accessionNumber, fileName)

	req, err := http.NewRequest("GET", url, nil)
	if err != nil {
		return "", fmt.Errorf("failed to create request for HTML content: %v", err)
	}
	s.setHeaders(req)

	log.Printf("Downloading SEC filing HTML for CIK: %s, Accession: %s, File: %s", cik, accessionNumber, fileName)

	resp, err := s.Client.Do(req)
	if err != nil {
		return "", fmt.Errorf("failed to download HTML content: %v", err)
	}
	defer resp.Body.Close()

	if resp.StatusCode != http.StatusOK {
		return "", fmt.Errorf("failed to download HTML content: %s", resp.Status)
	}

	body, err := io.ReadAll(resp.Body)
	if err != nil {
		return "", fmt.Errorf("failed to read HTML content: %v", err)
	}

	return string(body), nil
}

// GetXBRLData fetches XBRL (financial) data for a given CIK
func (s *SECFilings) GetXBRLData(cik string) (map[string]interface{}, error) {
	url := fmt.Sprintf("https://data.sec.gov/api/xbrl/companyfacts/CIK%s.json", strings.Repeat("0", 10-len(cik))+cik)

	req, err := http.NewRequest("GET", url, nil)
	if err != nil {
		return nil, fmt.Errorf("failed to create request for XBRL data: %v", err)
	}
	s.setHeaders(req)

	log.Printf("Fetching XBRL data for CIK: %s", cik)

	resp, err := s.Client.Do(req)
	if err != nil {
		return nil, fmt.Errorf("failed to fetch XBRL data for CIK %s: %v", cik, err)
	}
	defer resp.Body.Close()

	if resp.StatusCode != http.StatusOK {
		return nil, fmt.Errorf("failed to fetch XBRL data for CIK %s: %s", cik, resp.Status)
	}

	var xbrlData map[string]interface{}
	if err := json.NewDecoder(resp.Body).Decode(&xbrlData); err != nil {
		return nil, fmt.Errorf("failed to decode XBRL data for CIK %s: %v", cik, err)
	}

	return xbrlData, nil
}

// ProcessXBRLData extracts and processes XBRL facts based on a concept and unit
func (s *SECFilings) ProcessXBRLData(xbrlData map[string]interface{}, concept, unit string) ([]map[string]interface{}, error) {
	facts, ok := xbrlData["facts"].(map[string]interface{})["us-gaap"].(map[string]interface{})[concept].(map[string]interface{})["units"].(map[string]interface{})[unit].([]interface{})
	if !ok {
		return nil, fmt.Errorf("concept '%s' or unit '%s' not found in XBRL data", concept, unit)
	}

	var dataFrame []map[string]interface{}
	for _, fact := range facts {
		factMap, ok := fact.(map[string]interface{})
		if !ok {
			return nil, fmt.Errorf("failed to parse XBRL fact")
		}

		// Convert float values to decimal for better precision
		if val, ok := factMap["val"].(float64); ok {
			factMap["val"] = decimal.NewFromFloat(val).String()
		}

		dataFrame = append(dataFrame, factMap)
	}

	return dataFrame, nil
}

// Helper function to set headers in a request
func (s *SECFilings) setHeaders(req *http.Request) {
	for key, value := range s.Headers {
		req.Header.Set(key, value)
	}
}

// Helper function to safely truncate long response bodies in logs
func truncateString(s string, maxLen int) string {
	if len(s) > maxLen {
		return s[:maxLen] + "..."
	}
	return s
}
