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
    /// String constraints
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class DeviceManagementConfigurationStringSettingValueDefinition : global::AITGraph.Sdk.Models.DeviceManagementConfigurationSettingValueDefinition, IParsable
    {
        /// <summary>Supported file types for this setting.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? FileTypes { get; set; }
#nullable restore
#else
        public List<string> FileTypes { get; set; }
#endif
        /// <summary>The format property</summary>
        public global::AITGraph.Sdk.Models.DeviceManagementConfigurationStringFormat? Format { get; set; }
        /// <summary>Regular expression or any xml or json schema that the input string should match</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? InputValidationSchema { get; set; }
#nullable restore
#else
        public string InputValidationSchema { get; set; }
#endif
        /// <summary>Specifies whether the setting needs to be treated as a secret. Settings marked as yes will be encrypted in transit and at rest and will be displayed as asterisks when represented in the UX.</summary>
        public bool? IsSecret { get; set; }
        /// <summary>Maximum length of string</summary>
        public long? MaximumLength { get; set; }
        /// <summary>Minimum length of string</summary>
        public long? MinimumLength { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.DeviceManagementConfigurationStringSettingValueDefinition"/> and sets the default values.
        /// </summary>
        public DeviceManagementConfigurationStringSettingValueDefinition() : base()
        {
            OdataType = "#microsoft.graph.deviceManagementConfigurationStringSettingValueDefinition";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.DeviceManagementConfigurationStringSettingValueDefinition"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.DeviceManagementConfigurationStringSettingValueDefinition CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.DeviceManagementConfigurationStringSettingValueDefinition();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "fileTypes", n => { FileTypes = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "format", n => { Format = n.GetEnumValue<global::AITGraph.Sdk.Models.DeviceManagementConfigurationStringFormat>(); } },
                { "inputValidationSchema", n => { InputValidationSchema = n.GetStringValue(); } },
                { "isSecret", n => { IsSecret = n.GetBoolValue(); } },
                { "maximumLength", n => { MaximumLength = n.GetLongValue(); } },
                { "minimumLength", n => { MinimumLength = n.GetLongValue(); } },
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
            writer.WriteCollectionOfPrimitiveValues<string>("fileTypes", FileTypes);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.DeviceManagementConfigurationStringFormat>("format", Format);
            writer.WriteStringValue("inputValidationSchema", InputValidationSchema);
            writer.WriteBoolValue("isSecret", IsSecret);
            writer.WriteLongValue("maximumLength", MaximumLength);
            writer.WriteLongValue("minimumLength", MinimumLength);
        }
    }
}
#pragma warning restore CS0618
