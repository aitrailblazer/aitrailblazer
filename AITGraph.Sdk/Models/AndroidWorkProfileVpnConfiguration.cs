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
    /// By providing the configurations in this profile you can instruct the Android Work Profile device to connect to desired VPN endpoint. By specifying the authentication method and security types expected by VPN endpoint you can make the VPN connection seamless for end user.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class AndroidWorkProfileVpnConfiguration : global::AITGraph.Sdk.Models.DeviceConfiguration, IParsable
    {
        /// <summary>Whether or not to enable always-on VPN connection.</summary>
        public bool? AlwaysOn { get; set; }
        /// <summary>If always-on VPN connection is enabled, whether or not to lock network traffic when that VPN is disconnected.</summary>
        public bool? AlwaysOnLockdown { get; set; }
        /// <summary>VPN Authentication Method.</summary>
        public global::AITGraph.Sdk.Models.VpnAuthenticationMethod? AuthenticationMethod { get; set; }
        /// <summary>Connection name displayed to the user.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ConnectionName { get; set; }
#nullable restore
#else
        public string ConnectionName { get; set; }
#endif
        /// <summary>Android Work Profile VPN connection type.</summary>
        public global::AITGraph.Sdk.Models.AndroidWorkProfileVpnConnectionType? ConnectionType { get; set; }
        /// <summary>Custom data when connection type is set to Citrix. This collection can contain a maximum of 25 elements.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.KeyValue>? CustomData { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.KeyValue> CustomData { get; set; }
#endif
        /// <summary>Custom data when connection type is set to Citrix. This collection can contain a maximum of 25 elements.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.KeyValuePair>? CustomKeyValueData { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.KeyValuePair> CustomKeyValueData { get; set; }
#endif
        /// <summary>Fingerprint is a string that will be used to verify the VPN server can be trusted, which is only applicable when connection type is Check Point Capsule VPN.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Fingerprint { get; set; }
#nullable restore
#else
        public string Fingerprint { get; set; }
#endif
        /// <summary>Identity certificate for client authentication when authentication method is certificate.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.AndroidWorkProfileCertificateProfileBase? IdentityCertificate { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.AndroidWorkProfileCertificateProfileBase IdentityCertificate { get; set; }
#endif
        /// <summary>Microsoft Tunnel site ID.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? MicrosoftTunnelSiteId { get; set; }
#nullable restore
#else
        public string MicrosoftTunnelSiteId { get; set; }
#endif
        /// <summary>Proxy server.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.VpnProxyServer? ProxyServer { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.VpnProxyServer ProxyServer { get; set; }
#endif
        /// <summary>Realm when connection type is set to Pulse Secure.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Realm { get; set; }
#nullable restore
#else
        public string Realm { get; set; }
#endif
        /// <summary>Role when connection type is set to Pulse Secure.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Role { get; set; }
#nullable restore
#else
        public string Role { get; set; }
#endif
        /// <summary>List of VPN Servers on the network. Make sure end users can access these network locations. This collection can contain a maximum of 500 elements.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.VpnServer>? Servers { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.VpnServer> Servers { get; set; }
#endif
        /// <summary>Targeted mobile apps. This collection can contain a maximum of 500 elements.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.AppListItem>? TargetedMobileApps { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.AppListItem> TargetedMobileApps { get; set; }
#endif
        /// <summary>Targeted App package IDs.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? TargetedPackageIds { get; set; }
#nullable restore
#else
        public List<string> TargetedPackageIds { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.AndroidWorkProfileVpnConfiguration"/> and sets the default values.
        /// </summary>
        public AndroidWorkProfileVpnConfiguration() : base()
        {
            OdataType = "#microsoft.graph.androidWorkProfileVpnConfiguration";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.AndroidWorkProfileVpnConfiguration"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.AndroidWorkProfileVpnConfiguration CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.AndroidWorkProfileVpnConfiguration();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "alwaysOn", n => { AlwaysOn = n.GetBoolValue(); } },
                { "alwaysOnLockdown", n => { AlwaysOnLockdown = n.GetBoolValue(); } },
                { "authenticationMethod", n => { AuthenticationMethod = n.GetEnumValue<global::AITGraph.Sdk.Models.VpnAuthenticationMethod>(); } },
                { "connectionName", n => { ConnectionName = n.GetStringValue(); } },
                { "connectionType", n => { ConnectionType = n.GetEnumValue<global::AITGraph.Sdk.Models.AndroidWorkProfileVpnConnectionType>(); } },
                { "customData", n => { CustomData = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.KeyValue>(global::AITGraph.Sdk.Models.KeyValue.CreateFromDiscriminatorValue)?.AsList(); } },
                { "customKeyValueData", n => { CustomKeyValueData = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.KeyValuePair>(global::AITGraph.Sdk.Models.KeyValuePair.CreateFromDiscriminatorValue)?.AsList(); } },
                { "fingerprint", n => { Fingerprint = n.GetStringValue(); } },
                { "identityCertificate", n => { IdentityCertificate = n.GetObjectValue<global::AITGraph.Sdk.Models.AndroidWorkProfileCertificateProfileBase>(global::AITGraph.Sdk.Models.AndroidWorkProfileCertificateProfileBase.CreateFromDiscriminatorValue); } },
                { "microsoftTunnelSiteId", n => { MicrosoftTunnelSiteId = n.GetStringValue(); } },
                { "proxyServer", n => { ProxyServer = n.GetObjectValue<global::AITGraph.Sdk.Models.VpnProxyServer>(global::AITGraph.Sdk.Models.VpnProxyServer.CreateFromDiscriminatorValue); } },
                { "realm", n => { Realm = n.GetStringValue(); } },
                { "role", n => { Role = n.GetStringValue(); } },
                { "servers", n => { Servers = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.VpnServer>(global::AITGraph.Sdk.Models.VpnServer.CreateFromDiscriminatorValue)?.AsList(); } },
                { "targetedMobileApps", n => { TargetedMobileApps = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.AppListItem>(global::AITGraph.Sdk.Models.AppListItem.CreateFromDiscriminatorValue)?.AsList(); } },
                { "targetedPackageIds", n => { TargetedPackageIds = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
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
            writer.WriteBoolValue("alwaysOn", AlwaysOn);
            writer.WriteBoolValue("alwaysOnLockdown", AlwaysOnLockdown);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.VpnAuthenticationMethod>("authenticationMethod", AuthenticationMethod);
            writer.WriteStringValue("connectionName", ConnectionName);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.AndroidWorkProfileVpnConnectionType>("connectionType", ConnectionType);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.KeyValue>("customData", CustomData);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.KeyValuePair>("customKeyValueData", CustomKeyValueData);
            writer.WriteStringValue("fingerprint", Fingerprint);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.AndroidWorkProfileCertificateProfileBase>("identityCertificate", IdentityCertificate);
            writer.WriteStringValue("microsoftTunnelSiteId", MicrosoftTunnelSiteId);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.VpnProxyServer>("proxyServer", ProxyServer);
            writer.WriteStringValue("realm", Realm);
            writer.WriteStringValue("role", Role);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.VpnServer>("servers", Servers);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.AppListItem>("targetedMobileApps", TargetedMobileApps);
            writer.WriteCollectionOfPrimitiveValues<string>("targetedPackageIds", TargetedPackageIds);
        }
    }
}
#pragma warning restore CS0618
