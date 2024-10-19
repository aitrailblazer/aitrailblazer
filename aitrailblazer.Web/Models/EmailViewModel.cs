using AITGraph.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class EmailViewModel
{
    public Message OriginalMessage { get; set; }
    public string SenderName { get; set; }
    public string Subject { get; set; }
    public string ReceivedDateTimeFormatted { get; set; }

    public string BodyContent { get; set; }
    public BodyType BodyContentType { get; set; }

    public Recipient From => OriginalMessage.From;
    public List<Recipient> ToRecipients => OriginalMessage.ToRecipients;
    public DateTimeOffset? ReceivedDateTime => OriginalMessage.ReceivedDateTime;

    public bool HasAttachments { get; set; }
    public Importance? Importance { get; set; }
    public List<string> Categories { get; set; }
    public List<Recipient> CcRecipients { get; set; }
    public List<Recipient> BccRecipients { get; set; }

    public EmailViewModel(Message message)
    {
        OriginalMessage = message;
        SenderName = message.From?.EmailAddress?.Name ?? "Unknown";
        Subject = message.Subject ?? "No Subject";
        ReceivedDateTimeFormatted = message.ReceivedDateTime?.ToLocalTime().ToString("g") ?? "N/A";

        BodyContent = message.Body?.Content ?? "";
        BodyContentType = message.Body?.ContentType ?? BodyType.Text;

        HasAttachments = message.HasAttachments ?? false;
        Importance = message.Importance;
        Categories = message.Categories ?? new List<string>();
        CcRecipients = message.CcRecipients ?? new List<Recipient>();
        BccRecipients = message.BccRecipients ?? new List<Recipient>();
    }
}