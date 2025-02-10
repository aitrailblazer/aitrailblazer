using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class GoSECEdgarWSAppService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GoSECEdgarWSAppService> _logger;

    public GoSECEdgarWSAppService(HttpClient httpClient, ILogger<GoSECEdgarWSAppService> logger)
    {
        httpClient.Timeout = TimeSpan.FromMinutes(5);
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<string> GetHelloAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to fetch Hello message.");
            return "Service unavailable";
        }
    }

    public async Task<object?> GetCompanyInfoAsync(string ticker = null)
    {
        try
        {
            string requestUri = string.IsNullOrWhiteSpace(ticker) ? "/company-info" : $"/company-info?ticker={Uri.EscapeDataString(ticker)}";
            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return string.IsNullOrWhiteSpace(ticker)
                ? JsonConvert.DeserializeObject<List<CompanyInfo>>(jsonResponse)
                : JsonConvert.DeserializeObject<CompanyInfo>(jsonResponse);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to fetch company info for ticker {Ticker}.", ticker);
            return null;
        }
    }

    public async Task<SECFilingsResponse> GetSECFilingsAsync(string ticker)
    {
        if (string.IsNullOrWhiteSpace(ticker))
            return new SECFilingsResponse(); // Return empty response on invalid input

        string requestUri = $"/sec-filings?ticker={Uri.EscapeDataString(ticker)}";
        var response = await _httpClient.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Failed to fetch SEC filings for {Ticker}: {StatusCode}", ticker, response.StatusCode);
            return new SECFilingsResponse();
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();

        // ✅ Log the raw JSON response
        _logger.LogInformation("Raw JSON response for {Ticker}: {JsonResponse}", ticker, jsonResponse);

        try
        {
            // ✅ Deserialize the JSON directly into a List<SECFiling> (array-based response)
            var filingsList = JsonConvert.DeserializeObject<List<SECFiling>>(jsonResponse);

            return new SECFilingsResponse { Filings = filingsList ?? new List<SECFiling>() };
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "JSON deserialization failed for {Ticker}. Returning empty response.", ticker);
            return new SECFilingsResponse();
        }
    }
    public async Task<AvailableFormsResponse?> GetAvailableFormsAsync(string ticker)
    {
        if (string.IsNullOrWhiteSpace(ticker))
        {
            _logger.LogWarning("Ticker is required.");
            return null;
        }

        try
        {
            string requestUri = $"/forms?ticker={Uri.EscapeDataString(ticker)}";
            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<AvailableFormsResponse>(await response.Content.ReadAsStringAsync());
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to fetch available forms for {Ticker}.", ticker);
            return null;
        }
    }

    public async Task<string> DownloadLatestFilingHtmlAsync(string ticker, string formType)
    {
        if (string.IsNullOrWhiteSpace(ticker) || string.IsNullOrWhiteSpace(formType))
        {
            _logger.LogWarning("Ticker and Form Type are required.");
            return string.Empty;
        }

        try
        {
            var response = await _httpClient.PostAsJsonAsync("/latest-filing/html", new { ticker, form_type = formType });
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to download latest filing HTML for {Ticker}.", ticker);
            return string.Empty;
        }
    }
    public async Task<XBRLNode> listXBRLConceptsAsJSON(string ticker, int limit = 25)
    {
        if (string.IsNullOrWhiteSpace(ticker))
        {
            _logger.LogWarning("Ticker is required.");
            return null;
        }

        // Build the request URI for the non-streaming endpoint.
        // Optionally include the limit parameter if your endpoint supports it.
        string requestUri = $"/xbrl/concepts?ticker={Uri.EscapeDataString(ticker)}&limit={limit}";

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to fetch XBRL concepts for {Ticker}.", ticker);
            return null;
        }

        // Read the complete JSON response as a string.
        string jsonResponse = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("listXBRLConceptsAsJSON Received JSON response for ticker {Ticker}: {jsonResponse}", ticker, jsonResponse);

        try
        {
            // Deserialize the JSON into an XBRLNode object.
            var conceptTree = JsonConvert.DeserializeObject<XBRLNode>(jsonResponse);
            return conceptTree;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to deserialize JSON response for ticker {Ticker}.", ticker);
            return null;
        }
    }
    public async Task<string> DownloadFilingHtmlAsync(string cik, string accessionNumber, string primaryDocument)
    {
        if (string.IsNullOrWhiteSpace(cik) || string.IsNullOrWhiteSpace(accessionNumber) || string.IsNullOrWhiteSpace(primaryDocument))
        {
            _logger.LogWarning("CIK, Accession Number, and Primary Document are required.");
            return string.Empty;
        }

        try
        {
            var response = await _httpClient.PostAsJsonAsync("/filing/html", new { cik, accession_number = accessionNumber, primary_document = primaryDocument });
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to download filing HTML for {Cik}.", cik);
            return string.Empty;
        }
    }

    public async Task<byte[]> ConvertHtmlToPdfAsync(string htmlContent)
    {
        if (string.IsNullOrWhiteSpace(htmlContent))
        {
            _logger.LogWarning("HTML content is required for conversion.");
            return Array.Empty<byte>();
        }

        try
        {
            var response = await _httpClient.PostAsJsonAsync("/html-to-pdf", new { html = htmlContent });
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to convert HTML to PDF.");
            return Array.Empty<byte>();
        }
    }
    public async Task<XBRLConceptFull> GetXBRLConceptFullAsync(string ticker, string concept)
    {
        if (string.IsNullOrWhiteSpace(ticker) || string.IsNullOrWhiteSpace(concept))
        {
            _logger.LogWarning("Ticker and Concept are required.");
            return null;
        }

        // Build the request URI with proper escaping of query parameters.
        string requestUri = $"/xbrl/concept-full?ticker={Uri.EscapeDataString(ticker)}&concept={Uri.EscapeDataString(concept)}";

        try
        {
            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();

            // Log the raw JSON response if needed
           // _logger.LogInformation("Raw JSON response for ticker {Ticker} and concept {Concept}: {JsonResponse}",
           //     ticker, concept, jsonResponse);

            // Deserialize the JSON response into our XBRLConceptFull model.
            var conceptFull = JsonConvert.DeserializeObject<XBRLConceptFull>(jsonResponse);
            return conceptFull;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to fetch full XBRL concept for ticker {Ticker} and concept {Concept}.", ticker, concept);
            return null;
        }
    }
}