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
    /// A class containing the properties used for Windows10EnrollmentCompletionPageConfiguration PolicySetItem.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class Windows10EnrollmentCompletionPageConfigurationPolicySetItem : global::AITGraph.Sdk.Models.PolicySetItem, IParsable
    {
        /// <summary>Priority of the Windows10EnrollmentCompletionPageConfigurationPolicySetItem.</summary>
        public int? Priority { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.Windows10EnrollmentCompletionPageConfigurationPolicySetItem"/> and sets the default values.
        /// </summary>
        public Windows10EnrollmentCompletionPageConfigurationPolicySetItem() : base()
        {
            OdataType = "#microsoft.graph.windows10EnrollmentCompletionPageConfigurationPolicySetItem";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.Windows10EnrollmentCompletionPageConfigurationPolicySetItem"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.Windows10EnrollmentCompletionPageConfigurationPolicySetItem CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.Windows10EnrollmentCompletionPageConfigurationPolicySetItem();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "priority", n => { Priority = n.GetIntValue(); } },
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
            writer.WriteIntValue("priority", Priority);
        }
    }
}
#pragma warning restore CS0618
