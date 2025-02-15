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
    public partial class IncomingContext : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The id of the participant that is under observation. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ObservedParticipantId { get; set; }
#nullable restore
#else
        public string ObservedParticipantId { get; set; }
#endif
        /// <summary>The OdataType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OdataType { get; set; }
#nullable restore
#else
        public string OdataType { get; set; }
#endif
        /// <summary>The identity that the call is happening on behalf of.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.IdentitySet? OnBehalfOf { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.IdentitySet OnBehalfOf { get; set; }
#endif
        /// <summary>The id of the participant that triggered the incoming call. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SourceParticipantId { get; set; }
#nullable restore
#else
        public string SourceParticipantId { get; set; }
#endif
        /// <summary>The identity that transferred the call.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.IdentitySet? Transferor { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.IdentitySet Transferor { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.IncomingContext"/> and sets the default values.
        /// </summary>
        public IncomingContext()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.IncomingContext"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::AITGraph.Sdk.Models.IncomingContext CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.IncomingContext();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "observedParticipantId", n => { ObservedParticipantId = n.GetStringValue(); } },
                { "@odata.type", n => { OdataType = n.GetStringValue(); } },
                { "onBehalfOf", n => { OnBehalfOf = n.GetObjectValue<global::AITGraph.Sdk.Models.IdentitySet>(global::AITGraph.Sdk.Models.IdentitySet.CreateFromDiscriminatorValue); } },
                { "sourceParticipantId", n => { SourceParticipantId = n.GetStringValue(); } },
                { "transferor", n => { Transferor = n.GetObjectValue<global::AITGraph.Sdk.Models.IdentitySet>(global::AITGraph.Sdk.Models.IdentitySet.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("observedParticipantId", ObservedParticipantId);
            writer.WriteStringValue("@odata.type", OdataType);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.IdentitySet>("onBehalfOf", OnBehalfOf);
            writer.WriteStringValue("sourceParticipantId", SourceParticipantId);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.IdentitySet>("transferor", Transferor);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
