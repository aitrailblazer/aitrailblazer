using Microsoft.AspNetCore.Components;
using Markdig;
using System.Text;
using System.Text.RegularExpressions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System.Text.Json;

namespace aitrailblazer.net.Services;
public class Parameters
{
    public string CollectionName { get; set; } = string.Empty;

    public string FeatureUserFriendlyName { get; set; } = string.Empty;

    public string FeatureNameProject { get; set; } = string.Empty;
    public string FeatureName { get; set; } = string.Empty;

    public string FeatureName1 { get; set; } = string.Empty;
    public string FeatureName2 { get; set; } = string.Empty;

    public string FeatureName3 { get; set; } = string.Empty;

    public string ResponseLengthVal { get; set; } = string.Empty;

    public string CreativeAdjustmentsVal { get; set; } = string.Empty;

    public float Temperature { get; set; } = 0.7f;
    public float TopP { get; set; } = 0.95f;

    public string TopicType { get; set; } = string.Empty;
    public string BackdropTypeDescription { get; set; } = string.Empty;
    public string PhotostyleTypeDescription { get; set; } = string.Empty;
    public string PhotoshotTypeDescription { get; set; } = string.Empty;
    public string LightingTypeDescription { get; set; } = string.Empty;
    public string WritingStyleVal { get; set; } = string.Empty;
    public string AudienceLevelVal { get; set; } = string.Empty;
    public string RelationSettingsVal { get; set; } = string.Empty;
    public string ResponseStyleVal { get; set; } = string.Empty;
    public string PluginDir { get; set; } = string.Empty;
    public string CallFunction { get; set; } = string.Empty;
    public string MasterTextSetting { get; set; } = string.Empty;
    public string ChatSetting { get; set; } = string.Empty;
}
public class FormattingUtility
{
    public static string UpdateYamlContent(string yamlContent, int maxTokens, string temperature, string topP, int seed)
    {
        if (string.IsNullOrEmpty(yamlContent))
        {
            return "";
        }

        // Update max_tokens
        yamlContent = yamlContent.Replace("max_tokens: 4096", $"max_tokens: {maxTokens}");

        // Update temperature
        yamlContent = yamlContent.Replace("temperature: 0.7", $"temperature: {temperature}");

        // Update top_p
        yamlContent = yamlContent.Replace("top_p: 0.95", $"top_p: {topP}");

        // Update presence_penalty and frequency_penalty
        yamlContent = yamlContent.Replace("presence_penalty: 0.0", $"presence_penalty: {temperature}");

        yamlContent = yamlContent.Replace("frequency_penalty: 0.0", $"frequency_penalty: {temperature}");

        yamlContent = yamlContent.Replace("seed: 356", $"seed: {seed}");

        return yamlContent;
    }
    public static MarkupString RenderMarkdownToHtml(string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
            return new MarkupString();

        var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
        string html = Markdig.Markdown.ToHtml(markdown, pipeline);
        return new MarkupString(html);
    }
    public static string ExtractRemainderString(string input, string delimiter)
    {
        // Find the last occurrence of the delimiter in the input string
        int lastIndex = input.LastIndexOf(delimiter);

        // Check if the delimiter was found
        if (lastIndex != -1)
        {
            // Extract everything after the last occurrence of the delimiter
            string remainder = input.Substring(lastIndex + delimiter.Length);

            // Return the extracted string, trimmed of any leading or trailing whitespace
            return remainder.Trim();
        }

        // If the delimiter is not found, return an empty string or handle as needed
        return string.Empty;
    }
    public static string ExtractAndTrimString(string input, string delimiter)
    {
        // Extract the first line from the input string
        string firstLine = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ??
        string.Empty;

        // Check if the first line starts and ends with the delimiter
        if (firstLine.StartsWith(delimiter) && firstLine.EndsWith(delimiter) && firstLine.Length > 2 * delimiter.Length)
        {
            // Extract the string between the delimiters
            string betweenDelimiters = firstLine.Substring(delimiter.Length, firstLine.Length - 2 * delimiter.Length);

            // Trim any leading or trailing whitespace and return the result
            return betweenDelimiters.Trim();
        }

        // If delimiters are not found in the correct positions, return an empty string or handle as needed
        return string.Empty;
    }

