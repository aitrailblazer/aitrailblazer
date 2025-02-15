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
    public partial class Event : global::AITGraph.Sdk.Models.OutlookItem, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>true if the meeting organizer allows invitees to propose a new time when responding; otherwise false. Optional. Default is true.</summary>
        public bool? AllowNewTimeProposals { get; set; }
        /// <summary>The collection of FileAttachment, ItemAttachment, and referenceAttachment attachments for the event. Navigation property. Read-only. Nullable.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Attachment>? Attachments { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Attachment> Attachments { get; set; }
#endif
        /// <summary>The collection of attendees for the event.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Attendee>? Attendees { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Attendee> Attendees { get; set; }
#endif
        /// <summary>The body of the message associated with the event. It can be in HTML or text format.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.ItemBody? Body { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.ItemBody Body { get; set; }
#endif
        /// <summary>The preview of the message associated with the event. It is in text format.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? BodyPreview { get; set; }
#nullable restore
#else
        public string BodyPreview { get; set; }
#endif
        /// <summary>The calendar that contains the event. Navigation property. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.Calendar? Calendar { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.Calendar Calendar { get; set; }
#endif
        /// <summary>Contains occurrenceId property values of cancelled instances in a recurring series, if the event is the series master. Instances in a recurring series that are cancelled are called cancelledOccurences.Returned only on $select in a Get operation which specifies the id of a series master event (that is, the seriesMasterId property value).</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? CancelledOccurrences { get; set; }
#nullable restore
#else
        public List<string> CancelledOccurrences { get; set; }
#endif
        /// <summary>The date, time, and time zone that the event ends. By default, the end time is in UTC.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.DateTimeTimeZone? End { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.DateTimeTimeZone End { get; set; }
#endif
        /// <summary>The exceptionOccurrences property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Event>? ExceptionOccurrences { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Event> ExceptionOccurrences { get; set; }
#endif
        /// <summary>The collection of open extensions defined for the event. Nullable.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Extension>? Extensions { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Extension> Extensions { get; set; }
#endif
        /// <summary>Set to true if the event has attachments.</summary>
        public bool? HasAttachments { get; set; }
        /// <summary>When set to true, each attendee only sees themselves in the meeting request and meeting Tracking list. Default is false.</summary>
        public bool? HideAttendees { get; set; }
        /// <summary>The importance property</summary>
        public global::AITGraph.Sdk.Models.Importance? Importance { get; set; }
        /// <summary>The occurrences of a recurring series, if the event is a series master. This property includes occurrences that are part of the recurrence pattern, and exceptions that have been modified, but does not include occurrences that have been cancelled from the series. Navigation property. Read-only. Nullable.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Event>? Instances { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Event> Instances { get; set; }
#endif
        /// <summary>The isAllDay property</summary>
        public bool? IsAllDay { get; set; }
        /// <summary>The isCancelled property</summary>
        public bool? IsCancelled { get; set; }
        /// <summary>The isDraft property</summary>
        public bool? IsDraft { get; set; }
        /// <summary>The isOnlineMeeting property</summary>
        public bool? IsOnlineMeeting { get; set; }
        /// <summary>The isOrganizer property</summary>
        public bool? IsOrganizer { get; set; }
        /// <summary>The isReminderOn property</summary>
        public bool? IsReminderOn { get; set; }
        /// <summary>The location property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.Location? Location { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.Location Location { get; set; }
#endif
        /// <summary>The locations property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Location>? Locations { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Location> Locations { get; set; }
#endif
        /// <summary>The collection of multi-value extended properties defined for the event. Read-only. Nullable.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.MultiValueLegacyExtendedProperty>? MultiValueExtendedProperties { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.MultiValueLegacyExtendedProperty> MultiValueExtendedProperties { get; set; }
#endif
        /// <summary>The occurrenceId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OccurrenceId { get; set; }
#nullable restore
#else
        public string OccurrenceId { get; set; }
#endif
        /// <summary>The onlineMeeting property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.OnlineMeetingInfo? OnlineMeeting { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.OnlineMeetingInfo OnlineMeeting { get; set; }
