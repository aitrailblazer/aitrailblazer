package main

import (
	"encoding/csv"
	"encoding/json"
	"fmt"
	"io"
	"io/ioutil"
	"log"
	"math"
	"net/http"
	"os"
	"regexp"
	"sort"
	"strings"
	"time"

	"github.com/aitrailblazer/aitrailblazer/go-sec-edgar-ws/company_info"
	"github.com/aitrailblazer/aitrailblazer/go-sec-edgar-ws/filings"
	"github.com/aitrailblazer/aitrailblazer/go-sec-edgar-ws/xbrlparser"
)

// --------------------
// Global Static XBRL Entries
// --------------------

// XBRLEntry holds the title and file path for an XBRL file.
type XBRLEntry struct {
	Title    string
	FilePath string
}

// staticXBRLEntries is our global, pre‑loaded list of entries.
var staticXBRLEntries []XBRLEntry

// init loads and pre‑processes the static XBRL entries once at startup.
func init() {
	// Define the entries.
	staticXBRLEntries = []XBRLEntry{
		{
			Title:    "104000 - Statement - Statement of Financial Position, Classified",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-sfp-cls-pre-2024.xml",
		},

		{
			Title:    "108000 - Statement - Statement of Financial Position, Unclassified - Deposit Based Operations",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-sfp-dbo-pre-2024.xml",
		},
		{
			Title:    "108200 - Statement - Statement of Financial Position, Unclassified - Insurance Based Operations",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-sfp-ibo-pre-2024.xml",
		},
		{
			Title:    "110000 - Statement - Statement of Financial Position, Classified - Real Estate Operations",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-sfp-clreo-pre-2024.xml",
		},
		{
			Title:    "110200 - Statement - Statement of Financial Position, Unclassified - Real Estate Operations",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-sfp-ucreo-pre-2024.xml",
		},

		{
			Title:    "112000 - Statement - Statement of Financial Position, Unclassified - Securities Based Operations",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-sfp-sbo-pre-2024.xml",
		},

		{
			Title:    "124000 - Statement - Statement of Income (Including Gross Margin)",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-soi-pre-2024.xml",
		},

		{
			Title:    "124100 - Statement - Statement of Income",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-soi-egm-pre-2024.xml",
		},
		{
			Title:    "124200 - Statement - Statement of Income, Additional Statement of Income Elements",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-soi-indira-pre-2024.xml",
		},
		{
			Title:    "132001 - Statement - Statement of Income, Interest Based Revenue",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-soi-int-pre-2024.xml",
		},
		{
			Title:    "136000 - Statement - Statement of Income, Insurance Based Revenue",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-soi-ins-pre-2024.xml",
		},
		{
			Title:    "140400 - Statement - Statement of Income, Securities Based Income",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-soi-sbi-pre-2024.xml",
		},
		{
			Title:    "144000 - Statement - Statement of Income, Real Estate, Excluding REITs",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-soi-re-pre-2024.xml",
		},
		{
			Title:    "145000 - Statement - Statement of Income, Real Estate Investment Trusts",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-soi-reit-pre-2024.xml",
		},

		{
			Title:    "148400 - Statement - Statement of Comprehensive Income",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-soc-pre-2024.xml",
		},

		{
			Title:    "148600 - Statement - Statement of Shareholders' Equity",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-sheci-pre-2024.xml",
		},
		{
			Title:    "152000 - Statement - Statement of Partners' Capital",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-spc-pre-2024.xml",
		},
		{
			Title:    "152200 - Statement - Statement of Cash Flows",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-scf-indir-pre-2024.xml",
		},

		{
			Title:    "152201 - Statement - Statement of Cash Flows, Additional Cash Flow Elements",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-scf-indira-pre-2024.xml",
		},
		{
			Title:    "152205 - Statement - Statement of Cash Flows, Supplemental Disclosures",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-scf-sd-pre-2024.xml",
		},
		{
			Title:    "160000 - Statement - Statement of Cash Flows, Deposit Based Operations",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-scf-dbo-pre-2024.xml",
		},
		{
			Title:    "164000 - Statement - Statement of Cash Flows, Insurance Based Operations",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-scf-inv-pre-2024.xml",
		},
		{
			Title:    "168400 - Statement - Statement of Cash Flows, Securities Based Operations",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-scf-sbo-pre-2024.xml",
		},
		{
			Title:    "170000 - Statement - Statement of Cash Flows, Real Estate, Including REITs",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-scf-re-pre-2024.xml",
		},
		{
			Title:    "172600 - Statement - Statement of Cash Flows, Direct Method Operating Activities",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-scf-dir-pre-2024.xml",
		},

		//{
		//	Title:    "190000 - Statement - Common Domain Members",
		//	FilePath: "us-gaap-2024/stm/us-gaap-stm-com-pre-2024.xml",
		//},
	}

	// Remove the numeric prefix (e.g. "104000 - Statement - ") from each entry's Title.
	re := regexp.MustCompile(`^\d+\s*-\s*Statement\s*-\s*`)
	for i := range staticXBRLEntries {
		staticXBRLEntries[i].Title = re.ReplaceAllString(staticXBRLEntries[i].Title, "")
	}
}

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
func companyInfoHandler(ci *company_info.CompanyInfo, sf *filings.SECFilings) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
			return
		}

		ticker := r.URL.Query().Get("ticker")
		log.Printf("ticker %v", ticker)

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
		log.Printf("cik %v", cik)

		// Fetch XBRL data.
		//xbrlData, err := sf.GetXBRLData(cik)
		//if err != nil {
		//	log.Printf("[ERROR] Failed to fetch XBRL data for CIK %s: %v", cik, err)
		//	http.Error(w, `{"error": "Failed to retrieve XBRL data"}`, http.StatusInternalServerError)
		//	return
		//}
		// saveXBRLDataToFile(xbrlData, "xbrlData.json")

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
func saveXBRLDataToFile(xbrlData interface{}, filename string) {
	// Marshal the data with indentation.
	jsonData, err := json.MarshalIndent(xbrlData, "", "  ")
	if err != nil {
		log.Fatalf("Error marshalling xbrlData: %v", err)
	}

	// Optionally, print to console.
	fmt.Printf("xbrlData:\n%s\n", jsonData)

	// Write the JSON data to a file.
	err = ioutil.WriteFile(filename, jsonData, 0644)
	if err != nil {
		log.Fatalf("Error writing xbrlData to file: %v", err)
	}

	log.Printf("xbrlData saved successfully to %s", filename)
}

