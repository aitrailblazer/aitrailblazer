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
    /// The user experience analytics Device for work from anywhere report
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class UserExperienceAnalyticsWorkFromAnywhereDevice : global::AITGraph.Sdk.Models.Entity, IParsable
    {
        /// <summary>The user experience analytics work from anywhere intune device&apos;s autopilotProfileAssigned.</summary>
        public bool? AutoPilotProfileAssigned { get; set; }
        /// <summary>The user experience work from anywhere intune device&apos;s autopilotRegistered.</summary>
        public bool? AutoPilotRegistered { get; set; }
        /// <summary>The user experience work from anywhere azure Ad device Id.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AzureAdDeviceId { get; set; }
#nullable restore
#else
        public string AzureAdDeviceId { get; set; }
#endif
        /// <summary>The user experience work from anywhere device&apos;s azure Ad joinType.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AzureAdJoinType { get; set; }
#nullable restore
#else
        public string AzureAdJoinType { get; set; }
#endif
        /// <summary>The user experience work from anywhere device&apos;s azureAdRegistered.</summary>
        public bool? AzureAdRegistered { get; set; }
        /// <summary>The user experience work from anywhere per device cloud identity score. Valid values -1.79769313486232E+308 to 1.79769313486232E+308</summary>
        public double? CloudIdentityScore { get; set; }
        /// <summary>The user experience work from anywhere per device cloud management score. Valid values -1.79769313486232E+308 to 1.79769313486232E+308</summary>
        public double? CloudManagementScore { get; set; }
        /// <summary>The user experience work from anywhere per device cloud provisioning score. Valid values -1.79769313486232E+308 to 1.79769313486232E+308</summary>
        public double? CloudProvisioningScore { get; set; }
        /// <summary>The user experience work from anywhere device&apos;s compliancePolicySetToIntune.</summary>
        public bool? CompliancePolicySetToIntune { get; set; }
        /// <summary>The user experience work from anywhere device Id.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DeviceId { get; set; }
#nullable restore
#else
        public string DeviceId { get; set; }
#endif
        /// <summary>The work from anywhere device&apos;s name.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DeviceName { get; set; }
#nullable restore
#else
        public string DeviceName { get; set; }
#endif
        /// <summary>The healthStatus property</summary>
        public global::AITGraph.Sdk.Models.UserExperienceAnalyticsHealthState? HealthStatus { get; set; }
        /// <summary>The user experience work from anywhere device&apos;s Cloud Management Gateway for Configuration Manager is enabled.</summary>
        public bool? IsCloudManagedGatewayEnabled { get; set; }
        /// <summary>The user experience work from anywhere management agent of the device.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ManagedBy { get; set; }
#nullable restore
#else
        public string ManagedBy { get; set; }
#endif
        /// <summary>The user experience work from anywhere device&apos;s manufacturer.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Manufacturer { get; set; }
#nullable restore
#else
        public string Manufacturer { get; set; }
#endif
        /// <summary>The user experience work from anywhere device&apos;s model.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Model { get; set; }
#nullable restore
#else
        public string Model { get; set; }
#endif
        /// <summary>The user experience work from anywhere device, Is OS check failed for device to upgrade to the latest version of windows.</summary>
        public bool? OsCheckFailed { get; set; }
        /// <summary>The user experience work from anywhere device&apos;s OS Description.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OsDescription { get; set; }
#nullable restore
#else
        public string OsDescription { get; set; }
#endif
        /// <summary>The user experience work from anywhere device&apos;s OS Version.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OsVersion { get; set; }
#nullable restore
#else
        public string OsVersion { get; set; }
#endif
        /// <summary>The user experience work from anywhere device&apos;s otherWorkloadsSetToIntune.</summary>
        public bool? OtherWorkloadsSetToIntune { get; set; }
        /// <summary>The user experience work from anywhere device&apos;s ownership.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Ownership { get; set; }
#nullable restore
#else
        public string Ownership { get; set; }
#endif
        /// <summary>The user experience work from anywhere device, Is processor hardware 64-bit architecture check failed for device to upgrade to the latest version of windows.</summary>
        public bool? Processor64BitCheckFailed { get; set; }
        /// <summary>The user experience work from anywhere device, Is processor hardware core count check failed for device to upgrade to the latest version of windows.</summary>
        public bool? ProcessorCoreCountCheckFailed { get; set; }
        /// <summary>The user experience work from anywhere device, Is processor hardware family check failed for device to upgrade to the latest version of windows.</summary>
        public bool? ProcessorFamilyCheckFailed { get; set; }
        /// <summary>The user experience work from anywhere device, Is processor hardware speed check failed for device to upgrade to the latest version of windows.</summary>
        public bool? ProcessorSpeedCheckFailed { get; set; }
        /// <summary>Is the user experience analytics work from anywhere device RAM hardware check failed for device to upgrade to the latest version of windows</summary>
        public bool? RamCheckFailed { get; set; }
        /// <summary>The user experience work from anywhere device, Is secure boot hardware check failed for device to upgrade to the latest version of windows.</summary>
        public bool? SecureBootCheckFailed { get; set; }
        /// <summary>The user experience work from anywhere device&apos;s serial number.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SerialNumber { get; set; }
#nullable restore
#else
        public string SerialNumber { get; set; }
#endif
        /// <summary>The user experience work from anywhere device, Is storage hardware check failed for device to upgrade to the latest version of windows.</summary>
        public bool? StorageCheckFailed { get; set; }
        /// <summary>The user experience work from anywhere device&apos;s tenantAttached.</summary>
        public bool? TenantAttached { get; set; }
        /// <summary>The user experience work from anywhere device, Is Trusted Platform Module (TPM) hardware check failed for device to the latest version of upgrade to windows.</summary>
        public bool? TpmCheckFailed { get; set; }
        /// <summary>Work From Anywhere windows device upgrade eligibility status</summary>
        public global::AITGraph.Sdk.Models.OperatingSystemUpgradeEligibility? UpgradeEligibility { get; set; }
        /// <summary>The user experience work from anywhere per device windows score. Valid values -1.79769313486232E+308 to 1.79769313486232E+308</summary>
        public double? WindowsScore { get; set; }
        /// <summary>The user experience work from anywhere per device overall score. Valid values -1.79769313486232E+308 to 1.79769313486232E+308</summary>
        public double? WorkFromAnywhereScore { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.UserExperienceAnalyticsWorkFromAnywhereDevice"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.UserExperienceAnalyticsWorkFromAnywhereDevice CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.UserExperienceAnalyticsWorkFromAnywhereDevice();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "autoPilotProfileAssigned", n => { AutoPilotProfileAssigned = n.GetBoolValue(); } },
                { "autoPilotRegistered", n => { AutoPilotRegistered = n.GetBoolValue(); } },
                { "azureAdDeviceId", n => { AzureAdDeviceId = n.GetStringValue(); } },
                { "azureAdJoinType", n => { AzureAdJoinType = n.GetStringValue(); } },
                { "azureAdRegistered", n => { AzureAdRegistered = n.GetBoolValue(); } },
                { "cloudIdentityScore", n => { CloudIdentityScore = n.GetDoubleValue(); } },
                { "cloudManagementScore", n => { CloudManagementScore = n.GetDoubleValue(); } },
                { "cloudProvisioningScore", n => { CloudProvisioningScore = n.GetDoubleValue(); } },
                { "compliancePolicySetToIntune", n => { CompliancePolicySetToIntune = n.GetBoolValue(); } },
                { "deviceId", n => { DeviceId = n.GetStringValue(); } },
                { "deviceName", n => { DeviceName = n.GetStringValue(); } },
                { "healthStatus", n => { HealthStatus = n.GetEnumValue<global::AITGraph.Sdk.Models.UserExperienceAnalyticsHealthState>(); } },
                { "isCloudManagedGatewayEnabled", n => { IsCloudManagedGatewayEnabled = n.GetBoolValue(); } },
                { "managedBy", n => { ManagedBy = n.GetStringValue(); } },
                { "manufacturer", n => { Manufacturer = n.GetStringValue(); } },
                { "model", n => { Model = n.GetStringValue(); } },
                { "osCheckFailed", n => { OsCheckFailed = n.GetBoolValue(); } },
                { "osDescription", n => { OsDescription = n.GetStringValue(); } },
                { "osVersion", n => { OsVersion = n.GetStringValue(); } },
                { "otherWorkloadsSetToIntune", n => { OtherWorkloadsSetToIntune = n.GetBoolValue(); } },
                { "ownership", n => { Ownership = n.GetStringValue(); } },
                { "processor64BitCheckFailed", n => { Processor64BitCheckFailed = n.GetBoolValue(); } },
                { "processorCoreCountCheckFailed", n => { ProcessorCoreCountCheckFailed = n.GetBoolValue(); } },
                { "processorFamilyCheckFailed", n => { ProcessorFamilyCheckFailed = n.GetBoolValue(); } },
                { "processorSpeedCheckFailed", n => { ProcessorSpeedCheckFailed = n.GetBoolValue(); } },
                { "ramCheckFailed", n => { RamCheckFailed = n.GetBoolValue(); } },
                { "secureBootCheckFailed", n => { SecureBootCheckFailed = n.GetBoolValue(); } },
                { "serialNumber", n => { SerialNumber = n.GetStringValue(); } },
                { "storageCheckFailed", n => { StorageCheckFailed = n.GetBoolValue(); } },
                { "tenantAttached", n => { TenantAttached = n.GetBoolValue(); } },
                { "tpmCheckFailed", n => { TpmCheckFailed = n.GetBoolValue(); } },
                { "upgradeEligibility", n => { UpgradeEligibility = n.GetEnumValue<global::AITGraph.Sdk.Models.OperatingSystemUpgradeEligibility>(); } },
                { "windowsScore", n => { WindowsScore = n.GetDoubleValue(); } },
                { "workFromAnywhereScore", n => { WorkFromAnywhereScore = n.GetDoubleValue(); } },
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
            writer.WriteBoolValue("autoPilotProfileAssigned", AutoPilotProfileAssigned);
            writer.WriteBoolValue("autoPilotRegistered", AutoPilotRegistered);
            writer.WriteStringValue("azureAdDeviceId", AzureAdDeviceId);
            writer.WriteStringValue("azureAdJoinType", AzureAdJoinType);
            writer.WriteBoolValue("azureAdRegistered", AzureAdRegistered);
            writer.WriteDoubleValue("cloudIdentityScore", CloudIdentityScore);
            writer.WriteDoubleValue("cloudManagementScore", CloudManagementScore);
            writer.WriteDoubleValue("cloudProvisioningScore", CloudProvisioningScore);
            writer.WriteBoolValue("compliancePolicySetToIntune", CompliancePolicySetToIntune);
            writer.WriteStringValue("deviceId", DeviceId);
            writer.WriteStringValue("deviceName", DeviceName);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.UserExperienceAnalyticsHealthState>("healthStatus", HealthStatus);
            writer.WriteBoolValue("isCloudManagedGatewayEnabled", IsCloudManagedGatewayEnabled);
            writer.WriteStringValue("managedBy", ManagedBy);
            writer.WriteStringValue("manufacturer", Manufacturer);
            writer.WriteStringValue("model", Model);
            writer.WriteBoolValue("osCheckFailed", OsCheckFailed);
            writer.WriteStringValue("osDescription", OsDescription);
            writer.WriteStringValue("osVersion", OsVersion);
            writer.WriteBoolValue("otherWorkloadsSetToIntune", OtherWorkloadsSetToIntune);
            writer.WriteStringValue("ownership", Ownership);
            writer.WriteBoolValue("processor64BitCheckFailed", Processor64BitCheckFailed);
            writer.WriteBoolValue("processorCoreCountCheckFailed", ProcessorCoreCountCheckFailed);
            writer.WriteBoolValue("processorFamilyCheckFailed", ProcessorFamilyCheckFailed);
            writer.WriteBoolValue("processorSpeedCheckFailed", ProcessorSpeedCheckFailed);
            writer.WriteBoolValue("ramCheckFailed", RamCheckFailed);
            writer.WriteBoolValue("secureBootCheckFailed", SecureBootCheckFailed);
            writer.WriteStringValue("serialNumber", SerialNumber);
            writer.WriteBoolValue("storageCheckFailed", StorageCheckFailed);
            writer.WriteBoolValue("tenantAttached", TenantAttached);
            writer.WriteBoolValue("tpmCheckFailed", TpmCheckFailed);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.OperatingSystemUpgradeEligibility>("upgradeEligibility", UpgradeEligibility);
            writer.WriteDoubleValue("windowsScore", WindowsScore);
            writer.WriteDoubleValue("workFromAnywhereScore", WorkFromAnywhereScore);
        }
    }
}
#pragma warning restore CS0618
