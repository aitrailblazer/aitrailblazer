using Microsoft.AspNetCore.Components;
using Markdig;
using System.Text;
using System.Text.RegularExpressions;
using SixLabors.ImageSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; // Added for JObject manipulations if needed
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using aitrailblazer.net.Services;
using aitrailblazer.net.Models;
using System.IO;

namespace aitrailblazer.net.Services
{
    /// <summary>
    /// Represents the parameters used across various functionalities.
    /// </summary>
    public class Parameters
    {
        [JsonProperty("collectionName")]
        public string CollectionName { get; set; } = string.Empty;

        [JsonProperty("featureUserFriendlyName")]
        public string FeatureUserFriendlyName { get; set; } = string.Empty;

        [JsonProperty("featureNameProject")]
        public string FeatureNameProject { get; set; } = string.Empty;

        [JsonProperty("featureName")]
        public string FeatureName { get; set; } = string.Empty;

        [JsonProperty("featureName1")]
        public string FeatureName1 { get; set; } = string.Empty;

        [JsonProperty("featureName2")]
        public string FeatureName2 { get; set; } = string.Empty;

        [JsonProperty("featureName3")]
        public string FeatureName3 { get; set; } = string.Empty;

        [JsonProperty("responseLengthVal")]
        public string ResponseLengthVal { get; set; } = string.Empty;

        [JsonProperty("creativeAdjustmentsVal")]
        public string CreativeAdjustmentsVal { get; set; } = string.Empty;

        [JsonProperty("temperature")]
        public float Temperature { get; set; } = 0.7f;

        [JsonProperty("topP")]
        public float TopP { get; set; } = 0.95f;

        [JsonProperty("topicType")]
        public string TopicType { get; set; } = string.Empty;

        [JsonProperty("backdropTypeDescription")]
        public string BackdropTypeDescription { get; set; } = string.Empty;

        [JsonProperty("photostyleTypeDescription")]
        public string PhotostyleTypeDescription { get; set; } = string.Empty;

        [JsonProperty("photoshotTypeDescription")]
        public string PhotoshotTypeDescription { get; set; } = string.Empty;

        [JsonProperty("lightingTypeDescription")]
        public string LightingTypeDescription { get; set; } = string.Empty;

        [JsonProperty("writingStyleVal")]
        public string WritingStyleVal { get; set; } = string.Empty;

        [JsonProperty("audienceLevelVal")]
        public string AudienceLevelVal { get; set; } = string.Empty;

        [JsonProperty("relationSettingsVal")]
        public string RelationSettingsVal { get; set; } = string.Empty;

        [JsonProperty("responseStyleVal")]
        public string ResponseStyleVal { get; set; } = string.Empty;

        [JsonProperty("pluginDir")]
        public string PluginDir { get; set; } = string.Empty;

        [JsonProperty("callFunction")]
        public string CallFunction { get; set; } = string.Empty;

        [JsonProperty("masterTextSetting")]
        public string MasterTextSetting { get; set; } = string.Empty;

        [JsonProperty("chatSetting")]
        public string ChatSetting { get; set; } = string.Empty;
    }

