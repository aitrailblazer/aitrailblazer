public class HelloWorldApiClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    // Implementation of GetHelloAsync
    public async Task<string> GetHelloAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetStringAsync("/hello", cancellationToken);
            return response;
        }
        catch (HttpRequestException ex)
        {
            // Log the exception or handle the error as needed
            return $"Error fetching hello message: {ex.Message}";
        }
    }
}