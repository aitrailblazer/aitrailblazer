package main

import (
	"encoding/csv"
	"flag"
	"fmt"
	"log"
	"os"
	"regexp"
	"strings"

	"github.com/aitrailblazer/aitrailblazer/go-sec-edgar-ws/xbrl_tree/xbrlparser"
)

// XBRLEntry holds the title and file path for an XBRL file.
type XBRLEntry struct {
	Title    string
	FilePath string
}

func main() {
	// The path to the concepts JSON file is shared across all entries.
	jsonPath := flag.String("json", "TSLAconcepts.json", "Path to the concepts JSON file")
	flag.Parse()

	gaapCSV := "us-gaap-2024/xbrl_elements_20241108.csv"
	/*
	  - 104000 - Statement - Statement of Financial Position, Classified
	  us-gaap-stm-sfp-cls-pre-2024.xml
	  - 108000 - Statement - Statement of Financial Position, Unclassified - Deposit Based Operations
	  us-gaap-stm-sfp-dbo-pre-2024.xml
	  - 108200 - Statement - Statement of Financial Position, Unclassified - Insurance Based Operations
	  us-gaap-stm-sfp-ibo-pre-2024.xml
	  - 110000 - Statement - Statement of Financial Position, Classified - Real Estate Operations
	  us-gaap-stm-sfp-clreo-pre-2024.xml
	  - 110200 - Statement - Statement of Financial Position, Unclassified - Real Estate Operations
	  us-gaap-stm-sfp-ucreo-pre-2024.xml
	  - 112000 - Statement - Statement of Financial Position, Unclassified - Securities Based Operations
	  us-gaap-stm-sfp-sbo-pre-2024.xml
	  - 124000 - Statement - Statement of Income (Including Gross Margin)
	  us-gaap-stm-soi-pre-2024.xml
	  - 124100 - Statement - Statement of Income
	  us-gaap-stm-soi-egm-pre-2024.xml
	  - 124200 - Statement - Statement of Income, Additional Statement of Income Elements
	  us-gaap-stm-soi-indira-pre-2024.xml
	  - 132001 - Statement - Statement of Income, Interest Based Revenue
	  us-gaap-stm-soi-int-pre-2024.xml
	  - 136000 - Statement - Statement of Income, Insurance Based Revenue
	  us-gaap-stm-soi-ins-pre-2024.xml
	  - 140400 - Statement - Statement of Income, Securities Based Income
	  us-gaap-stm-soi-sbi-pre-2024.xml
	  - 144000 - Statement - Statement of Income, Real Estate, Excluding REITs
	  us-gaap-stm-soi-re-pre-2024.xml
	  - 145000 - Statement - Statement of Income, Real Estate Investment Trusts
	  us-gaap-stm-soi-reit-pre-2024.xml
	  - 148400 - Statement - Statement of Comprehensive Income
	  us-gaap-stm-soc-pre-2024.xml
	  - 148600 - Statement - Statement of Shareholders' Equity
	  us-gaap-stm-sheci-pre-2024.xml
	  - 152000 - Statement - Statement of Partners' Capital
	  us-gaap-stm-spc-pre-2024.xml
	  - 152200 - Statement - Statement of Cash Flows
	  us-gaap-stm-scf-indir-pre-2024.xml
	  - 152201 - Statement - Statement of Cash Flows, Additional Cash Flow Elements
	  us-gaap-stm-scf-indira-pre-2024.xml
	  - 152205 - Statement - Statement of Cash Flows, Supplemental Disclosures
	  us-gaap-stm-scf-sd-pre-2024.xml
	  - 160000 - Statement - Statement of Cash Flows, Deposit Based Operations
	  us-gaap-stm-scf-dbo-pre-2024.xml
	  - 164000 - Statement - Statement of Cash Flows, Insurance Based Operations
	  us-gaap-stm-scf-inv-pre-2024.xml
	  - 168400 - Statement - Statement of Cash Flows, Securities Based Operations
	  us-gaap-stm-scf-sbo-pre-2024.xml
	  - 170000 - Statement - Statement of Cash Flows, Real Estate, Including REITs
	  us-gaap-stm-scf-re-pre-2024.xml
	  - 172600 - Statement - Statement of Cash Flows, Direct Method Operating Activities
	  us-gaap-stm-scf-dir-pre-2024.xml
	  - 190000 - Statement - Common Domain Members
	  us-gaap-stm-com-pre-2024.xml

	  195000 - Disclosure - Comprehensive Text Block List
	  200000 - Disclosure - Organization, Consolidation and Presentation of Financial Statements
	  210000 - Disclosure - Balance Sheet Offsetting
	  220400 - Disclosure - Disaggregation of Income Statement Expense
	  250000 - Disclosure - Accounting Changes and Error Corrections
	  275000 - Disclosure - Risks and Uncertainties
	  285000 - Disclosure - Interim Reporting
	  290000 - Disclosure - Accounting Policies
	  300000 - Disclosure - Cash and Cash Equivalents
	  320000 - Disclosure - Receivables, Loans, Notes Receivable, and Others
	  326000 - Disclosure - Credit Losses
	  330000 - Disclosure - Investments, Debt and Equity Securities
	  333000 - Disclosure - Investments, Equity Method and Joint Ventures
	  336000 - Disclosure - Investments, All Other Investments
	  340000 - Disclosure - Inventory
	  350000 - Disclosure - Deferred Costs, Capitalized, Prepaid, and Other Assets
	  360000 - Disclosure - Property, Plant, and Equipment
	  370000 - Disclosure - Intangible Assets, Goodwill and Other
	  400000 - Disclosure - Payables and Accruals
	  420000 - Disclosure - Asset Retirement Obligations
	  425000 - Disclosure - Environmental Remediation Obligations
	  430000 - Disclosure - Restructuring and Related Activities
	  440000 - Disclosure - Revenue Recognition and Deferred Revenue
	  450000 - Disclosure - Commitment and Contingencies
	  456000 - Disclosure - Guarantees
	  460000 - Disclosure - Debt
	  470000 - Disclosure - Other Liabilities
	  472000 - Disclosure - Noncontrolling Interest
	  480000 - Disclosure - Temporary Equity
	  500000 - Disclosure - Equity
	  606000 - Disclosure - Revenue from Contract with Customer
	  705000 - Disclosure - Compensation Related Costs, General
	  710000 - Disclosure - Compensation Related Costs, Share Based Payments
	  730000 - Disclosure - Compensation Related Costs, Retirement Benefits
	  740000 - Disclosure - Compensation Related Costs, Postemployment Benefits
	  750000 - Disclosure - Other Income and Expenses
	  760000 - Disclosure - Research and Development
	  770000 - Disclosure - Income Taxes
	  775000 - Disclosure - Discontinued Operations and Disposal Groups
	  778000 - Disclosure - Unusual or Infrequently Occurring Items
	  780000 - Disclosure - Earnings Per Share
	  790000 - Disclosure - Segment Reporting
	  800000 - Disclosure - Business Combinations, Asset Acquisitions, Transaction between Entities under Common Control, and Joint Venture Formation
	  802000 - Disclosure - Reorganizations
	  805000 - Disclosure - Derivative Instruments and Hedging Activities
	  815000 - Disclosure - Fair Value Measures and Disclosures
	  820000 - Disclosure - Foreign Operations and Currency Translation
	  831000 - Disclosure - Leases, Codification Topic 840
	  832000 - Disclosure - Government Assistance
	  840000 - Disclosure - Nonmonetary Transactions
	  842000 - Disclosure - Leases, Codification Topic 842
	  845000 - Disclosure - Related Party Disclosures
	  865000 - Disclosure - Transfers and Servicing
	  870000 - Disclosure - Subsequent Events
	  910000 - Disclosure - Contractors
	  939000 - Disclosure - Financial Services, Federal Home Loan Banks
	  940000 - Disclosure - Financial Services, Banking and Thrift
	  940050 - Disclosure - Financial Services, Banking and Thrift
	  942000 - Disclosure - Financial Services, Brokers and Dealers
	  944000 - Disclosure - Financial Services, Insurance
	  946000 - Disclosure - Financial Services, Investment Company
	  948000 - Disclosure - Financial Services, Mortgage Banking
	  955000 - Disclosure - Health Care Organizations
	  965000 - Disclosure - Extractive Industries
	  975000 - Disclosure - Real Estate
	  980000 - Disclosure - Regulated Operations
	  985000 - Disclosure - Other Industries
	  990000 - Disclosure - SEC Disclosure, Security Registered or Being Registered
	  991000 - Disclosure - SEC Schedule, Article 12-04, Condensed Financial Information of Registrant
	  993000 - Disclosure - SEC Schedule, Article 12-09, Valuation and Qualifying Accounts
	  993200 - Disclosure - SEC Schedule, Article 12-28, Real Estate and Accumulated Depreciation
	  993400 - Disclosure - SEC Schedule, Article 12-29, Mortgage Loans on Real Estate
	  993500 - Disclosure - SEC Schedule, Article 12-12, Investments in Securities of Unaffiliated Issuers
	  993510 - Disclosure - SEC Schedule, Article 12-13D, Investments Other than Those Presented in 12-12 Through 12-13C
	  993520 - Disclosure - Summary of Investment Holdings
	  993530 - Disclosure - Investments Federal Income Tax Note
	  993540 - Disclosure - SEC Schedule, Article 12-12A, Investments in Securities Sold Short
	  993560 - Disclosure - SEC Schedule, Article 12-13 through 13C, Open Option, Futures, Forward Foreign Currency, and Swap Contracts
	  993570 - Disclosure - SEC Schedule, Article 12-14, Investments in and Advances to Affiliates
	  993600 - Disclosure - SEC Schedule, Article 12-15, Summary of Investments - Other than Investments in Related Parties
	  993800 - Disclosure - SEC Schedule, Article 12-16, Supplementary Insurance Information
	  994000 - Disclosure - SEC Schedule, Article 12-17, Reinsurance
	  994200 - Disclosure - SEC Schedule, Article 12-18, Supplemental Information (for Property-Casualty Insurance Underwriters)
	  995000 - Disclosure - Private Company Supplemental
	  995100 - Document - Cover
	  995300 - Document - Audit Information
	  995410 - Document - Country Code
	  995420 - Document - State or Province
	  995430 - Document - Currency
	  995440 - Document - Exchange
	*/
	// Define an array of XBRL entries.
	// Define an array of XBRL entries.
	entries := []XBRLEntry{
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
		{
			Title:    "190000 - Statement - Common Domain Members",
			FilePath: "us-gaap-2024/stm/us-gaap-stm-com-pre-2024.xml",
		},
	}
	// Define a regular expression to match the prefix pattern.
	// This regex looks for one or more digits, followed by " - Statement - " (with optional spaces)
	re := regexp.MustCompile(`^\d+\s*-\s*Statement\s*-\s*`)

	// Iterate over the entries and remove the prefix from each Title.
	for i, entry := range entries {
		entries[i].Title = re.ReplaceAllString(entry.Title, "")
	}

	// Print out the modified entries to verify the change.
	// for _, entry := range entries {
	// 	fmt.Printf("Title: %s\nFilePath: %s\n\n", entry.Title, entry.FilePath)
	// }
	// Load the concepts JSON file once.
	conceptMap, err := xbrlparser.LoadConcepts(*jsonPath)
	if err != nil {
		log.Fatalf("Error loading concepts: %v", err)
	}

	// Load the GAAP CSV mapping (elementName -> title).
	gaapMapping, err := loadGaapMapping(gaapCSV)
	if err != nil {
		log.Fatalf("Error loading GAAP CSV: %v", err)
	}

	// Prepare a slice to hold the parsed trees.
	var combinedChildren []*xbrlparser.Node

	// Iterate over each entry, parse the file, count matching nodes, and create a node.
	for _, entry := range entries {
		trees, err := xbrlparser.ParseXBRL(entry.FilePath, conceptMap)
		if err != nil {
			log.Printf("Error parsing XBRL for %s: %v", entry.Title, err)
			continue
		}

		// Count only the nodes that have a matching concept.
		matchingCount := 0
		for _, tree := range trees {
			matchingCount += countMatchingNodes(tree)
		}

		// Create a node for this entry with an updated label that includes the matching count.
		entryNode := &xbrlparser.Node{
			Label:    fmt.Sprintf("%s %d", entry.Title, matchingCount),
			Children: trees,
		}

		combinedChildren = append(combinedChildren, entryNode)
	}

	// Create a root node that combines all entries.
	root := &xbrlparser.Node{
		Label:    "START",
		Children: combinedChildren,
	}

	// Apply GAAP mapping to update any nodes whose key contains "Abstract".
	xbrlparser.ApplyGaapMapping(root, gaapMapping)

	// Prune out unwanted nodes.
	root.Children = pruneNodes(root.Children)

	// Print the combined tree.
	xbrlparser.PrintTree(root, 0)
}

