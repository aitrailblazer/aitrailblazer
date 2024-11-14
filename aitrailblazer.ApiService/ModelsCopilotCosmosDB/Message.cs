using Newtonsoft.Json;
using System;

namespace Cosmos.Copilot.Models
{
    /// <summary>
    /// Represents a single message within a chat thread.
    /// </summary>
    public class Message
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("tenantId")]
        public string TenantId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("threadId")]
        public string ThreadId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("timeStamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("inputTokenCount")]
        public int InputTokenCount { get; set; }

        [JsonProperty("outputTokenCount")]
        public int OutputTokenCount { get; set; }

        [JsonProperty("totalTokenCount")]
        public int TotalTokenCount { get; set; }

        [JsonProperty("userInput")]
        public string UserInput { get; set; }

        [JsonProperty("output")]
        public string Output { get; set; }

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
       
        // Embedding vectors for similarity searches
        [JsonProperty("vectors")]
        public float[] Vectors { get; set; }

        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        [JsonConstructor]
        private Message() { }

        /// <summary>
        /// Constructor for creating a new message.
        /// </summary>
        public Message(
            string threadId,
            string tenantId,
            string userId,
            string featureNameWorkflowName,
            string featureNameProject,
            string title,
            string prompt,
            string userInput,
            string output,
            int inputTokenCount,
            int outputTokenCount,
            int totalTokenCount,
            string masterTextSetting,
            string writingStyleVal,
            string audienceLevelVal,
            string responseLengthVal,
            string creativeAdjustmentsVal,
            string relationSettingsVal,
            string responseStyleVal,
            bool cacheHit = false)
        {
            if (string.IsNullOrWhiteSpace(threadId)) throw new ArgumentException("ThreadId cannot be null or empty.", nameof(threadId));
            if (string.IsNullOrWhiteSpace(tenantId)) throw new ArgumentException("TenantId cannot be null or empty.", nameof(tenantId));
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be null or empty.", nameof(title));

            string currentTime = DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");
            string uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 4);
            string sanitizedTitle = SanitizeTitle(title);

            Id = $"Message-{threadId}-{sanitizedTitle}-{currentTime}-{uniqueSuffix}";

            Type = nameof(Message);
            TenantId = tenantId;
            UserId = userId;
            FeatureNameWorkflowName = featureNameWorkflowName;
            FeatureNameProject = featureNameProject;
            ThreadId = threadId;
            Title = title;
            TimeStamp = DateTime.UtcNow;
            Prompt = prompt;
            UserInput = userInput;
            Output = output;

            InputTokenCount = inputTokenCount;
            OutputTokenCount = outputTokenCount;
            TotalTokenCount = totalTokenCount;

            CacheHit = cacheHit;

            MasterTextSetting = masterTextSetting;
            WritingStyleVal = writingStyleVal;
            AudienceLevelVal = audienceLevelVal;
            ResponseLengthVal = responseLengthVal;
            CreativeAdjustmentsVal = creativeAdjustmentsVal;
            RelationSettingsVal = relationSettingsVal;
            ResponseStyleVal = responseStyleVal;
        }

        /// <summary>
        /// Creates a sanitized copy of the message with tenant and user IDs replaced.
        /// </summary>
        public Message GetSanitizedCopy()
        {
            return new Message
            {
                Id = this.Id,
                Type = this.Type,
                TenantId = "SANITIZED_TENANT_ID",
                UserId = "SANITIZED_USER_ID",
                ThreadId = this.ThreadId,
                Title = this.Title,
                TimeStamp = this.TimeStamp,
                Prompt = this.Prompt,
                UserInput = this.UserInput,
                Output = this.Output,
                InputTokenCount = this.InputTokenCount,
                OutputTokenCount = this.OutputTokenCount,
                TotalTokenCount = this.TotalTokenCount,
                CacheHit = this.CacheHit,
                FeatureNameWorkflowName = this.FeatureNameWorkflowName,
                FeatureNameProject = this.FeatureNameProject,
                MasterTextSetting = this.MasterTextSetting,
                WritingStyleVal = this.WritingStyleVal,
                AudienceLevelVal = this.AudienceLevelVal,
                ResponseLengthVal = this.ResponseLengthVal,
                CreativeAdjustmentsVal = this.CreativeAdjustmentsVal,
                RelationSettingsVal = this.RelationSettingsVal,
                ResponseStyleVal = this.ResponseStyleVal,
                Vectors = this.Vectors
            };
        }

        /// <summary>
        /// Sanitizes the title to remove or replace characters that might interfere with identifier formatting.
        /// </summary>
        private string SanitizeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));

            char[] invalidChars = new char[] { '/', '\\', '\u0000' };

            foreach (var invalidChar in invalidChars)
            {
                title = title.Replace(invalidChar.ToString(), string.Empty);
            }

            title = title.Replace(" ", "_");

            int maxTitleLength = 255 - "ChatThread-".Length - "yyyyMMdd-HHmmss".Length - 1 - 4;
            if (title.Length > maxTitleLength)
            {
                title = title.Substring(0, maxTitleLength);
            }

            return title;
        }
    }
}
