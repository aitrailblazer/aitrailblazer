using System;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using SmartComponents.StaticAssets.Inference;
using SmartComponents;
using System;
using SmartComponents.AspNetCore;
using SmartComponents.Inference;
using SmartComponents.Infrastructure;
using SmartComponents.StaticAssets.Inference;
using Microsoft.Extensions.Logging;

public class MyFormSmartPasteInference : SmartPasteInference
{
    private readonly static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
    private readonly ILogger<MyFormSmartPasteInference> _logger;

    public MyFormSmartPasteInference(ILogger<MyFormSmartPasteInference> logger)
    {
        _logger = logger;
    }
    // Data structure to hold form field information
    public class SmartPasteRequestData
    {
        public FormField[]? FormFields { get; set; } // Array of form fields
        public string? ClipboardContents { get; set; } // Data from clipboard or user input
    }

    // Form field class with options for multiple-choice values, type, etc.
    public class FormField
    {
        public string? Identifier { get; set; } // Unique identifier for the field
        public string? Description { get; set; } // Description of the field
        public string?[]? AllowedValues { get; set; } // Predefined options for dropdowns/radio buttons
        public string? Type { get; set; } // Type of the form field (e.g., "text", "radio", "checkbox")
    }

    // Response data structure for SmartPaste inference result
    public readonly struct SmartPasteResponseData
    {
        public bool BadRequest { get; init; } // Whether the request was invalid
        public string? Response { get; init; } // The actual response from inference
    }

    // Deserialize form data from JSON and process it
   public Task<SmartPasteResponseData> GetFormCompletionsAsync(IInferenceBackend inferenceBackend, string dataJson)
    {
        try
        {
            _logger.LogInformation("Starting form completion process with data: {DataJson}", dataJson);

            var data = JsonSerializer.Deserialize<SmartPasteRequestData>(dataJson, jsonSerializerOptions)!;

            if (data.FormFields is null || data.FormFields.Length == 0 || string.IsNullOrEmpty(data.ClipboardContents))
            {
                _logger.LogWarning("Invalid form data received. FormFields: {FormFieldsCount}, ClipboardContents: {ClipboardContentsLength}",
                    data.FormFields?.Length ?? 0, data.ClipboardContents?.Length ?? 0);
                return Task.FromResult(new SmartPasteResponseData { BadRequest = true });
            }

            return GetFormCompletionsAsync(inferenceBackend, data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deserializing form data");
            return Task.FromResult(new SmartPasteResponseData { BadRequest = true });
        }
    }


    // Build prompt dynamically based on form data and clipboard contents
public ChatParameters BuildPrompt(SmartPasteRequestData data)
    {
        _logger.LogInformation("Building prompt for form completion");

        var systemMessage = @$"
Current date: {DateTime.Today.ToString("D", CultureInfo.InvariantCulture)}

Each response line matches the following format:
FIELD identifier^^^value

Give a response with the following lines only, with values inferred from USER_DATA:
{ToFieldOutputExamples(data.FormFields!)}
END_RESPONSE

Do not explain how the values were determined.
For fields without corresponding data in USER_DATA, use 'NO_DATA'.";

        var prompt = @$"
USER_DATA: {data.ClipboardContents}
";

        _logger.LogDebug("System message: {SystemMessage}", systemMessage);
        _logger.LogDebug("User prompt: {Prompt}", prompt);

        return new ChatParameters
        {
            Messages = new List<ChatMessage>
            {
                new ChatMessage(ChatMessageRole.System, systemMessage),
                new ChatMessage(ChatMessageRole.User, prompt)
            },
            Temperature = 0.4f,
            TopP = 0.9f,
            MaxTokens = 2000,
            FrequencyPenalty = 0.1f,
            PresencePenalty = 0,
            StopSequences = new List<string> { "END_RESPONSE" }
        };
    }


  public async Task<SmartPasteResponseData> GetFormCompletionsAsync(IInferenceBackend inferenceBackend, SmartPasteRequestData requestData)
    {
        _logger.LogInformation("Fetching form completions from inference backend");

        var chatOptions = BuildPrompt(requestData);
        var completionsResponse = await inferenceBackend.GetChatResponseAsync(chatOptions);

        _logger.LogDebug("Received response from inference backend: {CompletionsResponse}", completionsResponse);

        return new SmartPasteResponseData { Response = completionsResponse };
    }

    // Helper method to format the output for form fields in a user-readable format
 private static string ToFieldOutputExamples(FormField[] fields)
    {
        var sb = new StringBuilder();

        foreach (var field in fields)
        {
            sb.AppendLine();
            sb.Append($"FIELD {field.Identifier}^^^");

            if (!string.IsNullOrEmpty(field.Description))
            {
                sb.Append($"The {field.Description}");
            }

            if (field.AllowedValues is { Length: > 0 })
            {
                sb.Append(" (allowed values: ");
                sb.Append(string.Join(", ", field.AllowedValues));
                sb.Append(")");
            }
            else
            {
                sb.Append($" of type {field.Type ?? "unknown"}");
            }
        }

        return sb.ToString();
    }
}