    public static string ExtractQuestionsToPipeDelimited(string text)
    {
        // Define the pattern to find the enclosed questions text
        string pattern = @"\[FOLLOW-UP-QUESTIONS-START\](.*?)\[FOLLOW-UP-QUESTIONS-END\]";
        var match = Regex.Match(text, pattern, RegexOptions.Singleline);

        if (match.Success)
        {
            // Extract the content and split into individual questions based on '?'
            var content = match.Groups[1].Value;
            var questions = content.Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries);
            var formattedQuestions = new List<string>();

            foreach (var question in questions)
            {
                var trimmedQuestion = question.Trim();
                if (!string.IsNullOrWhiteSpace(trimmedQuestion))
                {
                    // Add '?' back to each question and prepare it for pipe-delimited output
                    formattedQuestions.Add(trimmedQuestion + "?");
                }
            }

            // Join the extracted questions with pipes
            return String.Join(" | ", formattedQuestions);
        }

        return string.Empty; // Return an empty string if no questions are found
    }
    public static string RemoveQuestionsBlock(string text)
    {
        // Regex pattern to match the whole block from start tag to end tag, including the tags
        string pattern = @"\[FOLLOW-UP-QUESTIONS-START\].*?\[FOLLOW-UP-QUESTIONS-END\]";

        // Replace the matched block with an empty string using Regex.Replace
        return Regex.Replace(text, pattern, "", RegexOptions.Singleline);
    }
    public static Parameters DeserializeJsonString(string jsonString)
    {
        return JsonSerializer.Deserialize<Parameters>(jsonString);
    }

    public static string FormatParametersToPipeDelimited(Parameters parameters)
    {
        var sb = new StringBuilder();


        sb.Append($"{parameters.FeatureUserFriendlyName} | ");

        // Always add MasterTextSetting and ChatSetting
        sb.Append($"{parameters.MasterTextSetting} | {parameters.ChatSetting} | ");

        // Conditionally add other parameters
        if (!string.IsNullOrEmpty(parameters.WritingStyleVal))
        {
            sb.Append($"{parameters.WritingStyleVal} | ");
        }

        if (!string.IsNullOrEmpty(parameters.AudienceLevelVal))
        {
            sb.Append($"{parameters.AudienceLevelVal} | ");
        }

        sb.Append($"{parameters.ResponseLengthVal} | ");

        sb.Append($"{parameters.CreativeAdjustmentsVal} | ");


        if (!string.IsNullOrEmpty(parameters.RelationSettingsVal))
        {
            sb.Append($"{parameters.RelationSettingsVal} | ");
        }

        if (!string.IsNullOrEmpty(parameters.ResponseStyleVal))
        {
            sb.Append($"{parameters.ResponseStyleVal} | ");
        }

        return sb.ToString().TrimEnd('|', ' ');
    }

    public static string SerializeParametersToJson(Parameters parameters)
    {
        return JsonSerializer.Serialize(parameters);
    }
    public static string ExtractJsonString(string input)
    {
        string pattern = @"\[PARAMETERS-START\](.*?)\[PARAMETERS-END\]";
        Match match = Regex.Match(input, pattern);
        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        return string.Empty;
    }
    public static string RemoveParametersTags(string input)
    {
        string pattern = @"\[PARAMETERS-START\].*?\[PARAMETERS-END\]";
        return Regex.Replace(input, pattern, string.Empty, RegexOptions.Singleline);
    }
    public static string ExtractAndFormatJsonString(string input)
    {
        string jsonString = ExtractJsonString(input);
        if (!string.IsNullOrEmpty(jsonString))
        {
            Parameters parameters = DeserializeJsonString(jsonString);
            return FormatParametersToPipeDelimited(parameters);
        }
        return string.Empty;
    }
}

public static class ImageUtility
{
    public static string ResizeImageBase64(string base64Image, int targetWidth)
    {
        // Sanitize the base64 string by removing data URI scheme part if present
        string base64Sanitized = SanitizeBase64String(base64Image);
        byte[] imageBytes = Convert.FromBase64String(base64Sanitized);
        string resizedImageBase64;

        try
        {
            using (MemoryStream originalImageStream = new MemoryStream(imageBytes))
            {
                using (Image image = Image.Load(originalImageStream))
                {
                    int newHeight = (int)(image.Height * (targetWidth / (float)image.Width));
                    image.Mutate(x => x.Resize(targetWidth, newHeight));

                    using (MemoryStream resizedImageStream = new MemoryStream())
                    {
                        image.SaveAsJpeg(resizedImageStream);
                        resizedImageBase64 = Convert.ToBase64String(resizedImageStream.ToArray());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            resizedImageBase64 = base64Image; // Consider whether to return the original or indicate failure differently
            Console.Error.WriteLine($"An error occurred while processing the image: {ex.Message}");
        }

        return resizedImageBase64;
    }

    private static string SanitizeBase64String(string base64String)
    {
        int indexOfBase64Start = base64String.IndexOf("base64,", StringComparison.OrdinalIgnoreCase);
        if (indexOfBase64Start >= 0)
        {
            return base64String.Substring(indexOfBase64Start + 7);
        }
        return base64String;
    }

}