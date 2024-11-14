// Models/EmailMessageRecipientInfo.cs
using Newtonsoft.Json;

namespace Cosmos.Copilot.Models
{
    /// <summary>
    /// Represents recipient information for an email.
    /// </summary>
    public class EmailMessageRecipientInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }
    }
}
