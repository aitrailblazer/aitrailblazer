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
    public partial class AccessReviewInstance : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Returns the collection of reviewers who were contacted to complete this review. While the reviewers and fallbackReviewers properties of the accessReviewScheduleDefinition might specify group owners or managers as reviewers, contactedReviewers returns their individual identities. Supports $select. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.AccessReviewReviewer>? ContactedReviewers { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.AccessReviewReviewer> ContactedReviewers { get; set; }
#endif
        /// <summary>Each user reviewed in an accessReviewInstance has a decision item representing if they were approved, denied, or not yet reviewed.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.AccessReviewInstanceDecisionItem>? Decisions { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.AccessReviewInstanceDecisionItem> Decisions { get; set; }
#endif
        /// <summary>There is exactly one accessReviewScheduleDefinition associated with each instance. It is the parent schedule for the instance, where instances are created for each recurrence of a review definition and each group selected to review by the definition.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.AccessReviewScheduleDefinition? Definition { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.AccessReviewScheduleDefinition Definition { get; set; }
#endif
        /// <summary>DateTime when review instance is scheduled to end.The DatetimeOffset type represents date and time information using ISO 8601 format and is always in UTC time. For example, midnight UTC on Jan 1, 2014 is 2014-01-01T00:00:00Z. Supports $select. Read-only.</summary>
        public DateTimeOffset? EndDateTime { get; set; }
        /// <summary>Collection of errors in an access review instance lifecycle. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.AccessReviewError>? Errors { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.AccessReviewError> Errors { get; set; }
#endif
        /// <summary>This collection of reviewer scopes is used to define the list of fallback reviewers. These fallback reviewers will be notified to take action if no users are found from the list of reviewers specified. This could occur when either the group owner is specified as the reviewer but the group owner does not exist, or manager is specified as reviewer but a user&apos;s manager does not exist. Supports $select.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.AccessReviewReviewerScope>? FallbackReviewers { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.AccessReviewReviewerScope> FallbackReviewers { get; set; }
#endif
        /// <summary>This collection of access review scopes is used to define who the reviewers are. Supports $select. For examples of options for assigning reviewers, see Assign reviewers to your access review definition using the Microsoft Graph API.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.AccessReviewReviewerScope>? Reviewers { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.AccessReviewReviewerScope> Reviewers { get; set; }
#endif
        /// <summary>Created based on scope and instanceEnumerationScope at the accessReviewScheduleDefinition level. Defines the scope of users reviewed in a group. Supports $select and $filter (contains only). Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.AccessReviewScope? Scope { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.AccessReviewScope Scope { get; set; }
#endif
        /// <summary>If the instance has multiple stages, this returns the collection of stages. A new stage will only be created when the previous stage ends. The existence, number, and settings of stages on a review instance are created based on the accessReviewStageSettings on the parent accessReviewScheduleDefinition.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.AccessReviewStage>? Stages { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.AccessReviewStage> Stages { get; set; }
#endif
        /// <summary>DateTime when review instance is scheduled to start. May be in the future. The DateTimeOffset type represents date and time information using ISO 8601 format and is always in UTC time. For example, midnight UTC on Jan 1, 2014 is 2014-01-01T00:00:00Z. Supports $select. Read-only.</summary>
        public DateTimeOffset? StartDateTime { get; set; }
        /// <summary>Specifies the status of an accessReview. Possible values: Initializing, NotStarted, Starting, InProgress, Completing, Completed, AutoReviewing, and AutoReviewed. Supports $select, $orderby, and $filter (eq only). Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Status { get; set; }
#nullable restore
#else
        public string Status { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.AccessReviewInstance"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.AccessReviewInstance CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.AccessReviewInstance();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "contactedReviewers", n => { ContactedReviewers = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.AccessReviewReviewer>(global::AITGraph.Sdk.Models.AccessReviewReviewer.CreateFromDiscriminatorValue)?.AsList(); } },
                { "decisions", n => { Decisions = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.AccessReviewInstanceDecisionItem>(global::AITGraph.Sdk.Models.AccessReviewInstanceDecisionItem.CreateFromDiscriminatorValue)?.AsList(); } },
                { "definition", n => { Definition = n.GetObjectValue<global::AITGraph.Sdk.Models.AccessReviewScheduleDefinition>(global::AITGraph.Sdk.Models.AccessReviewScheduleDefinition.CreateFromDiscriminatorValue); } },
                { "endDateTime", n => { EndDateTime = n.GetDateTimeOffsetValue(); } },
                { "errors", n => { Errors = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.AccessReviewError>(global::AITGraph.Sdk.Models.AccessReviewError.CreateFromDiscriminatorValue)?.AsList(); } },
                { "fallbackReviewers", n => { FallbackReviewers = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.AccessReviewReviewerScope>(global::AITGraph.Sdk.Models.AccessReviewReviewerScope.CreateFromDiscriminatorValue)?.AsList(); } },
                { "reviewers", n => { Reviewers = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.AccessReviewReviewerScope>(global::AITGraph.Sdk.Models.AccessReviewReviewerScope.CreateFromDiscriminatorValue)?.AsList(); } },
                { "scope", n => { Scope = n.GetObjectValue<global::AITGraph.Sdk.Models.AccessReviewScope>(global::AITGraph.Sdk.Models.AccessReviewScope.CreateFromDiscriminatorValue); } },
                { "stages", n => { Stages = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.AccessReviewStage>(global::AITGraph.Sdk.Models.AccessReviewStage.CreateFromDiscriminatorValue)?.AsList(); } },
                { "startDateTime", n => { StartDateTime = n.GetDateTimeOffsetValue(); } },
                { "status", n => { Status = n.GetStringValue(); } },
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
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.AccessReviewReviewer>("contactedReviewers", ContactedReviewers);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.AccessReviewInstanceDecisionItem>("decisions", Decisions);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.AccessReviewScheduleDefinition>("definition", Definition);
            writer.WriteDateTimeOffsetValue("endDateTime", EndDateTime);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.AccessReviewError>("errors", Errors);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.AccessReviewReviewerScope>("fallbackReviewers", FallbackReviewers);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.AccessReviewReviewerScope>("reviewers", Reviewers);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.AccessReviewScope>("scope", Scope);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.AccessReviewStage>("stages", Stages);
            writer.WriteDateTimeOffsetValue("startDateTime", StartDateTime);
            writer.WriteStringValue("status", Status);
        }
    }
}
#pragma warning restore CS0618
