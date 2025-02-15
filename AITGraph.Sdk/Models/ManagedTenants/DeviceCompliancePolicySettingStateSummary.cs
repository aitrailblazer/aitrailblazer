// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace AITGraph.Sdk.Models.ManagedTenants
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class DeviceCompliancePolicySettingStateSummary : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The number of devices in a conflict state. Optional. Read-only.</summary>
        public int? ConflictDeviceCount { get; set; }
        /// <summary>The number of devices in an error state. Optional. Read-only.</summary>
        public int? ErrorDeviceCount { get; set; }
        /// <summary>The number of devices in a failed state. Optional. Read-only.</summary>
        public int? FailedDeviceCount { get; set; }
        /// <summary>The identifer for the Microsoft Intune account. Required. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IntuneAccountId { get; set; }
#nullable restore
#else
        public string IntuneAccountId { get; set; }
#endif
        /// <summary>The identifier for the Intune setting. Optional. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IntuneSettingId { get; set; }
#nullable restore
#else
        public string IntuneSettingId { get; set; }
#endif
        /// <summary>Date and time the entity was last updated in the multi-tenant management platform. Optional. Read-only.</summary>
        public DateTimeOffset? LastRefreshedDateTime { get; set; }
        /// <summary>The number of devices in a not applicable state. Optional. Read-only.</summary>
        public int? NotApplicableDeviceCount { get; set; }
        /// <summary>The number of devices in a pending state. Optional. Read-only.</summary>
        public int? PendingDeviceCount { get; set; }
        /// <summary>The type for the device compliance policy. Optional. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PolicyType { get; set; }
#nullable restore
#else
        public string PolicyType { get; set; }
#endif
        /// <summary>The name for the setting within the device compliance policy. Optional. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SettingName { get; set; }
#nullable restore
#else
        public string SettingName { get; set; }
#endif
        /// <summary>The number of devices in a succeeded state. Optional. Read-only.</summary>
        public int? SucceededDeviceCount { get; set; }
        /// <summary>The display name for the managed tenant. Required. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? TenantDisplayName { get; set; }
#nullable restore
#else
        public string TenantDisplayName { get; set; }
#endif
        /// <summary>The Azure Active Directory tenant identifier for the managed tenant. Required. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? TenantId { get; set; }
#nullable restore
#else
        public string TenantId { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.ManagedTenants.DeviceCompliancePolicySettingStateSummary"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.ManagedTenants.DeviceCompliancePolicySettingStateSummary CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.ManagedTenants.DeviceCompliancePolicySettingStateSummary();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "conflictDeviceCount", n => { ConflictDeviceCount = n.GetIntValue(); } },
                { "errorDeviceCount", n => { ErrorDeviceCount = n.GetIntValue(); } },
                { "failedDeviceCount", n => { FailedDeviceCount = n.GetIntValue(); } },
                { "intuneAccountId", n => { IntuneAccountId = n.GetStringValue(); } },
                { "intuneSettingId", n => { IntuneSettingId = n.GetStringValue(); } },
                { "lastRefreshedDateTime", n => { LastRefreshedDateTime = n.GetDateTimeOffsetValue(); } },
                { "notApplicableDeviceCount", n => { NotApplicableDeviceCount = n.GetIntValue(); } },
                { "pendingDeviceCount", n => { PendingDeviceCount = n.GetIntValue(); } },
                { "policyType", n => { PolicyType = n.GetStringValue(); } },
                { "settingName", n => { SettingName = n.GetStringValue(); } },
                { "succeededDeviceCount", n => { SucceededDeviceCount = n.GetIntValue(); } },
                { "tenantDisplayName", n => { TenantDisplayName = n.GetStringValue(); } },
                { "tenantId", n => { TenantId = n.GetStringValue(); } },
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
            writer.WriteIntValue("conflictDeviceCount", ConflictDeviceCount);
            writer.WriteIntValue("errorDeviceCount", ErrorDeviceCount);
            writer.WriteIntValue("failedDeviceCount", FailedDeviceCount);
            writer.WriteStringValue("intuneAccountId", IntuneAccountId);
            writer.WriteStringValue("intuneSettingId", IntuneSettingId);
            writer.WriteDateTimeOffsetValue("lastRefreshedDateTime", LastRefreshedDateTime);
            writer.WriteIntValue("notApplicableDeviceCount", NotApplicableDeviceCount);
            writer.WriteIntValue("pendingDeviceCount", PendingDeviceCount);
            writer.WriteStringValue("policyType", PolicyType);
            writer.WriteStringValue("settingName", SettingName);
            writer.WriteIntValue("succeededDeviceCount", SucceededDeviceCount);
            writer.WriteStringValue("tenantDisplayName", TenantDisplayName);
            writer.WriteStringValue("tenantId", TenantId);
        }
    }
}
#pragma warning restore CS0618
