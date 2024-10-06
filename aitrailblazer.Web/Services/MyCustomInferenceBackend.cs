using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartComponents.StaticAssets.Inference;

namespace aitrailblazer.net.Services
{
    public class MyCustomInferenceBackend : IInferenceBackend
    {
        private readonly HttpClient _httpClient;
        private string _apiKey;
        private string _endpoint;
        private string _modelName;
        private readonly IConfiguration _configuration;
        private readonly ParametersAzureService _parametersAzureService;
        private readonly ILogger<MyCustomInferenceBackend> _logger;
        private bool _isInitialized = false;

        public MyCustomInferenceBackend(
            IConfiguration configuration,
            ParametersAzureService parametersAzureService,
            ILogger<MyCustomInferenceBackend> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _parametersAzureService = parametersAzureService ?? throw new ArgumentNullException(nameof(parametersAzureService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = new HttpClient();

            _logger.LogInformation("MyCustomInferenceBackend is being constructed.");
            Initialize();
        }

        private void Initialize()
        {
            if (_isInitialized)
            {
                _logger.LogInformation("MyCustomInferenceBackend is already initialized.");
                return;
            }

            _logger.LogInformation("Initializing MyCustomInferenceBackend.");

            _apiKey = _parametersAzureService.AzureOpenAIKey03;
            var baseEndpoint = _parametersAzureService.AzureOpenAIEndpoint03;
            _modelName = _parametersAzureService.AzureOpenAIModelName03;

            _endpoint = $"{baseEndpoint}openai/deployments/{_modelName}/chat/completions?api-version=2024-02-15-preview";

            if (string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(_endpoint))
            {
                _logger.LogError("Azure OpenAI API key or endpoint is not provided.");
                throw new ArgumentNullException("Azure OpenAI API key or endpoint is not provided.");
            }

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);

            _isInitialized = true;
            _logger.LogInformation("MyCustomInferenceBackend initialized successfully.");
        }

        public async Task<string> GetChatResponseAsync(ChatParameters options)
        {
            if (!_isInitialized)
            {
                _logger.LogError("MyCustomInferenceBackend has not been initialized.");
                throw new InvalidOperationException("MyCustomInferenceBackend has not been initialized.");
            }

            if (options.Messages == null || !options.Messages.Any())
            {
                _logger.LogError("Messages cannot be null or empty.");
                throw new ArgumentException("Messages cannot be null or empty.");
            }

            _logger.LogInformation("Preparing chat request payload.");

            var payload = new
            {
                messages = options.Messages.Select(message => new
                {
                    role = message.Role switch
                    {
                        ChatMessageRole.System => "system",
                        ChatMessageRole.User => "user",
                        ChatMessageRole.Assistant => "assistant",
                        _ => throw new InvalidOperationException($"Unknown chat message role: {message.Role}")
                    },
                    content = message.Text
                }).ToArray(),
                temperature = options.Temperature ?? 0.7,
                top_p = options.TopP ?? 0.95,
                max_tokens = options.MaxTokens ?? 800,
                stream = false
            };

            try
            {
                var jsonPayload = JsonConvert.SerializeObject(payload);
                _logger.LogDebug("Request payload: {Payload}", jsonPayload);

                _logger.LogInformation("Sending request to Azure OpenAI API.");
                var response = await _httpClient.PostAsync(_endpoint, new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogDebug("Received successful response: {Response}", responseContent);

                    var responseData = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    var result = responseData?.choices[0]?.message?.content?.ToString() ?? "No response received.";

                    _logger.LogInformation("Raw response from API: {RawResponse}", (string)result);

                    // Remove END_RESPONSE from the result
                    result = result.Replace("END_RESPONSE", "").Trim();

                    _logger.LogInformation("Processed response (END_RESPONSE removed): {ProcessedResponse}", (string)result);

                    return result;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Request failed. Status: {StatusCode}, Reason: {ReasonPhrase}, Error: {ErrorContent}",
                        response.StatusCode, response.ReasonPhrase, errorContent);

                    throw new HttpRequestException($"Request failed with status: {response.StatusCode}, {response.ReasonPhrase}. Error: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetChatResponseAsync");
                throw;
            }
        }
    }
}