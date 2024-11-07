// SessionChat.cs

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cosmos.Copilot.Models
{
    /// <summary>
    /// Represents a chat session with associated messages and metadata.
    /// </summary>
    public class SessionChat
    {
        /// <summary>
        /// Unique identifier for the session.
        /// Format: "ChatSession-{sanitizedTitle}-{currentTime}-{uniqueSuffix}"
        /// Example: "ChatSession-ProjectDiscussion-20240427-153000-ABCD"
        /// </summary>
        [JsonProperty("id")]
        public string Id { get;  set; } // Ensures the ID cannot be modified externally

        /// <summary>
        /// Type of the document. Useful for distinguishing between different document types in the same container.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get;  set; } // Ensures the Type cannot be modified externally

        /// <summary>
        /// Tenant ID.
        /// </summary>
        [JsonProperty("tenantId")]
        public string TenantId { get;  set; } // Ensures the TenantId cannot be modified externally

        /// <summary>
        /// User ID.
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get;  set; } // Ensures the UserId cannot be modified externally

        /// <summary>
        /// Partition key - unique per session.
        /// </summary>
        [JsonProperty("sessionId")]
        public string SessionId { get;  set; } // Changed from sessionChatId to sessionId

        /// <summary>
        /// Title of the session.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get;  set; } // Ensures the Title cannot be modified externally

        /// <summary>
        /// Total tokens used in the session.
        /// </summary>
        [JsonProperty("totalTokenCount")]
        public int TotalTokenCount { get;  set; } // Ensures the Tokens count cannot be modified externally

        /// <summary>
        /// Timestamp of the last activity in the session.
        /// </summary>
        [JsonProperty("timeStamp")]
        public DateTime TimeStamp { get;  set; } // Ensures the TimeStamp cannot be modified externally

        /// <summary>
        /// List of messages in the session. Ignored by Cosmos DB for storage.
        /// </summary>
        [JsonIgnore]
        public List<Message> Messages { get;  set; } // Ensures the Messages list cannot be modified externally

        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        [JsonConstructor]
         SessionChat()
        {
            // Required for deserialization
            Messages = new List<Message>();
        }

        /// <summary>
        /// Constructor for creating a new session.
        /// </summary>
        /// <param name="tenantId">Tenant identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <param name="title">Title of the session.</param>
        public SessionChat(string tenantId, string userId, string title)
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
            Id = $"ChatSession-{sanitizedTitle}-{currentTime}-{uniqueSuffix}"; // Ensures uniqueness

            Type = nameof(SessionChat);
            TenantId = tenantId;
            UserId = userId;
            SessionId = Id; // Set SessionId to Id for uniqueness and alignment with partition key
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

        /// <summary>
        /// Adds a message to the session and updates the token count and timestamp.
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
        /// Updates an existing message in the session and refreshes the timestamp.
        /// </summary>
        /// <param name="message">The updated message.</param>
        public void UpdateMessage(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message), "Message cannot be null.");

            var existingMessage = Messages.SingleOrDefault(m => m.Id == message.Id);
            if (existingMessage != null)
            {
                // Update token counts
                TotalTokenCount = TotalTokenCount - existingMessage.TotalTokenCount
                         + message.TotalTokenCount;

                // Replace the existing message with the updated one
                var index = Messages.IndexOf(existingMessage);
                Messages[index] = message;

                TimeStamp = DateTime.UtcNow;
            }
            else
            {
                throw new InvalidOperationException($"Message with Id {message.Id} does not exist in the session.");
            }
        }

        /// <summary>
        /// Removes a message from the session and updates the token count and timestamp.
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
                throw new InvalidOperationException($"Message with Id {messageId} does not exist in the session.");
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
        /// Retrieves all messages in the session.
        /// </summary>
        /// <returns>A read-only list of messages.</returns>
        public IReadOnlyList<Message> GetAllMessages()
        {
            return Messages.AsReadOnly();
        }

        /// <summary>
        /// Renames the chat session.
        /// </summary>
        /// <param name="newName">The new name for the chat session.</param>
        public void Rename(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new ArgumentException("New Title cannot be null or empty.", nameof(newTitle));

            Title = newTitle;
            TimeStamp = DateTime.UtcNow;
        }
    }
}
