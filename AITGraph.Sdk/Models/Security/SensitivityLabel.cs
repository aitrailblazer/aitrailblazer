// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace AITGraph.Sdk.Models.Security
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class SensitivityLabel : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The color that the UI should display for the label, if configured.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Color { get; set; }
#nullable restore
#else
        public string Color { get; set; }
#endif
        /// <summary>Returns the supported content formats for the label.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? ContentFormats { get; set; }
#nullable restore
#else
        public List<string> ContentFormats { get; set; }
#endif
        /// <summary>The admin-defined description for the label.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>Indicates whether the label has protection actions configured.</summary>
        public bool? HasProtection { get; set; }
        /// <summary>Indicates whether the label is active or not. Active labels should be hidden or disabled in the UI.</summary>
        public bool? IsActive { get; set; }
        /// <summary>Indicates whether the label can be applied to content. False if the label is a parent with child labels.</summary>
        public bool? IsAppliable { get; set; }
        /// <summary>The plaintext name of the label.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The parent label associated with a child label. Null if the label has no parent.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.Security.SensitivityLabel? Parent { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.Security.SensitivityLabel Parent { get; set; }
#endif
        /// <summary>The sensitivity value of the label, where lower is less sensitive.</summary>
        public int? Sensitivity { get; set; }
        /// <summary>The tooltip that should be displayed for the label in a UI.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Tooltip { get; set; }
#nullable restore
#else
        public string Tooltip { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.Security.SensitivityLabel"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.Security.SensitivityLabel CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.Security.SensitivityLabel();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "color", n => { Color = n.GetStringValue(); } },
                { "contentFormats", n => { ContentFormats = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "hasProtection", n => { HasProtection = n.GetBoolValue(); } },
                { "isActive", n => { IsActive = n.GetBoolValue(); } },
                { "isAppliable", n => { IsAppliable = n.GetBoolValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "parent", n => { Parent = n.GetObjectValue<global::AITGraph.Sdk.Models.Security.SensitivityLabel>(global::AITGraph.Sdk.Models.Security.SensitivityLabel.CreateFromDiscriminatorValue); } },
                { "sensitivity", n => { Sensitivity = n.GetIntValue(); } },
                { "tooltip", n => { Tooltip = n.GetStringValue(); } },
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
            writer.WriteStringValue("color", Color);
            writer.WriteCollectionOfPrimitiveValues<string>("contentFormats", ContentFormats);
            writer.WriteStringValue("description", Description);
            writer.WriteBoolValue("hasProtection", HasProtection);
            writer.WriteBoolValue("isActive", IsActive);
            writer.WriteBoolValue("isAppliable", IsAppliable);
            writer.WriteStringValue("name", Name);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.Security.SensitivityLabel>("parent", Parent);
            writer.WriteIntValue("sensitivity", Sensitivity);
            writer.WriteStringValue("tooltip", Tooltip);
        }
    }
}
#pragma warning restore CS0618