// SEC Filings Handler
func secFilingsHandler(ci *company_info.CompanyInfo, sf *filings.SECFilings) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		startTime := time.Now() // Capture request start time

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
		// Log the pretty-printed JSON response
		// Log.Printf("[INFO] SEC Filings Response for %s:\n%s", ticker, string(responseJSON))

		// Send JSON response
		w.Header().Set("Content-Type", "application/json")
		w.Write(responseJSON)
		// Log execution time
		log.Printf("[INFO] Processing time for %s: %v ms", ticker, time.Since(startTime).Milliseconds())

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
		//if len(forms) == 0 {
		//	log.Printf("[WARNING] No valid form types found for CIK %s", cik)
		//} else {
		//	log.Printf("[INFO] Extracted & Sorted Form Types for CIK %s: %+v", cik, forms)
		//}

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

// Handles downloading the latest filing as HTML
func downloadLatestFilingHTML(ci *company_info.CompanyInfo, sf *filings.SECFilings) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodPost {
			http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
			return
		}

		// Parse JSON request body
		var requestData struct {
			Ticker   string `json:"ticker"`
			FormType string `json:"form_type"`
		}

		body, err := ioutil.ReadAll(r.Body)
		if err != nil {
			http.Error(w, "Failed to read request body", http.StatusBadRequest)
			return
		}
		defer r.Body.Close()

		if err := json.Unmarshal(body, &requestData); err != nil {
			http.Error(w, "Invalid JSON format", http.StatusBadRequest)
			return
		}

		ticker, formType := requestData.Ticker, requestData.FormType

		// Validate input formats
		if !regexp.MustCompile(`^[A-Za-z0-9.-]+$`).MatchString(ticker) {
			http.Error(w, "Invalid ticker format", http.StatusBadRequest)
			return
		}
		if !regexp.MustCompile(`^[A-Za-z0-9/ -]+$`).MatchString(formType) {
			http.Error(w, "Invalid form type format", http.StatusBadRequest)
			return
		}

		// Get CIK for ticker
		cik, err := ci.GetCIKByTicker(ticker)
		if err != nil {
			http.Error(w, "Invalid ticker: CIK not found", http.StatusNotFound)
			return
		}

		// Fetch HTML content of the latest filing
		htmlContent, baseURL, err := sf.DownloadLatestFilingHTML(cik, ticker, formType)
		if err != nil {
			log.Printf("[ERROR] Failed to retrieve filing HTML for %s: %v", ticker, err)
			http.Error(w, "Failed to retrieve filing HTML", http.StatusInternalServerError)
			return
		}

		log.Printf("[INFO] Filing retrieved successfully for %s - URL: %s", ticker, baseURL)

		// Set headers and return HTML content
		w.Header().Set("Content-Disposition", fmt.Sprintf("attachment; filename=%s_%s.html", ticker, formType))
		w.Header().Set("Content-Type", "text/html")
		w.WriteHeader(http.StatusOK)
		_, _ = w.Write([]byte(htmlContent))
	}
}

// Handles downloading a specific SEC filing's HTML by accession number and primary document name.
func downloadHTMLContentHandler(ci *company_info.CompanyInfo, sf *filings.SECFilings) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		// Only allow POST for downloading content
		if r.Method != http.MethodPost {
			http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
			return
		}

		// Parse JSON body
		var reqData struct {
			Cik             string `json:"cik"`
			AccessionNumber string `json:"accession_number"`
			PrimaryDocument string `json:"primary_document"`
		}

		body, err := io.ReadAll(r.Body)
		if err != nil {
			http.Error(w, "Failed to read request body", http.StatusBadRequest)
			return
		}
		defer r.Body.Close()

		if err := json.Unmarshal(body, &reqData); err != nil {
			http.Error(w, "Invalid JSON format", http.StatusBadRequest)
			return
		}

		// Validate required fields
		missingFields := []string{}
		if reqData.Cik == "" {
			missingFields = append(missingFields, "cik")
		}
		if reqData.AccessionNumber == "" {
			missingFields = append(missingFields, "accession_number")
		}
		if reqData.PrimaryDocument == "" {
			missingFields = append(missingFields, "primary_document")
		}

		if len(missingFields) > 0 {
			http.Error(w, fmt.Sprintf("Missing required fields: %s", strings.Join(missingFields, ", ")), http.StatusBadRequest)
			return
		}

		// Remove dashes from Accession Number (SEC format requires this)
		formattedAccNum := strings.ReplaceAll(reqData.AccessionNumber, "-", "")

		// Download raw HTML
		htmlContent, err := sf.DownloadHTMLContent(reqData.Cik, formattedAccNum, reqData.PrimaryDocument)
		if err != nil {
			log.Printf("[ERROR] Failed to download HTML content: %v", err)
			http.Error(w, "Failed to retrieve filing HTML", http.StatusInternalServerError)
			return
		}

		// Set response headers for an HTML file download
		fileName := fmt.Sprintf(`"%s_%s.html"`, reqData.Cik, reqData.AccessionNumber)
		w.Header().Set("Content-Disposition", fmt.Sprintf("attachment; filename=%s", fileName))
		w.Header().Set("Content-Type", "text/html")
		w.WriteHeader(http.StatusOK)

		// Write the HTML response
		if _, err := w.Write([]byte(htmlContent)); err != nil {
			log.Printf("[ERROR] Failed writing HTML response: %v", err)
		}
	}
}

// newline-delimited JSON (NDJSON)
// contains is a helper function to check if a slice contains a given string.
func contains(slice []string, s string) bool {
	for _, item := range slice {
		if item == s {
			return true
		}
	}
	return false
}

// loadGaapMapping loads the GAAP CSV file and returns a mapping from elementName to title.
// It assumes the CSV columns are: areaTitle, topic, subTopic, section, paragraph, subParagraph, elementName, title, ...
func loadGaapMapping(csvPath string) (map[string]string, error) {
	file, err := os.Open(csvPath)
	if err != nil {
		return nil, err
	}
	defer file.Close()

	reader := csv.NewReader(file)
	reader.TrimLeadingSpace = true

	records, err := reader.ReadAll()
	if err != nil {
		return nil, err
	}

	gaapMap := make(map[string]string)
	// Skip header row.
	for i, record := range records {
		if i == 0 {
			continue
		}
		if len(record) < 8 {
			continue
		}
		elementName := strings.TrimSpace(record[6])
		title := strings.TrimSpace(record[7])
		gaapMap[elementName] = title
	}
	return gaapMap, nil
}

