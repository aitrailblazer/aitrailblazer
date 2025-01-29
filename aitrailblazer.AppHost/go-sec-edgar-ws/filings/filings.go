package filings

import (
	"encoding/json"
	"fmt"
	"io"
	"log"
	"net/http"
	"strings"
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

func (s *SECFilings) GetCompanyFilings(cik string) (map[string]interface{}, error) {
	url := fmt.Sprintf("https://data.sec.gov/submissions/CIK%s.json", padCIK(cik))

	req, err := http.NewRequest("GET", url, nil)
	if err != nil {
		return nil, fmt.Errorf("failed to create request for CIK %s: %v", cik, err)
	}
	s.setHeaders(req)

	log.Printf("[INFO] Fetching SEC filings for CIK: %s", cik)
	log.Printf("[INFO] Request URL: %s", url)

	resp, err := s.Client.Do(req)
	if err != nil {
		return nil, fmt.Errorf("failed to fetch data for CIK %s: %v", cik, err)
	}
	defer resp.Body.Close()

	log.Printf("[INFO] Response Status: %d", resp.StatusCode)

	if resp.StatusCode != http.StatusOK {
		return nil, fmt.Errorf("failed to fetch data for CIK %s: %s", cik, resp.Status)
	}

	body, err := io.ReadAll(resp.Body)
	if err != nil {
		return nil, fmt.Errorf("failed to read response body for CIK %s: %v", cik, err)
	}

	// Log full SEC response (limit it to avoid excessive logs)
	log.Printf("[DEBUG] Full SEC Response: %s", truncateString(string(body), 1000))

	var filings map[string]interface{}
	if err := json.Unmarshal(body, &filings); err != nil {
		return nil, fmt.Errorf("failed to decode response for CIK %s: %v", cik, err)
	}

	return filings, nil
}

func (s *SECFilings) FilingsToDataFrame(filings map[string]interface{}) ([]map[string]interface{}, error) {
	log.Printf("[DEBUG] Raw Filings Data: %+v", filings)

	// Check if "filings" exists
	filingsData, ok := filings["filings"].(map[string]interface{})
	if !ok {
		log.Printf("[ERROR] Missing or invalid 'filings' key in response")
		return nil, fmt.Errorf("unexpected response format: missing 'filings' key")
	}

	// Check if "recent" exists and is a map instead of a list
	recentData, ok := filingsData["recent"].(map[string]interface{})
	if !ok {
		log.Printf("[ERROR] 'recent' key is missing or not a valid map")
		return nil, fmt.Errorf("unexpected response format: 'recent' key is not a map")
	}

	// Extract filing data from the column-wise format (parallel arrays)
	numFilings := len(recentData["form"].([]interface{})) // Get the number of filings
	var dataFrame []map[string]interface{}

	for i := 0; i < numFilings; i++ {
		filing := make(map[string]interface{})

		// Iterate over each field in `recentData`
		for key, value := range recentData {
			if valueList, ok := value.([]interface{}); ok && i < len(valueList) {
				filing[key] = valueList[i] // Extract the ith filing's value
			} else {
				log.Printf("[WARNING] Key '%s' does not contain a valid list", key)
			}
		}

		dataFrame = append(dataFrame, filing)
	}

	if len(dataFrame) == 0 {
		log.Printf("[WARNING] No valid filings found in response")
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

	log.Printf("[INFO] Downloading SEC filing HTML for CIK: %s, Accession: %s, File: %s", cik, accessionNumber, fileName)

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
	url := fmt.Sprintf("https://data.sec.gov/api/xbrl/companyfacts/CIK%s.json", padCIK(cik))

	req, err := http.NewRequest("GET", url, nil)
	if err != nil {
		return nil, fmt.Errorf("failed to create request for XBRL data: %v", err)
	}
	s.setHeaders(req)

	log.Printf("[INFO] Fetching XBRL data for CIK: %s", cik)

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

// padCIK ensures CIK is always 10 digits long (SEC format)
func padCIK(cik string) string {
	return strings.Repeat("0", 10-len(cik)) + cik
}
