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
    public partial class AuthenticationMethodsPolicy : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Represents the settings for each authentication method. Automatically expanded on GET /policies/authenticationMethodsPolicy.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.AuthenticationMethodConfiguration>? AuthenticationMethodConfigurations { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.AuthenticationMethodConfiguration> AuthenticationMethodConfigurations { get; set; }
#endif
        /// <summary>A description of the policy.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>The name of the policy.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DisplayName { get; set; }
#nullable restore
#else
        public string DisplayName { get; set; }
#endif
        /// <summary>The date and time of the last update to the policy.</summary>
        public DateTimeOffset? LastModifiedDateTime { get; set; }
        /// <summary>The state of migration of the authentication methods policy from the legacy multifactor authentication and self-service password reset (SSPR) policies. The possible values are: premigration - means the authentication methods policy is used for authentication only, legacy policies are respected. migrationInProgress - means the authentication methods policy is used for both authenication and SSPR, legacy policies are respected. migrationComplete - means the authentication methods policy is used for authentication and SSPR, legacy policies are ignored. unknownFutureValue - Evolvable enumeration sentinel value. Do not use.</summary>
        public global::AITGraph.Sdk.Models.AuthenticationMethodsPolicyMigrationState? PolicyMigrationState { get; set; }
        /// <summary>The version of the policy in use.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PolicyVersion { get; set; }
#nullable restore
#else
        public string PolicyVersion { get; set; }
#endif
        /// <summary>Days before the user will be asked to reconfirm their method.</summary>
        public int? ReconfirmationInDays { get; set; }
        /// <summary>Enforce registration at sign-in time. This property can be used to remind users to set up targeted authentication methods.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.RegistrationEnforcement? RegistrationEnforcement { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.RegistrationEnforcement RegistrationEnforcement { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.AuthenticationMethodsPolicy"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.AuthenticationMethodsPolicy CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.AuthenticationMethodsPolicy();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "authenticationMethodConfigurations", n => { AuthenticationMethodConfigurations = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.AuthenticationMethodConfiguration>(global::AITGraph.Sdk.Models.AuthenticationMethodConfiguration.CreateFromDiscriminatorValue)?.AsList(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "displayName", n => { DisplayName = n.GetStringValue(); } },
                { "lastModifiedDateTime", n => { LastModifiedDateTime = n.GetDateTimeOffsetValue(); } },
                { "policyMigrationState", n => { PolicyMigrationState = n.GetEnumValue<global::AITGraph.Sdk.Models.AuthenticationMethodsPolicyMigrationState>(); } },
                { "policyVersion", n => { PolicyVersion = n.GetStringValue(); } },
                { "reconfirmationInDays", n => { ReconfirmationInDays = n.GetIntValue(); } },
                { "registrationEnforcement", n => { RegistrationEnforcement = n.GetObjectValue<global::AITGraph.Sdk.Models.RegistrationEnforcement>(global::AITGraph.Sdk.Models.RegistrationEnforcement.CreateFromDiscriminatorValue); } },
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
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.AuthenticationMethodConfiguration>("authenticationMethodConfigurations", AuthenticationMethodConfigurations);
            writer.WriteStringValue("description", Description);
            writer.WriteStringValue("displayName", DisplayName);
            writer.WriteDateTimeOffsetValue("lastModifiedDateTime", LastModifiedDateTime);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.AuthenticationMethodsPolicyMigrationState>("policyMigrationState", PolicyMigrationState);
            writer.WriteStringValue("policyVersion", PolicyVersion);
            writer.WriteIntValue("reconfirmationInDays", ReconfirmationInDays);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.RegistrationEnforcement>("registrationEnforcement", RegistrationEnforcement);
        }
    }
}
#pragma warning restore CS0618
