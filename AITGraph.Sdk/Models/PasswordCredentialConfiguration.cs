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
    public partial class PasswordCredentialConfiguration : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The maxLifetime property</summary>
        public TimeSpan? MaxLifetime { get; set; }
        /// <summary>The OdataType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OdataType { get; set; }
#nullable restore
#else
        public string OdataType { get; set; }
#endif
        /// <summary>Enforces the policy for an app created on or after the enforcement date. For existing applications, the enforcement date would be backdated. To apply to all applications, this date would be null.</summary>
        public DateTimeOffset? RestrictForAppsCreatedAfterDateTime { get; set; }
        /// <summary>The type of restriction being applied. The possible values are: passwordAddition, passwordLifetime, symmetricKeyAddition, symmetricKeyLifetime,customPasswordAddition, unknownFutureValue. Each value of restrictionType can be used only once per policy.</summary>
        public global::AITGraph.Sdk.Models.AppCredentialRestrictionType? RestrictionType { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.PasswordCredentialConfiguration"/> and sets the default values.
        /// </summary>
        public PasswordCredentialConfiguration()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.PasswordCredentialConfiguration"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::AITGraph.Sdk.Models.PasswordCredentialConfiguration CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.PasswordCredentialConfiguration();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "maxLifetime", n => { MaxLifetime = n.GetTimeSpanValue(); } },
                { "@odata.type", n => { OdataType = n.GetStringValue(); } },
                { "restrictForAppsCreatedAfterDateTime", n => { RestrictForAppsCreatedAfterDateTime = n.GetDateTimeOffsetValue(); } },
                { "restrictionType", n => { RestrictionType = n.GetEnumValue<global::AITGraph.Sdk.Models.AppCredentialRestrictionType>(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteTimeSpanValue("maxLifetime", MaxLifetime);
            writer.WriteStringValue("@odata.type", OdataType);
            writer.WriteDateTimeOffsetValue("restrictForAppsCreatedAfterDateTime", RestrictForAppsCreatedAfterDateTime);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.AppCredentialRestrictionType>("restrictionType", RestrictionType);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