// countMatchingNodes recursively counts nodes that are marked as matched.
func countMatchingNodes(node *xbrlparser.Node) int {
	if node == nil {
		return 0
	}
	count := 0
	if node.Matched {
		count = 1
	}
	for _, child := range node.Children {
		count += countMatchingNodes(child)
	}
	return count
}

// pruneNodes recursively prunes nodes whose label (lowercased) contains unwanted substrings.
// When a node is pruned, its children are lifted into the parent.
func pruneNodes(nodes []*xbrlparser.Node) []*xbrlparser.Node {
	var result []*xbrlparser.Node
	for _, node := range nodes {
		// Recursively prune children.
		node.Children = pruneNodes(node.Children)
		labelLower := strings.ToLower(strings.TrimSpace(node.Label))
		if strings.Contains(labelLower, "statementtable") || strings.Contains(labelLower, "statementlineitems") {
			// Lift children of this node.
			result = append(result, node.Children...)
		} else {
			result = append(result, node)
		}
	}
	return result
}

// hasMatchedDescendant returns true if any descendant of the node is matched.
func hasMatchedDescendant(n *xbrlparser.Node) bool {
	if n == nil {
		return false
	}
	if n.Matched && n.Concept != nil {
		return true
	}
	for _, child := range n.Children {
		if hasMatchedDescendant(child) {
			return true
		}
	}
	return false
}

// -----------------------------------------------------------------

// cleanLabel trims extra whitespace from a label.
func cleanLabel(label string) string {
	return strings.TrimSpace(label)
}

// pruneTree recursively builds a pruned copy of the tree that includes
// the full branch from the current node until the matching node.
func pruneTree(n *xbrlparser.Node) *xbrlparser.Node {
	// If the current node is matched, return a copy with no children.
	if n.Matched {
		return &xbrlparser.Node{
			Label: n.Label,
			//Href:     n.Href,
			Matched:  n.Matched,
			Concept:  n.Concept,
			Children: nil, // Stop here.
		}
	}

	// Process children recursively.
	var prunedChildren []*xbrlparser.Node
	for _, child := range n.Children {
		if prunedChild := pruneTree(child); prunedChild != nil {
			prunedChildren = append(prunedChildren, prunedChild)
		}
	}

	// If at least one child (or descendant) led to a match, keep this branch.
	if len(prunedChildren) > 0 {
		return &xbrlparser.Node{
			Label: n.Label,
			//Href:     n.Href,
			Matched:  n.Matched,
			Concept:  n.Concept,
			Children: prunedChildren,
		}
	}

	// If no descendant was matched, omit this branch.
	return nil
}

// undesiredLabels defines the statement labels to skip.
var undesiredLabels = map[string]bool{
	"Statement Of Financial Position": true,
	"loc_StatementTable":              true,
	//"loc_StatementLineItems":          true,
	"Income Statement": true,
	"Statement Of Income And Comprehensive Income": true,
	"Statement Of Stockholders Equity":             true,
	"Statement Of Partners Capital":                true,
	"Statement Of Cash Flows":                      true,
	"Operating Cash Flows Direct Method":           true,
	"Supplemental Cash Flow Elements":              true,
	"Additional Cash Flow Elements":                true,
}

// removeUndesiredNodes recursively processes the tree.
// If a node’s label is found in the undesiredLabels map,
// it is removed (i.e. not included in the result) and its children are lifted.
func removeUndesiredNodes(n *xbrlparser.Node, undesired map[string]bool) *xbrlparser.Node {
	if n == nil {
		return nil
	}

	// Process children first.
	var newChildren []*xbrlparser.Node
	for _, child := range n.Children {
		processedChild := removeUndesiredNodes(child, undesired)
		if processedChild != nil {
			newChildren = append(newChildren, processedChild)
		}
	}
	n.Children = newChildren

	// If the current node's label is undesired, lift its children.
	if undesired[n.Label] {
		// If there's exactly one child, return that child.
		if len(n.Children) == 1 {
			return n.Children[0]
		} else if len(n.Children) > 1 {
			// Otherwise, create a dummy node that wraps the lifted children.
			return &xbrlparser.Node{
				Label: "", // or set to a custom label if needed
				//Href:     "",
				Matched:  n.Matched,
				Concept:  n.Concept,
				Children: n.Children,
			}
		}
		// If no children, return nil.
		return nil
	}

	return n
}

// FilterTree applies pruneTree to the given root, then removes nodes with undesired labels.
func FilterTree(n *xbrlparser.Node) *xbrlparser.Node {
	pruned := pruneTree(n)
	return removeUndesiredNodes(pruned, undesiredLabels)
}

// cleanLabel is assumed to be defined elsewhere to clean up label strings.

// --------------------
// HTTP Handler: xbrlHandler
// --------------------
//
// xbrlHandler processes SEC XBRL data for a given ticker, builds an accumulated output
// of concept JSON objects, then uses that accumulated JSON (instead of loading from file)
// to build a concept map. It then processes static XBRL entries using that concept map,
// applies GAAP mapping, prunes unwanted nodes, filters the tree to output only desired nodes,
// and returns the filtered tree as pretty-printed JSON.

