using Newtonsoft.Json;
using System;

namespace Cosmos.Copilot.Models
{
    /// <summary>
    /// Represents a single message within a chat session.
    /// </summary>
    public class Message
    {
        // Existing properties

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("tenantId")]
        public string TenantId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("timeStamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("promptTokens")]
        public int PromptTokens { get; set; }

        [JsonProperty("userInput")]
        public string UserInput { get; set; }

        [JsonProperty("userInputTokens")]
        public int UserInputTokens { get; set; }

        [JsonProperty("output")]
        public string Output { get; set; }

        [JsonProperty("OutputTokens")]
        public int OutputTokens { get; set; }

        [JsonProperty("tokens")]
        public int Tokens { get; set; }

        [JsonProperty("cacheHit")]
        public bool CacheHit { get; set; }

        // New properties for feature names and additional settings
        [JsonProperty("featureNameWorkflowName")]
        public string FeatureNameWorkflowName { get; set; }

        [JsonProperty("featureNameProject")]
        public string FeatureNameProject { get; set; }

        [JsonProperty("masterTextSetting")]
        public string MasterTextSetting { get; set; }

        [JsonProperty("writingStyleVal")]
        public string WritingStyleVal { get; set; }

        [JsonProperty("audienceLevelVal")]
        public string AudienceLevelVal { get; set; }

        [JsonProperty("responseLengthVal")]
        public string ResponseLengthVal { get; set; }

        [JsonProperty("creativeAdjustmentsVal")]
        public string CreativeAdjustmentsVal { get; set; }

        [JsonProperty("relationSettingsVal")]
        public string RelationSettingsVal { get; set; }

        [JsonProperty("responseStyleVal")]
        public string ResponseStyleVal { get; set; }

        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        [JsonConstructor]
        Message() { }

        /// <summary>
        /// Constructor for creating a new message.
        /// </summary>
        public Message(
            string sessionId,
            string tenantId,
            string userId,
            string title,
            string prompt,
            int promptTokens,
            string userInput,
            int userInputTokens,
            string output,
            string masterTextSetting,
            string writingStyleVal,
            string audienceLevelVal,
            string responseLengthVal,
            string creativeAdjustmentsVal,
            string relationSettingsVal,
            string responseStyleVal,
            int outputTokens = 0,
            bool cacheHit = false)
        {
            if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("SessionId cannot be null or empty.", nameof(sessionId));
            if (string.IsNullOrWhiteSpace(tenantId)) throw new ArgumentException("TenantId cannot be null or empty.", nameof(tenantId));
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            if (string.IsNullOrWhiteSpace(prompt)) throw new ArgumentException("Prompt cannot be null or empty.", nameof(prompt));
            if (promptTokens < 0) throw new ArgumentOutOfRangeException(nameof(promptTokens), "PromptTokens cannot be negative.");
            if (outputTokens < 0) throw new ArgumentOutOfRangeException(nameof(outputTokens), "OutputTokens cannot be negative.");

            // Generate unique identifier
            string currentTime = DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");
            string uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 4);
            string sanitizedTitle = SanitizeTitle(title);

            Id = $"Message-{sessionId}-{sanitizedTitle}-{currentTime}-{uniqueSuffix}";

            Type = nameof(Message);
            TenantId = tenantId;
            UserId = userId;
            SessionId = sessionId;
            Title = title;
            TimeStamp = DateTime.UtcNow;
            Prompt = prompt;
            PromptTokens = promptTokens;
            UserInput = userInput;
            UserInputTokens = userInputTokens;
            Output = output;
            OutputTokens = outputTokens;
            Tokens = promptTokens + outputTokens + userInputTokens;
            CacheHit = cacheHit;

            // Initialize additional settings
            MasterTextSetting = masterTextSetting;
            WritingStyleVal = writingStyleVal;
            AudienceLevelVal = audienceLevelVal;
            ResponseLengthVal = responseLengthVal;
            CreativeAdjustmentsVal = creativeAdjustmentsVal;
            RelationSettingsVal = relationSettingsVal;
            ResponseStyleVal = responseStyleVal;

            // Extract featureNameWorkflowName and featureNameProject from title
            ParseTitle(title);
        }

        /// <summary>
        /// Parses the title to extract featureNameWorkflowName and featureNameProject.
        /// </summary>
        void ParseTitle(string title)
        {
            var parts = title.Split('-');
            if (parts.Length >= 2)
            {
                FeatureNameWorkflowName = parts[0];
                FeatureNameProject = parts[1];
            }
            else
            {
                FeatureNameWorkflowName = "UnknownWorkflow";
                FeatureNameProject = "UnknownProject";
            }
        }
//     "id": "Message-ChatSession-Writing-AIWritingAssistant-ASAPAutonomous_Systems_and_ProcessesASAP_is_a_no-code_multi-agen-20241104-071821-7e2a-Writing-AIWritingAssistant-ASAPAutonomous_Systems_and_ProcessesASAP_is_a_no-code_multi-agen-20241104-071824-5cfd",
//     "id": "Message-ChatSession-Writing-AIWritingAssistant-ASAPAutonomous_Systems_and_ProcessesASAP_is_a_no-code_multi-agen-20241104-071821-7e2a-Writing-AIWritingAssistant-Imagine_AI_saving_you_hours_each_day!With_AI_taking_care_of_tedi-20241104-071843-9452",

        // Other methods, such as UpdatePrompt, UpdateCompletion, etc., remain the same

        /// <summary>
        /// Sanitizes the title to remove or replace characters that might interfere with identifier formatting.
        /// </summary>
        /// <param name="title">Original title.</param>
        /// <returns>Sanitized title.</returns>
        string SanitizeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));

            // Define characters disallowed in Cosmos DB 'id'
            char[] invalidChars = new char[] { '/', '\\', '\u0000' };

            foreach (var invalidChar in invalidChars)
            {
                title = title.Replace(invalidChar.ToString(), string.Empty);
            }

            // Replace spaces with underscores for consistency
            title = title.Replace(" ", "_");

            // Optionally, truncate the title to ensure the 'id' does not exceed 255 characters
            // Considering the prefix and unique suffix added in the constructor
            // Example: "ChatSession-{sanitizedTitle}-{currentTime}-{uniqueSuffix}"
            int maxTitleLength = 255 - "ChatSession-".Length - "yyyyMMdd-HHmmss".Length - 1 - 4; // Adjust as needed
            if (title.Length > maxTitleLength)
            {
                title = title.Substring(0, maxTitleLength);
            }

            return title;
        }
    }
}