    /// <summary>
    /// Provides utility methods for formatting and processing data.
    /// </summary>
    public class FormattingUtility
    {
        /// <summary>
        /// Updates YAML content with the provided parameters.
        /// </summary>
        /// <param name="yamlContent">The original YAML content.</param>
        /// <param name="maxTokens">The maximum number of tokens.</param>
        /// <param name="temperature">The temperature setting.</param>
        /// <param name="topP">The top_p setting.</param>
        /// <param name="seed">The seed value.</param>
        /// <returns>The updated YAML content.</returns>
        public static string UpdateYamlContent(string yamlContent, int maxTokens, string temperature, string topP, int seed)
        {
            if (string.IsNullOrEmpty(yamlContent))
            {
                return string.Empty;
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

            // Update seed
            yamlContent = yamlContent.Replace("seed: 356", $"seed: {seed}");

            return yamlContent;
        }

        /// <summary>
        /// Converts Markdown text to HTML.
        /// </summary>
        /// <param name="markdown">The Markdown content.</param>
        /// <returns>The rendered HTML as a MarkupString.</returns>
        public static MarkupString RenderMarkdownToHtml(string markdown)
        {
            if (string.IsNullOrEmpty(markdown))
                return new MarkupString();

            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            string html = Markdig.Markdown.ToHtml(markdown, pipeline);
            return new MarkupString(html);
        }

        /// <summary>
        /// Extracts the remainder of a string after the last occurrence of a delimiter.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="delimiter">The delimiter string.</param>
        /// <returns>The extracted and trimmed remainder string.</returns>
        public static string ExtractRemainderString(string input, string delimiter)
        {
            // Find the last occurrence of the delimiter in the input string
            int lastIndex = input.LastIndexOf(delimiter, StringComparison.Ordinal);

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

        /// <summary>
        /// Extracts and trims a string enclosed by specified delimiters.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="delimiter">The delimiter string.</param>
        /// <returns>The extracted and trimmed string.</returns>
        public static string ExtractAndTrimString(string input, string delimiter)
        {
            // Extract the first line from the input string
            string firstLine = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? string.Empty;

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

        /// <summary>
        /// Extracts pipe-delimited questions from the text enclosed within specific tags.
        /// </summary>
        /// <param name="text">The input text containing questions.</param>
        /// <returns>Pipe-delimited string of questions.</returns>
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
                return string.Join(" | ", formattedQuestions);
            }

            return string.Empty; // Return an empty string if no questions are found
        }

        /// <summary>
        /// Removes the questions block from the input text.
        /// </summary>
        /// <param name="text">The input text containing the questions block.</param>
        /// <returns>The text with the questions block removed.</returns>
        public static string RemoveQuestionsBlock(string text)
        {
            // Regex pattern to match the whole block from start tag to end tag, including the tags
            string pattern = @"\[FOLLOW-UP-QUESTIONS-START\].*?\[FOLLOW-UP-QUESTIONS-END\]";

            // Replace the matched block with an empty string using Regex.Replace
            return Regex.Replace(text, pattern, string.Empty, RegexOptions.Singleline);
        }

        /// <summary>
        /// Deserializes a JSON string into a Parameters object.
        /// </summary>
        /// <param name="jsonString">The JSON string representing Parameters.</param>
        /// <returns>The deserialized Parameters object.</returns>
        public static Parameters DeserializeJsonString(string jsonString)
        {
            return JsonConvert.DeserializeObject<Parameters>(jsonString);
        }

        /// <summary>
        /// Formats the Parameters object into a pipe-delimited string.
        /// </summary>
        /// <param name="parameters">The Parameters object.</param>
        /// <returns>The pipe-delimited string.</returns>
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

        /// <summary>
        /// Serializes the Parameters object into a JSON string.
        /// </summary>
        /// <param name="parameters">The Parameters object.</param>
        /// <returns>The serialized JSON string.</returns>
        public static string SerializeParametersToJson(Parameters parameters)
        {
            return JsonConvert.SerializeObject(parameters);
        }

        /// <summary>
        /// Extracts the JSON string enclosed within specific tags from the input.
        /// </summary>
        /// <param name="input">The input string containing the JSON tags.</param>
        /// <returns>The extracted JSON string.</returns>
        public static string ExtractJsonString(string input)
        {
            string pattern = @"\[PARAMETERS-START\](.*?)\[PARAMETERS-END\]";
            Match match = Regex.Match(input, pattern, RegexOptions.Singleline);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// Removes the parameters tags from the input string.
        /// </summary>
        /// <param name="input">The input string containing the parameters tags.</param>
        /// <returns>The input string without the parameters tags.</returns>
        public static string RemoveParametersTags(string input)
        {
            string pattern = @"\[PARAMETERS-START\].*?\[PARAMETERS-END\]";
            return Regex.Replace(input, pattern, string.Empty, RegexOptions.Singleline);
        }

        /// <summary>
        /// Extracts the JSON string from the input and formats it into a pipe-delimited string.
        /// </summary>
        /// <param name="input">The input string containing the JSON and tags.</param>
        /// <returns>The formatted pipe-delimited string.</returns>
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

    /// <summary>
    /// Provides utility methods for image processing.
    /// </summary>
/// <summary>
    /// Provides utility methods for image processing.
    /// </summary>
    public static class ImageUtility
    {
        /// <summary>
        /// Resizes a base64-encoded image to the specified target width while maintaining aspect ratio.
        /// </summary>
        /// <param name="base64Image">The base64-encoded image string.</param>
        /// <param name="targetWidth">The desired width of the resized image.</param>
        /// <returns>The resized image as a base64-encoded string.</returns>
        public static string ResizeImageBase64(string base64Image, int targetWidth)
        {
            // Sanitize the base64 string by removing data URI scheme part if present
            string base64Sanitized = SanitizeBase64String(base64Image);
            byte[] imageBytes;

            try
            {
                imageBytes = Convert.FromBase64String(base64Sanitized);
            }
            catch (FormatException ex)
            {
                Console.Error.WriteLine($"Invalid base64 string: {ex.Message}");
                return base64Image; // Return the original if conversion fails
            }

            string resizedImageBase64;

            try
            {
                using (MemoryStream originalImageStream = new MemoryStream(imageBytes))
                {
                    // Detect the image format
                    IImageFormat format = Image.DetectFormat(originalImageStream);

                    // Reset the stream position after detection
                    originalImageStream.Position = 0;

                    using (Image image = Image.Load(originalImageStream))
                    {
                        int newHeight = (int)(image.Height * (targetWidth / (float)image.Width));
                        image.Mutate(x => x.Resize(targetWidth, newHeight));

                        using (MemoryStream resizedImageStream = new MemoryStream())
                        {
                            image.Save(resizedImageStream, format); // Save in detected format
                            resizedImageBase64 = Convert.ToBase64String(resizedImageStream.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resizedImageBase64 = base64Image; // Return the original if processing fails
                Console.Error.WriteLine($"An error occurred while processing the image: {ex.Message}");
            }

            return resizedImageBase64;
        }

        /// <summary>
        /// Removes the data URI scheme from a base64-encoded string if present.
        /// </summary>
        /// <param name="base64String">The base64-encoded string.</param>
        /// <returns>The sanitized base64 string.</returns>
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
}