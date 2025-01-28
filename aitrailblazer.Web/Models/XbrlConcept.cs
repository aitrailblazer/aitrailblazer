using Newtonsoft.Json;
using System.Collections.Generic;

namespace aitrailblazer.Web.Models
{
    // Root class for the JSON response
    public class XbrlConceptResponse
    {
        [JsonProperty("ticker")]
        public string? Ticker { get; set; } // Allow nullable in case the "ticker" field is missing

        [JsonProperty("concepts")]
        public List<XbrlConcept>? Concepts { get; set; } // Allow nullable in case "concepts" is null or missing
    }

    // Class representing individual XBRL concepts
    public class XbrlConcept
    {
        [JsonProperty("name")]
        public string? Name { get; set; } // Allow nullable in case "name" is missing

        [JsonProperty("label")]
        public string? Label { get; set; } // Allow nullable in case "label" is missing

        [JsonProperty("inferred_freq")]
        public bool? InferredFreq { get; set; } // New field: Nullable boolean for inferred frequency status
    }
}