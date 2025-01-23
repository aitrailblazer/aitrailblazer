using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class GoApiService
{
    private readonly HttpClient _httpClient;

    public GoApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetHelloAsync()
    {
        var response = await _httpClient.GetAsync("/hello");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
