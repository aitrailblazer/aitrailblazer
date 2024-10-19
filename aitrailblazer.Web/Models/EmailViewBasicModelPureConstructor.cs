using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using AITGraph.Sdk.Models;
using HtmlAgilityPack;
using System.Net;
using System.Text.RegularExpressions;
using AITMessageHeader = AITGraph.Sdk.Models.InternetMessageHeader;

/// <summary>
/// Represents the result containing a list of emails.
/// </summary>
public sealed class EmailResultPureConstructor
{
    /// <summary>
    /// Gets or sets the list of emails.
    /// </summary>
    [JsonProperty("emails")]
    public List<EmailViewBasicPureConstructorModel> Emails { get; set; } = new List<EmailViewBasicPureConstructorModel>();
}

/// <summary>
/// Represents a basic email view model.
/// </summary>
public class EmailViewBasicPureConstructorModel
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("subject")]
    public string Subject { get; set; } = "No Subject";

    //[JsonProperty("bodyPreview")]
    //public string BodyPreview { get; set; } = string.Empty;

    [JsonProperty("bodyContent")]
    public string BodyContent { get; set; } = string.Empty;

    [JsonProperty("bodyContentText")]
    public string BodyContentText { get; set; } = string.Empty;

    [JsonProperty("bodyContentType")]
    public string BodyContentType { get; set; } = "Text";

    [JsonProperty("hasAttachments")]
    public bool HasAttachments { get; set; }

    [JsonProperty("importance")]
    public string Importance { get; set; } = "Normal";

    [JsonProperty("categories")]
    public List<string> Categories { get; set; } = new List<string>();

    [JsonProperty("ccRecipients")]
    public List<RecipientInfo> CcRecipients { get; set; } = new List<RecipientInfo>();

    [JsonProperty("bccRecipients")]
    public List<RecipientInfo> BccRecipients { get; set; } = new List<RecipientInfo>();

    [JsonProperty("toRecipients")]
    public List<RecipientInfo> ToRecipients { get; set; } = new List<RecipientInfo>();

    [JsonProperty("from")]
    public RecipientInfo From { get; set; } = new RecipientInfo();

    //[JsonProperty("sender")]
    //public RecipientInfo Sender { get; set; } = new RecipientInfo();

    [JsonProperty("isRead")]
    public bool IsRead { get; set; }

    [JsonProperty("internetMessageId")]
    public string InternetMessageId { get; set; } = string.Empty;

    [JsonProperty("conversationId")]
    public string ConversationId { get; set; } = string.Empty;

    [JsonProperty("webLink")]
    public string WebLink { get; set; } = string.Empty;

    [JsonProperty("parentFolderId")]
    public string ParentFolderId { get; set; } = string.Empty;

    [JsonProperty("receivedDateTime")]
    public string ReceivedDateTime { get; set; } = string.Empty;

    //[JsonProperty("sentDateTime")]
    //public string SentDateTime { get; set; } = string.Empty;

    [JsonProperty("inferenceClassification")]
    public string InferenceClassification { get; set; } = string.Empty;

    [JsonProperty("isDraft")]
    public bool IsDraft { get; set; }

    [JsonProperty("replyTo")]
    public List<RecipientInfo> ReplyTo { get; set; } = new List<RecipientInfo>();

    [JsonProperty("flag")]
    public string Flag { get; set; } = string.Empty;

    [JsonProperty("isDeliveryReceiptRequested")]
    public bool IsDeliveryReceiptRequested { get; set; }

    [JsonProperty("isReadReceiptRequested")]
    public bool IsReadReceiptRequested { get; set; }

    [JsonProperty("conversationIndex")]
    public string? ConversationIndex { get; set; }

    //[JsonProperty("internetMessageHeaders")]
    //public List<InternetMessageHeader> InternetMessageHeaders { get; set; } = new List<InternetMessageHeader>();

    // Computed Properties - These will not be serialized/deserialized
    [JsonIgnore]
    public string SenderName => From?.Name ?? "Unknown";

    [JsonIgnore]
    public string SenderEmail => From?.EmailAddress ?? "";

    [JsonIgnore]
    public string ReceivedDateTimeFormatted => DateTime.TryParse(ReceivedDateTime, out var dt) ? dt.ToLocalTime().ToString("g") : "N/A";

    [JsonIgnore]
    public string Category => string.Join(", ", Categories);

    // Constructors
    public EmailViewBasicPureConstructorModel() { }

    public EmailViewBasicPureConstructorModel(Message message)
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message), "Message cannot be null.");
        }

        Id = message.Id ?? string.Empty;
        Subject = message.Subject ?? "No Subject";
        //BodyPreview = message.BodyPreview ?? string.Empty;
        BodyContent = message.Body?.Content;//ExtractTextFromHtml(message.Body?.Content ?? string.Empty);
        BodyContentText = ExtractTextFromHtml(message.Body?.Content ?? string.Empty);
        BodyContentType = message.Body?.ContentType?.ToString() ?? "Text";
        HasAttachments = message.HasAttachments ?? false;
        Importance = message.Importance?.ToString() ?? "Normal";
        Categories = message.Categories ?? new List<string>();
        CcRecipients = message.CcRecipients?.Select(r => new RecipientInfo(r)).ToList() ?? new List<RecipientInfo>();
        BccRecipients = message.BccRecipients?.Select(r => new RecipientInfo(r)).ToList() ?? new List<RecipientInfo>();
        ToRecipients = message.ToRecipients?.Select(r => new RecipientInfo(r)).ToList() ?? new List<RecipientInfo>();

        // Safely handle null 'From' and 'Sender' properties
        From = new RecipientInfo(message.From);
        //Sender = new RecipientInfo(message.Sender);

        IsRead = message.IsRead ?? false;
        InternetMessageId = message.InternetMessageId ?? string.Empty;
        ConversationId = message.ConversationId ?? string.Empty;
        WebLink = message.WebLink ?? string.Empty;
        ParentFolderId = message.ParentFolderId ?? string.Empty;
        ReceivedDateTime = message.ReceivedDateTime?.ToString("o") ?? string.Empty;
        //SentDateTime = message.SentDateTime?.ToString("o") ?? string.Empty;
        InferenceClassification = message.InferenceClassification?.ToString() ?? string.Empty;
        IsDraft = message.IsDraft ?? false;
        ReplyTo = message.ReplyTo?.Select(r => new RecipientInfo(r)).ToList() ?? new List<RecipientInfo>();
        Flag = message.Flag?.FlagStatus?.ToString() ?? string.Empty;
        IsDeliveryReceiptRequested = message.IsDeliveryReceiptRequested ?? false;
        IsReadReceiptRequested = message.IsReadReceiptRequested ?? false;
        ConversationIndex = message.ConversationIndex != null ? Convert.ToBase64String(message.ConversationIndex) : null;
        //InternetMessageHeaders = message.InternetMessageHeaders?.Select(h => new InternetMessageHeader
        //{
        //    Name = h.Name ?? string.Empty,
        //    Value = h.Value ?? string.Empty
        //}).ToList() ?? new List<InternetMessageHeader>();
    }

    /// <summary>
    /// Extracts plain text from HTML content by removing script and style tags.
    /// </summary>
    /// <param name="html">The HTML content.</param>
    /// <returns>Plain text extracted from HTML.</returns>
    private string ExtractTextFromHtml(string html)
    {
        if (string.IsNullOrEmpty(html))
        {
            return string.Empty;
        }

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        // Remove script and style nodes
        var nodes = doc.DocumentNode.SelectNodes("//script|//style");
        if (nodes != null)
        {
            foreach (var node in nodes)
            {
                node.Remove();
            }
        }

        // Get the inner text
        string plainText = doc.DocumentNode.InnerText;
        plainText = WebUtility.HtmlDecode(plainText);
        plainText = Regex.Replace(plainText, @"\s+", " ").Trim();

        return plainText;
    }

    /// <summary>
    /// Gets a semicolon-separated list of recipient email addresses.
    /// </summary>
    /// <returns>Formatted recipients list.</returns>
    public string GetRecipientsList()
    {
        return string.Join("; ", ToRecipients.Select(r => r.EmailAddress).Where(e => !string.IsNullOrEmpty(e)));
    }

    /// <summary>
    /// Gets a formatted preview of the email body content.
    /// </summary>
    /// <returns>Formatted preview string.</returns>
    public string GetFormattedPreview()
    {
        return BodyContent.Length > 100 ? BodyContent.Substring(0, 100) + "..." : BodyContent;
    }
}