// --------------------
// HTTP Handler: xbrlHandler
// --------------------
//
// xbrlHandler processes SEC XBRL data for a given ticker, builds an accumulated output
// of concept JSON objects, then uses that accumulated JSON (instead of loading from file)
// to build a concept map. It then processes static XBRL entries using that concept map,
// applies GAAP mapping, prunes unwanted nodes, filters the tree to output only desired nodes,
// and returns the filtered tree as pretty-printed JSON.
func listXBRLConceptsAsJSON(ci *company_info.CompanyInfo, sf *filings.SECFilings) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		startTime := time.Now()

		// Process query parameters.
		gaapCSVPath := r.URL.Query().Get("gaap_csv")
		if gaapCSVPath == "" {
			gaapCSVPath = "us-gaap-2024/xbrl_elements_20241108.csv"
		}

		ticker := r.URL.Query().Get("ticker")
		// Validate ticker.
		if ticker == "" || !regexp.MustCompile(`^[A-Za-z0-9]+$`).MatchString(ticker) {
			http.Error(w, `{"error": "Invalid ticker format. Only alphanumeric characters are allowed."}`, http.StatusBadRequest)
			return
		}
		log.Printf("[INFO] Processing ticker: %s", ticker)

		// Get CIK.
		cik, err := ci.GetCIKByTicker(ticker)
		if err != nil {
			log.Printf("[ERROR] CIK not found for ticker %s", ticker)
			http.Error(w, fmt.Sprintf(`{"error": "CIK not found for ticker: %s"}`, ticker), http.StatusNotFound)
			return
		}

		// Fetch SEC XBRL data.
		xbrlData, err := sf.GetXBRLData(cik)
		if err != nil {
			log.Printf("[ERROR] Failed to fetch XBRL data for CIK %s: %v", cik, err)
			http.Error(w, `{"error": "Failed to retrieve XBRL data"}`, http.StatusInternalServerError)
			return
		}
		// log.Printf("xbrlData: %v", xbrlData)

		// Extract concepts.
		concepts, ok := xbrlData["facts"].(map[string]interface{})["us-gaap"].(map[string]interface{})
		if !ok || len(concepts) == 0 {
			log.Printf("[ERROR] No XBRL concepts found for ticker: %s", ticker)
			http.Error(w, `{"error": "No XBRL concepts found for ticker"}`, http.StatusNotFound)
			return
		}

		// Collect and sort concept names.
		var conceptNames []string
		for name := range concepts {
			conceptNames = append(conceptNames, name)
		}
		sort.Strings(conceptNames)

		// Accumulate concept JSON objects in a slice.
		var output []interface{}
		for _, conceptName := range conceptNames {
			conceptDetails, ok := concepts[conceptName].(map[string]interface{})
			if !ok {
				continue
			}

			label := fmt.Sprintf("%v", conceptDetails["label"])
			// Skip deprecated concepts.
			if strings.Contains(label, "Deprecated") {
				continue
			}
			// Extract available units.
			unitsMap, ok := conceptDetails["units"].(map[string]interface{})
			if !ok {
				continue
			}
			var availableUnits []string
			for unit := range unitsMap {
				availableUnits = append(availableUnits, unit)
			}
			sort.Strings(availableUnits)
			if !contains(availableUnits, "USD") {
				continue
			}
			description := fmt.Sprintf("%v", conceptDetails["description"])

			//log.Printf("description: %v", description)

			// (CSV fetching and frequency inference code is commented out.)
			/*
				xbrlDF, err := sf.FetchXBRLCSV(cik, conceptName, "USD")
				if err != nil {
					log.Printf("[ERROR] Failed fetching XBRL CSV for %s: %v", conceptName, err)
					continue
				}
				var dateSeries []time.Time
				for _, record := range xbrlDF {
					dateSeries = append(dateSeries, record.EndDate)
				}
				inferredFreq := InferFrequency(dateSeries)
				if inferredFreq == "Unknown" {
					log.Printf("[INFO] Skipping concept %s because inferred frequency is Unknown", conceptName)
					continue
				}
			*/
			if strings.Contains(label, "nil") {
				label = camelCaseToSpaced(conceptName)
			}
			// Build the concept JSON object.
			conceptJSON := map[string]interface{}{
				"name":            conceptName,
				"label":           label,
				"description":     description,
				"available_units": availableUnits,
				//"inferred_freq":   inferredFreq, // Uncomment if needed.
			}
			output = append(output, conceptJSON)
		}

		// Marshal the accumulated output to pretty-printed JSON.
		accumulatedJSON, err := json.MarshalIndent(output, "", "  ")
		if err != nil {
			http.Error(w, fmt.Sprintf("Failed to encode JSON: %v", err), http.StatusInternalServerError)
			return
		}

		// Instead of loading from a file, load the concept map from the accumulated JSON.
		conceptMap, err := xbrlparser.LoadConceptsFromJSON(string(accumulatedJSON))
		if err != nil {
			http.Error(w, fmt.Sprintf("Error loading concepts from JSON: %v", err), http.StatusInternalServerError)
			return
		}

		// Load the GAAP mapping.
		gaapMapping, err := loadGaapMapping(gaapCSVPath)
		if err != nil {
			log.Printf("Error loading GAAP CSV mapping: %v", err)
			gaapMapping = nil // Continue without mapping.
		}

		// Prepare a slice to hold the parsed trees.
		var combinedChildren []*xbrlparser.Node
		totalCount := 0

		// Process each static XBRL entry.
		for _, entry := range staticXBRLEntries {
			trees, err := xbrlparser.ParseXBRL(entry.FilePath, conceptMap)
			if err != nil {
				log.Printf("Error parsing XBRL for %s: %v", entry.Title, err)
				continue
			}
			// Count nodes that matched.
			matchingCount := 0
			for _, tree := range trees {
				matchingCount += countMatchingNodes(tree)
			}
			// Add to the total count.
			totalCount += matchingCount
			// Create a node for this entry with an updated label that includes the matching count.
			entryNode := &xbrlparser.Node{
				Label:    fmt.Sprintf("%s (%d)", entry.Title, matchingCount),
				Children: trees,
			}
			combinedChildren = append(combinedChildren, entryNode)
		}

		// Create a root node that combines all entry nodes.
		root := &xbrlparser.Node{
			Label:    fmt.Sprintf("Statements (%d)", totalCount),
			Children: combinedChildren,
		}

		// Apply GAAP mapping to update node labels.
		xbrlparser.ApplyGaapMapping(root, gaapMapping)
		// Prune unwanted nodes.
		//root.Children = pruneNodes(root.Children)

		// Filter the tree so that only desired nodes remain.
		filtered := FilterTree(root)

		// Marshal the filtered tree to pretty-printed JSON.
		responseJSON, err := json.MarshalIndent(filtered, "", "  ")
		if err != nil {
			http.Error(w, fmt.Sprintf("Error marshalling combined tree to JSON: %v", err), http.StatusInternalServerError)
			return
		}
		w.Header().Set("Content-Type", "application/json")
		w.Write(responseJSON)
		log.Printf("xbrlHandler completed in %.3f ms", float64(time.Since(startTime).Milliseconds()))
	}
}
func listXBRLConceptsAsJSONFull(ci *company_info.CompanyInfo, sf *filings.SECFilings) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		startTime := time.Now()
		ticker := r.URL.Query().Get("ticker")
		// Validate ticker.
		if ticker == "" || !regexp.MustCompile(`^[A-Za-z0-9]+$`).MatchString(ticker) {
			http.Error(w, `{"error": "Invalid ticker format. Only alphanumeric characters are allowed."}`, http.StatusBadRequest)
			return
		}
		log.Printf("[INFO] Processing ticker: %s", ticker)
		// Get CIK.
		cik, err := ci.GetCIKByTicker(ticker)
		if err != nil {
			log.Printf("[ERROR] CIK not found for ticker %s", ticker)
			http.Error(w, fmt.Sprintf(`{"error": "CIK not found for ticker: %s"}`, ticker), http.StatusNotFound)
			return
		}
		// Fetch XBRL data.
		xbrlData, err := sf.GetXBRLData(cik)
		if err != nil {
			log.Printf("[ERROR] Failed to fetch XBRL data for CIK %s: %v", cik, err)
			http.Error(w, `{"error": "Failed to retrieve XBRL data"}`, http.StatusInternalServerError)
			return
		}
		// Extract concepts.
		concepts, ok := xbrlData["facts"].(map[string]interface{})["us-gaap"].(map[string]interface{})
		if !ok || len(concepts) == 0 {
			log.Printf("[ERROR] No XBRL concepts found for ticker: %s", ticker)
			http.Error(w, `{"error": "No XBRL concepts found for ticker"}`, http.StatusNotFound)
			return
		}
		// Collect and sort concept names.
		var conceptNames []string
		for name := range concepts {
			conceptNames = append(conceptNames, name)
		}
		sort.Strings(conceptNames)

		// Accumulate concept JSON objects in a slice.
		var output []interface{}
		for _, conceptName := range conceptNames {
			conceptDetails, ok := concepts[conceptName].(map[string]interface{})
			if !ok {
				continue
			}
			label := fmt.Sprintf("%v", conceptDetails["label"])
			// Skip deprecated concepts.
			if strings.Contains(label, "Deprecated") {
				continue
			}
			// Extract available units.
			unitsMap, ok := conceptDetails["units"].(map[string]interface{})
			if !ok {
				continue
			}
			var availableUnits []string
			for unit := range unitsMap {
				availableUnits = append(availableUnits, unit)
			}
			sort.Strings(availableUnits)
			if !contains(availableUnits, "USD") {
				continue
			}

			// Fetch XBRL CSV data.
			xbrlDF, err := sf.FetchXBRLCSV(cik, conceptName, "USD")
			if err != nil {
				log.Printf("[ERROR] Failed fetching XBRL CSV for %s: %v", conceptName, err)
				continue
			}
			// Extract dates from CSV data.
			var dateSeries []time.Time
			for _, record := range xbrlDF {
				dateSeries = append(dateSeries, record.EndDate)
			}
			// Infer frequency.
			inferredFreq := InferFrequency(dateSeries)
			if inferredFreq == "Unknown" {
				log.Printf("[INFO] Skipping concept %s because inferred frequency is Unknown", conceptName)
				continue
			}

			//log.Printf("[INFO] Processed %s", conceptName)
			// Build the concept JSON object.
			// Check if the label contains "nil" (here, exactly "<nil>")
			// If the label contains "nil", update it based on conceptName.
			if strings.Contains(label, "nil") {
				label = camelCaseToSpaced(conceptName)
			}
			conceptJSON := map[string]interface{}{
				"name":            conceptName,
				"label":           label,
				"data":            xbrlDF,
				"available_units": availableUnits,
				"inferred_freq":   inferredFreq,
			}
			output = append(output, conceptJSON)
		}
		// Marshal the accumulated output to pretty-printed JSON.
		responseJSON, err := json.MarshalIndent(output, "", "  ")
		if err != nil {
			http.Error(w, fmt.Sprintf("Failed to encode JSON: %v", err), http.StatusInternalServerError)
			return
		}
		w.Header().Set("Content-Type", "application/json")
		w.Write(responseJSON)
		log.Printf("[INFO] Completed processing for %s in %.3f ms", ticker, float64(time.Since(startTime).Milliseconds()))
	}
}

