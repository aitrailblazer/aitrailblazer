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
    /// Base for Android Work Profile EAS Email profiles
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class AndroidWorkProfileEasEmailProfileBase : global::AITGraph.Sdk.Models.DeviceConfiguration, IParsable
    {
        /// <summary>Exchange Active Sync authentication method.</summary>
        public global::AITGraph.Sdk.Models.EasAuthenticationMethod? AuthenticationMethod { get; set; }
        /// <summary>Possible values for email sync duration.</summary>
        public global::AITGraph.Sdk.Models.EmailSyncDuration? DurationOfEmailToSync { get; set; }
        /// <summary>Possible values for username source or email source.</summary>
        public global::AITGraph.Sdk.Models.UserEmailSource? EmailAddressSource { get; set; }
        /// <summary>Exchange location (URL) that the mail app connects to.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? HostName { get; set; }
#nullable restore
#else
        public string HostName { get; set; }
#endif
        /// <summary>Identity certificate.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.AndroidWorkProfileCertificateProfileBase? IdentityCertificate { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.AndroidWorkProfileCertificateProfileBase IdentityCertificate { get; set; }
#endif
        /// <summary>Indicates whether or not to use SSL.</summary>
        public bool? RequireSsl { get; set; }
        /// <summary>Android username source.</summary>
        public global::AITGraph.Sdk.Models.AndroidUsernameSource? UsernameSource { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.AndroidWorkProfileEasEmailProfileBase"/> and sets the default values.
        /// </summary>
        public AndroidWorkProfileEasEmailProfileBase() : base()
        {
            OdataType = "#microsoft.graph.androidWorkProfileEasEmailProfileBase";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.AndroidWorkProfileEasEmailProfileBase"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.AndroidWorkProfileEasEmailProfileBase CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            var mappingValue = parseNode.GetChildNode("@odata.type")?.GetStringValue();
            return mappingValue switch
            {
                "#microsoft.graph.androidWorkProfileGmailEasConfiguration" => new global::AITGraph.Sdk.Models.AndroidWorkProfileGmailEasConfiguration(),
                "#microsoft.graph.androidWorkProfileNineWorkEasConfiguration" => new global::AITGraph.Sdk.Models.AndroidWorkProfileNineWorkEasConfiguration(),
                _ => new global::AITGraph.Sdk.Models.AndroidWorkProfileEasEmailProfileBase(),
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
                { "authenticationMethod", n => { AuthenticationMethod = n.GetEnumValue<global::AITGraph.Sdk.Models.EasAuthenticationMethod>(); } },
                { "durationOfEmailToSync", n => { DurationOfEmailToSync = n.GetEnumValue<global::AITGraph.Sdk.Models.EmailSyncDuration>(); } },
                { "emailAddressSource", n => { EmailAddressSource = n.GetEnumValue<global::AITGraph.Sdk.Models.UserEmailSource>(); } },
                { "hostName", n => { HostName = n.GetStringValue(); } },
                { "identityCertificate", n => { IdentityCertificate = n.GetObjectValue<global::AITGraph.Sdk.Models.AndroidWorkProfileCertificateProfileBase>(global::AITGraph.Sdk.Models.AndroidWorkProfileCertificateProfileBase.CreateFromDiscriminatorValue); } },
                { "requireSsl", n => { RequireSsl = n.GetBoolValue(); } },
                { "usernameSource", n => { UsernameSource = n.GetEnumValue<global::AITGraph.Sdk.Models.AndroidUsernameSource>(); } },
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
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.EasAuthenticationMethod>("authenticationMethod", AuthenticationMethod);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.EmailSyncDuration>("durationOfEmailToSync", DurationOfEmailToSync);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.UserEmailSource>("emailAddressSource", EmailAddressSource);
            writer.WriteStringValue("hostName", HostName);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.AndroidWorkProfileCertificateProfileBase>("identityCertificate", IdentityCertificate);
            writer.WriteBoolValue("requireSsl", RequireSsl);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.AndroidUsernameSource>("usernameSource", UsernameSource);
        }
    }
}
#pragma warning restore CS0618
