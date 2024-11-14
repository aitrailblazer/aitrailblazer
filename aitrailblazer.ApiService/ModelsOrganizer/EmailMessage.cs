// Models/EmailMessage.cs
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Cosmos.Copilot.Models
{
    public class EmailMessage
    {
        [JsonProperty("id")]
        public string Id { get; private set; }

        [JsonProperty("type")]
        public string Type { get; private set; }

        [JsonProperty("tenantId")]
        public string TenantId { get; private set; }

        [JsonProperty("userId")]
        public string UserId { get; private set; }

        [JsonProperty("sanitizedSubject")]
        public string SanitizedSubject => Subject.Replace(" ", "_").Replace(":", "").ToLower();

        [JsonProperty("categoryId")]
        public string CategoryId => CategoryIds != null && CategoryIds.Count > 0 ? CategoryIds[0] : "uncategorized";

        [JsonProperty("partitionKey")]
        public string PartitionKey => $"{TenantId}_{UserId}_{CategoryId}";


        [JsonProperty("keypoints")]
        public string KeyPoints { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("bodyContent")]
        public string BodyContent { get; set; }

        [JsonProperty("bodyContentText")]
        public string BodyContentText { get; set; }

        [JsonProperty("bodyContentType")]
        public string BodyContentType { get; set; }

        [JsonProperty("hasAttachments")]
        public bool HasAttachments { get; set; }

        [JsonProperty("importance")]
        public string Importance { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("categoryIds")]
        public List<string> CategoryIds { get; set; }

        [JsonProperty("toRecipients")]
        public List<EmailMessageRecipientInfo> ToRecipients { get; set; } = new List<EmailMessageRecipientInfo>();

        [JsonProperty("ccRecipients")]
        public List<EmailMessageRecipientInfo> CcRecipients { get; set; } = new List<EmailMessageRecipientInfo>();

        [JsonProperty("bccRecipients")]
        public List<EmailMessageRecipientInfo> BccRecipients { get; set; } = new List<EmailMessageRecipientInfo>();

        [JsonProperty("from")]
        public EmailMessageRecipientInfo From { get; set; }

        [JsonProperty("receivedDateTime")]
        public string ReceivedDateTime { get; set; }

        [JsonProperty("isRead")]
        public bool IsRead { get; set; }

        [JsonProperty("conversationId")]
        public string ConversationId { get; set; }

        [JsonProperty("webLink")]
        public string WebLink { get; set; }

        // Add the Vectors property for embeddings
        [JsonProperty("vectors")]
        public float[] Vectors { get; set; }

        public EmailMessage(string tenantId, string userId, List<string> categoryIds, string subject)
        {
            TenantId = tenantId;
            UserId = userId;
            CategoryIds = categoryIds ?? new List<string>();
            Subject = subject;

            Id = $"{tenantId}-{userId}-EmailMessage-subject-{SanitizedSubject}";
            Type = nameof(EmailMessage);
            ReceivedDateTime = DateTime.UtcNow.ToString("o");  // ISO 8601 format
        }
    }
}
