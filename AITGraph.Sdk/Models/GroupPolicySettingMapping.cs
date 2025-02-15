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
    /// The Group Policy setting to MDM/Intune mapping.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class GroupPolicySettingMapping : global::AITGraph.Sdk.Models.Entity, IParsable
    {
        /// <summary>Admx Group Policy Id</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AdmxSettingDefinitionId { get; set; }
#nullable restore
#else
        public string AdmxSettingDefinitionId { get; set; }
#endif
        /// <summary>List of Child Ids of the group policy setting.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? ChildIdList { get; set; }
#nullable restore
#else
        public List<string> ChildIdList { get; set; }
#endif
        /// <summary>The Intune Setting Definition Id</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IntuneSettingDefinitionId { get; set; }
#nullable restore
#else
        public string IntuneSettingDefinitionId { get; set; }
#endif
        /// <summary>The list of Intune Setting URIs this group policy setting maps to</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? IntuneSettingUriList { get; set; }
#nullable restore
#else
        public List<string> IntuneSettingUriList { get; set; }
#endif
        /// <summary>Indicates if the setting is supported by Intune or not</summary>
        public bool? IsMdmSupported { get; set; }
        /// <summary>The CSP name this group policy setting maps to.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? MdmCspName { get; set; }
#nullable restore
#else
        public string MdmCspName { get; set; }
#endif
        /// <summary>The minimum OS version this mdm setting supports.</summary>
        public int? MdmMinimumOSVersion { get; set; }
        /// <summary>The MDM CSP URI this group policy setting maps to.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? MdmSettingUri { get; set; }
#nullable restore
#else
        public string MdmSettingUri { get; set; }
#endif
        /// <summary>Mdm Support Status of the setting.</summary>
        public global::AITGraph.Sdk.Models.MdmSupportedState? MdmSupportedState { get; set; }
        /// <summary>Parent Id of the group policy setting.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ParentId { get; set; }
#nullable restore
#else
        public string ParentId { get; set; }
#endif
        /// <summary>The category the group policy setting is in.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SettingCategory { get; set; }
#nullable restore
#else
        public string SettingCategory { get; set; }
#endif
        /// <summary>The display name of this group policy setting.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SettingDisplayName { get; set; }
#nullable restore
#else
        public string SettingDisplayName { get; set; }
#endif
        /// <summary>The display value of this group policy setting.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SettingDisplayValue { get; set; }
#nullable restore
#else
        public string SettingDisplayValue { get; set; }
#endif
        /// <summary>The display value type of this group policy setting.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SettingDisplayValueType { get; set; }
#nullable restore
#else
        public string SettingDisplayValueType { get; set; }
#endif
        /// <summary>The name of this group policy setting.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SettingName { get; set; }
#nullable restore
#else
        public string SettingName { get; set; }
#endif
        /// <summary>Scope of the group policy setting.</summary>
        public global::AITGraph.Sdk.Models.GroupPolicySettingScope? SettingScope { get; set; }
        /// <summary>Setting type of the group policy.</summary>
        public global::AITGraph.Sdk.Models.GroupPolicySettingType? SettingType { get; set; }
        /// <summary>The value of this group policy setting.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SettingValue { get; set; }
#nullable restore
#else
        public string SettingValue { get; set; }
#endif
        /// <summary>The display units of this group policy setting value</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SettingValueDisplayUnits { get; set; }
#nullable restore
#else
        public string SettingValueDisplayUnits { get; set; }
#endif
        /// <summary>The value type of this group policy setting.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SettingValueType { get; set; }
#nullable restore
#else
        public string SettingValueType { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.GroupPolicySettingMapping"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.GroupPolicySettingMapping CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.GroupPolicySettingMapping();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "admxSettingDefinitionId", n => { AdmxSettingDefinitionId = n.GetStringValue(); } },
                { "childIdList", n => { ChildIdList = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "intuneSettingDefinitionId", n => { IntuneSettingDefinitionId = n.GetStringValue(); } },
                { "intuneSettingUriList", n => { IntuneSettingUriList = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "isMdmSupported", n => { IsMdmSupported = n.GetBoolValue(); } },
                { "mdmCspName", n => { MdmCspName = n.GetStringValue(); } },
                { "mdmMinimumOSVersion", n => { MdmMinimumOSVersion = n.GetIntValue(); } },
                { "mdmSettingUri", n => { MdmSettingUri = n.GetStringValue(); } },
                { "mdmSupportedState", n => { MdmSupportedState = n.GetEnumValue<global::AITGraph.Sdk.Models.MdmSupportedState>(); } },
                { "parentId", n => { ParentId = n.GetStringValue(); } },
                { "settingCategory", n => { SettingCategory = n.GetStringValue(); } },
                { "settingDisplayName", n => { SettingDisplayName = n.GetStringValue(); } },
                { "settingDisplayValue", n => { SettingDisplayValue = n.GetStringValue(); } },
                { "settingDisplayValueType", n => { SettingDisplayValueType = n.GetStringValue(); } },
                { "settingName", n => { SettingName = n.GetStringValue(); } },
                { "settingScope", n => { SettingScope = n.GetEnumValue<global::AITGraph.Sdk.Models.GroupPolicySettingScope>(); } },
                { "settingType", n => { SettingType = n.GetEnumValue<global::AITGraph.Sdk.Models.GroupPolicySettingType>(); } },
                { "settingValue", n => { SettingValue = n.GetStringValue(); } },
                { "settingValueDisplayUnits", n => { SettingValueDisplayUnits = n.GetStringValue(); } },
                { "settingValueType", n => { SettingValueType = n.GetStringValue(); } },
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
            writer.WriteStringValue("admxSettingDefinitionId", AdmxSettingDefinitionId);
            writer.WriteCollectionOfPrimitiveValues<string>("childIdList", ChildIdList);
            writer.WriteStringValue("intuneSettingDefinitionId", IntuneSettingDefinitionId);
            writer.WriteCollectionOfPrimitiveValues<string>("intuneSettingUriList", IntuneSettingUriList);
            writer.WriteBoolValue("isMdmSupported", IsMdmSupported);
            writer.WriteStringValue("mdmCspName", MdmCspName);
            writer.WriteIntValue("mdmMinimumOSVersion", MdmMinimumOSVersion);
            writer.WriteStringValue("mdmSettingUri", MdmSettingUri);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.MdmSupportedState>("mdmSupportedState", MdmSupportedState);
            writer.WriteStringValue("parentId", ParentId);
            writer.WriteStringValue("settingCategory", SettingCategory);
            writer.WriteStringValue("settingDisplayName", SettingDisplayName);
            writer.WriteStringValue("settingDisplayValue", SettingDisplayValue);
            writer.WriteStringValue("settingDisplayValueType", SettingDisplayValueType);
            writer.WriteStringValue("settingName", SettingName);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.GroupPolicySettingScope>("settingScope", SettingScope);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.GroupPolicySettingType>("settingType", SettingType);
            writer.WriteStringValue("settingValue", SettingValue);
            writer.WriteStringValue("settingValueDisplayUnits", SettingValueDisplayUnits);
            writer.WriteStringValue("settingValueType", SettingValueType);
        }
    }
}
#pragma warning restore CS0618
