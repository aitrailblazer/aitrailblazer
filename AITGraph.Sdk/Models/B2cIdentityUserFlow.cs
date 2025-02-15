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
    public partial class B2cIdentityUserFlow : global::AITGraph.Sdk.Models.IdentityUserFlow, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Configuration for enabling an API connector for use as part of the user flow. You can only obtain the value of this object using Get userFlowApiConnectorConfiguration.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.UserFlowApiConnectorConfiguration? ApiConnectorConfiguration { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.UserFlowApiConnectorConfiguration ApiConnectorConfiguration { get; set; }
#endif
        /// <summary>Indicates the default language of the b2cIdentityUserFlow that is used when no ui_locale tag is specified in the request. This field is RFC 5646 compliant.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DefaultLanguageTag { get; set; }
#nullable restore
#else
        public string DefaultLanguageTag { get; set; }
#endif
        /// <summary>The identityProviders property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.IdentityProvider>? IdentityProviders { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.IdentityProvider> IdentityProviders { get; set; }
#endif
        /// <summary>The property that determines whether language customization is enabled within the B2C user flow. Language customization is not enabled by default for B2C user flows.</summary>
        public bool? IsLanguageCustomizationEnabled { get; set; }
        /// <summary>The languages supported for customization within the user flow. Language customization is not enabled by default in B2C user flows.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.UserFlowLanguageConfiguration>? Languages { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.UserFlowLanguageConfiguration> Languages { get; set; }
#endif
        /// <summary>The user attribute assignments included in the user flow.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.IdentityUserFlowAttributeAssignment>? UserAttributeAssignments { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.IdentityUserFlowAttributeAssignment> UserAttributeAssignments { get; set; }
#endif
        /// <summary>The userFlowIdentityProviders property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.IdentityProviderBase>? UserFlowIdentityProviders { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.IdentityProviderBase> UserFlowIdentityProviders { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.B2cIdentityUserFlow"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.B2cIdentityUserFlow CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.B2cIdentityUserFlow();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "apiConnectorConfiguration", n => { ApiConnectorConfiguration = n.GetObjectValue<global::AITGraph.Sdk.Models.UserFlowApiConnectorConfiguration>(global::AITGraph.Sdk.Models.UserFlowApiConnectorConfiguration.CreateFromDiscriminatorValue); } },
                { "defaultLanguageTag", n => { DefaultLanguageTag = n.GetStringValue(); } },
                { "identityProviders", n => { IdentityProviders = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.IdentityProvider>(global::AITGraph.Sdk.Models.IdentityProvider.CreateFromDiscriminatorValue)?.AsList(); } },
                { "isLanguageCustomizationEnabled", n => { IsLanguageCustomizationEnabled = n.GetBoolValue(); } },
                { "languages", n => { Languages = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.UserFlowLanguageConfiguration>(global::AITGraph.Sdk.Models.UserFlowLanguageConfiguration.CreateFromDiscriminatorValue)?.AsList(); } },
                { "userAttributeAssignments", n => { UserAttributeAssignments = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.IdentityUserFlowAttributeAssignment>(global::AITGraph.Sdk.Models.IdentityUserFlowAttributeAssignment.CreateFromDiscriminatorValue)?.AsList(); } },
                { "userFlowIdentityProviders", n => { UserFlowIdentityProviders = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.IdentityProviderBase>(global::AITGraph.Sdk.Models.IdentityProviderBase.CreateFromDiscriminatorValue)?.AsList(); } },
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
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.UserFlowApiConnectorConfiguration>("apiConnectorConfiguration", ApiConnectorConfiguration);
            writer.WriteStringValue("defaultLanguageTag", DefaultLanguageTag);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.IdentityProvider>("identityProviders", IdentityProviders);
            writer.WriteBoolValue("isLanguageCustomizationEnabled", IsLanguageCustomizationEnabled);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.UserFlowLanguageConfiguration>("languages", Languages);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.IdentityUserFlowAttributeAssignment>("userAttributeAssignments", UserAttributeAssignments);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.IdentityProviderBase>("userFlowIdentityProviders", UserFlowIdentityProviders);
        }
    }
}
#pragma warning restore CS0618
