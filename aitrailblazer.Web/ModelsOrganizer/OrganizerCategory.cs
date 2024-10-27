// Models/OrganizerCategory.cs
using Newtonsoft.Json;

namespace Cosmos.Copilot.Models
{
    public class OrganizerCategory
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tenantId")]
        public string TenantId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("categoryId")]
        public string CategoryId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the OrganizerCategory class.
        /// </summary>
        public OrganizerCategory(string tenantId, string userId, string categoryId, string type = "category")
        {
            // Generate a consistent Id based on tenantId, userId, and categoryId
            Id = $"{tenantId}-{userId}-categoryId-{categoryId}";
            TenantId = tenantId;
            UserId = userId;
            CategoryId = categoryId;
            Type = nameof(OrganizerCategory);

        }
    }
}
