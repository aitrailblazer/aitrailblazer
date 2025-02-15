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
    public partial class AccessPackageQuestion : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>ID of the question.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>Specifies whether the requestor is allowed to edit answers to questions.</summary>
        public bool? IsAnswerEditable { get; set; }
        /// <summary>Whether the requestor is required to supply an answer or not.</summary>
        public bool? IsRequired { get; set; }
        /// <summary>The OdataType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OdataType { get; set; }
#nullable restore
#else
        public string OdataType { get; set; }
#endif
        /// <summary>Relative position of this question when displaying a list of questions to the requestor.</summary>
        public int? Sequence { get; set; }
        /// <summary>The text of the question to show to the requestor.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.AccessPackageLocalizedContent? Text { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.AccessPackageLocalizedContent Text { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.AccessPackageQuestion"/> and sets the default values.
        /// </summary>
        public AccessPackageQuestion()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.AccessPackageQuestion"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::AITGraph.Sdk.Models.AccessPackageQuestion CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            var mappingValue = parseNode.GetChildNode("@odata.type")?.GetStringValue();
            return mappingValue switch
            {
                "#microsoft.graph.accessPackageMultipleChoiceQuestion" => new global::AITGraph.Sdk.Models.AccessPackageMultipleChoiceQuestion(),
                "#microsoft.graph.accessPackageTextInputQuestion" => new global::AITGraph.Sdk.Models.AccessPackageTextInputQuestion(),
                _ => new global::AITGraph.Sdk.Models.AccessPackageQuestion(),
            };
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "id", n => { Id = n.GetStringValue(); } },
                { "isAnswerEditable", n => { IsAnswerEditable = n.GetBoolValue(); } },
                { "isRequired", n => { IsRequired = n.GetBoolValue(); } },
                { "@odata.type", n => { OdataType = n.GetStringValue(); } },
                { "sequence", n => { Sequence = n.GetIntValue(); } },
                { "text", n => { Text = n.GetObjectValue<global::AITGraph.Sdk.Models.AccessPackageLocalizedContent>(global::AITGraph.Sdk.Models.AccessPackageLocalizedContent.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("id", Id);
            writer.WriteBoolValue("isAnswerEditable", IsAnswerEditable);
            writer.WriteBoolValue("isRequired", IsRequired);
            writer.WriteStringValue("@odata.type", OdataType);
            writer.WriteIntValue("sequence", Sequence);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.AccessPackageLocalizedContent>("text", Text);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
