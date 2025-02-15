// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace AITGraph.Sdk.Models
{
    /// <summary>
    /// Settings for Android For Work.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class AndroidForWorkSettings : global::AITGraph.Sdk.Models.Entity, IParsable
    {
        /// <summary>Bind status of the tenant with the Google EMM API</summary>
        public global::AITGraph.Sdk.Models.AndroidForWorkBindStatus? BindStatus { get; set; }
        /// <summary>Indicates if this account is flighting for Android Device Owner Management with CloudDPC.</summary>
        public bool? DeviceOwnerManagementEnabled { get; set; }
        /// <summary>Android for Work device management targeting type for the account</summary>
        public global::AITGraph.Sdk.Models.AndroidForWorkEnrollmentTarget? EnrollmentTarget { get; set; }
        /// <summary>Last completion time for app sync</summary>
        public DateTimeOffset? LastAppSyncDateTime { get; set; }
        /// <summary>Sync status of the tenant with the Google EMM API</summary>
        public global::AITGraph.Sdk.Models.AndroidForWorkSyncStatus? LastAppSyncStatus { get; set; }
        /// <summary>Last modification time for Android for Work settings</summary>
        public DateTimeOffset? LastModifiedDateTime { get; set; }
        /// <summary>Organization name used when onboarding Android for Work</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OwnerOrganizationName { get; set; }
#nullable restore
#else
        public string OwnerOrganizationName { get; set; }
#endif
        /// <summary>Owner UPN that created the enterprise</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OwnerUserPrincipalName { get; set; }
#nullable restore
#else
        public string OwnerUserPrincipalName { get; set; }
#endif
        /// <summary>Specifies which AAD groups can enroll devices in Android for Work device management if enrollmentTarget is set to &apos;Targeted&apos;</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? TargetGroupIds { get; set; }
#nullable restore
#else
        public List<string> TargetGroupIds { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.AndroidForWorkSettings"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.AndroidForWorkSettings CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.AndroidForWorkSettings();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "bindStatus", n => { BindStatus = n.GetEnumValue<global::AITGraph.Sdk.Models.AndroidForWorkBindStatus>(); } },
                { "deviceOwnerManagementEnabled", n => { DeviceOwnerManagementEnabled = n.GetBoolValue(); } },
                { "enrollmentTarget", n => { EnrollmentTarget = n.GetEnumValue<global::AITGraph.Sdk.Models.AndroidForWorkEnrollmentTarget>(); } },
                { "lastAppSyncDateTime", n => { LastAppSyncDateTime = n.GetDateTimeOffsetValue(); } },
                { "lastAppSyncStatus", n => { LastAppSyncStatus = n.GetEnumValue<global::AITGraph.Sdk.Models.AndroidForWorkSyncStatus>(); } },
                { "lastModifiedDateTime", n => { LastModifiedDateTime = n.GetDateTimeOffsetValue(); } },
                { "ownerOrganizationName", n => { OwnerOrganizationName = n.GetStringValue(); } },
                { "ownerUserPrincipalName", n => { OwnerUserPrincipalName = n.GetStringValue(); } },
                { "targetGroupIds", n => { TargetGroupIds = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
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
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.AndroidForWorkBindStatus>("bindStatus", BindStatus);
            writer.WriteBoolValue("deviceOwnerManagementEnabled", DeviceOwnerManagementEnabled);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.AndroidForWorkEnrollmentTarget>("enrollmentTarget", EnrollmentTarget);
            writer.WriteDateTimeOffsetValue("lastAppSyncDateTime", LastAppSyncDateTime);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.AndroidForWorkSyncStatus>("lastAppSyncStatus", LastAppSyncStatus);
            writer.WriteDateTimeOffsetValue("lastModifiedDateTime", LastModifiedDateTime);
            writer.WriteStringValue("ownerOrganizationName", OwnerOrganizationName);
            writer.WriteStringValue("ownerUserPrincipalName", OwnerUserPrincipalName);
            writer.WriteCollectionOfPrimitiveValues<string>("targetGroupIds", TargetGroupIds);
        }
    }
}
#pragma warning restore CS0618