// camelCaseToSpaced takes a camelCase or PascalCase string
// and inserts a space before each uppercase letter (except the first).
func camelCaseToSpaced(s string) string {
	// Regular expression to find positions before uppercase letters.
	re := regexp.MustCompile(`([A-Z][a-z]+)`)
	// Find all matches.
	matches := re.FindAllString(s, -1)
	// This approach works well when your string is in PascalCase.
	// However, if your string might have consecutive uppercase letters,
	// you might need a more advanced regular expression.
	return strings.TrimSpace(strings.Join(matches, " "))
}
func listXBRLConceptsAsStreamingJSON(ci *company_info.CompanyInfo, sf *filings.SECFilings) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		startTime := time.Now()
		ticker := r.URL.Query().Get("ticker")

		// Validate ticker format.
		if ticker == "" || !regexp.MustCompile(`^[A-Za-z0-9]+$`).MatchString(ticker) {
			http.Error(w, `{"error": "Invalid ticker format. Only alphanumeric characters are allowed."}`, http.StatusBadRequest)
			return
		}

		log.Printf("[INFO] Processing ticker: %s", ticker)

		// Get CIK for the given ticker.
		cik, err := ci.GetCIKByTicker(ticker)
		if err != nil {
			log.Printf("[ERROR] CIK not found for ticker %s", ticker)
			http.Error(w, fmt.Sprintf(`{"error": "CIK not found for ticker: %s"}`, ticker), http.StatusNotFound)
			return
		}

		// Fetch XBRL data.
		xbrlData, err := sf.GetXBRLData(cik)
		if err != nil {
			log.Printf("[ERROR] Failed to fetch XBRL data for CIK %s: %v", cik, err)
			http.Error(w, `{"error": "Failed to retrieve XBRL data"}`, http.StatusInternalServerError)
			return
		}

		// Extract concepts from the XBRL data.
		concepts, ok := xbrlData["facts"].(map[string]interface{})["us-gaap"].(map[string]interface{})
		if !ok || len(concepts) == 0 {
			log.Printf("[ERROR] No XBRL concepts found for ticker: %s", ticker)
			http.Error(w, `{"error": "No XBRL concepts found for ticker"}`, http.StatusNotFound)
			return
		}

		// Collect and sort concept names.
		var conceptNames []string
		for name := range concepts {
			conceptNames = append(conceptNames, name)
		}
		sort.Strings(conceptNames)

		// Parameterize the limit.
		// Default limit is 25 if no "limit" parameter is provided.
		//limit := 25
		//if limitStr := r.URL.Query().Get("limit"); limitStr != "" {
		//	if parsedLimit, err := strconv.Atoi(limitStr); err == nil && parsedLimit > 0 {
		//		limit = parsedLimit
		//	}
		//}
		//if len(conceptNames) > limit {
		//	conceptNames = conceptNames[:limit]
		//}

		// Ensure the response writer supports streaming.
		flusher, ok := w.(http.Flusher)
		if !ok {
			http.Error(w, "Streaming unsupported!", http.StatusInternalServerError)
			return
		}

		// Set the content type for NDJSON.
		w.Header().Set("Content-Type", "application/x-ndjson")
		w.WriteHeader(http.StatusOK)

		// Process each concept and stream it as a separate JSON line.
		for _, conceptName := range conceptNames {
			conceptStartTime := time.Now()

			conceptDetails, ok := concepts[conceptName].(map[string]interface{})
			if !ok {
				continue
			}
			label := fmt.Sprintf("%v", conceptDetails["label"])

			// Skip deprecated concepts.
			if strings.Contains(label, "Deprecated") {
				continue
			}

			// Extract available units.
			unitsMap, ok := conceptDetails["units"].(map[string]interface{})
			if !ok {
				continue
			}
			var availableUnits []string
			for unit := range unitsMap {
				availableUnits = append(availableUnits, unit)
			}
			sort.Strings(availableUnits)

			// Ensure USD is available.
			if !contains(availableUnits, "USD") {
				continue
			}

			// Fetch XBRL CSV data for the concept.
			xbrlDF, err := sf.FetchXBRLCSV(cik, conceptName, "USD")
			if err != nil {
				log.Printf("[ERROR] Failed fetching XBRL CSV for %s: %v", conceptName, err)
				continue
			}

			// Extract dates for frequency inference.
			var dateSeries []time.Time
			for _, record := range xbrlDF {
				dateSeries = append(dateSeries, record.EndDate)
			}

			// Infer frequency from dates.
			inferredFreq := InferFrequency(dateSeries)
			if inferredFreq == "Unknown" {
				log.Printf("[INFO] Skipping concept %s because inferred frequency is Unknown", conceptName)
				continue
			}

			conceptDuration := time.Since(conceptStartTime)
			log.Printf("[INFO] Processed %s in %.3f ms", conceptName, float64(conceptDuration.Milliseconds()))

			// Build the JSON object for this concept.
			conceptJSON := map[string]interface{}{
				"name":            conceptName,
				"label":           label,
				"available_units": availableUnits,
				//"inferred_freq":   inferredFreq,
			}

			// Marshal the JSON object.
			data, err := json.Marshal(conceptJSON)
			if err != nil {
				log.Printf("[ERROR] Failed to marshal JSON for %s: %v", conceptName, err)
				continue
			}

			// Write the JSON object followed by a newline.
			w.Write(data)
			w.Write([]byte("\n"))
			flusher.Flush()
		}

		log.Printf("[INFO] Completed processing for %s in %.3f ms", ticker, float64(time.Since(startTime).Milliseconds()))
	}
}

