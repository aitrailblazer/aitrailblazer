public sealed class PureEmailResult
{
    public List<PureEmailViewBasicModel> Emails { get; set; }
}

public class PureEmailViewBasicModel
{
    public string Id { get; set; }
    public string Subject { get; set; }
    //public string BodyPreview { get; set; }
    public string BodyContent { get; set; }
    public string BodyContentText { get; set; }

    public string BodyContentType { get; set; }
    public bool HasAttachments { get; set; }
    public string Importance { get; set; }
    public string Priority { get; set; }
    
    public string ActionRequired { get; set; }

    public List<string> Categories { get; set; }
    public List<PureRecipientInfo> CcRecipients { get; set; }
    public List<PureRecipientInfo> BccRecipients { get; set; }
    public List<PureRecipientInfo> ToRecipients { get; set; }
    public PureRecipientInfo From { get; set; }
    //public PureRecipientInfo Sender { get; set; }
    public bool IsRead { get; set; }
    public string InternetMessageId { get; set; }
    public string ConversationId { get; set; }
    public string WebLink { get; set; }
    public string ParentFolderId { get; set; }
    public string ReceivedDateTime { get; set; }
    //public string SentDateTime { get; set; }
    public string InferenceClassification { get; set; }
    public bool IsDraft { get; set; }
    public List<PureRecipientInfo> ReplyTo { get; set; }
    public string Flag { get; set; }
    public bool IsDeliveryReceiptRequested { get; set; }
    public bool IsReadReceiptRequested { get; set; }
    public string ConversationIndex { get; set; }
    //public List<PureInternetMessageHeader> InternetMessageHeaders { get; set; }
}

public class PureRecipientInfo
{
    public string Name { get; set; }
    public string EmailAddress { get; set; }
}

public class PureInternetMessageHeader
{
    public string Name { get; set; }
    public string Value { get; set; }
}
