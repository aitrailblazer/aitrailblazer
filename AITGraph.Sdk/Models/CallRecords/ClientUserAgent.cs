// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace AITGraph.Sdk.Models.CallRecords
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class ClientUserAgent : global::AITGraph.Sdk.Models.CallRecords.UserAgent, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The unique identifier of the Azure AD application used by this endpoint.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AzureADAppId { get; set; }
#nullable restore
#else
        public string AzureADAppId { get; set; }
#endif
        /// <summary>Immutable resource identifier of the Azure Communication Service associated with this endpoint based on Communication Services APIs.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CommunicationServiceId { get; set; }
#nullable restore
#else
        public string CommunicationServiceId { get; set; }
#endif
        /// <summary>The platform property</summary>
        public global::AITGraph.Sdk.Models.CallRecords.ClientPlatform? Platform { get; set; }
        /// <summary>The productFamily property</summary>
        public global::AITGraph.Sdk.Models.CallRecords.ProductFamily? ProductFamily { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.CallRecords.ClientUserAgent"/> and sets the default values.
        /// </summary>
        public ClientUserAgent() : base()
        {
            OdataType = "#microsoft.graph.callRecords.clientUserAgent";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.CallRecords.ClientUserAgent"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.CallRecords.ClientUserAgent CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.CallRecords.ClientUserAgent();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "azureADAppId", n => { AzureADAppId = n.GetStringValue(); } },
                { "communicationServiceId", n => { CommunicationServiceId = n.GetStringValue(); } },
                { "platform", n => { Platform = n.GetEnumValue<global::AITGraph.Sdk.Models.CallRecords.ClientPlatform>(); } },
                { "productFamily", n => { ProductFamily = n.GetEnumValue<global::AITGraph.Sdk.Models.CallRecords.ProductFamily>(); } },
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
            writer.WriteStringValue("azureADAppId", AzureADAppId);
            writer.WriteStringValue("communicationServiceId", CommunicationServiceId);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.CallRecords.ClientPlatform>("platform", Platform);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.CallRecords.ProductFamily>("productFamily", ProductFamily);
        }
    }
}
#pragma warning restore CS0618
