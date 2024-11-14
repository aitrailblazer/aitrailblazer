using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cosmos.Copilot.Models
{
    /// <summary>
    /// Represents a chat thread with associated messages and metadata.
    /// </summary>
    public class ThreadChat
    {
        /// <summary>
        /// Unique identifier for the thread.
        /// Format: "ChatThread-{sanitizedTitle}-{currentTime}-{uniqueSuffix}"
        /// Example: "ChatThread-ProjectDiscussion-20240427-153000-ABCD"
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Type of the document. Useful for distinguishing between different document types in the same container.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Tenant ID.
        /// </summary>
        [JsonProperty("tenantId")]
        public string TenantId { get; set; }

        /// <summary>
        /// User ID.
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Partition key - unique per thread.
        /// </summary>
        [JsonProperty("threadId")]
        public string ThreadId { get; set; }

        /// <summary>
        /// Title of the thread.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Total tokens used in the thread.
        /// </summary>
        [JsonProperty("totalTokenCount")]
        public int TotalTokenCount { get; set; }

        /// <summary>
        /// Timestamp of the last activity in the thread.
        /// </summary>
        [JsonProperty("timeStamp")]
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// List of messages in the thread. Ignored by Cosmos DB for storage.
        /// </summary>
        [JsonIgnore]
        public List<Message> Messages { get; set; }

        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        [JsonConstructor]
        private ThreadChat()
        {
            Messages = new List<Message>();
        }

        /// <summary>
        /// Constructor for creating a new thread.
        /// </summary>
        /// <param name="tenantId">Tenant identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <param name="title">Title of the thread.</param>
        public ThreadChat(string tenantId, string userId, string title)
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(tenantId))
                throw new ArgumentException("TenantId cannot be null or empty.", nameof(tenantId));

            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));

            // Generate unique identifier
            string currentTime = DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");
            string uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 4); // 4-character unique suffix
            string sanitizedTitle = SanitizeTitle(title);
            Id = $"ChatThread-{sanitizedTitle}-{currentTime}-{uniqueSuffix}";

            Type = nameof(ThreadChat);
            TenantId = tenantId;
            UserId = userId;
            ThreadId = Id;
            Title = title;

            TotalTokenCount = 0;
            TimeStamp = DateTime.UtcNow;
            Messages = new List<Message>();
        }

        /// <summary>
        /// Sanitizes the title to remove or replace characters that might interfere with identifier formatting.
        /// </summary>
        /// <param name="title">Original title.</param>
        /// <returns>Sanitized title.</returns>
        private string SanitizeTitle(string title)
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
            int maxTitleLength = 255 - "ChatThread-".Length - "yyyyMMdd-HHmmss".Length - 1 - 4;
            if (title.Length > maxTitleLength)
            {
                title = title.Substring(0, maxTitleLength);
            }

            return title;
        }

        /// <summary>
        /// Adds a message to the thread and updates the token count and timestamp.
        /// </summary>
        /// <param name="message">The message to add.</param>
        public void AddMessage(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message), "Message cannot be null.");

            Messages.Add(message);
            TotalTokenCount += message.TotalTokenCount;
            TimeStamp = DateTime.UtcNow;
        }

        /// <summary>
        /// Updates an existing message in the thread and refreshes the timestamp.
        /// </summary>
        /// <param name="message">The updated message.</param>
        public void UpdateMessage(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message), "Message cannot be null.");

            var existingMessage = Messages.SingleOrDefault(m => m.Id == message.Id);
            if (existingMessage != null)
            {
                TotalTokenCount = TotalTokenCount - existingMessage.TotalTokenCount + message.TotalTokenCount;

                var index = Messages.IndexOf(existingMessage);
                Messages[index] = message;

                TimeStamp = DateTime.UtcNow;
            }
            else
            {
                throw new InvalidOperationException($"Message with Id {message.Id} does not exist in the thread.");
            }
        }

        /// <summary>
        /// Removes a message from the thread and updates the token count and timestamp.
        /// </summary>
        /// <param name="messageId">The ID of the message to remove.</param>
        public void RemoveMessage(string messageId)
        {
            if (string.IsNullOrWhiteSpace(messageId))
                throw new ArgumentException("MessageId cannot be null or empty.", nameof(messageId));

            var message = Messages.SingleOrDefault(m => m.Id == messageId);
            if (message != null)
            {
                Messages.Remove(message);
                TotalTokenCount -= message.TotalTokenCount;
                TimeStamp = DateTime.UtcNow;
            }
            else
            {
                throw new InvalidOperationException($"Message with Id {messageId} does not exist in the thread.");
            }
        }

        /// <summary>
        /// Retrieves a message by its ID.
        /// </summary>
        /// <param name="messageId">The ID of the message to retrieve.</param>
        /// <returns>The message if found; otherwise, null.</returns>
        public Message GetMessage(string messageId)
        {
            if (string.IsNullOrWhiteSpace(messageId))
                throw new ArgumentException("MessageId cannot be null or empty.", nameof(messageId));

            return Messages.SingleOrDefault(m => m.Id == messageId);
        }

        /// <summary>
        /// Retrieves all messages in the thread.
        /// </summary>
        /// <returns>A read-only list of messages.</returns>
        public IReadOnlyList<Message> GetAllMessages()
        {
            return Messages.AsReadOnly();
        }

        /// <summary>
        /// Renames the chat thread.
        /// </summary>
        /// <param name="newTitle">The new title for the chat thread.</param>
        public void Rename(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new ArgumentException("New title cannot be null or empty.", nameof(newTitle));

            Title = newTitle;
            TimeStamp = DateTime.UtcNow;
        }

        /// <summary>
        /// Returns a sanitized copy of the ThreadChat object with tenantId and userId replaced.
        /// </summary>
        public ThreadChat GetSanitizedCopy()
        {
            return new ThreadChat
            {
                Id = this.Id,
                Type = this.Type,
                TenantId = "SANITIZED_TENANT_ID",
                UserId = "SANITIZED_USER_ID",
                ThreadId = this.ThreadId,
                Title = this.Title,
                TotalTokenCount = this.TotalTokenCount,
                TimeStamp = this.TimeStamp,
                Messages = this.Messages // Messages can also be sanitized if needed
            };
        }
    }
}
