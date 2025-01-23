using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

public class GotenbergWSAppService
{
    private readonly HttpClient _httpClient;

    public GotenbergWSAppService(HttpClient httpClient)
    {
        // Set a custom timeout for long-running requests
        httpClient.Timeout = TimeSpan.FromMinutes(5);
        _httpClient = httpClient;
    }

    /// <summary>
    /// Fetches a "Hello" response from the root endpoint.
    /// </summary>
    public async Task<string> GetHelloAsync()
    {
        var response = await _httpClient.GetAsync("/");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// Converts an HTML string to a PDF using the Go web service.
    /// </summary>
    /// <param name="htmlContent">The HTML content to convert.</param>
    /// <returns>Byte array containing the PDF file.</returns>
    public async Task<byte[]> ConvertHtmlToPdfAsync(string htmlContent)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(htmlContent))
            throw new ArgumentException("HTML content cannot be null or empty.", nameof(htmlContent));
        try
        {
            // HTML file into PDF
            // convertHtmlRoute returns an [api.Route] which can convert an HTML file to
            // PDF.
            string endpoint = "/forms/chromium/convert/html";

            // Create the request payload
            var payload = new { html = htmlContent };

            // Send the POST request to the Go service
            var response = await _httpClient.PostAsJsonAsync(endpoint, payload);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Read the PDF content as a byte array
            return await response.Content.ReadAsByteArrayAsync();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP error while converting HTML to PDF: {ex.Message}");
            throw new InvalidOperationException("Failed to convert HTML to PDF using the Go web service.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while converting HTML to PDF: {ex.Message}");
            throw new InvalidOperationException("An unexpected error occurred while converting HTML to PDF.", ex);
        }
    }
    /*
    curl \
--request POST http://localhost:3000/forms/chromium/convert/url \
--form url=https://www.sec.gov/Archives/edgar/data/1318605/000162828024002390/tsla-20231231.htm \
--form 'userAgent=Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36' \
--form-string 'extraHttpHeaders={"User-Agent":"your_email@example.com","X-Header":"value","X-Scoped-Header":"value;scope=https?:\\/\\/([a-zA-Z0-9-]+\\.)*domain\\.com\\/.*"}' \
-o tsla.pdf
    */
    public async Task<byte[]> ConvertUrlToPdfAsync(string url, string email)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("URL cannot be null or empty.", nameof(url));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));

        try
        {
            // Define the endpoint for the conversion service
            string endpoint = "/forms/chromium/convert/url";

            // Prepare the form data for the POST request
            using var formData = new MultipartFormDataContent();

            // Add URL
            formData.Add(new StringContent(url), "url");

            // JSON string for extra headers, only include User-Agent with email
            var extraHeadersJson = $"{{\"User-Agent\":\"{email}\"}}";
            formData.Add(new StringContent(extraHeadersJson), "extraHttpHeaders");

            // Log the content of formData
            Console.WriteLine($"Endpoint: {endpoint}");
            foreach (var content in formData)
            {
                var contentDisposition = content.Headers.ContentDisposition;
                if (contentDisposition != null)
                {
                    var name = contentDisposition.Name?.Trim('"');
                    var contentString = await content.ReadAsStringAsync();
                    Console.WriteLine($"Form Data - Name: {name}, Content: {contentString}");
                }
            }

            // Send the POST request to the service
            var response = await _httpClient.PostAsync(endpoint, formData);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Log response headers for debugging
            Console.WriteLine($"Response Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Content-Type: {response.Content.Headers.ContentType}");

            // Read the PDF content as a byte array
            var pdfBytes = await response.Content.ReadAsByteArrayAsync();

            // Log the length of the PDF bytes for debugging
            Console.WriteLine($"PDF Bytes Length: {pdfBytes.Length}");

            return pdfBytes;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP error while converting URL to PDF: {ex.Message}");
            throw new InvalidOperationException("Failed to convert URL to PDF using the Go web service.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while converting URL to PDF: {ex.Message}");
            throw new InvalidOperationException("An unexpected error occurred while converting URL to PDF.", ex);
        }
    }
    /// <summary>
    /// Converts a single, full HTML document into a PDF, expecting Gotenberg to handle pagination.
    /// </summary>
    /// <param name="htmlContent">The full HTML content to convert.</param>
    /// <returns>Byte array containing the PDF file.</returns>
    public async Task<byte[]> ConvertFullHtmlToPdfAsync(string htmlContent)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(htmlContent))
            throw new ArgumentException("HTML content cannot be null or empty.", nameof(htmlContent));

        try
        {
            string endpoint = "/forms/chromium/convert/html";

            using var formData = new MultipartFormDataContent();

            // Add the HTML content as a single file in the form data with the correct key 'files'
            // and name it 'index.html' as expected by Gotenberg
            formData.Add(new StringContent(htmlContent), "files", "index.html");

            // Send the POST request to the service
            var response = await _httpClient.PostAsync(endpoint, formData);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Log response headers for debugging
            Console.WriteLine($"Response Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Content-Type: {response.Content.Headers.ContentType}");

            // Read the PDF content as a byte array
            var pdfBytes = await response.Content.ReadAsByteArrayAsync();

            // Log the length of the PDF bytes for debugging
            Console.WriteLine($"PDF Bytes Length: {pdfBytes.Length}");

            return pdfBytes;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP error while converting full HTML to PDF: {ex.Message}");
            throw new InvalidOperationException("Failed to convert full HTML to PDF using the Go web service.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while converting full HTML to PDF: {ex.Message}");
            throw new InvalidOperationException("An unexpected error occurred while converting full HTML to PDF.", ex);
        }
    }
}
