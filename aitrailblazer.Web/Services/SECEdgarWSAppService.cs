using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
public class SECEdgarWSAppService
{
    private readonly HttpClient _httpClient;

    public SECEdgarWSAppService(HttpClient httpClient)
    {
        // Set a custom timeout for long-running requests
        httpClient.Timeout = TimeSpan.FromMinutes(5); // Increase timeout to 5 minutes
        _httpClient = httpClient;
    }

    /// <summary>
    /// Fetches a "Hello, World!" response from the root endpoint.
    /// </summary>
    public async Task<string> GetHelloWorldAsync()
    {
        var response = await _httpClient.GetAsync("/");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// Fetches the CIK (Central Index Key) for a given stock ticker from the FastAPI server.
    /// </summary>
    /// <param name="ticker">The stock ticker symbol (e.g., AAPL).</param>
    /// <returns>The CIK as a string.</returns>
    public async Task<string?> GetCIKAsync(string ticker)
    {
        try
        {
            // Validate ticker format before making the request
            if (string.IsNullOrWhiteSpace(ticker) || !System.Text.RegularExpressions.Regex.IsMatch(ticker, @"^[A-Za-z0-9]+$"))
            {
                Console.WriteLine("Invalid ticker format. Only alphanumeric characters are allowed.");
                return string.Empty; // Return empty string for unexpected errors
            }

            // Construct the endpoint URL for fetching the CIK
            string endpoint = $"/cik/{ticker}";

            // Send a GET request to the endpoint
            var response = await _httpClient.GetAsync(endpoint);

            // If the response status is 404 (Not Found), return null
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Ticker not found: {ticker}");
                return string.Empty; // Return empty string for unexpected errors
            }

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Parse the response as plain text
            var cik = await response.Content.ReadAsStringAsync();

            // Validate the CIK
            if (string.IsNullOrWhiteSpace(cik) || !long.TryParse(cik, out _))
            {
                Console.WriteLine("Invalid or empty CIK returned by the server.");
                return string.Empty; // Return empty string for unexpected errors
            }

            // Log the retrieved CIK
            Console.WriteLine($"Fetched CIK for {ticker}: {cik}");

            return cik;
        }
        catch (HttpRequestException httpEx)
        {
            // Log the HTTP request error
            Console.WriteLine($"HTTP request error: {httpEx.Message}");
            return string.Empty; // Return empty string for unexpected errors
        }
        catch (Exception ex)
        {
            // Log any unexpected error
            Console.WriteLine($"Unexpected error: {ex.Message}");
            return string.Empty; // Return empty string for unexpected errors
        }
    }

  
    public async Task<string> GetNameAsync(string ticker)
    {
        try
        {
            // Validate ticker format before making the request
            if (string.IsNullOrWhiteSpace(ticker) || !System.Text.RegularExpressions.Regex.IsMatch(ticker, @"^[A-Za-z0-9]+$"))
            {
                Console.WriteLine("Invalid ticker format provided.");
                return string.Empty; // Return an empty string for invalid ticker
            }

            // Construct the endpoint URL for fetching the company name
            string endpoint = $"/name/{ticker}";

            // Send a GET request to the endpoint
            var response = await _httpClient.GetAsync(endpoint);

            // If the response status is 404 (Not Found), return null
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Ticker not found: {ticker}");
                return string.Empty; // Return empty string for unexpected errors
            }

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Parse the response as plain text
            var name = await response.Content.ReadAsStringAsync();

            // Validate the name
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Invalid or empty name returned by the server.");
                return string.Empty; // Return empty string for unexpected errors
            }

            // Log the retrieved name
            Console.WriteLine($"Fetched name for {ticker}: {name}");