#endif
        /// <summary>The onlineMeetingProvider property</summary>
        public global::AITGraph.Sdk.Models.OnlineMeetingProviderType? OnlineMeetingProvider { get; set; }
        /// <summary>The onlineMeetingUrl property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OnlineMeetingUrl { get; set; }
#nullable restore
#else
        public string OnlineMeetingUrl { get; set; }
#endif
        /// <summary>The organizer property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.Recipient? Organizer { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.Recipient Organizer { get; set; }
#endif
        /// <summary>The originalEndTimeZone property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OriginalEndTimeZone { get; set; }
#nullable restore
#else
        public string OriginalEndTimeZone { get; set; }
#endif
        /// <summary>The originalStart property</summary>
        public DateTimeOffset? OriginalStart { get; set; }
        /// <summary>The originalStartTimeZone property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OriginalStartTimeZone { get; set; }
#nullable restore
#else
        public string OriginalStartTimeZone { get; set; }
#endif
        /// <summary>The recurrence property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.PatternedRecurrence? Recurrence { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.PatternedRecurrence Recurrence { get; set; }
#endif
        /// <summary>The reminderMinutesBeforeStart property</summary>
        public int? ReminderMinutesBeforeStart { get; set; }
        /// <summary>The responseRequested property</summary>
        public bool? ResponseRequested { get; set; }
        /// <summary>The responseStatus property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.ResponseStatus? ResponseStatus { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.ResponseStatus ResponseStatus { get; set; }
#endif
        /// <summary>The sensitivity property</summary>
        public global::AITGraph.Sdk.Models.Sensitivity? Sensitivity { get; set; }
        /// <summary>The seriesMasterId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SeriesMasterId { get; set; }
#nullable restore
#else
        public string SeriesMasterId { get; set; }
#endif
        /// <summary>The showAs property</summary>
        public global::AITGraph.Sdk.Models.FreeBusyStatus? ShowAs { get; set; }
        /// <summary>The collection of single-value extended properties defined for the event. Read-only. Nullable.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.SingleValueLegacyExtendedProperty>? SingleValueExtendedProperties { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.SingleValueLegacyExtendedProperty> SingleValueExtendedProperties { get; set; }
#endif
        /// <summary>The start property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.DateTimeTimeZone? Start { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.DateTimeTimeZone Start { get; set; }
#endif
        /// <summary>The subject property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Subject { get; set; }
#nullable restore
#else
        public string Subject { get; set; }
#endif
        /// <summary>The transactionId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? TransactionId { get; set; }
#nullable restore
#else
        public string TransactionId { get; set; }
#endif
        /// <summary>The type property</summary>
        public global::AITGraph.Sdk.Models.EventType? Type { get; set; }
        /// <summary>The uid property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Uid { get; set; }
#nullable restore
#else
        public string Uid { get; set; }
#endif
        /// <summary>The webLink property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? WebLink { get; set; }
