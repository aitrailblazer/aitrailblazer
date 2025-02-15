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
    /// Entity representing a setting category
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class DeviceManagementSettingCategory : global::AITGraph.Sdk.Models.Entity, IParsable
    {
        /// <summary>The category name</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DisplayName { get; set; }
#nullable restore
#else
        public string DisplayName { get; set; }
#endif
        /// <summary>The category contains top level required setting</summary>
        public bool? HasRequiredSetting { get; set; }
        /// <summary>The setting definitions this category contains</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.DeviceManagementSettingDefinition>? SettingDefinitions { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.DeviceManagementSettingDefinition> SettingDefinitions { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.DeviceManagementSettingCategory"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.DeviceManagementSettingCategory CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            var mappingValue = parseNode.GetChildNode("@odata.type")?.GetStringValue();
            return mappingValue switch
            {
                "#microsoft.graph.deviceManagementIntentSettingCategory" => new global::AITGraph.Sdk.Models.DeviceManagementIntentSettingCategory(),
                "#microsoft.graph.deviceManagementTemplateSettingCategory" => new global::AITGraph.Sdk.Models.DeviceManagementTemplateSettingCategory(),
                _ => new global::AITGraph.Sdk.Models.DeviceManagementSettingCategory(),
            };
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
                { "hasRequiredSetting", n => { HasRequiredSetting = n.GetBoolValue(); } },
                { "settingDefinitions", n => { SettingDefinitions = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.DeviceManagementSettingDefinition>(global::AITGraph.Sdk.Models.DeviceManagementSettingDefinition.CreateFromDiscriminatorValue)?.AsList(); } },
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
            writer.WriteBoolValue("hasRequiredSetting", HasRequiredSetting);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.DeviceManagementSettingDefinition>("settingDefinitions", SettingDefinitions);
        }
    }
}
#pragma warning restore CS0618