// fetchXBRLConceptJSON handles fetching and processing XBRL data for a given concept.
func fetchXBRLConceptJSON(ci *company_info.CompanyInfo, sf *filings.SECFilings) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		// Extract query parameters
		ticker := r.URL.Query().Get("ticker")
		concept := r.URL.Query().Get("concept")
		unit := r.URL.Query().Get("unit")

		if ticker == "" {
			http.Error(w, "Ticker is required", http.StatusBadRequest)
			return
		}
		if concept == "" {
			concept = "AssetsCurrent"
		}
		if unit == "" {
			unit = "USD"
		}

		// Extract concept name (ignoring extra metadata if present)
		conceptName := concept
		log.Printf("fetchXBRLConceptJSON Received request: ticker=%s, concept=%s, unit=%s", ticker, conceptName, unit)

		// Get CIK for the given ticker
		cik, err := ci.GetCIKByTicker(ticker)
		if err != nil {
			log.Printf("[ERROR] No CIK found for ticker %s: %v", ticker, err)
			http.Error(w, "CIK not found for ticker", http.StatusNotFound)
			return
		}

		// Fetch XBRL data
		xbrlData, err := sf.GetXBRLData(cik)
		if err != nil {
			log.Printf("[ERROR] Failed to fetch XBRL data for CIK %s: %v", cik, err)
			http.Error(w, "Failed to retrieve XBRL data", http.StatusInternalServerError)
			return
		}
		// Save the fetched XBRL data to a file.
		// Marshal the data with indentation for readability.
		//fileData, err := json.MarshalIndent(xbrlData, "", "  ")
		//if err != nil {
		//	log.Printf("[ERROR] Failed to marshal XBRL data for file save: %v", err)
		//} else {
		// You can customize the file name or path as needed.
		//	fileName := fmt.Sprintf("%s_xbrlData.json", cik)
		//	if err := ioutil.WriteFile(fileName, fileData, 0644); err != nil {
		//		log.Printf("[ERROR] Failed to save XBRL data to file %s: %v", fileName, err)
		//	} else {
		//		log.Printf("[INFO] XBRL data saved to file: %s", fileName)
		//	}
		//}
		// Process XBRL data into structured format
		xbrlDF, err := sf.ProcessXBRLData(xbrlData, conceptName, unit)
		if err != nil {
			log.Printf("[ERROR] Failed to process XBRL data for %s: %v", ticker, err)
			http.Error(w, "Failed to process XBRL data", http.StatusInternalServerError)
			return
		}

		// Extract column names from the first record
		var columnNames []string
		if len(xbrlDF) > 0 {
			for col := range xbrlDF[0] {
				columnNames = append(columnNames, col)
			}
			sort.Strings(columnNames) // Sort columns alphabetically
		}

		// Add concept column
		for i := range xbrlDF {
			xbrlDF[i]["concept"] = conceptName
		}

		// Build JSON response
		response := map[string]interface{}{
			"columns": columnNames,
			"data":    xbrlDF,
		}

		// Convert response to JSON with indentation
		responseJSON, err := json.MarshalIndent(response, "", "  ")
		if err != nil {
			log.Printf("[ERROR] Failed to format JSON response: %v", err)
			http.Error(w, "Failed to encode JSON response", http.StatusInternalServerError)
			return
		}

		// Send JSON response
		w.Header().Set("Content-Type", "application/json")
		w.Write(responseJSON)
	}
}

