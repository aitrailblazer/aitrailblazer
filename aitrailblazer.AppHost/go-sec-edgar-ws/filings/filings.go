package filings

import (
	"encoding/json"
	"errors"
	"fmt"
	"io"
	"log"
	"net/http"
	"regexp"
	"sort"
	"strings"
	"time"
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

// https://data.sec.gov/submissions/CIK0001318605.json
func (s *SECFilings) GetCompanyFilings(cik string) (map[string]interface{}, error) {
	url := fmt.Sprintf("https://data.sec.gov/submissions/CIK%s.json", padCIK(cik))

	req, err := http.NewRequest("GET", url, nil)
	if err != nil {
		return nil, fmt.Errorf("failed to create request for CIK %s: %v", cik, err)
	}
	s.setHeaders(req)

	fmt.Println("[INFO] Fetching SEC filings for CIK:", cik)
	fmt.Println("[INFO] Request URL:", url)

	resp, err := s.Client.Do(req)
	if err != nil {
		return nil, fmt.Errorf("failed to fetch data for CIK %s: %v", cik, err)
	}
	defer resp.Body.Close()

	fmt.Println("[INFO] Response Status: %d", resp.StatusCode)

	if resp.StatusCode != http.StatusOK {
		return nil, fmt.Errorf("failed to fetch data for CIK %s: %s", cik, resp.Status)
	}

	body, err := io.ReadAll(resp.Body)
	if err != nil {
		return nil, fmt.Errorf("failed to read response body for CIK %s: %v", cik, err)
	}

	// Log full SEC response (limit it to avoid excessive logs)
	//fmt.Println("[DEBUG] Full SEC Response: %s", truncateString(string(body), 1000))

	var filings map[string]interface{}
	if err := json.Unmarshal(body, &filings); err != nil {
		return nil, fmt.Errorf("failed to decode response for CIK %s: %v", cik, err)
	}

	return filings, nil
}

func (s *SECFilings) FilingsToDataFrame(filings map[string]interface{}) ([]map[string]interface{}, error) {
	//log.Printf("[DEBUG] Raw Filings Data: %+v", filings)

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

// DownloadLatestFilingHTML retrieves the latest filing's HTML content for a given ticker and form type.
func (s *SECFilings) DownloadLatestFilingHTML(cik, ticker, formType string) (string, string, error) {
	// Fetch SEC filings
	filings, err := s.GetCompanyFilings(cik)
	if err != nil {
		return "", "", fmt.Errorf("failed to get filings for ticker %s: %v", ticker, err)
	}

	// Extract SEC filings data
	filingsData, filingsExists := filings["filings"].(map[string]interface{})
	if !filingsExists {
		return "", "", fmt.Errorf("missing 'filings' key in response for CIK %s", cik)
	}

	recentData, recentExists := filingsData["recent"].(map[string]interface{})
	if !recentExists {
		return "", "", fmt.Errorf("missing 'recent' key in response for CIK %s", cik)
	}

	// Extract required filing fields
	formList, formExists := recentData["form"].([]interface{})
	accessionList, accessionExists := recentData["accessionNumber"].([]interface{})
	primaryDocList, docExists := recentData["primaryDocument"].([]interface{})

	if !formExists || !accessionExists || !docExists {
		return "", "", fmt.Errorf("SEC response missing required keys for CIK %s", cik)
	}

	log.Printf("[DEBUG] Available forms for %s: %v", ticker, formList)

	// Find the latest filing that matches the requested form type
	for i, form := range formList {
		if formStr, ok := form.(string); ok && formStr == formType {
			accessionNum, accOk := accessionList[i].(string)
			primaryDoc, docOk := primaryDocList[i].(string)
			if accOk && docOk {
				// Format accession number correctly (remove dashes)
				formattedAccessionNum := strings.ReplaceAll(accessionNum, "-", "")
				baseURL := fmt.Sprintf("https://www.sec.gov/Archives/edgar/data/%s/%s/", cik, formattedAccessionNum)
				filingURL := fmt.Sprintf("%s%s", baseURL, primaryDoc)

				log.Printf("[INFO] Fetching HTML content from: %s", filingURL)

				// Download the filing HTML content
				htmlContent, err := s.DownloadHTMLContent(cik, formattedAccessionNum, primaryDoc)
				if err != nil {
					return "", "", fmt.Errorf("failed to download HTML for form %s, CIK %s: %v", formType, cik, err)
				}

				// Fix relative image links in HTML content
				htmlContent = fixRelativeImageLinks(htmlContent, baseURL)

				return htmlContent, baseURL, nil
			}
		}
	}

	return "", "", fmt.Errorf("no available filings found for ticker %s and form type %s", ticker, formType)
}

// fixRelativeImageLinks updates relative image URLs in HTML content
func fixRelativeImageLinks(htmlContent, baseURL string) string {
	re := regexp.MustCompile(`(?i)(<img\s+[^>]*src=")([^":]+)`)
	return re.ReplaceAllString(htmlContent, fmt.Sprintf(`${1}%s$2`, baseURL))
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

// ProcessXBRLData extracts and structures XBRL data for a given concept and unit.
func (sf *SECFilings) ProcessXBRLData(xbrlData map[string]interface{}, concept string, unit string) ([]map[string]interface{}, error) {
	// Navigate to "facts -> us-gaap -> concept -> units -> unit"
	usGaap, ok := xbrlData["facts"].(map[string]interface{})["us-gaap"].(map[string]interface{})
	if !ok {
		return nil, fmt.Errorf("missing 'us-gaap' section in XBRL data")
	}

	conceptData, exists := usGaap[concept].(map[string]interface{})
	if !exists {
		return nil, fmt.Errorf("concept '%s' not found in XBRL data", concept)
	}

	units, exists := conceptData["units"].(map[string]interface{})
	if !exists {
		return nil, fmt.Errorf("units not found for concept '%s'", concept)
	}

	unitData, exists := units[unit].([]interface{})
	if !exists {
		return nil, fmt.Errorf("unit '%s' not found for concept '%s'", unit, concept)
	}

	// Process each record into structured format
	var results []map[string]interface{}
	for _, entry := range unitData {
		entryMap, ok := entry.(map[string]interface{})
		if !ok {
			log.Printf("[WARNING] Unexpected data format for %s/%s", concept, unit)
			continue
		}

		// Extract relevant fields
		endDate, _ := entryMap["end"].(string)
		startDate, _ := entryMap["start"].(string)
		value, _ := entryMap["val"].(float64) // Ensure numerical type

		// Append structured data
		results = append(results, map[string]interface{}{
			"EndDate":    endDate,
			"Value":      value,
			"Unit":       unit,
			"Concept":    concept,
			"Start Date": startDate,
		})
	}

	// Sort results by End Date (ascending)
	sort.Slice(results, func(i, j int) bool {
		timeI, _ := time.Parse("2006-01-02", results[i]["End Date"].(string))
		timeJ, _ := time.Parse("2006-01-02", results[j]["End Date"].(string))
		return timeI.Before(timeJ)
	})

	log.Printf("Processed %d records for %s/%s", len(results), concept, unit)
	return results, nil
}

// XBRLData represents the structured XBRL response.
type XBRLData struct {
	ReportingPeriod string    `json:"ReportingPeriod`
	EndDate         time.Time `json:"enddate"`
	Value           float64   `json:"value"`
	Unit            string    `json:"Unit"`
	Concept         string    `json:"Concept"`
	StartDate       time.Time `json:"StartDate,omitempty"`
	AccessionNumber string    `json:"AccessionNumber,omitempty"`
	FiscalYear      int       `json:"FiscalYear,omitempty"`
	FiscalPeriod    string    `json:"FiscalPeriod,omitempty"`
	FormType        string    `json:"FormType,omitempty"`
	FilingDate      time.Time `json:"FilingDate,omitempty"`
}

// FetchXBRLCSV retrieves and processes XBRL data as a structured slice.
func (s *SECFilings) FetchXBRLCSV(cik, concept, unit string) ([]XBRLData, error) {
	// Fetch raw XBRL data using the instance method
	xbrlRaw, err := s.GetXBRLData(cik)
	if err != nil || xbrlRaw == nil {
		log.Printf("[ERROR] No XBRL data found for CIK: %s", cik)
		return nil, fmt.Errorf("No XBRL data found for CIK: %s", cik)
	}

	// Extract concept data
	facts, ok := xbrlRaw["facts"].(map[string]interface{})
	if !ok {
		log.Printf("[ERROR] Invalid facts format for CIK: %s", cik)
		return nil, errors.New("Invalid XBRL facts structure")
	}

	usGaap, ok := facts["us-gaap"].(map[string]interface{})
	if !ok {
		log.Printf("[ERROR] No US-GAAP data found for CIK: %s", cik)
		return nil, errors.New("No US-GAAP data available")
	}

	conceptData, ok := usGaap[concept].(map[string]interface{})
	if !ok {
		log.Printf("[ERROR] Concept %s not found for CIK: %s", concept, cik)
		return nil, fmt.Errorf("Concept %s not found for CIK %s", concept, cik)
	}

	// Extract unit values
	units, ok := conceptData["units"].(map[string]interface{})
	if !ok {
		log.Printf("[ERROR] No unit data available for concept %s in CIK: %s", concept, cik)
		return nil, fmt.Errorf("No unit data available for concept %s", concept)
	}

	// Ensure USD data is available
	unitData, ok := units[unit].([]interface{})
	if !ok || len(unitData) == 0 {
		log.Printf("[ERROR] No USD unit data found for concept %s in CIK: %s", concept, cik)
		return nil, fmt.Errorf("No USD data available for concept %s", concept)
	}

	// Process records
	var xbrlRecords []XBRLData
	for _, item := range unitData {
		entry, ok := item.(map[string]interface{})
		if !ok {
			continue
		}

		// Parse necessary fields
		endDate, err := parseDate(entry["end"])
		if err != nil {
			continue
		}

		value, _ := parseFloat(entry["val"])
		startDate, _ := parseDate(entry["start"])
		filingDate, _ := parseDate(entry["filed"])
		fy, _ := parseInt(entry["fy"])

		// Map fields to the structured response
		xbrlRecords = append(xbrlRecords, XBRLData{
			ReportingPeriod: safeString(entry["frame"]),
			EndDate:         endDate,
			Value:           value,
			Unit:            unit,
			Concept:         concept,
			StartDate:       startDate,
			AccessionNumber: fmt.Sprintf("%v", entry["accn"]),
			FiscalYear:      fy,
			FiscalPeriod:    fmt.Sprintf("%v", entry["fp"]),
			FormType:        fmt.Sprintf("%v", entry["form"]),
			FilingDate:      filingDate,
		})
	}

	// Sort by End Date
	sort.Slice(xbrlRecords, func(i, j int) bool {
		return xbrlRecords[i].EndDate.Before(xbrlRecords[j].EndDate)
	})

	// Ensure at least one valid record
	if len(xbrlRecords) == 0 {
		log.Printf("[ERROR] XBRL data is empty for CIK: %s, concept: %s", cik, concept)
		return nil, fmt.Errorf("No XBRL data available for CIK: %s, concept: %s", cik, concept)
	}

	return xbrlRecords, nil
}

// safeString returns "N/A" if the given value is nil, otherwise returns its string representation.
func safeString(val interface{}) string {
	if val == nil {
		return "N/A"
	}
	s := fmt.Sprintf("%v", val)
	if s == "<nil>" {
		return "N/A"
	}
	return s
}

// parseDate safely parses an interface{} to a time.Time
func parseDate(value interface{}) (time.Time, error) {
	if str, ok := value.(string); ok {
		parsed, err := time.Parse("2006-01-02", str)
		return parsed, err
	}
	return time.Time{}, errors.New("invalid date format")
}

// parseFloat safely parses an interface{} to float64
func parseFloat(value interface{}) (float64, error) {
	switch v := value.(type) {
	case float64:
		return v, nil
	case string:
		return 0, fmt.Errorf("unexpected string instead of float64: %s", v)
	default:
		return 0, errors.New("invalid float format")
	}
}

// parseInt safely parses an interface{} to int
func parseInt(value interface{}) (int, error) {
	switch v := value.(type) {
	case float64:
		return int(v), nil
	case string:
		return 0, fmt.Errorf("unexpected string instead of int: %s", v)
	default:
		return 0, errors.New("invalid int format")
	}
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
