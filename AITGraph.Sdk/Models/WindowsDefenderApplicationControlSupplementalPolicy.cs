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
    public partial class WindowsDefenderApplicationControlSupplementalPolicy : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The associated group assignments for this WindowsDefenderApplicationControl supplemental policy.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicyAssignment>? Assignments { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicyAssignment> Assignments { get; set; }
#endif
        /// <summary>The WindowsDefenderApplicationControl supplemental policy content in byte array format.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public byte[]? Content { get; set; }
#nullable restore
#else
        public byte[] Content { get; set; }
#endif
        /// <summary>The WindowsDefenderApplicationControl supplemental policy content&apos;s file name.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ContentFileName { get; set; }
#nullable restore
#else
        public string ContentFileName { get; set; }
#endif
        /// <summary>The date and time when the WindowsDefenderApplicationControl supplemental policy was uploaded.</summary>
        public DateTimeOffset? CreationDateTime { get; set; }
        /// <summary>WindowsDefenderApplicationControl supplemental policy deployment summary.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicyDeploymentSummary? DeploySummary { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicyDeploymentSummary DeploySummary { get; set; }
#endif
        /// <summary>The description of WindowsDefenderApplicationControl supplemental policy.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>The list of device deployment states for this WindowsDefenderApplicationControl supplemental policy.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicyDeploymentStatus>? DeviceStatuses { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicyDeploymentStatus> DeviceStatuses { get; set; }
#endif
        /// <summary>The display name of WindowsDefenderApplicationControl supplemental policy.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DisplayName { get; set; }
#nullable restore
#else
        public string DisplayName { get; set; }
#endif
        /// <summary>The date and time when the WindowsDefenderApplicationControl supplemental policy was last modified.</summary>
        public DateTimeOffset? LastModifiedDateTime { get; set; }
        /// <summary>List of Scope Tags for this WindowsDefenderApplicationControl supplemental policy entity.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? RoleScopeTagIds { get; set; }
#nullable restore
#else
        public List<string> RoleScopeTagIds { get; set; }
#endif
        /// <summary>The WindowsDefenderApplicationControl supplemental policy&apos;s version.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Version { get; set; }
#nullable restore
#else
        public string Version { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicy"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicy CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicy();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "assignments", n => { Assignments = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicyAssignment>(global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicyAssignment.CreateFromDiscriminatorValue)?.AsList(); } },
                { "content", n => { Content = n.GetByteArrayValue(); } },
                { "contentFileName", n => { ContentFileName = n.GetStringValue(); } },
                { "creationDateTime", n => { CreationDateTime = n.GetDateTimeOffsetValue(); } },
                { "deploySummary", n => { DeploySummary = n.GetObjectValue<global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicyDeploymentSummary>(global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicyDeploymentSummary.CreateFromDiscriminatorValue); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "deviceStatuses", n => { DeviceStatuses = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicyDeploymentStatus>(global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicyDeploymentStatus.CreateFromDiscriminatorValue)?.AsList(); } },
                { "displayName", n => { DisplayName = n.GetStringValue(); } },
                { "lastModifiedDateTime", n => { LastModifiedDateTime = n.GetDateTimeOffsetValue(); } },
                { "roleScopeTagIds", n => { RoleScopeTagIds = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "version", n => { Version = n.GetStringValue(); } },
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
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicyAssignment>("assignments", Assignments);
            writer.WriteByteArrayValue("content", Content);
            writer.WriteStringValue("contentFileName", ContentFileName);
            writer.WriteDateTimeOffsetValue("creationDateTime", CreationDateTime);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicyDeploymentSummary>("deploySummary", DeploySummary);
            writer.WriteStringValue("description", Description);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.WindowsDefenderApplicationControlSupplementalPolicyDeploymentStatus>("deviceStatuses", DeviceStatuses);
            writer.WriteStringValue("displayName", DisplayName);
            writer.WriteDateTimeOffsetValue("lastModifiedDateTime", LastModifiedDateTime);
            writer.WriteCollectionOfPrimitiveValues<string>("roleScopeTagIds", RoleScopeTagIds);
            writer.WriteStringValue("version", Version);
        }
    }
}
#pragma warning restore CS0618
