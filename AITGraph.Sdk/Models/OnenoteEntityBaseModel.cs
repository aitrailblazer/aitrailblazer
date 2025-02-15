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
    public partial class OnenoteEntityBaseModel : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The self property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Self { get; set; }
#nullable restore
#else
        public string Self { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.OnenoteEntityBaseModel"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.OnenoteEntityBaseModel CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            var mappingValue = parseNode.GetChildNode("@odata.type")?.GetStringValue();
            return mappingValue switch
            {
                "#microsoft.graph.notebook" => new global::AITGraph.Sdk.Models.Notebook(),
                "#microsoft.graph.onenoteEntityHierarchyModel" => new global::AITGraph.Sdk.Models.OnenoteEntityHierarchyModel(),
                "#microsoft.graph.onenoteEntitySchemaObjectModel" => new global::AITGraph.Sdk.Models.OnenoteEntitySchemaObjectModel(),
                "#microsoft.graph.onenotePage" => new global::AITGraph.Sdk.Models.OnenotePage(),
                "#microsoft.graph.onenoteResource" => new global::AITGraph.Sdk.Models.OnenoteResource(),
                "#microsoft.graph.onenoteSection" => new global::AITGraph.Sdk.Models.OnenoteSection(),
                "#microsoft.graph.sectionGroup" => new global::AITGraph.Sdk.Models.SectionGroup(),
                _ => new global::AITGraph.Sdk.Models.OnenoteEntityBaseModel(),
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
                { "self", n => { Self = n.GetStringValue(); } },
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
            writer.WriteStringValue("self", Self);
        }
    }
}
#pragma warning restore CS0618
