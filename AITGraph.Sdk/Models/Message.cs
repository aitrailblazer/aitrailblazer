// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace AITGraph.Sdk.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class Message : global::AITGraph.Sdk.Models.OutlookItem, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The fileAttachment and itemAttachment attachments for the message.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Attachment>? Attachments { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Attachment> Attachments { get; set; }
#endif
        /// <summary>The Bcc: recipients for the message.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Recipient>? BccRecipients { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Recipient> BccRecipients { get; set; }
#endif
        /// <summary>The body of the message. It can be in HTML or text format. Find out about safe HTML in a message body.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.ItemBody? Body { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.ItemBody Body { get; set; }
#endif
        /// <summary>The first 255 characters of the message body. It is in text format. If the message contains instances of mention, this property would contain a concatenation of these mentions as well.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? BodyPreview { get; set; }
#nullable restore
#else
        public string BodyPreview { get; set; }
#endif
        /// <summary>The Cc: recipients for the message.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Recipient>? CcRecipients { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Recipient> CcRecipients { get; set; }
#endif
        /// <summary>The ID of the conversation the email belongs to.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ConversationId { get; set; }
#nullable restore
#else
        public string ConversationId { get; set; }
#endif
        /// <summary>Indicates the position of the message within the conversation.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public byte[]? ConversationIndex { get; set; }
#nullable restore
#else
        public byte[] ConversationIndex { get; set; }
#endif
        /// <summary>The collection of open extensions defined for the message. Nullable.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Extension>? Extensions { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Extension> Extensions { get; set; }
#endif
        /// <summary>The flag value that indicates the status, start date, due date, or completion date for the message.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.FollowupFlag? Flag { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.FollowupFlag Flag { get; set; }
#endif
        /// <summary>The owner of the mailbox from which the message is sent. In most cases, this value is the same as the sender property, except for sharing or delegation scenarios. The value must correspond to the actual mailbox used. Find out more about setting the from and sender properties of a message.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.Recipient? From { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.Recipient From { get; set; }
#endif
        /// <summary>Indicates whether the message has attachments. This property doesn&apos;t include inline attachments, so if a message contains only inline attachments, this property is false. To verify the existence of inline attachments, parse the body property to look for a src attribute, such as &lt;IMG src=&apos;cid:image001.jpg@01D26CD8.6C05F070&apos;&gt;.</summary>
        public bool? HasAttachments { get; set; }
        /// <summary>The importance property</summary>
        public global::AITGraph.Sdk.Models.Importance? Importance { get; set; }
        /// <summary>The inferenceClassification property</summary>
        public global::AITGraph.Sdk.Models.InferenceClassificationType? InferenceClassification { get; set; }
        /// <summary>The internetMessageHeaders property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.InternetMessageHeader>? InternetMessageHeaders { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.InternetMessageHeader> InternetMessageHeaders { get; set; }
#endif
        /// <summary>The internetMessageId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? InternetMessageId { get; set; }
#nullable restore
#else
        public string InternetMessageId { get; set; }
#endif
        /// <summary>The isDeliveryReceiptRequested property</summary>
        public bool? IsDeliveryReceiptRequested { get; set; }
        /// <summary>The isDraft property</summary>
        public bool? IsDraft { get; set; }
        /// <summary>The isRead property</summary>
        public bool? IsRead { get; set; }
        /// <summary>The isReadReceiptRequested property</summary>
        public bool? IsReadReceiptRequested { get; set; }
        /// <summary>A collection of mentions in the message, ordered by the createdDateTime from the newest to the oldest. By default, a GET /messages does not return this property unless you apply $expand on the property.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Mention>? Mentions { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Mention> Mentions { get; set; }
#endif
        /// <summary>The mentionsPreview property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.MentionsPreview? MentionsPreview { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.MentionsPreview MentionsPreview { get; set; }
#endif
        /// <summary>The collection of multi-value extended properties defined for the message. Nullable.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.MultiValueLegacyExtendedProperty>? MultiValueExtendedProperties { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.MultiValueLegacyExtendedProperty> MultiValueExtendedProperties { get; set; }
#endif
        /// <summary>The parentFolderId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ParentFolderId { get; set; }
#nullable restore
#else
        public string ParentFolderId { get; set; }
#endif
        /// <summary>The receivedDateTime property</summary>
        public DateTimeOffset? ReceivedDateTime { get; set; }
        /// <summary>The replyTo property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Recipient>? ReplyTo { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Recipient> ReplyTo { get; set; }
#endif
        /// <summary>The sender property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.Recipient? Sender { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.Recipient Sender { get; set; }