            return name;
        }
        catch (HttpRequestException httpEx)
        {
            // Log the HTTP request error
            Console.WriteLine($"HTTP request error: {httpEx.Message}");
            return string.Empty; // Return empty string for unexpected errors
        }
        catch (Exception ex)
        {
            // Log any unexpected error
            Console.WriteLine($"Unexpected error: {ex.Message}");
            return string.Empty; // Return empty string for unexpected errors
        }
    }
    public async Task<string> GetExchangeAsync(string ticker)
    {
        try
        {
            // Validate ticker format before making the request
            if (string.IsNullOrWhiteSpace(ticker) || !System.Text.RegularExpressions.Regex.IsMatch(ticker, @"^[A-Za-z0-9]+$"))
            {
                Console.WriteLine("Invalid ticker format provided.");
                return string.Empty; // Return an empty string for invalid ticker
            }

            // Construct the endpoint URL for fetching the company name
            string endpoint = $"/exchange/{ticker}";

            // Send a GET request to the endpoint
            var response = await _httpClient.GetAsync(endpoint);

            // If the response status is 404 (Not Found), return null
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Ticker not found: {ticker}");
                return string.Empty; // Return empty string for unexpected errors
            }

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Parse the response as plain text
            var exchange = await response.Content.ReadAsStringAsync();

            // Validate the name
            if (string.IsNullOrEmpty(exchange))
            {
                Console.WriteLine("Invalid or empty exchange returned by the server.");
                return string.Empty; // Return empty string for unexpected errors
            }

            // Log the retrieved name
            Console.WriteLine($"Fetched exchange for {ticker}: {exchange}");

            return exchange;
        }
        catch (HttpRequestException httpEx)
        {
            // Log the HTTP request error
            Console.WriteLine($"HTTP request error: {httpEx.Message}");
            return string.Empty; // Return empty string for unexpected errors
        }
        catch (Exception ex)
        {
            // Log any unexpected error
            Console.WriteLine($"Unexpected error: {ex.Message}");
            return string.Empty; // Return empty string for unexpected errors
        }
    }
    /// <summary>
    /// Fetches the filing history for a given stock ticker.
    /// </summary>
    /// <param name="ticker">The stock ticker symbol (e.g., AAPL).</param>
    public async Task<string> GetFilingsAsync(string ticker)
    {
        // Construct the endpoint URL for fetching filings
        string endpoint = $"/filings/{ticker}";

        // Send a GET request to the endpoint
        var response = await _httpClient.GetAsync(endpoint);

        // Ensure the request was successful
        response.EnsureSuccessStatusCode();

        // Return the JSON response as a string
        return await response.Content.ReadAsStringAsync();
    }
    /// <summary>
    /// Fetches the available forms for a given stock ticker.
    /// </summary>
    /// <param name="ticker">The stock ticker symbol (e.g., AAPL).</param>
    public async Task<string[]> GetAvailableFormsAsync(string ticker)
    {
        try
        {
            // Validate the ticker format before making the request
            if (string.IsNullOrWhiteSpace(ticker) || !Regex.IsMatch(ticker, @"^[A-Za-z0-9]+$"))
            {
                Console.WriteLine("Invalid ticker format provided.");
                return Array.Empty<string>(); // Return an empty array for invalid ticker
            }

            // Construct the endpoint URL
            string endpoint = $"/forms/{ticker}";

            // Send the GET request
            var response = await _httpClient.GetAsync(endpoint);

            // Handle non-successful responses gracefully
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"No forms found for ticker {ticker}. Returning an empty array.");
                    return Array.Empty<string>(); // Return an empty array if forms are not found
                }

                Console.WriteLine($"Failed to fetch forms for ticker {ticker}. Status: {response.StatusCode}");
                return Array.Empty<string>(); // Return an empty array for other HTTP errors
            }

            // Parse the JSON response
            var jsonResponse = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(jsonResponse);

            // Extract the "forms" property
            if (document.RootElement.TryGetProperty("forms", out var formsElement) && formsElement.ValueKind == JsonValueKind.Array)
            {
                var forms = formsElement.Deserialize<string[]>();
                if (forms != null && forms.Any())
                {
                    Console.WriteLine($"Fetched forms for ticker {ticker}: {string.Join(", ", forms)}");
                    return forms; // Return the list of forms
                }

                Console.WriteLine($"No forms available in the response for ticker {ticker}.");
                return Array.Empty<string>(); // Return an empty array if no forms are available
            }

            Console.WriteLine($"Forms property not found in the response for ticker {ticker}.");
            return Array.Empty<string>(); // Return an empty array if "forms" property is missing
        }
        catch (JsonException jsonEx)
        {
            Console.WriteLine($"JSON parsing error: {jsonEx.Message}");
            return Array.Empty<string>(); // Return an empty array for JSON parsing errors
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine($"HTTP request error: {httpEx.Message}");
            return Array.Empty<string>(); // Return an empty array for network errors
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while fetching forms for ticker {ticker}: {ex.Message}");
            return Array.Empty<string>(); // Return an empty array for unexpected errors
        }
    }

    /// <summary>
    /// Downloads the latest filing of a specified form type as raw HTML.
    /// </summary>
    /// <param name="ticker">The stock ticker symbol (e.g., AAPL).</param>
    /// <param name="formType">The form type (e.g., 10-K, 10-Q).</param>
    /// <returns>HTML content as a string.</returns>
    /// <summary>
    /// Downloads the latest filing of a specified form type as raw HTML using a POST request.
    /// </summary>
    /// <param name="ticker">The stock ticker symbol (e.g., AAPL).</param>
    /// <param name="formType">The form type (e.g., 10-K, 10-Q).</param>
    /// <returns>HTML content as a string.</returns>
    public async Task<string> DownloadLatestFilingHtmlAsync(string ticker, string formType)
    {
        try
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(ticker) || !Regex.IsMatch(ticker, @"^[A-Za-z0-9]+$"))
            {
                Console.WriteLine("Invalid ticker format provided.");
                return string.Empty; // Return an empty string for invalid ticker
            }

            if (string.IsNullOrWhiteSpace(formType))
            {
                Console.WriteLine("Form type cannot be empty.");
                return string.Empty; // Return an empty string for missing form type
            }

            // Set the endpoint URL for the POST request
            string endpoint = "/filing/html";

            // Prepare the POST request payload
            var payload = new
            {
                ticker,
                form_type = formType
            };

            // Send the POST request to the server
            var response = await _httpClient.PostAsJsonAsync(endpoint, payload);

            // Handle non-successful responses
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Filing not found for ticker {ticker} and form type {formType}.");
                    return string.Empty; // Return an empty string for not found
                }

                Console.WriteLine($"Failed to fetch HTML content for ticker {ticker} and form type {formType}. Status: {response.StatusCode}");
                return string.Empty; // Return an empty string for other errors
            }

            // Parse and return the response content as plain text
            var htmlContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Successfully fetched HTML content for ticker {ticker} and form type {formType}.");
            return htmlContent;
        }
        catch (HttpRequestException httpEx)
        {
            // Log the HTTP request error
            Console.WriteLine($"HTTP request error: {httpEx.Message}");
            return string.Empty; // Return an empty string for network-related issues
        }
        catch (Exception ex)
        {
            // Log any unexpected errors
            Console.WriteLine($"Unexpected error: {ex.Message}");
            return string.Empty; // Return an empty string for unexpected errors
        }
    }
  /// <summary>
    /// Fetches the HTML content of a filing given the CIK, accession number, and primary document name.
    /// </summary>
    /// <param name="cik">The Central Index Key (CIK) of the company.</param>
    /// <param name="accessionNumber">The accession number of the filing.</param>
    /// <param name="primaryDocument">The primary document name (e.g., "10-k.htm").</param>
    /// <returns>HTML content as a string.</returns>
    public async Task<string> DownloadHtmlContentAsync(string cik, string accessionNumber, string primaryDocument)
    {
        try
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(cik) || !Regex.IsMatch(cik, @"^\d+$"))
            {
                Console.WriteLine("Invalid CIK format. Only numeric characters are allowed.");
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(primaryDocument))
            {
                Console.WriteLine("Primary document cannot be empty.");
                return string.Empty;
            }

            // Construct the endpoint URL (POST request)
            string endpoint = "/html/download";
            Console.WriteLine($"DownloadHtmlContentAsync Endpoint: {endpoint}");
            Console.WriteLine($"DownloadHtmlContentAsync cik: {cik}");
            Console.WriteLine($"DownloadHtmlContentAsync accessionNumber: {accessionNumber}");
            Console.WriteLine($"DownloadHtmlContentAsync primaryDocument: {primaryDocument}");

            // Prepare the POST request payload
            var payload = new
            {
                cik,
                accession_number = accessionNumber,
                primary_document = primaryDocument
            };

            // Send a POST request to the endpoint
            var response = await _httpClient.PostAsJsonAsync(endpoint, payload);

            // Handle non-successful responses
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Filing not found: CIK={cik}, AccessionNumber={accessionNumber}, Document={primaryDocument}");
                    return string.Empty;
                }

                Console.WriteLine($"Failed to fetch HTML content for CIK={cik}, AccessionNumber={accessionNumber}. Status: {response.StatusCode}");
                return string.Empty;
            }

            // Parse and return the response content as plain text
            var htmlContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"DownloadHtmlContentAsync Successfully fetched HTML content for CIK={cik}, AccessionNumber={accessionNumber}");
            //Console.WriteLine($"DownloadHtmlContentAsync htmlContent {htmlContent}");
            return htmlContent;
        }
        catch (HttpRequestException httpEx)
        {
            // Log HTTP request errors
            Console.WriteLine($"HTTP request error: {httpEx.Message}");
            return string.Empty;
        }
        catch (Exception ex)
        {
            // Log unexpected errors
            Console.WriteLine($"Unexpected error: {ex.Message}");
            return string.Empty;
        }
    }
    public async Task<string> GetXBRLPlotAsync(string ticker, string concept = "AssetsCurrent", string unit = "USD")
    {
        // Construct the endpoint URL with query parameters
        string endpoint = $"/xbrl/plot/{ticker}?concept={concept}&unit={unit}";

        // Send a GET request to the endpoint
        var response = await _httpClient.GetAsync(endpoint);

        // Ensure the request was successful
        response.EnsureSuccessStatusCode();

        // Return the HTML content as a string
        return await response.Content.ReadAsStringAsync();
    }

}
