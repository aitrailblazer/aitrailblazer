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
    public partial class Fido2KeyRestrictions : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>A collection of Authenticator Attestation GUIDs. AADGUIDs define key types and manufacturers.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? AaGuids { get; set; }
#nullable restore
#else
        public List<string> AaGuids { get; set; }
#endif
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Enforcement type. Possible values are: allow, block.</summary>
        public global::AITGraph.Sdk.Models.Fido2RestrictionEnforcementType? EnforcementType { get; set; }
        /// <summary>Determines if the configured key enforcement is enabled.</summary>
        public bool? IsEnforced { get; set; }
        /// <summary>The OdataType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OdataType { get; set; }
#nullable restore
#else
        public string OdataType { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.Fido2KeyRestrictions"/> and sets the default values.
        /// </summary>
        public Fido2KeyRestrictions()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.Fido2KeyRestrictions"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::AITGraph.Sdk.Models.Fido2KeyRestrictions CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.Fido2KeyRestrictions();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "aaGuids", n => { AaGuids = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "enforcementType", n => { EnforcementType = n.GetEnumValue<global::AITGraph.Sdk.Models.Fido2RestrictionEnforcementType>(); } },
                { "isEnforced", n => { IsEnforced = n.GetBoolValue(); } },
                { "@odata.type", n => { OdataType = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfPrimitiveValues<string>("aaGuids", AaGuids);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.Fido2RestrictionEnforcementType>("enforcementType", EnforcementType);
            writer.WriteBoolValue("isEnforced", IsEnforced);
            writer.WriteStringValue("@odata.type", OdataType);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