#endif
        /// <summary>The sentDateTime property</summary>
        public DateTimeOffset? SentDateTime { get; set; }
        /// <summary>The collection of single-value extended properties defined for the message. Nullable.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.SingleValueLegacyExtendedProperty>? SingleValueExtendedProperties { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.SingleValueLegacyExtendedProperty> SingleValueExtendedProperties { get; set; }
#endif
        /// <summary>The subject property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Subject { get; set; }
#nullable restore
#else
        public string Subject { get; set; }
#endif
        /// <summary>The toRecipients property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Recipient>? ToRecipients { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Recipient> ToRecipients { get; set; }
#endif
        /// <summary>The uniqueBody property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.ItemBody? UniqueBody { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.ItemBody UniqueBody { get; set; }
#endif
        /// <summary>The unsubscribeData property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? UnsubscribeData { get; set; }
#nullable restore
#else
        public List<string> UnsubscribeData { get; set; }
#endif
        /// <summary>The unsubscribeEnabled property</summary>
        public bool? UnsubscribeEnabled { get; set; }
        /// <summary>The webLink property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? WebLink { get; set; }
#nullable restore
#else
        public string WebLink { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.Message"/> and sets the default values.
        /// </summary>
        public Message() : base()
        {
            OdataType = "#microsoft.graph.message";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.Message"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.Message CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            var mappingValue = parseNode.GetChildNode("@odata.type")?.GetStringValue();
            return mappingValue switch
            {
                "#microsoft.graph.calendarSharingMessage" => new global::AITGraph.Sdk.Models.CalendarSharingMessage(),
                "#microsoft.graph.eventMessage" => new global::AITGraph.Sdk.Models.EventMessage(),
                "#microsoft.graph.eventMessageRequest" => new global::AITGraph.Sdk.Models.EventMessageRequest(),
                "#microsoft.graph.eventMessageResponse" => new global::AITGraph.Sdk.Models.EventMessageResponse(),
                _ => new global::AITGraph.Sdk.Models.Message(),
            };
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "attachments", n => { Attachments = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Attachment>(global::AITGraph.Sdk.Models.Attachment.CreateFromDiscriminatorValue)?.AsList(); } },
                { "bccRecipients", n => { BccRecipients = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Recipient>(global::AITGraph.Sdk.Models.Recipient.CreateFromDiscriminatorValue)?.AsList(); } },
                { "body", n => { Body = n.GetObjectValue<global::AITGraph.Sdk.Models.ItemBody>(global::AITGraph.Sdk.Models.ItemBody.CreateFromDiscriminatorValue); } },
                { "bodyPreview", n => { BodyPreview = n.GetStringValue(); } },
                { "ccRecipients", n => { CcRecipients = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Recipient>(global::AITGraph.Sdk.Models.Recipient.CreateFromDiscriminatorValue)?.AsList(); } },
                { "conversationId", n => { ConversationId = n.GetStringValue(); } },
                { "conversationIndex", n => { ConversationIndex = n.GetByteArrayValue(); } },
                { "extensions", n => { Extensions = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Extension>(global::AITGraph.Sdk.Models.Extension.CreateFromDiscriminatorValue)?.AsList(); } },
                { "flag", n => { Flag = n.GetObjectValue<global::AITGraph.Sdk.Models.FollowupFlag>(global::AITGraph.Sdk.Models.FollowupFlag.CreateFromDiscriminatorValue); } },
                { "from", n => { From = n.GetObjectValue<global::AITGraph.Sdk.Models.Recipient>(global::AITGraph.Sdk.Models.Recipient.CreateFromDiscriminatorValue); } },
                { "hasAttachments", n => { HasAttachments = n.GetBoolValue(); } },
                { "importance", n => { Importance = n.GetEnumValue<global::AITGraph.Sdk.Models.Importance>(); } },
                { "inferenceClassification", n => { InferenceClassification = n.GetEnumValue<global::AITGraph.Sdk.Models.InferenceClassificationType>(); } },
                { "internetMessageHeaders", n => { InternetMessageHeaders = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.InternetMessageHeader>(global::AITGraph.Sdk.Models.InternetMessageHeader.CreateFromDiscriminatorValue)?.AsList(); } },
                { "internetMessageId", n => { InternetMessageId = n.GetStringValue(); } },
                { "isDeliveryReceiptRequested", n => { IsDeliveryReceiptRequested = n.GetBoolValue(); } },
                { "isDraft", n => { IsDraft = n.GetBoolValue(); } },
                { "isRead", n => { IsRead = n.GetBoolValue(); } },
                { "isReadReceiptRequested", n => { IsReadReceiptRequested = n.GetBoolValue(); } },
                { "mentions", n => { Mentions = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Mention>(global::AITGraph.Sdk.Models.Mention.CreateFromDiscriminatorValue)?.AsList(); } },
                { "mentionsPreview", n => { MentionsPreview = n.GetObjectValue<global::AITGraph.Sdk.Models.MentionsPreview>(global::AITGraph.Sdk.Models.MentionsPreview.CreateFromDiscriminatorValue); } },
                { "multiValueExtendedProperties", n => { MultiValueExtendedProperties = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.MultiValueLegacyExtendedProperty>(global::AITGraph.Sdk.Models.MultiValueLegacyExtendedProperty.CreateFromDiscriminatorValue)?.AsList(); } },
                { "parentFolderId", n => { ParentFolderId = n.GetStringValue(); } },
                { "receivedDateTime", n => { ReceivedDateTime = n.GetDateTimeOffsetValue(); } },
                { "replyTo", n => { ReplyTo = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Recipient>(global::AITGraph.Sdk.Models.Recipient.CreateFromDiscriminatorValue)?.AsList(); } },
                { "sender", n => { Sender = n.GetObjectValue<global::AITGraph.Sdk.Models.Recipient>(global::AITGraph.Sdk.Models.Recipient.CreateFromDiscriminatorValue); } },
                { "sentDateTime", n => { SentDateTime = n.GetDateTimeOffsetValue(); } },
                { "singleValueExtendedProperties", n => { SingleValueExtendedProperties = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.SingleValueLegacyExtendedProperty>(global::AITGraph.Sdk.Models.SingleValueLegacyExtendedProperty.CreateFromDiscriminatorValue)?.AsList(); } },
                { "subject", n => { Subject = n.GetStringValue(); } },
                { "toRecipients", n => { ToRecipients = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Recipient>(global::AITGraph.Sdk.Models.Recipient.CreateFromDiscriminatorValue)?.AsList(); } },
                { "uniqueBody", n => { UniqueBody = n.GetObjectValue<global::AITGraph.Sdk.Models.ItemBody>(global::AITGraph.Sdk.Models.ItemBody.CreateFromDiscriminatorValue); } },
                { "unsubscribeData", n => { UnsubscribeData = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "unsubscribeEnabled", n => { UnsubscribeEnabled = n.GetBoolValue(); } },
                { "webLink", n => { WebLink = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public override void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            base.Serialize(writer);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Attachment>("attachments", Attachments);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Recipient>("bccRecipients", BccRecipients);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.ItemBody>("body", Body);
            writer.WriteStringValue("bodyPreview", BodyPreview);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Recipient>("ccRecipients", CcRecipients);
            writer.WriteStringValue("conversationId", ConversationId);
            writer.WriteByteArrayValue("conversationIndex", ConversationIndex);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Extension>("extensions", Extensions);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.FollowupFlag>("flag", Flag);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.Recipient>("from", From);
            writer.WriteBoolValue("hasAttachments", HasAttachments);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.Importance>("importance", Importance);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.InferenceClassificationType>("inferenceClassification", InferenceClassification);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.InternetMessageHeader>("internetMessageHeaders", InternetMessageHeaders);
            writer.WriteStringValue("internetMessageId", InternetMessageId);
            writer.WriteBoolValue("isDeliveryReceiptRequested", IsDeliveryReceiptRequested);
            writer.WriteBoolValue("isDraft", IsDraft);
            writer.WriteBoolValue("isRead", IsRead);
            writer.WriteBoolValue("isReadReceiptRequested", IsReadReceiptRequested);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Mention>("mentions", Mentions);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.MentionsPreview>("mentionsPreview", MentionsPreview);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.MultiValueLegacyExtendedProperty>("multiValueExtendedProperties", MultiValueExtendedProperties);
            writer.WriteStringValue("parentFolderId", ParentFolderId);
            writer.WriteDateTimeOffsetValue("receivedDateTime", ReceivedDateTime);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Recipient>("replyTo", ReplyTo);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.Recipient>("sender", Sender);
            writer.WriteDateTimeOffsetValue("sentDateTime", SentDateTime);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.SingleValueLegacyExtendedProperty>("singleValueExtendedProperties", SingleValueExtendedProperties);
            writer.WriteStringValue("subject", Subject);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Recipient>("toRecipients", ToRecipients);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.ItemBody>("uniqueBody", UniqueBody);
            writer.WriteCollectionOfPrimitiveValues<string>("unsubscribeData", UnsubscribeData);
            writer.WriteBoolValue("unsubscribeEnabled", UnsubscribeEnabled);
            writer.WriteStringValue("webLink", WebLink);
        }
    }
}
#pragma warning restore CS0618
