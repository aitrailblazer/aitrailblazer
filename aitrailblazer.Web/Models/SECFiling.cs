using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// Represents a company's basic information.
/// </summary>
public class CompanyInfo
{
    [JsonProperty("cik")]
    public string Cik { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("ticker")]
    public string Ticker { get; set; } = string.Empty;

    [JsonProperty("exchange")]
    public string Exchange { get; set; } = string.Empty;
}

/// <summary>
/// Represents the available forms for a company.
/// </summary>
public class AvailableFormsResponse
{
    [JsonProperty("ticker")]
    public string Ticker { get; set; } = string.Empty;

    [JsonProperty("forms")]
    public List<string> Forms { get; set; } = new();
}


/// <summary>
/// Represents detailed data for an XBRL concept.
/// </summary>
public class XBRLConceptData
{
    [JsonProperty("columns")]
    public List<string> Columns { get; set; } = new();

    [JsonProperty("data")]
    public List<Dictionary<string, object>> Data { get; set; } = new();
}
public class XBRLNode
{
    [JsonProperty("Label")]
    public string Label { get; set; }
    
    [JsonProperty("Href")]
    public string Href { get; set; }
    
    [JsonProperty("Children")]
    public List<XBRLNode> Children { get; set; }
    
    [JsonProperty("Matched")]
    public bool Matched { get; set; }
    
    [JsonProperty("Concept")]
    public XBRLConcept Concept { get; set; }
}

public class XBRLConceptFull
{
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("label")]
    public string Label { get; set; }
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;
    // Depending on your JSON, you may want to strongly type this property.
    // Here we use a list of dictionaries to hold the CSV record data.
    [JsonProperty("data")]
    public List<Dictionary<string, object>> Data { get; set; }
    
    [JsonProperty("available_units")]
    public List<string> AvailableUnits { get; set; }
    
    [JsonProperty("inferred_freq")]
    public string InferredFrequency { get; set; }
}
/*
{"available_units":["USD"],"inferred_freq":"Quarterly","label":"Adjustments to Additional Paid in Capital, Warrant Issued","name":"AdjustmentsToAdditionalPaidInCapitalWarrantIssued"}
*/
/// <summary>
/// Represents an XBRL concept from the API.
/// </summary>
public class XBRLConcept
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;
    
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("available_units")]
    public List<string> AvailableUnits { get; set; } = new();

}
/// <summary>
/// Represents a single SEC filing.
/// </summary>
public class SECFiling
{
    [JsonProperty("acceptanceDateTime")]
    public DateTime? AcceptanceDateTime { get; set; }

    [JsonProperty("accessionNumber")]
    public string AccessionNumber { get; set; } = string.Empty;

    [JsonProperty("act")]
    public string Act { get; set; } = string.Empty;

    [JsonProperty("core_type")]
    public string CoreType { get; set; } = string.Empty;

    [JsonProperty("fileNumber")]
    public string FileNumber { get; set; } = string.Empty;

    [JsonProperty("filingDate")]
    public DateTime? FilingDate { get; set; }

    [JsonProperty("filmNumber")]
    public string FilmNumber { get; set; } = string.Empty;

    [JsonProperty("form")]
    public string Form { get; set; } = string.Empty;

    [JsonProperty("isInlineXBRL")]
    public bool IsInlineXBRL { get; set; }

    [JsonProperty("isXBRL")]
    public bool IsXBRL { get; set; }

    [JsonProperty("items")]
    public string Items { get; set; } = string.Empty;

    [JsonProperty("primaryDocDescription")]
    public string PrimaryDocDescription { get; set; } = string.Empty;

    [JsonProperty("primaryDocument")]
    public string PrimaryDocument { get; set; } = string.Empty;

    [JsonProperty("reportDate")]
    public string ReportDate { get; set; } = string.Empty;

    [JsonProperty("size")]
    public long? Size { get; set; }

    [JsonProperty("pdfDataUrl")]
    public string PdfDataUrl { get; set; } = string.Empty;
}

/// <summary>
/// Represents the SEC filings response as a list instead of an object.
/// </summary>
public class SECFilingsResponse
{
    [JsonProperty("filings")]
    public List<SECFiling> Filings { get; set; } = new();
}

/// <summary>
/// Extension methods for XBRLConcept conversion.
/// </summary>
public static class XBRLConceptExtensions
{
    /// <summary>
    /// Converts XBRLConcept to another format if needed.
    /// </summary>
    public static XBRLConcept ToXbrlConcept(this XBRLConcept concept)
    {
        return concept ?? new XBRLConcept(); // Avoids unnecessary allocation.
    }
}