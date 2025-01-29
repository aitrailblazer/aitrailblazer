package company_info

import (
	"encoding/json"
	"errors"
	"fmt"
	"io/ioutil"
	"strings"
)

type CompanyInfo struct {
	Fields  []string        `json:"fields"`
	Records [][]interface{} `json:"data"`
}

// NewCompanyInfo initializes a CompanyInfo object by reading a JSON file
func NewCompanyInfo(jsonPath string) (*CompanyInfo, error) {
	data, err := ioutil.ReadFile(jsonPath)
	if err != nil {
		return nil, err
	}

	var companyInfo CompanyInfo
	err = json.Unmarshal(data, &companyInfo)
	if err != nil {
		return nil, err
	}

	return &companyInfo, nil
}

// ToDataFrame converts the raw records into a structured map
func (ci *CompanyInfo) ToDataFrame() map[string][]interface{} {
	df := make(map[string][]interface{})
	for _, field := range ci.Fields {
		df[field] = make([]interface{}, len(ci.Records))
	}

	for i, record := range ci.Records {
		for j, field := range ci.Fields {
			switch v := record[j].(type) {
			case float64:
				// Convert float64 to string for CIK or numeric fields
				df[field][i] = fmt.Sprintf("%.0f", v)
			default:
				df[field][i] = v
			}
		}
	}

	return df
}

// GetCIKByTicker retrieves the CIK for a given ticker
func (ci *CompanyInfo) GetCIKByTicker(ticker string) (string, error) {
	df := ci.ToDataFrame()
	for i, t := range df["ticker"] {
		if t == ticker {
			if cik, ok := df["cik"][i].(string); ok {
				return cik, nil
			}
		}
	}
	return "", errors.New("no company found with ticker: " + ticker)
}

// GetNameByTicker retrieves the company name for a given ticker
func (ci *CompanyInfo) GetNameByTicker(ticker string) (string, error) {
	df := ci.ToDataFrame()
	for i, t := range df["ticker"] {
		if t == ticker {
			if name, ok := df["name"][i].(string); ok {
				return name, nil
			}
		}
	}
	return "", errors.New("no company found with ticker: " + ticker)
}

// GetExchangeByTicker retrieves the exchange for a given ticker
func (ci *CompanyInfo) GetExchangeByTicker(ticker string) (string, error) {
	df := ci.ToDataFrame()
	for i, t := range df["ticker"] {
		if t == ticker {
			if exchange, ok := df["exchange"][i].(string); ok {
				return exchange, nil
			}
		}
	}
	return "", errors.New("no company found with ticker: " + ticker)
}

// GetTickersByExchange retrieves all tickers for a given exchange
func (ci *CompanyInfo) GetTickersByExchange(exchange string) []string {
	df := ci.ToDataFrame()
	var tickers []string
	for i, e := range df["exchange"] {
		if strings.EqualFold(e.(string), exchange) {
			tickers = append(tickers, df["ticker"][i].(string))
		}
	}
	return tickers
}

// GetAllExchanges retrieves all unique exchanges
func (ci *CompanyInfo) GetAllExchanges() []string {
	df := ci.ToDataFrame()
	exchangeMap := make(map[string]bool)
	for _, e := range df["exchange"] {
		exchangeMap[e.(string)] = true
	}

	var exchanges []string
	for exchange := range exchangeMap {
		exchanges = append(exchanges, exchange)
	}
	return exchanges
}

// GetCompaniesByExchange retrieves all companies for a given exchange
func (ci *CompanyInfo) GetCompaniesByExchange(exchange string) ([]map[string]interface{}, error) {
	df := ci.ToDataFrame()
	var companies []map[string]interface{}
	for i, e := range df["exchange"] {
		if e == exchange {
			company := map[string]interface{}{
				"cik":    df["cik"][i],
				"name":   df["name"][i],
				"ticker": df["ticker"][i],
			}
			companies = append(companies, company)
		}
	}
	if len(companies) == 0 {
		return nil, errors.New("no companies found for exchange: " + exchange)
	}
	return companies, nil
}

// SearchByName searches for companies whose names contain a substring
func (ci *CompanyInfo) SearchByName(substring string) []map[string]interface{} {
	df := ci.ToDataFrame()
	var results []map[string]interface{}
	for i, name := range df["name"] {
		if strings.Contains(strings.ToLower(name.(string)), strings.ToLower(substring)) {
			result := map[string]interface{}{
				"cik":    df["cik"][i],
				"name":   df["name"][i],
				"ticker": df["ticker"][i],
			}
			results = append(results, result)
		}
	}
	return results
}

// GetCompanyNameByTicker retrieves the company name for a given ticker
func (ci *CompanyInfo) GetCompanyNameByTicker(ticker string) string {
	df := ci.ToDataFrame()
	for i, t := range df["ticker"] {
		if fmt.Sprintf("%v", t) == ticker { // Ensure comparison works for different types
			return fmt.Sprintf("%v", df["name"][i]) // Convert to string
		}
	}
	return "Unknown"
}

// GetCompanyExchangeByTicker retrieves the exchange for a given ticker
func (ci *CompanyInfo) GetCompanyExchangeByTicker(ticker string) string {
	df := ci.ToDataFrame()
	for i, t := range df["ticker"] {
		if fmt.Sprintf("%v", t) == ticker {
			return fmt.Sprintf("%v", df["exchange"][i])
		}
	}
	return "Unknown"
}
