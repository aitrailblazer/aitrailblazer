using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

public class KQLFunctions
{
    private readonly ILogger<KQLFunctions> _logger;

    public KQLFunctions(ILogger<KQLFunctions> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Generates a basic KQL query with a free-text keyword.
    /// </summary>
    /// <param name="keyword">The keyword to search for.</param>
    /// <returns>A basic KQL query string.</returns>
    public string GenerateBasicKQLQuery(string keyword)
    {
        var query = $"\"{EscapeQuotes(keyword)}\"";
        _logger.LogInformation($"Generated basic KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query for a specific field and value.
    /// </summary>
    /// <param name="field">The field name.</param>
    /// <param name="value">The value to search for in the field.</param>
    /// <returns>A field-specific KQL query string.</returns>
    public string GenerateFieldSpecificKQLQuery(string field, string value)
    {
        var query = $"{field}:\"{EscapeQuotes(value)}\"";
        _logger.LogInformation($"Generated field-specific KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query to search for email attachments.
    /// </summary>
    /// <param name="fileName">The name of the attachment file.</param>
    /// <returns>An attachment-specific KQL query string.</returns>
    public string GenerateAttachmentKQLQuery(string fileName)
    {
        var query = $"attachment:\"{EscapeQuotes(fileName)}\"";
        _logger.LogInformation($"Generated attachment KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query to search BCC recipients.
    /// </summary>
    /// <param name="emailOrName">The email address or name of the BCC recipient.</param>
    /// <returns>A BCC-specific KQL query string.</returns>
    public string GenerateBccKQLQuery(string emailOrName)
    {
        var query = $"bcc:\"{EscapeQuotes(emailOrName)}\"";
        _logger.LogInformation($"Generated BCC KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query to search the email body content.
    /// </summary>
    /// <param name="content">The content to search within the email body.</param>
    /// <returns>A body-specific KQL query string.</returns>
    public string GenerateBodyKQLQuery(string content)
    {
        var query = $"body:\"{EscapeQuotes(content)}\"";
        _logger.LogInformation($"Generated body KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query to search CC recipients.
    /// </summary>
    /// <param name="emailOrName">The email address or name of the CC recipient.</param>
    /// <returns>A CC-specific KQL query string.</returns>
    public string GenerateCcKQLQuery(string emailOrName)
    {
        var query = $"cc:\"{EscapeQuotes(emailOrName)}\"";
        _logger.LogInformation($"Generated CC KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query to search the email sender.
    /// </summary>
    /// <param name="emailOrName">The email address or name of the sender.</param>
    /// <returns>A sender-specific KQL query string.</returns>
    public string GenerateFromKQLQuery(string emailOrName)
    {
        var query = $"from:\"{EscapeQuotes(emailOrName)}\"";
        _logger.LogInformation($"Generated From KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query to filter emails based on attachment presence.
    /// </summary>
    /// <param name="hasAttachment">True to include emails with attachments, false otherwise.</param>
    /// <returns>A hasAttachments-specific KQL query string.</returns>
    public string GenerateHasAttachmentKQLQuery(bool hasAttachment)
    {
        var query = $"hasAttachments:{hasAttachment.ToString().ToLower()}";
        _logger.LogInformation($"Generated HasAttachment KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query to filter emails based on importance.
    /// </summary>
    /// <param name="importance">The importance level ('low', 'normal', 'high').</param>
    /// <returns>An importance-specific KQL query string.</returns>
    public string GenerateImportanceKQLQuery(string importance)
    {
        var normalizedImportance = importance.ToLower();
        if (!new[] { "low", "normal", "high" }.Contains(normalizedImportance))
        {
            _logger.LogWarning($"Invalid importance value: {importance}");
            throw new ArgumentException("Importance must be 'low', 'normal', or 'high'");
        }
        var query = $"importance:{normalizedImportance}";
        _logger.LogInformation($"Generated Importance KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query to filter emails based on the kind of message.
    /// </summary>
    /// <param name="kind">The kind of message (e.g., 'email', 'voicemail').</param>
    /// <returns>A kind-specific KQL query string.</returns>
    public string GenerateKindKQLQuery(string kind)
    {
        var validKinds = new[] { "contacts", "docs", "email", "faxes", "im", "journals", "meetings", "notes", "posts", "rssfeeds", "tasks", "voicemail" };
        var normalizedKind = kind.ToLower();
        if (!validKinds.Contains(normalizedKind))
        {
            _logger.LogWarning($"Invalid kind value: {kind}");
            throw new ArgumentException($"Invalid kind. Must be one of: {string.Join(", ", validKinds)}");
        }
        var query = $"kind:{normalizedKind}";
        _logger.LogInformation($"Generated Kind KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query to search participants in emails.
    /// </summary>
    /// <param name="emailOrName">The email address or name of a participant.</param>
    /// <returns>A participants-specific KQL query string.</returns>
    public string GenerateParticipantsKQLQuery(string emailOrName)
    {
        var query = $"participants:\"{EscapeQuotes(emailOrName)}\"";
        _logger.LogInformation($"Generated Participants KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query to filter emails based on the received date.
    /// </summary>
    /// <param name="date">The date the email was received.</param>
    /// <returns>A received date-specific KQL query string.</returns>
    public string GenerateReceivedKQLQuery(DateTime date)
    {
        var query = $"received:{date:yyyy-MM-dd}";
        _logger.LogInformation($"Generated Received KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query to search email recipients.
    /// </summary>
    /// <param name="emailOrName">The email address or name of a recipient.</param>
    /// <returns>A recipients-specific KQL query string.</returns>
    public string GenerateRecipientsKQLQuery(string emailOrName)
    {
        var query = $"recipients:\"{EscapeQuotes(emailOrName)}\"";
        _logger.LogInformation($"Generated Recipients KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query to filter emails based on the sent date.
    /// </summary>
    /// <param name="date">The date the email was sent.</param>
    /// <returns>A sent date-specific KQL query string.</returns>
    public string GenerateSentKQLQuery(DateTime date)
    {
        var query = $"sent:{date:yyyy-MM-dd}";
        _logger.LogInformation($"Generated Sent KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query to filter emails based on size range.
    /// </summary>
    /// <param name="minSize">Minimum size in bytes.</param>
    /// <param name="maxSize">Maximum size in bytes.</param>
    /// <returns>A size range-specific KQL query string.</returns>
    public string GenerateSizeKQLQuery(int minSize, int maxSize)
    {
        if (minSize < 0 || maxSize < 0)
        {
            _logger.LogWarning($"Invalid size range: {minSize}..{maxSize}");
            throw new ArgumentException("Size values must be non-negative integers.");
        }

        if (minSize > maxSize)
        {
            _logger.LogWarning($"Minimum size {minSize} is greater than maximum size {maxSize}");
            throw new ArgumentException("Minimum size cannot be greater than maximum size.");
        }

        var query = $"size:{minSize}..{maxSize}";
        _logger.LogInformation($"Generated Size KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query to search the email subject.
    /// </summary>
    /// <param name="subject">The subject text to search for.</param>
    /// <returns>A subject-specific KQL query string.</returns>
    public string GenerateSubjectKQLQuery(string subject)
    {
        var query = $"subject:\"{EscapeQuotes(subject)}\"";
        _logger.LogInformation($"Generated Subject KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query to search the 'To' field of emails.
    /// </summary>
    /// <param name="emailOrName">The email address or name of the recipient in the 'To' field.</param>
    /// <returns>A 'To' field-specific KQL query string.</returns>
    public string GenerateToKQLQuery(string emailOrName)
    {
        var query = $"to:\"{EscapeQuotes(emailOrName)}\"";
        _logger.LogInformation($"Generated To KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Combines two KQL queries with a specified Boolean operator.
    /// </summary>
    /// <param name="query1">The first KQL query.</param>
    /// <param name="query2">The second KQL query.</param>
    /// <param name="operatorr">The Boolean operator ('AND', 'OR', 'NOT').</param>
    /// <returns>A combined KQL query string.</returns>
    public string CombineKQLQueries(string query1, string query2, string operatorr)
    {
        var upperOperator = operatorr.ToUpper();
        if (!new[] { "AND", "OR", "NOT" }.Contains(upperOperator))
        {
            _logger.LogWarning($"Invalid Boolean operator: {operatorr}");
            throw new ArgumentException("Boolean operator must be 'AND', 'OR', or 'NOT'");
        }

        string combinedQuery = upperOperator == "NOT"
            ? $"({query1}) NOT ({query2})"
            : $"({query1}) {upperOperator} ({query2})";

        _logger.LogInformation($"Combined KQL queries: {combinedQuery}");
        return combinedQuery;
    }

    /// <summary>
    /// Generates a KQL query using proximity operators.
    /// </summary>
    /// <param name="term1">The first search term.</param>
    /// <param name="term2">The second search term.</param>
    /// <param name="distance">Maximum distance between terms.</param>
    /// <param name="preserveOrder">If true, preserves the order of terms using ONEAR; otherwise, uses NEAR.</param>
    /// <returns>A proximity-specific KQL query string.</returns>
    public string GenerateProximityKQLQuery(string term1, string term2, int distance = 8, bool preserveOrder = false)
    {
        string operatorName = preserveOrder ? "ONEAR" : "NEAR";
        var query = $"\"{EscapeQuotes(term1)}\" {operatorName}(n={distance}) \"{EscapeQuotes(term2)}\"";
        _logger.LogInformation($"Generated Proximity KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query for range-based searches.
    /// </summary>
    /// <param name="property">The property to apply the range on.</param>
    /// <param name="start">The start value of the range.</param>
    /// <param name="end">The end value of the range.</param>
    /// <returns>A range-specific KQL query string.</returns>
    public string GenerateRangeKQLQuery(string property, string start, string end)
    {
        var query = $"{property}:{start}..{end}";
        _logger.LogInformation($"Generated Range KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Groups multiple KQL queries using a specified Boolean operator.
    /// </summary>
    /// <param name="queries">A collection of KQL query strings.</param>
    /// <param name="groupingOperator">The Boolean operator to use for grouping ('AND', 'OR').</param>
    /// <returns>A grouped KQL query string.</returns>
    public string GroupKQLQueries(IEnumerable<string> queries, string groupingOperator = "AND")
    {
        var upperOperator = groupingOperator.ToUpper();
        if (!new[] { "AND", "OR" }.Contains(upperOperator))
        {
            _logger.LogWarning($"Invalid grouping operator: {groupingOperator}");
            throw new ArgumentException("Grouping operator must be 'AND' or 'OR'");
        }

        var combined = string.Join($" {upperOperator} ", queries.Select(q => $"({q})"));
        _logger.LogInformation($"Generated Grouped KQL query: {combined}");
        return combined;
    }

    /// <summary>
    /// Generates a KQL query from a natural language description.
    /// </summary>
    /// <param name="naturalLanguageQuery">The natural language description of the search.</param>
    /// <returns>A KQL query string.</returns>
    public string GenerateKQLQueryFromNaturalLanguage(string naturalLanguageQuery)
    {
        _logger.LogInformation($"Generating KQL query from natural language: {naturalLanguageQuery}");
        var queryBuilder = new List<string>();

        // Example parsing logic - can be enhanced with NLP techniques
        // This basic implementation looks for specific prefixes and constructs queries accordingly
        if (naturalLanguageQuery.Contains("attachment:", StringComparison.OrdinalIgnoreCase))
        {
            string attachment = ExtractValue(naturalLanguageQuery, "attachment:");
            queryBuilder.Add(GenerateAttachmentKQLQuery(attachment));
        }

        if (naturalLanguageQuery.Contains("from:", StringComparison.OrdinalIgnoreCase))
        {
            string from = ExtractValue(naturalLanguageQuery, "from:");
            queryBuilder.Add(GenerateFromKQLQuery(from));
        }

        if (naturalLanguageQuery.Contains("to:", StringComparison.OrdinalIgnoreCase))
        {
            string to = ExtractValue(naturalLanguageQuery, "to:");
            queryBuilder.Add(GenerateToKQLQuery(to));
        }

        if (naturalLanguageQuery.Contains("subject:", StringComparison.OrdinalIgnoreCase))
        {
            string subject = ExtractValue(naturalLanguageQuery, "subject:");
            queryBuilder.Add(GenerateSubjectKQLQuery(subject));
        }

        if (naturalLanguageQuery.Contains("body:", StringComparison.OrdinalIgnoreCase))
        {
            string body = ExtractValue(naturalLanguageQuery, "body:");
            queryBuilder.Add(GenerateBodyKQLQuery(body));
        }

        if (naturalLanguageQuery.Contains("hasAttachments:", StringComparison.OrdinalIgnoreCase))
        {
            bool hasAttachments = bool.Parse(ExtractValue(naturalLanguageQuery, "hasAttachments:"));
            queryBuilder.Add(GenerateHasAttachmentKQLQuery(hasAttachments));
        }

        if (naturalLanguageQuery.Contains("importance:", StringComparison.OrdinalIgnoreCase))
        {
            string importance = ExtractValue(naturalLanguageQuery, "importance:");
            queryBuilder.Add(GenerateImportanceKQLQuery(importance));
        }

        if (naturalLanguageQuery.Contains("received:", StringComparison.OrdinalIgnoreCase))
        {
            DateTime receivedDate = DateTime.Parse(ExtractValue(naturalLanguageQuery, "received:"));
            queryBuilder.Add(GenerateReceivedKQLQuery(receivedDate));
        }

        if (naturalLanguageQuery.Contains("sent:", StringComparison.OrdinalIgnoreCase))
        {
            DateTime sentDate = DateTime.Parse(ExtractValue(naturalLanguageQuery, "sent:"));
            queryBuilder.Add(GenerateSentKQLQuery(sentDate));
        }

        // Additional parsing can be added here for other properties and operators

        // Combine all generated queries using AND operator by default
        var finalQuery = queryBuilder.Count > 0 ? string.Join(" AND ", queryBuilder) : GenerateBasicKQLQuery(naturalLanguageQuery);
        _logger.LogInformation($"Generated KQL query from natural language: {finalQuery}");
        return finalQuery;
    }

    /// <summary>
    /// Escapes double quotes in a string for safe inclusion in KQL queries.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>The escaped string.</returns>
    private string EscapeQuotes(string input)
    {
        return input.Replace("\"", "\\\"");
    }

    /// <summary>
    /// Extracts the value associated with a given prefix in the input string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="prefix">The prefix to search for.</param>
    /// <returns>The extracted value.</returns>
    private string ExtractValue(string input, string prefix)
    {
        int startIndex = input.IndexOf(prefix, StringComparison.OrdinalIgnoreCase);
        if (startIndex == -1)
        {
            _logger.LogWarning($"Prefix '{prefix}' not found in input.");
            return string.Empty;
        }

        startIndex += prefix.Length;
        int endIndex = input.IndexOf(' ', startIndex);
        var extractedValue = endIndex == -1 ? input.Substring(startIndex) : input.Substring(startIndex, endIndex - startIndex);
        _logger.LogDebug($"Extracted value for prefix '{prefix}': {extractedValue}");
        return extractedValue.Trim();
    }

    /// <summary>
    /// Generates a KQL query using the XRANK operator to boost dynamic ranking.
    /// </summary>
    /// <param name="matchExpression">The main match expression.</param>
    /// <param name="rankExpression">The rank expression to boost.</param>
    /// <param name="cb">Constant boost value.</param>
    /// <param name="nb">Normalized boost value.</param>
    /// <param name="rb">Range boost value.</param>
    /// <param name="pb">Percentage boost value.</param>
    /// <param name="avgb">Average boost value.</param>
    /// <param name="stdb">Standard deviation boost value.</param>
    /// <param name="n">Number of results to compute statistics from.</param>
    /// <returns>A KQL query string with XRANK operator.</returns>
    public string GenerateXRANKKQLQuery(
        string matchExpression,
        string rankExpression,
        float cb = 0f,
        float nb = 0f,
        float rb = 0f,
        float pb = 0f,
        float avgb = 0f,
        float stdb = 0f,
        int n = 0)
    {
        var parameters = new List<string>();

        if (cb != 0f) parameters.Add($"cb={cb}");
        if (nb != 0f) parameters.Add($"nb={nb}");
        if (rb != 0f) parameters.Add($"rb={rb}");
        if (pb != 0f) parameters.Add($"pb={pb}");
        if (avgb != 0f) parameters.Add($"avgb={avgb}");
        if (stdb != 0f) parameters.Add($"stdb={stdb}");
        if (n > 0) parameters.Add($"n={n}");

        var paramsString = string.Join(", ", parameters);
        var query = $"{matchExpression} XRANK({paramsString}) {rankExpression}";
        _logger.LogInformation($"Generated XRANK KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query using the WORDS operator to handle synonyms.
    /// </summary>
    /// <param name="term1">The first synonym term.</param>
    /// <param name="term2">The second synonym term.</param>
    /// <returns>A KQL query string using WORDS operator.</returns>
    public string GenerateSynonymKQLQuery(string term1, string term2)
    {
        var query = $"WORDS({EscapeQuotes(term1)}, {EscapeQuotes(term2)})";
        _logger.LogInformation($"Generated Synonym KQL query: {query}");
        return query;
    }

    /// <summary>
    /// Generates a KQL query using the wildcard operator for prefix matching.
    /// </summary>
    /// <param name="prefix">The prefix to match.</param>
    /// <returns>A KQL query string with wildcard operator.</returns>
    public string GenerateWildcardKQLQuery(string prefix)
    {
        var query = $"{EscapeQuotes(prefix)}*";
        _logger.LogInformation($"Generated Wildcard KQL query: {query}");
        return query;
    }
}
