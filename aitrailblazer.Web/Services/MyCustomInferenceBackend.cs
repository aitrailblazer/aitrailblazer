// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Linq;
using System.Net.Http;
using System.Globalization;
using System.Text;
using Newtonsoft.Json; // Retained for Newtonsoft.Json usage
using Newtonsoft.Json.Linq; // Added for JObject manipulations if needed
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmartComponents.StaticAssets.Inference;

namespace AITrailblazer.net.Services
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

        public async Task<string> GetChatResponseAsync(SmartComponents.StaticAssets.Inference.ChatParameters options)
        {
            if (!_isInitialized)
            {
                _logger.LogError("MyCustomInferenceBackend has not been initialized.");
                throw new InvalidOperationException("MyCustomInferenceBackend has not been initialized.");
            }

            if (options.Messages == null || !options.Messages.Any())
            {
                _logger.LogError("GetChatResponseAsync Messages cannot be null or empty.");
                throw new ArgumentException("GetChatResponseAsync Messages cannot be null or empty.");
            }

            var chatPayload = JsonConvert.SerializeObject(new
            {
                messages = options.Messages.Select(message => new
                {
                    role = message.Role.ToString().ToLower(),
                    content = message.Text
                }).ToArray(),
                temperature = options.Temperature,
                top_p = options.TopP,
                max_tokens = options.MaxTokens,
                frequency_penalty = options.FrequencyPenalty,
                presence_penalty = options.PresencePenalty,
                stop = options.StopSequences,
                stream = false
            });

            _logger.LogInformation($"GetChatResponseAsync Chat request payload: {chatPayload}");

            try
            {
                var response = await _httpClient.PostAsync(_endpoint, new StringContent(chatPayload, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"GetChatResponseAsync Received successful chat response: {responseContent}");

                    var responseData = JsonConvert.DeserializeObject<JObject>(responseContent);
                    var result = responseData?["choices"]?[0]?["message"]?["content"]?.ToString() ?? "No response received.";

                    // Modify the result to use current time
                    result = AdjustEventTimes(result);

                    _logger.LogInformation($"Processed chat response: {result}");
                    return result;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Chat request failed. Status: {response.StatusCode}, Reason: {response.ReasonPhrase}, Error: {errorContent}");

                    throw new HttpRequestException($"Chat request failed with status: {response.StatusCode}, {response.ReasonPhrase}. Error: {errorContent}");
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "HTTP request error while handling chat request.");
                throw;
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "JSON error while processing chat response.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "General error while handling chat request.");
                throw;
            }
        }

        private string AdjustEventTimes(string result)
        {
            DateTime startDateTime;
            DateTime endDateTime = DateTime.MinValue; // Initialize with a default value

            var lines = result.Split('\n').ToList();

            // Remove the lines for start and end dates
            lines.RemoveAll(line => line.StartsWith("FIELD EventStartDateFromReceived^^^") ||
                                    line.StartsWith("FIELD EventEndDateFromReceived^^^"));

            // Adjust the times
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("FIELD EventStartTimeFromReceived^^^"))
                {
                    // Parse the start time from the line
                    var startTimeString = lines[i].Split("^^^")[1];
                    if (DateTime.TryParseExact(startTimeString, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDateTime))
                    {
                        // Add 30 minutes to get the end time
                        endDateTime = startDateTime.AddMinutes(30);
                    }
                }
                else if (lines[i].StartsWith("FIELD EventEndTimeFromReceived^^^"))
                {
                    // Check if endDateTime was set from the previous parsing
                    if (endDateTime != DateTime.MinValue)
                    {
                        // Update the line with the calculated end time
                        lines[i] = $"FIELD EventEndTimeFromReceived^^^{endDateTime:HH:mm}";
                    }
                }
            }

            return string.Join('\n', lines);
        }

        private DateTime RoundToNext30MinuteInterval(DateTime dateTime)
        {
            var minutes = dateTime.Minute;
            var adjustment = minutes % 30 == 0 ? 0 : 30 - (minutes % 30);
            return dateTime.AddMinutes(adjustment).AddSeconds(-dateTime.Second);
        }
    }

    public class SmartPasteInference
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None
        };

        public class SmartPasteRequestData
        {
            public FormField[]? FormFields { get; set; }
            public string? ClipboardContents { get; set; }
        }

        public class FormField
        {
            public string? Identifier { get; set; }
            public string? Description { get; set; }
            public string?[]? AllowedValues { get; set; }
            public string? Type { get; set; }
        }

        public readonly struct SmartPasteResponseData
        {
            public bool BadRequest { get; init; }
            public string? Response { get; init; }
        }

        public Task<SmartPasteResponseData> GetFormCompletionsAsync(IInferenceBackend inferenceBackend, string dataJson)
        {
            var data = JsonConvert.DeserializeObject<SmartPasteRequestData>(dataJson, _jsonSerializerSettings)!;
            if (data.FormFields is null || data.FormFields.Length == 0 || string.IsNullOrEmpty(data.ClipboardContents))
            {
                return Task.FromResult(new SmartPasteResponseData { BadRequest = true });
            }

            return GetFormCompletionsAsync(inferenceBackend, data);
        }

        public virtual SmartComponents.StaticAssets.Inference.ChatParameters BuildPrompt(SmartPasteRequestData data)
        {
            var systemMessage = @$"
Current date: {DateTime.Today.ToString("D", CultureInfo.InvariantCulture)}

Respond with a JSON object with ONLY the following keys. For each key, infer a value from USER_DATA:

{ToFieldOutputExamples(data.FormFields!)}

Do not explain how the values were determined.
For fields without any corresponding information in USER_DATA, use the value null.";

            var prompt = @$"
USER_DATA: {data.ClipboardContents}
";

            return new SmartComponents.StaticAssets.Inference.ChatParameters
            {
                Messages = new List<ChatMessage>
                {
                    new ChatMessage(ChatMessageRole.System, systemMessage),
                    new ChatMessage(ChatMessageRole.User, prompt),
                },
                Temperature = 0,
                TopP = 0.1f,
                MaxTokens = 2000,
                FrequencyPenalty = 0.1f,
                PresencePenalty = 0,
            };
        }

        public virtual async Task<SmartPasteResponseData> GetFormCompletionsAsync(IInferenceBackend inferenceBackend, SmartPasteRequestData requestData)
        {
            var chatOptions = BuildPrompt(requestData);
            var completionsResponse = await inferenceBackend.GetChatResponseAsync(chatOptions);
            return new SmartPasteResponseData { Response = completionsResponse };
        }

        private static string ToFieldOutputExamples(FormField[] fields)
        {
            var sb = new StringBuilder();
            sb.AppendLine("{");

            var firstField = true;
            foreach (var field in fields)
            {
                if (firstField)
                {
                    firstField = false;
                }
                else
                {
                    sb.AppendLine(",");
                }

                sb.Append($"  \"{field.Identifier}\": /* ");

                if (!string.IsNullOrEmpty(field.Description))
                {
                    sb.Append($"The {field.Description}");
                }

                if (field.AllowedValues is { Length: > 0 })
                {
                    sb.Append($" (multiple choice, with allowed values: ");
                    var first = true;
                    foreach (var value in field.AllowedValues)
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            sb.Append(",");
                        }
                        sb.Append($"\"{value}\"");
                    }
                    sb.Append(")");
                }
                else
                {
                    sb.Append($" of type {field.Type}");
                }

                sb.Append(" */");
            }

            sb.AppendLine();
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
