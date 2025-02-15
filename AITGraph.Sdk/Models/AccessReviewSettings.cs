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
    public partial class AccessReviewSettings : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Indicates whether showing recommendations to reviewers is enabled.</summary>
        public bool? AccessRecommendationsEnabled { get; set; }
        /// <summary>The number of days of user activities to show to reviewers.</summary>
        public int? ActivityDurationInDays { get; set; }
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Indicates whether the auto-apply capability, to automatically change the target object access resource, is enabled.  If not enabled, a user must, after the review completes, apply the access review.</summary>
        public bool? AutoApplyReviewResultsEnabled { get; set; }
        /// <summary>Indicates whether a decision should be set if the reviewer did not supply one. For use when auto-apply is enabled. If you don&apos;t want to have a review decision recorded unless the reviewer makes an explicit choice, set it to false.</summary>
        public bool? AutoReviewEnabled { get; set; }
        /// <summary>Detailed settings for how the feature should set the review decision. For use when auto-apply is enabled.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.AutoReviewSettings? AutoReviewSettings { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.AutoReviewSettings AutoReviewSettings { get; set; }
#endif
        /// <summary>Indicates whether reviewers are required to provide a justification when reviewing access.</summary>
        public bool? JustificationRequiredOnApproval { get; set; }
        /// <summary>Indicates whether sending mails to reviewers and the review creator is enabled.</summary>
        public bool? MailNotificationsEnabled { get; set; }
        /// <summary>The OdataType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OdataType { get; set; }
#nullable restore
#else
        public string OdataType { get; set; }
#endif
        /// <summary>Detailed settings for recurrence.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.AccessReviewRecurrenceSettings? RecurrenceSettings { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.AccessReviewRecurrenceSettings RecurrenceSettings { get; set; }
#endif
        /// <summary>Indicates whether sending reminder emails to reviewers is enabled.</summary>
        public bool? RemindersEnabled { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.AccessReviewSettings"/> and sets the default values.
        /// </summary>
        public AccessReviewSettings()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.AccessReviewSettings"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::AITGraph.Sdk.Models.AccessReviewSettings CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            var mappingValue = parseNode.GetChildNode("@odata.type")?.GetStringValue();
            return mappingValue switch
            {
                "#microsoft.graph.businessFlowSettings" => new global::AITGraph.Sdk.Models.BusinessFlowSettings(),
                _ => new global::AITGraph.Sdk.Models.AccessReviewSettings(),
            };
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "accessRecommendationsEnabled", n => { AccessRecommendationsEnabled = n.GetBoolValue(); } },
                { "activityDurationInDays", n => { ActivityDurationInDays = n.GetIntValue(); } },
                { "autoApplyReviewResultsEnabled", n => { AutoApplyReviewResultsEnabled = n.GetBoolValue(); } },
                { "autoReviewEnabled", n => { AutoReviewEnabled = n.GetBoolValue(); } },
                { "autoReviewSettings", n => { AutoReviewSettings = n.GetObjectValue<global::AITGraph.Sdk.Models.AutoReviewSettings>(global::AITGraph.Sdk.Models.AutoReviewSettings.CreateFromDiscriminatorValue); } },
                { "justificationRequiredOnApproval", n => { JustificationRequiredOnApproval = n.GetBoolValue(); } },
                { "mailNotificationsEnabled", n => { MailNotificationsEnabled = n.GetBoolValue(); } },
                { "@odata.type", n => { OdataType = n.GetStringValue(); } },
                { "recurrenceSettings", n => { RecurrenceSettings = n.GetObjectValue<global::AITGraph.Sdk.Models.AccessReviewRecurrenceSettings>(global::AITGraph.Sdk.Models.AccessReviewRecurrenceSettings.CreateFromDiscriminatorValue); } },
                { "remindersEnabled", n => { RemindersEnabled = n.GetBoolValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteBoolValue("accessRecommendationsEnabled", AccessRecommendationsEnabled);
            writer.WriteIntValue("activityDurationInDays", ActivityDurationInDays);
            writer.WriteBoolValue("autoApplyReviewResultsEnabled", AutoApplyReviewResultsEnabled);
            writer.WriteBoolValue("autoReviewEnabled", AutoReviewEnabled);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.AutoReviewSettings>("autoReviewSettings", AutoReviewSettings);
            writer.WriteBoolValue("justificationRequiredOnApproval", JustificationRequiredOnApproval);
            writer.WriteBoolValue("mailNotificationsEnabled", MailNotificationsEnabled);
            writer.WriteStringValue("@odata.type", OdataType);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.AccessReviewRecurrenceSettings>("recurrenceSettings", RecurrenceSettings);
            writer.WriteBoolValue("remindersEnabled", RemindersEnabled);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
