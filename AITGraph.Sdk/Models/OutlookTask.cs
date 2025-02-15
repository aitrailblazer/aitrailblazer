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
    public partial class OutlookTask : global::AITGraph.Sdk.Models.OutlookItem, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The name of the person who has been assigned the task in Outlook. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AssignedTo { get; set; }
#nullable restore
#else
        public string AssignedTo { get; set; }
#endif
        /// <summary>The collection of fileAttachment, itemAttachment, and referenceAttachment attachments for the task.  Read-only. Nullable.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Attachment>? Attachments { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Attachment> Attachments { get; set; }
#endif
        /// <summary>The task body that typically contains information about the task. Note that only HTML type is supported.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.ItemBody? Body { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.ItemBody Body { get; set; }
#endif
        /// <summary>The date in the specified time zone that the task was finished.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.DateTimeTimeZone? CompletedDateTime { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.DateTimeTimeZone CompletedDateTime { get; set; }
#endif
        /// <summary>The date in the specified time zone that the task is to be finished.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.DateTimeTimeZone? DueDateTime { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.DateTimeTimeZone DueDateTime { get; set; }
#endif
        /// <summary>Set to true if the task has attachments.</summary>
        public bool? HasAttachments { get; set; }
        /// <summary>The importance property</summary>
        public global::AITGraph.Sdk.Models.Importance? Importance { get; set; }
        /// <summary>The isReminderOn property</summary>
        public bool? IsReminderOn { get; set; }
        /// <summary>The collection of multi-value extended properties defined for the task. Read-only. Nullable.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.MultiValueLegacyExtendedProperty>? MultiValueExtendedProperties { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.MultiValueLegacyExtendedProperty> MultiValueExtendedProperties { get; set; }
#endif
        /// <summary>The owner property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Owner { get; set; }
#nullable restore
#else
        public string Owner { get; set; }
#endif
        /// <summary>The parentFolderId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ParentFolderId { get; set; }
#nullable restore
#else
        public string ParentFolderId { get; set; }
#endif
        /// <summary>The recurrence property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.PatternedRecurrence? Recurrence { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.PatternedRecurrence Recurrence { get; set; }
#endif
        /// <summary>The reminderDateTime property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.DateTimeTimeZone? ReminderDateTime { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.DateTimeTimeZone ReminderDateTime { get; set; }
#endif
        /// <summary>The sensitivity property</summary>
        public global::AITGraph.Sdk.Models.Sensitivity? Sensitivity { get; set; }
        /// <summary>The collection of single-value extended properties defined for the task. Read-only. Nullable.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.SingleValueLegacyExtendedProperty>? SingleValueExtendedProperties { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.SingleValueLegacyExtendedProperty> SingleValueExtendedProperties { get; set; }
#endif
        /// <summary>The startDateTime property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.DateTimeTimeZone? StartDateTime { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.DateTimeTimeZone StartDateTime { get; set; }
#endif
        /// <summary>The status property</summary>
        public global::AITGraph.Sdk.Models.TaskStatus? Status { get; set; }
        /// <summary>The subject property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Subject { get; set; }
#nullable restore
#else
        public string Subject { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.OutlookTask"/> and sets the default values.
        /// </summary>
        public OutlookTask() : base()
        {
            OdataType = "#microsoft.graph.outlookTask";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.OutlookTask"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.OutlookTask CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.OutlookTask();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "assignedTo", n => { AssignedTo = n.GetStringValue(); } },
                { "attachments", n => { Attachments = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Attachment>(global::AITGraph.Sdk.Models.Attachment.CreateFromDiscriminatorValue)?.AsList(); } },
                { "body", n => { Body = n.GetObjectValue<global::AITGraph.Sdk.Models.ItemBody>(global::AITGraph.Sdk.Models.ItemBody.CreateFromDiscriminatorValue); } },
                { "completedDateTime", n => { CompletedDateTime = n.GetObjectValue<global::AITGraph.Sdk.Models.DateTimeTimeZone>(global::AITGraph.Sdk.Models.DateTimeTimeZone.CreateFromDiscriminatorValue); } },
                { "dueDateTime", n => { DueDateTime = n.GetObjectValue<global::AITGraph.Sdk.Models.DateTimeTimeZone>(global::AITGraph.Sdk.Models.DateTimeTimeZone.CreateFromDiscriminatorValue); } },
                { "hasAttachments", n => { HasAttachments = n.GetBoolValue(); } },
                { "importance", n => { Importance = n.GetEnumValue<global::AITGraph.Sdk.Models.Importance>(); } },
                { "isReminderOn", n => { IsReminderOn = n.GetBoolValue(); } },
                { "multiValueExtendedProperties", n => { MultiValueExtendedProperties = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.MultiValueLegacyExtendedProperty>(global::AITGraph.Sdk.Models.MultiValueLegacyExtendedProperty.CreateFromDiscriminatorValue)?.AsList(); } },
                { "owner", n => { Owner = n.GetStringValue(); } },
                { "parentFolderId", n => { ParentFolderId = n.GetStringValue(); } },
                { "recurrence", n => { Recurrence = n.GetObjectValue<global::AITGraph.Sdk.Models.PatternedRecurrence>(global::AITGraph.Sdk.Models.PatternedRecurrence.CreateFromDiscriminatorValue); } },
                { "reminderDateTime", n => { ReminderDateTime = n.GetObjectValue<global::AITGraph.Sdk.Models.DateTimeTimeZone>(global::AITGraph.Sdk.Models.DateTimeTimeZone.CreateFromDiscriminatorValue); } },
                { "sensitivity", n => { Sensitivity = n.GetEnumValue<global::AITGraph.Sdk.Models.Sensitivity>(); } },
                { "singleValueExtendedProperties", n => { SingleValueExtendedProperties = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.SingleValueLegacyExtendedProperty>(global::AITGraph.Sdk.Models.SingleValueLegacyExtendedProperty.CreateFromDiscriminatorValue)?.AsList(); } },
                { "startDateTime", n => { StartDateTime = n.GetObjectValue<global::AITGraph.Sdk.Models.DateTimeTimeZone>(global::AITGraph.Sdk.Models.DateTimeTimeZone.CreateFromDiscriminatorValue); } },
                { "status", n => { Status = n.GetEnumValue<global::AITGraph.Sdk.Models.TaskStatus>(); } },
                { "subject", n => { Subject = n.GetStringValue(); } },
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
            writer.WriteStringValue("assignedTo", AssignedTo);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Attachment>("attachments", Attachments);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.ItemBody>("body", Body);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.DateTimeTimeZone>("completedDateTime", CompletedDateTime);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.DateTimeTimeZone>("dueDateTime", DueDateTime);
            writer.WriteBoolValue("hasAttachments", HasAttachments);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.Importance>("importance", Importance);
            writer.WriteBoolValue("isReminderOn", IsReminderOn);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.MultiValueLegacyExtendedProperty>("multiValueExtendedProperties", MultiValueExtendedProperties);
            writer.WriteStringValue("owner", Owner);
            writer.WriteStringValue("parentFolderId", ParentFolderId);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.PatternedRecurrence>("recurrence", Recurrence);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.DateTimeTimeZone>("reminderDateTime", ReminderDateTime);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.Sensitivity>("sensitivity", Sensitivity);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.SingleValueLegacyExtendedProperty>("singleValueExtendedProperties", SingleValueExtendedProperties);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.DateTimeTimeZone>("startDateTime", StartDateTime);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.TaskStatus>("status", Status);
            writer.WriteStringValue("subject", Subject);
        }
    }
}
#pragma warning restore CS0618
