package main

import (
	"encoding/json"
	"errors"
	"io/ioutil"
	"strings"
)

type CompanyInfo struct {
	Fields  []string        `json:"fields"`
	Records [][]interface{} `json:"data"`
}

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

func (ci *CompanyInfo) ToDataFrame() map[string][]interface{} {
	df := make(map[string][]interface{})
	for _, field := range ci.Fields {
		df[field] = make([]interface{}, len(ci.Records))
	}

	for i, record := range ci.Records {
		for j, field := range ci.Fields {
			df[field][i] = record[j]
		}
	}

	return df
}

func (ci *CompanyInfo) GetCIKByTicker(ticker string) (string, error) {
	df := ci.ToDataFrame()
	for i, t := range df["ticker"] {
		if t == ticker {
			return df["cik"][i].(string), nil
		}
	}
	return "", errors.New("no company found with ticker: " + ticker)
}

func (ci *CompanyInfo) GetNameByTicker(ticker string) (string, error) {
	df := ci.ToDataFrame()
	for i, t := range df["ticker"] {
		if t == ticker {
			return df["name"][i].(string), nil
		}
	}
	return "", errors.New("no company found with ticker: " + ticker)
}

func (ci *CompanyInfo) GetExchangeByTicker(ticker string) (string, error) {
	df := ci.ToDataFrame()
	for i, t := range df["ticker"] {
		if t == ticker {
			return df["exchange"][i].(string), nil
		}
	}
	return "", errors.New("no company found with ticker: " + ticker)
}

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
