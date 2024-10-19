using System;
using System.Globalization;
using System.Collections.Generic;
using AITGraph.Sdk.Models;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;


    // Enum for Event Importance
    public enum EventImportance
    {
        Low = 0,
        Normal = 1,
        High = 2
    }

    // ViewModel for Events
    public class EventViewModel
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Subject")]
        public string Subject { get; set; }

        [JsonProperty("BodyPreview")]
        public string BodyPreview { get; set; }

        [JsonProperty("StartDateTimeFormatted")]
        public string StartDateTimeFormatted { get; set; }

        [JsonProperty("EndDateTimeFormatted")]
        public string EndDateTimeFormatted { get; set; }

        [JsonProperty("Location")]
        public Location Location { get; set; } // Complex object

        [JsonProperty("Attendees")]
        public List<Attendee> Attendees { get; set; } // Changed to List<Attendee>

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("WebLink")]
        public string WebLink { get; set; }

        [JsonProperty("Importance")]
        public EventImportance Importance { get; set; }

        [JsonProperty("IsAllDay")]
        public bool IsAllDay { get; set; }

        [JsonProperty("IsCancelled")]
        public bool IsCancelled { get; set; }

        [JsonProperty("Categories")]
        public List<string> Categories { get; set; }

        // Parsed DateTimeOffset properties
        [JsonProperty("StartDateTime")]
        public DateTimeOffset? StartDateTime { get; set; }

        [JsonProperty("EndDateTime")]
        public DateTimeOffset? EndDateTime { get; set; }

        // Computed Property for Location Display Name
        [JsonIgnore]
        public string LocationDisplayName => Location?.DisplayName ?? "No Location";

        // Computed Property for Attendees Display
        [JsonIgnore]
        public string AttendeesDisplay => Attendees != null && Attendees.Any()
            ? string.Join(", ", Attendees.Select(a => a.EmailAddress?.Name ?? a.EmailAddress?.Address ?? "Unknown Attendee"))
            : "No Attendees";

        // Parameterless constructor for deserialization
        public EventViewModel()
        {
            Attendees = new List<Attendee>();
            Categories = new List<string>();
        }

        // Optional: Method to initialize properties after deserialization if needed
        public void Initialize(Event evt)
        {
            if (evt == null)
            {
                throw new ArgumentNullException(nameof(evt), "Event cannot be null");
            }

            // Manual mapping if necessary
            // However, since we're using deserialization, this might not be needed
        }
    }