// fetchXBRLConceptJSONFull returns a handler that processes a single concept.
// It expects two query parameters: "ticker" and "concept".
func fetchXBRLConceptJSONFull(ci *company_info.CompanyInfo, sf *filings.SECFilings) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		startTime := time.Now()

		// Read query parameters.
		ticker := r.URL.Query().Get("ticker")
		conceptName := r.URL.Query().Get("concept")

		// Validate ticker.
		if ticker == "" || !regexp.MustCompile(`^[A-Za-z0-9]+$`).MatchString(ticker) {
			http.Error(w, `{"error": "Invalid ticker format. Only alphanumeric characters are allowed."}`, http.StatusBadRequest)
			return
		}
		// Validate concept.
		if conceptName == "" {
			http.Error(w, `{"error": "Concept name is required."}`, http.StatusBadRequest)
			return
		}
		log.Printf("[INFO] Processing ticker: %s for concept: %s", ticker, conceptName)

		// Get CIK.
		cik, err := ci.GetCIKByTicker(ticker)
		if err != nil {
			log.Printf("[ERROR] CIK not found for ticker %s", ticker)
			http.Error(w, fmt.Sprintf(`{"error": "CIK not found for ticker: %s"}`, ticker), http.StatusNotFound)
			return
		}

		// Fetch XBRL data.
		xbrlData, err := sf.GetXBRLData(cik)
		if err != nil {
			log.Printf("[ERROR] Failed to fetch XBRL data for CIK %s: %v", cik, err)
			http.Error(w, `{"error": "Failed to retrieve XBRL data"}`, http.StatusInternalServerError)
			return
		}

		// Extract the "us-gaap" concepts.
		usGaapRaw, ok := xbrlData["facts"].(map[string]interface{})["us-gaap"]
		if !ok {
			log.Printf("[ERROR] No XBRL concepts found for ticker: %s", ticker)
			http.Error(w, `{"error": "No XBRL concepts found for ticker"}`, http.StatusNotFound)
			return
		}
		usGaap, ok := usGaapRaw.(map[string]interface{})
		if !ok || len(usGaap) == 0 {
			log.Printf("[ERROR] Invalid or empty US-GAAP data for ticker: %s", ticker)
			http.Error(w, `{"error": "Invalid XBRL concepts for ticker"}`, http.StatusNotFound)
			return
		}

		// Check if the requested concept exists.
		conceptDetailsRaw, exists := usGaap[conceptName]
		if !exists {
			log.Printf("[ERROR] Concept %s not found for ticker: %s", conceptName, ticker)
			http.Error(w, fmt.Sprintf(`{"error": "Concept %s not found for ticker: %s"}`, conceptName, ticker), http.StatusNotFound)
			return
		}
		conceptDetails, ok := conceptDetailsRaw.(map[string]interface{})
		if !ok {
			log.Printf("[ERROR] Invalid format for concept %s for ticker: %s", conceptName, ticker)
			http.Error(w, fmt.Sprintf(`{"error": "Invalid concept format for %s"}`, conceptName), http.StatusInternalServerError)
			return
		}

		// Extract the label.
		label := fmt.Sprintf("%v", conceptDetails["label"])
		// Skip deprecated concepts.
		if strings.Contains(label, "Deprecated") {
			http.Error(w, fmt.Sprintf(`{"error": "Concept %s is deprecated"}`, conceptName), http.StatusNotFound)
			return
		}

		// Extract available units.
		unitsRaw, ok := conceptDetails["units"]
		if !ok {
			http.Error(w, fmt.Sprintf(`{"error": "No unit data available for concept %s"}`, conceptName), http.StatusNotFound)
			return
		}
		unitsMap, ok := unitsRaw.(map[string]interface{})
		if !ok {
			http.Error(w, fmt.Sprintf(`{"error": "Invalid unit data format for concept %s"}`, conceptName), http.StatusInternalServerError)
			return
		}
		var availableUnits []string
		for unit := range unitsMap {
			availableUnits = append(availableUnits, unit)
		}
		sort.Strings(availableUnits)
		if !contains(availableUnits, "USD") {
			http.Error(w, fmt.Sprintf(`{"error": "Concept %s does not have USD data"}`, conceptName), http.StatusNotFound)
			return
		}

		// Fetch XBRL CSV data.
		xbrlDF, err := sf.FetchXBRLCSV(cik, conceptName, "USD")
		if err != nil {
			log.Printf("[ERROR] Failed fetching XBRL CSV for %s: %v", conceptName, err)
			http.Error(w, fmt.Sprintf(`{"error": "Failed fetching XBRL CSV for %s"}`, conceptName), http.StatusInternalServerError)
			return
		}

		// Extract date series from the CSV data.
		var dateSeries []time.Time
		for _, record := range xbrlDF {
			dateSeries = append(dateSeries, record.EndDate)
		}

		// Infer frequency.
		inferredFreq := InferFrequency(dateSeries)
		if inferredFreq == "Unknown" {
			http.Error(w, fmt.Sprintf(`{"error": "Inferred frequency is unknown for concept %s"}`, conceptName), http.StatusNotFound)
			return
		}

		// If the label is missing or contains "<nil>", update it.
		if strings.Contains(label, "nil") {
			label = camelCaseToSpaced(conceptName)
		}

		// Build the concept JSON object.
		conceptJSON := map[string]interface{}{
			"name":            conceptName,
			"label":           label,
			"data":            xbrlDF,
			"available_units": availableUnits,
			"inferred_freq":   inferredFreq,
		}

		// Marshal the output to pretty-printed JSON.
		responseJSON, err := json.MarshalIndent(conceptJSON, "", "  ")
		if err != nil {
			http.Error(w, fmt.Sprintf("Failed to encode JSON: %v", err), http.StatusInternalServerError)
			return
		}
		// Save the fetched XBRL data to a file.
		// Marshal the data with indentation for readability.
		/*
			fileData, err := json.MarshalIndent(xbrlDF, "", "  ")
			if err != nil {
				log.Printf("[ERROR] Failed to marshal XBRL data for file save: %v", err)
			} else {
				fileName := fmt.Sprintf("%s_%s.json", ticker, conceptName)
				if err := ioutil.WriteFile(fileName, fileData, 0644); err != nil {
					log.Printf("[ERROR] Failed to save XBRL data to file %s: %v", fileName, err)
				} else {
					log.Printf("[INFO] XBRL data saved to file: %s", fileName)
				}
			}
		*/
		// Write the response.
		w.Header().Set("Content-Type", "application/json")
		w.Write(responseJSON)
		log.Printf("[INFO] Completed processing for ticker %s and concept %s in %.3f ms", ticker, conceptName, float64(time.Since(startTime).Milliseconds()))
	}
}