// countMatchingNodes recursively counts nodes that have a matching concept.
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

// pruneNodes recursively processes a slice of nodes and removes any node whose label,
// after trimming and lowercasing, contains "statementtable" or "statementlineitems".
// For each node that is pruned, its children are lifted into the parent's slice.
func pruneNodes(nodes []*xbrlparser.Node) []*xbrlparser.Node {
	var result []*xbrlparser.Node
	for _, node := range nodes {
		// First, recursively prune the children.
		node.Children = pruneNodes(node.Children)

		// Check if the node label contains unwanted substrings.
		labelLower := strings.ToLower(strings.TrimSpace(node.Label))
		if strings.Contains(labelLower, "statementtable") || strings.Contains(labelLower, "statementlineitems") {
			// Node is to be pruned: lift its children into the parent's slice.
			result = append(result, node.Children...)
		} else {
			result = append(result, node)
		}
	}
	return result
}

// loadGaapMapping loads the GAAP CSV file and returns a mapping from elementName to title.
// It assumes the CSV columns are:
// areaTitle, topic, subTopic, section, paragraph, subParagraph, elementName, title, elementID, refRole, status, lastUpdated
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
	// Skip header row (assumed to be the first row).
	for i, record := range records {
		if i == 0 {
			continue
		}
		// Ensure we have at least 8 columns.
		if len(record) < 8 {
			continue
		}
		// Trim spaces to ensure proper matching.
		elementName := strings.TrimSpace(record[6]) // 7th column: elementName
		title := strings.TrimSpace(record[7])       // 8th column: title
		gaapMap[elementName] = title
	}
	return gaapMap, nil
}
