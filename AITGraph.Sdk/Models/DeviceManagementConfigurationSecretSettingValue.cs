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
    /// Graph model for a secret setting value
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class DeviceManagementConfigurationSecretSettingValue : global::AITGraph.Sdk.Models.DeviceManagementConfigurationSimpleSettingValue, IParsable
    {
        /// <summary>Value of the secret setting.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Value { get; set; }
#nullable restore
#else
        public string Value { get; set; }
#endif
        /// <summary>type tracking the encryption state of a secret setting value</summary>
        public global::AITGraph.Sdk.Models.DeviceManagementConfigurationSecretSettingValueState? ValueState { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.DeviceManagementConfigurationSecretSettingValue"/> and sets the default values.
        /// </summary>
        public DeviceManagementConfigurationSecretSettingValue() : base()
        {
            OdataType = "#microsoft.graph.deviceManagementConfigurationSecretSettingValue";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.DeviceManagementConfigurationSecretSettingValue"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.DeviceManagementConfigurationSecretSettingValue CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.DeviceManagementConfigurationSecretSettingValue();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "value", n => { Value = n.GetStringValue(); } },
                { "valueState", n => { ValueState = n.GetEnumValue<global::AITGraph.Sdk.Models.DeviceManagementConfigurationSecretSettingValueState>(); } },
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
            writer.WriteStringValue("value", Value);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.DeviceManagementConfigurationSecretSettingValueState>("valueState", ValueState);
        }
    }
}
#pragma warning restore CS0618
