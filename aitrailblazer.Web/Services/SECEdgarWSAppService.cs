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
    public async Task<Stream> GetXBRLCsvAsync(string ticker, string concept = "AssetsCurrent", string unit = "USD")
    {
        if (string.IsNullOrWhiteSpace(ticker))
        {
            throw new ArgumentException("Ticker must not be null or empty.", nameof(ticker));
        }

        try
        {
            // Construct the endpoint URL with query parameters
            string endpoint = $"/xbrl/{ticker}/csv?concept={concept}&unit={unit}";

            // Log the request (optional, useful for debugging)
            Console.WriteLine($"Fetching XBRL CSV from endpoint: {endpoint}");

            // Send a GET request to the endpoint
            var response = await _httpClient.GetAsync(endpoint);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Return the CSV content as a stream
            return await response.Content.ReadAsStreamAsync();
        }
        catch (HttpRequestException ex)
        {
            // Handle HTTP-specific exceptions
            Console.WriteLine($"HTTP request error: {ex.Message}");
            throw new Exception("Failed to fetch XBRL CSV data. Please try again later.", ex);
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw new Exception("An unexpected error occurred while fetching XBRL CSV data.", ex);
        }
    }
    public async Task<string> GetXBRLConceptJsonAsync(string ticker, string concept)
    {
        try
        {
            // Validate the ticker format
            if (string.IsNullOrWhiteSpace(ticker) || !Regex.IsMatch(ticker, @"^[A-Za-z0-9]+$"))
            {
                throw new ArgumentException("Invalid ticker format. Only alphanumeric characters are allowed.");
            }

            // Construct the endpoint URL for fetching XBRL concepts
            string endpoint = $"/xbrl/{ticker}/json?concept={concept}";

            // Log the request
            Console.WriteLine($"Fetching XBRL concepts JSON for ticker: {ticker} from endpoint: {endpoint}");

            // Send the GET request
            var response = await _httpClient.GetAsync(endpoint);

            // Handle non-successful responses gracefully
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"No XBRL concepts found for ticker: {ticker}");
                    return string.Empty; // Return an empty string if no concepts are found
                }

                throw new HttpRequestException($"Failed to fetch XBRL concepts JSON. Status: {response.StatusCode}");
            }

            // Parse the JSON response as a string
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Successfully fetched XBRL concepts JSON for ticker: {ticker}");

            return jsonResponse; // Return the JSON response
        }
        catch (ArgumentException argEx)
        {
            // Log argument-related errors
            Console.WriteLine($"Argument error: {argEx.Message}");
            return string.Empty;
        }
        catch (HttpRequestException httpEx)
        {
            // Log HTTP-specific exceptions
            Console.WriteLine($"HTTP request error: {httpEx.Message}");
            return string.Empty;
        }
        catch (Exception ex)
        {
            // Log any other unexpected errors
            Console.WriteLine($"Unexpected error in {nameof(GetXBRLConceptJsonAsync)}: {ex.Message}");
            return string.Empty;
        }
    }
    public async Task<(bool IsSuccess, string PlotHtml, string OriginalDataJson, string ForecastDataJson, string CombinedDataJson, string ErrorCode)> GetForecastPlotHtmlAsync(
       string ticker, string concept = "AssetsCurrent", string unit = "USD", int horizon = 12)
    {
        try
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(ticker) || !Regex.IsMatch(ticker, @"^[A-Za-z0-9]+$"))
            {
                Console.WriteLine("Invalid ticker format. Only alphanumeric characters are allowed.");
                return (false, string.Empty, string.Empty, string.Empty, string.Empty, "InvalidTicker");
            }

            if (string.IsNullOrWhiteSpace(concept))
            {
                Console.WriteLine("Concept cannot be empty.");
                return (false, string.Empty, string.Empty, string.Empty, string.Empty, "EmptyConcept");
            }

            if (horizon <= 0)
            {
                Console.WriteLine("Horizon must be a positive integer.");
                return (false, string.Empty, string.Empty, string.Empty, string.Empty, "InvalidHorizon");
            }

            // Construct the endpoint URL with query parameters
            string endpoint = $"/xbrl/{ticker}/forecast/plot?concept={concept}&unit={unit}&h={horizon}";

            // Log the request
            Console.WriteLine($"Fetching forecast plot from endpoint: {endpoint}");

            // Send a GET request to the endpoint
            var response = await _httpClient.GetAsync(endpoint);

            // Handle unsuccessful responses
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to fetch forecast plot. Status code: {response.StatusCode}");
                return (false, string.Empty, string.Empty, string.Empty, string.Empty, $"HttpError_{(int)response.StatusCode}");
            }

            // Parse the JSON response
            var jsonResponse = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(jsonResponse);

            string plotHtml = string.Empty;
            string originalDataJson = string.Empty;
            string forecastDataJson = string.Empty;
            string combinedDataJson = string.Empty;

            // Extract 'plot_html'
            if (document.RootElement.TryGetProperty("plot_html", out var plotElement))
            {
                plotHtml = plotElement.GetString() ?? string.Empty;
            }

            // Extract 'original_data_json'
            if (document.RootElement.TryGetProperty("original_data_json", out var originalDataElement))
            {
                originalDataJson = originalDataElement.GetRawText(); // Get as raw JSON
            }

            // Extract 'forecast_data_json'
            if (document.RootElement.TryGetProperty("forecast_data_json", out var forecastDataElement))
            {
                forecastDataJson = forecastDataElement.GetRawText(); // Get as raw JSON
            }

            // Extract 'combined_data_json'
            if (document.RootElement.TryGetProperty("combined_data_json", out var combinedDataElement))
            {
                combinedDataJson = combinedDataElement.GetRawText(); // Get as raw JSON
            }

            if (!string.IsNullOrEmpty(plotHtml))
            {
                Console.WriteLine("Successfully fetched forecast plot and data.");
                return (true, plotHtml, originalDataJson, forecastDataJson, combinedDataJson, string.Empty);
            }

            Console.WriteLine("Forecast plot is empty or invalid.");
            return (false, string.Empty, originalDataJson, forecastDataJson, combinedDataJson, "EmptyPlot");
        }
        catch (JsonException jsonEx)
        {
            Console.WriteLine($"JSON parsing error: {jsonEx.Message}");
            return (false, string.Empty, string.Empty, string.Empty, string.Empty, "JsonError");
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine($"HTTP request error: {httpEx.Message}");
            return (false, string.Empty, string.Empty, string.Empty, string.Empty, "HttpRequestError");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            return (false, string.Empty, string.Empty, string.Empty, string.Empty, "UnexpectedError");
        }
    }
    /// <summary>
    /// Fetches XBRL data, detects anomalies, and returns the results as a PNG plot.
    /// </summary>
    /// <param name="ticker">The stock ticker symbol (e.g., AAPL).</param>
    /// <param name="concept">The XBRL concept (e.g., "AssetsCurrent|Assets").</param>
    /// <param name="unit">The unit of measurement (e.g., "USD").</param>
    /// <returns>Byte array containing the PNG image of the plot.</returns>
    public async Task<string> GetXBRLAnomaliesPlotPngAsync(string ticker, string concept = "AssetsCurrent|Assets", string unit = "USD")
    {
        try
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(ticker) || !Regex.IsMatch(ticker, @"^[A-Za-z0-9]+$"))
            {
                Console.WriteLine("Invalid ticker format. Only alphanumeric characters are allowed.");
                return string.Empty; // Return an empty string for invalid ticker
            }

            if (string.IsNullOrWhiteSpace(concept))
            {
                Console.WriteLine("Concept cannot be empty.");
                return string.Empty; // Return an empty string for empty concept
            }

            // Split concept into name and label
            var conceptParts = concept.Split('|');
            var conceptName = conceptParts[0];
            var conceptLabel = conceptParts.Length > 1 ? conceptParts[1].Trim() : conceptName;

            // Construct the endpoint URL
            string endpoint = $"/xbrl/{ticker}/anomalies/plot?concept={conceptName}&unit={unit}";

            // Log the request
            Console.WriteLine($"Fetching anomalies plot (JSON) from endpoint: {endpoint}");

            // Send the GET request
            var response = await _httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Anomalies data not found for ticker: {ticker}, concept: {conceptName}");
                    return string.Empty; // Return an empty string if no data is found
                }

                Console.WriteLine($"Failed to fetch anomalies plot. HTTP Status: {response.StatusCode}");
                return string.Empty; // Return an empty string for failed HTTP request
            }

            // Parse the JSON response
            var jsonResponse = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(jsonResponse);

            // Extract the Base64 plot field
            if (document.RootElement.TryGetProperty("plot", out var plotElement))
            {
                string base64Png = plotElement.GetString();
                if (!string.IsNullOrEmpty(base64Png))
                {
                    Console.WriteLine($"Successfully fetched Base64 anomalies plot for ticker: {ticker}");
                    return base64Png; // Return the Base64-encoded PNG string
                }
                else
                {
                    Console.WriteLine($"The 'plot' field is empty or null for ticker: {ticker}.");
                    return string.Empty; // Return an empty string if 'plot' is empty or null
                }
            }
            else
            {
                Console.WriteLine($"No 'plot' field found in the response for ticker: {ticker}.");
                return string.Empty; // Return an empty string if 'plot' field is missing
            }
        }
        catch (ArgumentException argEx)
        {
            // Handle input validation errors
            Console.WriteLine($"Argument error: {argEx.Message}");
            return string.Empty;
        }
        catch (HttpRequestException httpEx)
        {
            // Handle HTTP-specific errors
            Console.WriteLine($"HTTP request error: {httpEx.Message}");
            return string.Empty;
        }
        catch (JsonException jsonEx)
        {
            // Handle JSON parsing errors
            Console.WriteLine($"JSON parsing error: {jsonEx.Message}");
            return string.Empty;
        }
        catch (Exception ex)
        {
            // Handle unexpected errors
            Console.WriteLine($"Unexpected error in {nameof(GetXBRLAnomaliesPlotPngAsync)}: {ex.Message}");
            return string.Empty;
        }
    }
    /// <summary>
    /// Fetches the forecasted XBRL data as CSV for a given stock ticker, concept, unit, and forecast horizon.
    /// </summary>
    /// <param name="ticker">The stock ticker symbol (e.g., AAPL).</param>
    /// <param name="concept">The XBRL concept to forecast (e.g., "AssetsCurrent").</param>
    /// <param name="unit">The unit of the XBRL data (e.g., "USD").</param>
    /// <param name="horizon">The forecast horizon in periods.</param>
    /// <returns>The forecasted XBRL data as a CSV string.</returns>
    public async Task<string> GetForecastXBRLCsvAsync(string ticker, string concept = "AssetsCurrent", string unit = "USD", int horizon = 12)
    {
        try
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(ticker) || !Regex.IsMatch(ticker, @"^[A-Za-z0-9]+$"))
            {
                throw new ArgumentException("Invalid ticker format. Only alphanumeric characters are allowed.");
            }

            if (string.IsNullOrWhiteSpace(concept))
            {
                throw new ArgumentException("Concept cannot be empty.");
            }

            if (horizon <= 0)
            {
                throw new ArgumentException("Horizon must be a positive integer.");
            }

            // Construct the endpoint URL with query parameters
            string endpoint = $"/xbrl/{ticker}/forecast/csv?concept={concept}&unit={unit}&h={horizon}";

            // Log the request
            Console.WriteLine($"Fetching forecasted XBRL CSV data from endpoint: {endpoint}");

            // Send the GET request to the endpoint
            var response = await _httpClient.GetAsync(endpoint);

            // Handle non-successful responses gracefully
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Forecasted XBRL data not found for ticker: {ticker}, concept: {concept}");
                    return string.Empty; // Return an empty string for not found
                }

                throw new HttpRequestException($"Failed to fetch forecasted XBRL CSV data. Status: {response.StatusCode}");
            }

            // Parse and return the response content as plain text (CSV)
            var csvContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Successfully fetched forecasted XBRL CSV data for ticker: {ticker}");

            return csvContent; // Return the CSV content
        }
        catch (ArgumentException argEx)
        {
            // Log argument-related errors
            Console.WriteLine($"Argument error: {argEx.Message}");
            return string.Empty;
        }
        catch (HttpRequestException httpEx)
        {
            // Log HTTP-specific exceptions
            Console.WriteLine($"HTTP request error: {httpEx.Message}");
            return string.Empty;
        }
        catch (Exception ex)
        {
            // Log any other unexpected errors
            Console.WriteLine($"Unexpected error in {nameof(GetForecastXBRLCsvAsync)}: {ex.Message}");
            return string.Empty;
        }
    }
    public async Task<(string plotHtml, string dataJson)> GetXBRLPlotHtmlAsync(string ticker, string concept = "AssetsCurrent", string unit = "USD")
    {
        try
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(ticker) || !Regex.IsMatch(ticker, @"^[A-Za-z0-9]+$"))
            {
                Console.WriteLine("Invalid ticker format. Only alphanumeric characters are allowed.");
                return (string.Empty, string.Empty);
            }

            if (string.IsNullOrWhiteSpace(concept))
            {
                Console.WriteLine("Concept cannot be empty.");
                return (string.Empty, string.Empty);
            }

            // Construct the endpoint URL with query parameters
            string endpoint = $"/xbrl/{ticker}/plot?concept={concept}&unit={unit}";

            // Log the request
            Console.WriteLine($"Fetching XBRL plot from endpoint: {endpoint}");

            // Send a GET request to the endpoint
            var response = await _httpClient.GetAsync(endpoint);

            // Ensure the request was successful
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to fetch XBRL plot. Status code: {response.StatusCode}");
                return (string.Empty, string.Empty);
            }

            // Parse the response as JSON
            var jsonResponse = await response.Content.ReadAsStringAsync();

            using var jsonDoc = JsonDocument.Parse(jsonResponse);
            var root = jsonDoc.RootElement;

            string plotHtml = string.Empty;
            string dataJson = string.Empty;

            // Extract 'plot_html' field
            if (root.TryGetProperty("plot_html", out var plotHtmlElement) && plotHtmlElement.ValueKind == JsonValueKind.String)
            {
                plotHtml = plotHtmlElement.GetString() ?? string.Empty;
            }

            // Extract 'data' field
            if (root.TryGetProperty("data_json", out var dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                dataJson = dataElement.GetRawText(); // Extract 'data' field as raw JSON string
            }

            Console.WriteLine($"Successfully fetched XBRL plot for ticker {ticker}.");
            Console.WriteLine($"PlotHtml Length: {plotHtml.Length}, Data Length: {dataJson.Length}");
            return (plotHtml, dataJson);
        }
        catch (JsonException jsonEx)
        {
            // Handle JSON parsing errors
            Console.WriteLine($"JSON parsing error: {jsonEx.Message}");
            return (string.Empty, string.Empty);
        }
        catch (HttpRequestException httpEx)
        {
            // Log the HTTP request error
            Console.WriteLine($"HTTP request error while fetching XBRL plot: {httpEx.Message}");
            return (string.Empty, string.Empty);
        }
        catch (Exception ex)
        {
            // Log unexpected errors
            Console.WriteLine($"Unexpected error while fetching XBRL plot: {ex.Message}");
            return (string.Empty, string.Empty);
        }
    }
    // Fallback logic
    public async Task<(string PlotHtml, string DataJson)> GetPlotHtmlWithFallbackAsync(
        string ticker,
        string concept = "AssetsCurrent",
        string unit = "USD",
        int horizon = 12)
    {
        // 1. Parse concept string to extract Name, Label, InferredFreq
        var parts = concept?.Split('|');
        var conceptName = (parts != null && parts.Length > 0) ? parts[0].Trim() : "AssetsCurrent";
        var conceptLabel = (parts != null && parts.Length > 1) ? parts[1].Trim() : conceptName;
        var inferredFreq = (parts != null && parts.Length > 2) ? parts[2].Trim() : string.Empty;

        // Create the short concept (without inferred frequency)
        var shortConcept = $"{conceptName}|{conceptLabel}";

        try
        {
            // Track whether we tried forecast and what error code we got
            bool forecastAttempted = false;
            string errorCode = "N/A";

            // 2. Attempt forecast only if we have a non-empty inferred frequency
            if (!string.IsNullOrEmpty(inferredFreq))
            {
                forecastAttempted = true;

                var (isSuccess, plotHtml, originalDataJson, forecastDataJson, combinedDataJson, forecastErrorCode) =
                    await GetForecastPlotHtmlAsync(ticker, shortConcept, unit, horizon);

                // Store the error code so we can log it below if needed
                errorCode = forecastErrorCode;

                if (isSuccess && !string.IsNullOrWhiteSpace(plotHtml))
                {
                    Console.WriteLine("Successfully fetched forecast plot.");
                    return (plotHtml, combinedDataJson);
                }
                
                Console.WriteLine($"Forecast plot failed with error code: {errorCode}.");
            }
            else
            {
                Console.WriteLine("InferredFreq is empty; skipping forecast step.");
            }

            // If forecast was attempted (and failed) or skipped, fall back to XBRL
            if (forecastAttempted)
                Console.WriteLine($"Falling back to XBRL plot after forecast attempt. (Error Code: {errorCode})");
            else
                Console.WriteLine("No forecast attempted. Falling back to XBRL plot...");

            // 3. Attempt to fetch the XBRL plot as a fallback
            var (xbrlPlotHtml, xbrlDataJson) = await GetXBRLPlotHtmlAsync(ticker, shortConcept, unit);

            if (!string.IsNullOrWhiteSpace(xbrlPlotHtml))
            {
                Console.WriteLine("Successfully fetched XBRL plot.");
                return (xbrlPlotHtml, xbrlDataJson);
            }

            Console.WriteLine("Both forecast and XBRL plot fetch attempts failed.");
            return ("Error: Both forecast and XBRL plot fetch attempts failed.", string.Empty);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error during plot fetch: {ex.Message}");
            return ($"Error: An unexpected error occurred - {ex.Message}", string.Empty);
        }
    }
    /// <summary>
    /// Fetches the list of all available XBRL concepts for a given stock ticker, streaming the data line by line.
    /// </summary>
    /// <param name="ticker">The stock ticker symbol (e.g., AAPL).</param>
    /// <returns>An IEnumerable of JSON strings, each representing a line of the streaming response.</returns>
    public async IAsyncEnumerable<string> GetXBRLConceptsAsStreamAsync(string ticker)
    {
        if (string.IsNullOrWhiteSpace(ticker) || !Regex.IsMatch(ticker, @"^[A-Za-z0-9]+$"))
        {
            Console.WriteLine("Invalid ticker format provided.");
            yield break; // Exit gracefully for invalid ticker
        }

        string endpoint = $"/xbrl/concepts/{ticker}/json";
        Console.WriteLine($"Fetching XBRL concepts from endpoint: {endpoint}");

        Stream? responseStream = null;
        StreamReader? streamReader = null;

        try
        {
            // Send the HTTP request
            var response = await _httpClient.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to fetch XBRL concepts for ticker {ticker}. Status: {response.StatusCode}");
                yield break;
            }

            // Get the response stream
            responseStream = await response.Content.ReadAsStreamAsync();
            streamReader = new StreamReader(responseStream);
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine($"HTTP request error: {httpEx.Message}");
            yield break; // Exit gracefully
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            yield break; // Exit gracefully
        }

        // Read and yield lines outside of the try-catch block
        try
        {
            string? line;
            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                yield return line;
            }
        }
        finally
        {
            // Ensure resources are disposed properly
            streamReader?.Dispose();
            responseStream?.Dispose();
        }
    }
}
