package filings

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"net/http"
	"os"
	"strings"

	"github.com/shopspring/decimal"
)

type SECFilings struct {
	Email   string
	Headers map[string]string
}

func NewSECFilings(email string) *SECFilings {
	headers := map[string]string{
		"User-Agent": email,
	}
	return &SECFilings{
		Email:   email,
		Headers: headers,
	}
}

func (s *SECFilings) GetCompanyFilings(cik string) (map[string]interface{}, error) {
	url := fmt.Sprintf("https://data.sec.gov/submissions/CIK%s.json", strings.Repeat("0", 10-len(cik))+cik)
	resp, err := http.Get(url)
	if err != nil {
		return nil, fmt.Errorf("failed to fetch data for CIK %s: %v", cik, err)
	}
	defer resp.Body.Close()

	if resp.StatusCode != http.StatusOK {
		return nil, fmt.Errorf("failed to fetch data for CIK %s: %s", cik, resp.Status)
	}

	var filings map[string]interface{}
	if err := json.NewDecoder(resp.Body).Decode(&filings); err != nil {
		return nil, fmt.Errorf("failed to decode response for CIK %s: %v", cik, err)
	}

	return filings, nil
}

func (s *SECFilings) FilingsToDataFrame(filings map[string]interface{}) ([]map[string]interface{}, error) {
	recentFilings, ok := filings["filings"].(map[string]interface{})["recent"].([]interface{})
	if !ok {
		return nil, fmt.Errorf("failed to parse recent filings")
	}

	var dataFrame []map[string]interface{}
	for _, filing := range recentFilings {
		filingMap, ok := filing.(map[string]interface{})
		if !ok {
			return nil, fmt.Errorf("failed to parse filing")
		}
		dataFrame = append(dataFrame, filingMap)
	}

	return dataFrame, nil
}

func (s *SECFilings) DownloadHTMLContent(cik, accessionNumber, fileName string) (string, error) {
	baseURL := fmt.Sprintf("https://www.sec.gov/Archives/edgar/data/%s/%s/%s", cik, accessionNumber, fileName)
	resp, err := http.Get(baseURL)
	if err != nil {
		return "", fmt.Errorf("failed to download HTML content: %v", err)
	}
	defer resp.Body.Close()

	if resp.StatusCode != http.StatusOK {
		return "", fmt.Errorf("failed to download HTML content: %s", resp.Status)
	}

	content, err := ioutil.ReadAll(resp.Body)
	if err != nil {
		return "", fmt.Errorf("failed to read HTML content: %v", err)
	}

	return string(content), nil
}

func (s *SECFilings) GetXBRLData(cik string) (map[string]interface{}, error) {
	url := fmt.Sprintf("https://data.sec.gov/api/xbrl/companyfacts/CIK%s.json", strings.Repeat("0", 10-len(cik))+cik)
	resp, err := http.Get(url)
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

		// Handle out-of-range float values
		if val, ok := factMap["val"].(float64); ok {
			factMap["val"] = decimal.NewFromFloat(val).String()
		}

		dataFrame = append(dataFrame, factMap)
	}

	return dataFrame, nil
}
