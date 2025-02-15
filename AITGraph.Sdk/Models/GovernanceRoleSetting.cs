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
    public partial class GovernanceRoleSetting : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The rule settings that are evaluated when an administrator tries to add an eligible role assignment.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.GovernanceRuleSetting>? AdminEligibleSettings { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.GovernanceRuleSetting> AdminEligibleSettings { get; set; }
#endif
        /// <summary>The rule settings that are evaluated when an administrator tries to add a direct member role assignment.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.GovernanceRuleSetting>? AdminMemberSettings { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.GovernanceRuleSetting> AdminMemberSettings { get; set; }
#endif
        /// <summary>Read-only. Indicate if the roleSetting is a default roleSetting</summary>
        public bool? IsDefault { get; set; }
        /// <summary>Read-only. The display name of the administrator who last updated the roleSetting.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? LastUpdatedBy { get; set; }
#nullable restore
#else
        public string LastUpdatedBy { get; set; }
#endif
        /// <summary>Read-only. The time when the role setting was last updated. The Timestamp type represents date and time information using ISO 8601 format and is always in UTC time. For example, midnight UTC on Jan 1, 2014 is 2014-01-01T00:00:00Z</summary>
        public DateTimeOffset? LastUpdatedDateTime { get; set; }
        /// <summary>Read-only. The associated resource for this role setting.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.GovernanceResource? Resource { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.GovernanceResource Resource { get; set; }
#endif
        /// <summary>Required. The id of the resource that the role setting is associated with.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ResourceId { get; set; }
#nullable restore
#else
        public string ResourceId { get; set; }
#endif
        /// <summary>Read-only. The role definition that is enforced with this role setting.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.GovernanceRoleDefinition? RoleDefinition { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.GovernanceRoleDefinition RoleDefinition { get; set; }
#endif
        /// <summary>Required. The id of the role definition that the role setting is associated with.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? RoleDefinitionId { get; set; }
#nullable restore
#else
        public string RoleDefinitionId { get; set; }
#endif
        /// <summary>The rule settings that are evaluated when a user tries to add an eligible role assignment. The setting is not supported for now.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.GovernanceRuleSetting>? UserEligibleSettings { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.GovernanceRuleSetting> UserEligibleSettings { get; set; }
#endif
        /// <summary>The rule settings that are evaluated when a user tries to activate his role assignment.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.GovernanceRuleSetting>? UserMemberSettings { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.GovernanceRuleSetting> UserMemberSettings { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.GovernanceRoleSetting"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.GovernanceRoleSetting CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.GovernanceRoleSetting();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "adminEligibleSettings", n => { AdminEligibleSettings = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.GovernanceRuleSetting>(global::AITGraph.Sdk.Models.GovernanceRuleSetting.CreateFromDiscriminatorValue)?.AsList(); } },
                { "adminMemberSettings", n => { AdminMemberSettings = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.GovernanceRuleSetting>(global::AITGraph.Sdk.Models.GovernanceRuleSetting.CreateFromDiscriminatorValue)?.AsList(); } },
                { "isDefault", n => { IsDefault = n.GetBoolValue(); } },
                { "lastUpdatedBy", n => { LastUpdatedBy = n.GetStringValue(); } },
                { "lastUpdatedDateTime", n => { LastUpdatedDateTime = n.GetDateTimeOffsetValue(); } },
                { "resource", n => { Resource = n.GetObjectValue<global::AITGraph.Sdk.Models.GovernanceResource>(global::AITGraph.Sdk.Models.GovernanceResource.CreateFromDiscriminatorValue); } },
                { "resourceId", n => { ResourceId = n.GetStringValue(); } },
                { "roleDefinition", n => { RoleDefinition = n.GetObjectValue<global::AITGraph.Sdk.Models.GovernanceRoleDefinition>(global::AITGraph.Sdk.Models.GovernanceRoleDefinition.CreateFromDiscriminatorValue); } },
                { "roleDefinitionId", n => { RoleDefinitionId = n.GetStringValue(); } },
                { "userEligibleSettings", n => { UserEligibleSettings = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.GovernanceRuleSetting>(global::AITGraph.Sdk.Models.GovernanceRuleSetting.CreateFromDiscriminatorValue)?.AsList(); } },
                { "userMemberSettings", n => { UserMemberSettings = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.GovernanceRuleSetting>(global::AITGraph.Sdk.Models.GovernanceRuleSetting.CreateFromDiscriminatorValue)?.AsList(); } },
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
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.GovernanceRuleSetting>("adminEligibleSettings", AdminEligibleSettings);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.GovernanceRuleSetting>("adminMemberSettings", AdminMemberSettings);
            writer.WriteBoolValue("isDefault", IsDefault);
            writer.WriteStringValue("lastUpdatedBy", LastUpdatedBy);
            writer.WriteDateTimeOffsetValue("lastUpdatedDateTime", LastUpdatedDateTime);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.GovernanceResource>("resource", Resource);
            writer.WriteStringValue("resourceId", ResourceId);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.GovernanceRoleDefinition>("roleDefinition", RoleDefinition);
            writer.WriteStringValue("roleDefinitionId", RoleDefinitionId);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.GovernanceRuleSetting>("userEligibleSettings", UserEligibleSettings);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.GovernanceRuleSetting>("userMemberSettings", UserMemberSettings);
        }
    }
}
#pragma warning restore CS0618
