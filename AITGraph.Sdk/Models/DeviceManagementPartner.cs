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
    /// Entity which represents a connection to device management partner.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class DeviceManagementPartner : global::AITGraph.Sdk.Models.Entity, IParsable
    {
        /// <summary>Partner display name</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DisplayName { get; set; }
#nullable restore
#else
        public string DisplayName { get; set; }
#endif
        /// <summary>User groups that specifies whether enrollment is through partner.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.DeviceManagementPartnerAssignment>? GroupsRequiringPartnerEnrollment { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.DeviceManagementPartnerAssignment> GroupsRequiringPartnerEnrollment { get; set; }
#endif
        /// <summary>Whether device management partner is configured or not</summary>
        public bool? IsConfigured { get; set; }
        /// <summary>Timestamp of last heartbeat after admin enabled option Connect to Device management Partner</summary>
        public DateTimeOffset? LastHeartbeatDateTime { get; set; }
        /// <summary>Partner App Type.</summary>
        public global::AITGraph.Sdk.Models.DeviceManagementPartnerAppType? PartnerAppType { get; set; }
        /// <summary>Partner state of this tenant.</summary>
        public global::AITGraph.Sdk.Models.DeviceManagementPartnerTenantState? PartnerState { get; set; }
        /// <summary>Partner Single tenant App id</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SingleTenantAppId { get; set; }
#nullable restore
#else
        public string SingleTenantAppId { get; set; }
#endif
        /// <summary>DateTime in UTC when PartnerDevices will be marked as NonCompliant</summary>
        public DateTimeOffset? WhenPartnerDevicesWillBeMarkedAsNonCompliantDateTime { get; set; }
        /// <summary>DateTime in UTC when PartnerDevices will be removed</summary>
        public DateTimeOffset? WhenPartnerDevicesWillBeRemovedDateTime { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.DeviceManagementPartner"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.DeviceManagementPartner CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.DeviceManagementPartner();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "displayName", n => { DisplayName = n.GetStringValue(); } },
                { "groupsRequiringPartnerEnrollment", n => { GroupsRequiringPartnerEnrollment = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.DeviceManagementPartnerAssignment>(global::AITGraph.Sdk.Models.DeviceManagementPartnerAssignment.CreateFromDiscriminatorValue)?.AsList(); } },
                { "isConfigured", n => { IsConfigured = n.GetBoolValue(); } },
                { "lastHeartbeatDateTime", n => { LastHeartbeatDateTime = n.GetDateTimeOffsetValue(); } },
                { "partnerAppType", n => { PartnerAppType = n.GetEnumValue<global::AITGraph.Sdk.Models.DeviceManagementPartnerAppType>(); } },
                { "partnerState", n => { PartnerState = n.GetEnumValue<global::AITGraph.Sdk.Models.DeviceManagementPartnerTenantState>(); } },
                { "singleTenantAppId", n => { SingleTenantAppId = n.GetStringValue(); } },
                { "whenPartnerDevicesWillBeMarkedAsNonCompliantDateTime", n => { WhenPartnerDevicesWillBeMarkedAsNonCompliantDateTime = n.GetDateTimeOffsetValue(); } },
                { "whenPartnerDevicesWillBeRemovedDateTime", n => { WhenPartnerDevicesWillBeRemovedDateTime = n.GetDateTimeOffsetValue(); } },
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
            writer.WriteStringValue("displayName", DisplayName);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.DeviceManagementPartnerAssignment>("groupsRequiringPartnerEnrollment", GroupsRequiringPartnerEnrollment);
            writer.WriteBoolValue("isConfigured", IsConfigured);
            writer.WriteDateTimeOffsetValue("lastHeartbeatDateTime", LastHeartbeatDateTime);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.DeviceManagementPartnerAppType>("partnerAppType", PartnerAppType);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.DeviceManagementPartnerTenantState>("partnerState", PartnerState);
            writer.WriteStringValue("singleTenantAppId", SingleTenantAppId);
            writer.WriteDateTimeOffsetValue("whenPartnerDevicesWillBeMarkedAsNonCompliantDateTime", WhenPartnerDevicesWillBeMarkedAsNonCompliantDateTime);
            writer.WriteDateTimeOffsetValue("whenPartnerDevicesWillBeRemovedDateTime", WhenPartnerDevicesWillBeRemovedDateTime);
        }
    }
}
#pragma warning restore CS0618