#nullable restore
#else
        public string WebLink { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.Event"/> and sets the default values.
        /// </summary>
        public Event() : base()
        {
            OdataType = "#microsoft.graph.event";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.Event"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.Event CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.Event();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "allowNewTimeProposals", n => { AllowNewTimeProposals = n.GetBoolValue(); } },
                { "attachments", n => { Attachments = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Attachment>(global::AITGraph.Sdk.Models.Attachment.CreateFromDiscriminatorValue)?.AsList(); } },
                { "attendees", n => { Attendees = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Attendee>(global::AITGraph.Sdk.Models.Attendee.CreateFromDiscriminatorValue)?.AsList(); } },
                { "body", n => { Body = n.GetObjectValue<global::AITGraph.Sdk.Models.ItemBody>(global::AITGraph.Sdk.Models.ItemBody.CreateFromDiscriminatorValue); } },
                { "bodyPreview", n => { BodyPreview = n.GetStringValue(); } },
                { "calendar", n => { Calendar = n.GetObjectValue<global::AITGraph.Sdk.Models.Calendar>(global::AITGraph.Sdk.Models.Calendar.CreateFromDiscriminatorValue); } },
                { "cancelledOccurrences", n => { CancelledOccurrences = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "end", n => { End = n.GetObjectValue<global::AITGraph.Sdk.Models.DateTimeTimeZone>(global::AITGraph.Sdk.Models.DateTimeTimeZone.CreateFromDiscriminatorValue); } },
                { "exceptionOccurrences", n => { ExceptionOccurrences = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Event>(global::AITGraph.Sdk.Models.Event.CreateFromDiscriminatorValue)?.AsList(); } },
                { "extensions", n => { Extensions = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Extension>(global::AITGraph.Sdk.Models.Extension.CreateFromDiscriminatorValue)?.AsList(); } },
                { "hasAttachments", n => { HasAttachments = n.GetBoolValue(); } },
                { "hideAttendees", n => { HideAttendees = n.GetBoolValue(); } },
                { "importance", n => { Importance = n.GetEnumValue<global::AITGraph.Sdk.Models.Importance>(); } },
                { "instances", n => { Instances = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Event>(global::AITGraph.Sdk.Models.Event.CreateFromDiscriminatorValue)?.AsList(); } },
                { "isAllDay", n => { IsAllDay = n.GetBoolValue(); } },
                { "isCancelled", n => { IsCancelled = n.GetBoolValue(); } },
                { "isDraft", n => { IsDraft = n.GetBoolValue(); } },
                { "isOnlineMeeting", n => { IsOnlineMeeting = n.GetBoolValue(); } },
                { "isOrganizer", n => { IsOrganizer = n.GetBoolValue(); } },
                { "isReminderOn", n => { IsReminderOn = n.GetBoolValue(); } },
                { "location", n => { Location = n.GetObjectValue<global::AITGraph.Sdk.Models.Location>(global::AITGraph.Sdk.Models.Location.CreateFromDiscriminatorValue); } },
                { "locations", n => { Locations = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Location>(global::AITGraph.Sdk.Models.Location.CreateFromDiscriminatorValue)?.AsList(); } },
                { "multiValueExtendedProperties", n => { MultiValueExtendedProperties = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.MultiValueLegacyExtendedProperty>(global::AITGraph.Sdk.Models.MultiValueLegacyExtendedProperty.CreateFromDiscriminatorValue)?.AsList(); } },
                { "occurrenceId", n => { OccurrenceId = n.GetStringValue(); } },
                { "onlineMeeting", n => { OnlineMeeting = n.GetObjectValue<global::AITGraph.Sdk.Models.OnlineMeetingInfo>(global::AITGraph.Sdk.Models.OnlineMeetingInfo.CreateFromDiscriminatorValue); } },
                { "onlineMeetingProvider", n => { OnlineMeetingProvider = n.GetEnumValue<global::AITGraph.Sdk.Models.OnlineMeetingProviderType>(); } },
                { "onlineMeetingUrl", n => { OnlineMeetingUrl = n.GetStringValue(); } },
                { "organizer", n => { Organizer = n.GetObjectValue<global::AITGraph.Sdk.Models.Recipient>(global::AITGraph.Sdk.Models.Recipient.CreateFromDiscriminatorValue); } },
                { "originalEndTimeZone", n => { OriginalEndTimeZone = n.GetStringValue(); } },
                { "originalStart", n => { OriginalStart = n.GetDateTimeOffsetValue(); } },
                { "originalStartTimeZone", n => { OriginalStartTimeZone = n.GetStringValue(); } },
                { "recurrence", n => { Recurrence = n.GetObjectValue<global::AITGraph.Sdk.Models.PatternedRecurrence>(global::AITGraph.Sdk.Models.PatternedRecurrence.CreateFromDiscriminatorValue); } },
                { "reminderMinutesBeforeStart", n => { ReminderMinutesBeforeStart = n.GetIntValue(); } },
                { "responseRequested", n => { ResponseRequested = n.GetBoolValue(); } },
                { "responseStatus", n => { ResponseStatus = n.GetObjectValue<global::AITGraph.Sdk.Models.ResponseStatus>(global::AITGraph.Sdk.Models.ResponseStatus.CreateFromDiscriminatorValue); } },
                { "sensitivity", n => { Sensitivity = n.GetEnumValue<global::AITGraph.Sdk.Models.Sensitivity>(); } },
                { "seriesMasterId", n => { SeriesMasterId = n.GetStringValue(); } },
                { "showAs", n => { ShowAs = n.GetEnumValue<global::AITGraph.Sdk.Models.FreeBusyStatus>(); } },
                { "singleValueExtendedProperties", n => { SingleValueExtendedProperties = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.SingleValueLegacyExtendedProperty>(global::AITGraph.Sdk.Models.SingleValueLegacyExtendedProperty.CreateFromDiscriminatorValue)?.AsList(); } },
                { "start", n => { Start = n.GetObjectValue<global::AITGraph.Sdk.Models.DateTimeTimeZone>(global::AITGraph.Sdk.Models.DateTimeTimeZone.CreateFromDiscriminatorValue); } },
                { "subject", n => { Subject = n.GetStringValue(); } },
                { "transactionId", n => { TransactionId = n.GetStringValue(); } },
                { "type", n => { Type = n.GetEnumValue<global::AITGraph.Sdk.Models.EventType>(); } },
                { "uid", n => { Uid = n.GetStringValue(); } },
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
            writer.WriteBoolValue("allowNewTimeProposals", AllowNewTimeProposals);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Attachment>("attachments", Attachments);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Attendee>("attendees", Attendees);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.ItemBody>("body", Body);
            writer.WriteStringValue("bodyPreview", BodyPreview);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.Calendar>("calendar", Calendar);
            writer.WriteCollectionOfPrimitiveValues<string>("cancelledOccurrences", CancelledOccurrences);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.DateTimeTimeZone>("end", End);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Event>("exceptionOccurrences", ExceptionOccurrences);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Extension>("extensions", Extensions);
            writer.WriteBoolValue("hasAttachments", HasAttachments);
            writer.WriteBoolValue("hideAttendees", HideAttendees);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.Importance>("importance", Importance);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Event>("instances", Instances);
            writer.WriteBoolValue("isAllDay", IsAllDay);
            writer.WriteBoolValue("isCancelled", IsCancelled);
            writer.WriteBoolValue("isDraft", IsDraft);
            writer.WriteBoolValue("isOnlineMeeting", IsOnlineMeeting);
            writer.WriteBoolValue("isOrganizer", IsOrganizer);
            writer.WriteBoolValue("isReminderOn", IsReminderOn);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.Location>("location", Location);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Location>("locations", Locations);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.MultiValueLegacyExtendedProperty>("multiValueExtendedProperties", MultiValueExtendedProperties);
            writer.WriteStringValue("occurrenceId", OccurrenceId);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.OnlineMeetingInfo>("onlineMeeting", OnlineMeeting);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.OnlineMeetingProviderType>("onlineMeetingProvider", OnlineMeetingProvider);
            writer.WriteStringValue("onlineMeetingUrl", OnlineMeetingUrl);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.Recipient>("organizer", Organizer);
            writer.WriteStringValue("originalEndTimeZone", OriginalEndTimeZone);
            writer.WriteDateTimeOffsetValue("originalStart", OriginalStart);
            writer.WriteStringValue("originalStartTimeZone", OriginalStartTimeZone);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.PatternedRecurrence>("recurrence", Recurrence);
            writer.WriteIntValue("reminderMinutesBeforeStart", ReminderMinutesBeforeStart);
            writer.WriteBoolValue("responseRequested", ResponseRequested);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.ResponseStatus>("responseStatus", ResponseStatus);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.Sensitivity>("sensitivity", Sensitivity);
            writer.WriteStringValue("seriesMasterId", SeriesMasterId);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.FreeBusyStatus>("showAs", ShowAs);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.SingleValueLegacyExtendedProperty>("singleValueExtendedProperties", SingleValueExtendedProperties);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.DateTimeTimeZone>("start", Start);
            writer.WriteStringValue("subject", Subject);
            writer.WriteStringValue("transactionId", TransactionId);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.EventType>("type", Type);
            writer.WriteStringValue("uid", Uid);
            writer.WriteStringValue("webLink", WebLink);
        }
    }
}
#pragma warning restore CS0618
