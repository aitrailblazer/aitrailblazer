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
    public partial class CredentialUserRegistrationDetails : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Represents the authentication method that the user has registered. Possible values are: email, mobilePhone, officePhone,  securityQuestion (only used for self-service password reset), appNotification,  appCode, alternateMobilePhone (supported only in registration),  fido,  appPassword,  unknownFutureValue.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.RegistrationAuthMethod?>? AuthMethods { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.RegistrationAuthMethod?> AuthMethods { get; set; }
#endif
        /// <summary>Indicates whether the user is ready to perform self-service password reset or MFA.</summary>
        public bool? IsCapable { get; set; }
        /// <summary>Indicates whether the user enabled to perform self-service password reset.</summary>
        public bool? IsEnabled { get; set; }
        /// <summary>Indicates whether the user is registered for MFA.</summary>
        public bool? IsMfaRegistered { get; set; }
        /// <summary>Indicates whether the user has registered any authentication methods for self-service password reset.</summary>
        public bool? IsRegistered { get; set; }
        /// <summary>Provides the user name of the corresponding user.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? UserDisplayName { get; set; }
#nullable restore
#else
        public string UserDisplayName { get; set; }
#endif
        /// <summary>Provides the user principal name of the corresponding user.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? UserPrincipalName { get; set; }
#nullable restore
#else
        public string UserPrincipalName { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.CredentialUserRegistrationDetails"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.CredentialUserRegistrationDetails CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.CredentialUserRegistrationDetails();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "authMethods", n => { AuthMethods = n.GetCollectionOfEnumValues<global::AITGraph.Sdk.Models.RegistrationAuthMethod>()?.AsList(); } },
                { "isCapable", n => { IsCapable = n.GetBoolValue(); } },
                { "isEnabled", n => { IsEnabled = n.GetBoolValue(); } },
                { "isMfaRegistered", n => { IsMfaRegistered = n.GetBoolValue(); } },
                { "isRegistered", n => { IsRegistered = n.GetBoolValue(); } },
                { "userDisplayName", n => { UserDisplayName = n.GetStringValue(); } },
                { "userPrincipalName", n => { UserPrincipalName = n.GetStringValue(); } },
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
            writer.WriteCollectionOfEnumValues<global::AITGraph.Sdk.Models.RegistrationAuthMethod>("authMethods", AuthMethods);
            writer.WriteBoolValue("isCapable", IsCapable);
            writer.WriteBoolValue("isEnabled", IsEnabled);
            writer.WriteBoolValue("isMfaRegistered", IsMfaRegistered);
            writer.WriteBoolValue("isRegistered", IsRegistered);
            writer.WriteStringValue("userDisplayName", UserDisplayName);
            writer.WriteStringValue("userPrincipalName", UserPrincipalName);
        }
    }
}
#pragma warning restore CS0618
