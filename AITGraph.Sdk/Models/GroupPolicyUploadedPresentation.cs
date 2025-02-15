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
    /// Represents an ADMX checkBox element and an ADMX boolean element.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class GroupPolicyUploadedPresentation : global::AITGraph.Sdk.Models.GroupPolicyPresentation, IParsable
    {
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.GroupPolicyUploadedPresentation"/> and sets the default values.
        /// </summary>
        public GroupPolicyUploadedPresentation() : base()
        {
            OdataType = "#microsoft.graph.groupPolicyUploadedPresentation";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.GroupPolicyUploadedPresentation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.GroupPolicyUploadedPresentation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            var mappingValue = parseNode.GetChildNode("@odata.type")?.GetStringValue();
            return mappingValue switch
            {
                "#microsoft.graph.groupPolicyPresentationCheckBox" => new global::AITGraph.Sdk.Models.GroupPolicyPresentationCheckBox(),
                "#microsoft.graph.groupPolicyPresentationComboBox" => new global::AITGraph.Sdk.Models.GroupPolicyPresentationComboBox(),
                "#microsoft.graph.groupPolicyPresentationDecimalTextBox" => new global::AITGraph.Sdk.Models.GroupPolicyPresentationDecimalTextBox(),
                "#microsoft.graph.groupPolicyPresentationDropdownList" => new global::AITGraph.Sdk.Models.GroupPolicyPresentationDropdownList(),
                "#microsoft.graph.groupPolicyPresentationListBox" => new global::AITGraph.Sdk.Models.GroupPolicyPresentationListBox(),
                "#microsoft.graph.groupPolicyPresentationLongDecimalTextBox" => new global::AITGraph.Sdk.Models.GroupPolicyPresentationLongDecimalTextBox(),
                "#microsoft.graph.groupPolicyPresentationMultiTextBox" => new global::AITGraph.Sdk.Models.GroupPolicyPresentationMultiTextBox(),
                "#microsoft.graph.groupPolicyPresentationText" => new global::AITGraph.Sdk.Models.GroupPolicyPresentationText(),
                "#microsoft.graph.groupPolicyPresentationTextBox" => new global::AITGraph.Sdk.Models.GroupPolicyPresentationTextBox(),
                _ => new global::AITGraph.Sdk.Models.GroupPolicyUploadedPresentation(),
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
        }
    }
}
#pragma warning restore CS0618
