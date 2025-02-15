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
    public partial class RiskyServicePrincipalHistoryItem : global::AITGraph.Sdk.Models.RiskyServicePrincipal, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The activity related to service principal risk level change.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.RiskServicePrincipalActivity? Activity { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.RiskServicePrincipalActivity Activity { get; set; }
#endif
        /// <summary>The identifier of the actor of the operation.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? InitiatedBy { get; set; }
#nullable restore
#else
        public string InitiatedBy { get; set; }
#endif
        /// <summary>The identifier of the service principal.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ServicePrincipalId { get; set; }
#nullable restore
#else
        public string ServicePrincipalId { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.RiskyServicePrincipalHistoryItem"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.RiskyServicePrincipalHistoryItem CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.RiskyServicePrincipalHistoryItem();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "activity", n => { Activity = n.GetObjectValue<global::AITGraph.Sdk.Models.RiskServicePrincipalActivity>(global::AITGraph.Sdk.Models.RiskServicePrincipalActivity.CreateFromDiscriminatorValue); } },
                { "initiatedBy", n => { InitiatedBy = n.GetStringValue(); } },
                { "servicePrincipalId", n => { ServicePrincipalId = n.GetStringValue(); } },
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
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.RiskServicePrincipalActivity>("activity", Activity);
            writer.WriteStringValue("initiatedBy", InitiatedBy);
            writer.WriteStringValue("servicePrincipalId", ServicePrincipalId);
        }
    }
}
#pragma warning restore CS0618