// InferFrequency determines the most common time interval between dates and maps
// that interval to a standard frequency (Daily, Weekly, Monthly, Quarterly, or Yearly).
// If no standard frequency fits, it returns an approximate number of days or, if the interval
// is close to an integer number of years, that year count.
func InferFrequency(dates []time.Time) string {
	// Require at least 4 dates (i.e. 3 intervals) for a robust frequency inference.
	if len(dates) < 4 {
		log.Println("Insufficient dates provided for frequency inference.")
		return "Unknown"
	}

	// Sort the dates in ascending order.
	sort.Slice(dates, func(i, j int) bool {
		return dates[i].Before(dates[j])
	})

	// Log the sorted dates.
	log.Println("Sorted dates:")
	for _, d := range dates {
		log.Println(d)
	}

	// Compute differences between consecutive dates.
	// Round each diff to the nearest minute to reduce minor noise.
	deltas := make(map[time.Duration]int)
	var durations []time.Duration
	for i := 1; i < len(dates); i++ {
		diff := dates[i].Sub(dates[i-1])
		if diff > 0 {
			rounded := diff.Round(time.Minute)
			durations = append(durations, rounded)
			deltas[rounded]++
		}
	}

	if len(durations) == 0 {
		log.Println("No positive differences found between dates.")
		return "Unknown"
	}

	// Log the computed differences.
	log.Println("Computed differences (rounded to nearest minute):")
	for i, d := range durations {
		log.Printf("Interval %d: %v", i, d)
	}

	// Find the most common (mode) interval.
	var mostCommon time.Duration
	maxCount := 0
	for delta, count := range deltas {
		if count > maxCount {
			mostCommon = delta
			maxCount = count
		}
	}
	log.Printf("Most common interval: %v (occurred %d times)", mostCommon, maxCount)

	// Define standard durations.
	const (
		day     = 24 * time.Hour
		week    = 7 * day
		month   = 30 * day  // approximate month
		quarter = 90 * day  // approximate quarter
		year    = 365 * day // approximate year
	)

	// Define tolerances for each standard frequency.
	const (
		dailyTol     = 2 * time.Hour
		weeklyTol    = 2 * day
		monthlyTol   = 5 * day
		quarterlyTol = 10 * day
		yearlyTol    = 20 * day
	)

	// Check which standard frequency the most common interval fits into.
	switch {
	case mostCommon >= day-dailyTol && mostCommon <= day+dailyTol:
		log.Println("Inferred frequency: Daily")
		return "Daily"
	case mostCommon >= week-weeklyTol && mostCommon <= week+weeklyTol:
		log.Println("Inferred frequency: Weekly")
		return "Weekly"
	case mostCommon >= month-monthlyTol && mostCommon <= month+monthlyTol:
		log.Println("Inferred frequency: Monthly")
		return "Monthly"
	case mostCommon >= quarter-quarterlyTol && mostCommon <= quarter+quarterlyTol:
		log.Println("Inferred frequency: Quarterly")
		return "Quarterly"
	case mostCommon >= year-yearlyTol && mostCommon <= year+yearlyTol:
		log.Println("Inferred frequency: Yearly")
		return "Yearly"
	}

	// Additional check: if the interval is very close to an integer number of years,
	// return that as the inferred frequency.
	approxDays := mostCommon.Hours() / 24
	years := approxDays / 365.0
	roundedYears := math.Round(years)
	if math.Abs(years-roundedYears) < 0.1 {
		yr := int(roundedYears)
		if yr == 1 {
			log.Println("Inferred frequency: Yearly")
			return "Yearly"
		}
		inferred := fmt.Sprintf("%d Years", yr)
		log.Printf("Inferred frequency: %s", inferred)
		return inferred
	}

	// Fallback: return the approximate number of days.
	inferred := fmt.Sprintf("%d days", int(approxDays))
	log.Printf("Inferred frequency: %s", inferred)
	return inferred
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
	http.HandleFunc("/company-info", measureSpeedMiddleware(companyInfoHandler(companyInfo, secFilings)))
	http.HandleFunc("/sec-filings", measureSpeedMiddleware(secFilingsHandler(companyInfo, secFilings)))
	http.HandleFunc("/forms", measureSpeedMiddleware(getAvailableForms(companyInfo, secFilings)))
	http.HandleFunc("/latest-filing/html", measureSpeedMiddleware(downloadLatestFilingHTML(companyInfo, secFilings)))
	http.HandleFunc("/xbrl/concepts", measureSpeedMiddleware(listXBRLConceptsAsJSON(companyInfo, secFilings)))
	http.HandleFunc("/xbrl/concepts-full", measureSpeedMiddleware(listXBRLConceptsAsJSONFull(companyInfo, secFilings)))
	// http.HandleFunc("/xbrl/concept", measureSpeedMiddleware(fetchXBRLConceptJSON(companyInfo, secFilings)))
	http.HandleFunc("/xbrl/concept-full", measureSpeedMiddleware(fetchXBRLConceptJSONFull(companyInfo, secFilings)))
	http.HandleFunc("/filing/html", measureSpeedMiddleware(downloadHTMLContentHandler(companyInfo, secFilings)))
	port := "8001"
	fmt.Printf("Starting server on 0.0.0.0:%s...\n", port)
	log.Fatal(http.ListenAndServe("0.0.0.0:"+port, nil))
}
