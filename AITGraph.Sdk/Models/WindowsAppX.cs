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
    /// Contains properties and inherited properties for Windows AppX Line Of Business apps.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WindowsAppX : global::AITGraph.Sdk.Models.MobileLobApp, IParsable
    {
        /// <summary>Contains properties for Windows architecture.</summary>
        public global::AITGraph.Sdk.Models.WindowsArchitecture? ApplicableArchitectures { get; set; }
        /// <summary>The Identity Name.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IdentityName { get; set; }
#nullable restore
#else
        public string IdentityName { get; set; }
#endif
        /// <summary>The Identity Publisher Hash.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IdentityPublisherHash { get; set; }
#nullable restore
#else
        public string IdentityPublisherHash { get; set; }
#endif
        /// <summary>The Identity Resource Identifier.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IdentityResourceIdentifier { get; set; }
#nullable restore
#else
        public string IdentityResourceIdentifier { get; set; }
#endif
        /// <summary>The identity version.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IdentityVersion { get; set; }
#nullable restore
#else
        public string IdentityVersion { get; set; }
#endif
        /// <summary>Whether or not the app is a bundle.</summary>
        public bool? IsBundle { get; set; }
        /// <summary>The minimum operating system required for a Windows mobile app.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.WindowsMinimumOperatingSystem? MinimumSupportedOperatingSystem { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.WindowsMinimumOperatingSystem MinimumSupportedOperatingSystem { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.WindowsAppX"/> and sets the default values.
        /// </summary>
        public WindowsAppX() : base()
        {
            OdataType = "#microsoft.graph.windowsAppX";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.WindowsAppX"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.WindowsAppX CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.WindowsAppX();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "applicableArchitectures", n => { ApplicableArchitectures = n.GetEnumValue<global::AITGraph.Sdk.Models.WindowsArchitecture>(); } },
                { "identityName", n => { IdentityName = n.GetStringValue(); } },
                { "identityPublisherHash", n => { IdentityPublisherHash = n.GetStringValue(); } },
                { "identityResourceIdentifier", n => { IdentityResourceIdentifier = n.GetStringValue(); } },
                { "identityVersion", n => { IdentityVersion = n.GetStringValue(); } },
                { "isBundle", n => { IsBundle = n.GetBoolValue(); } },
                { "minimumSupportedOperatingSystem", n => { MinimumSupportedOperatingSystem = n.GetObjectValue<global::AITGraph.Sdk.Models.WindowsMinimumOperatingSystem>(global::AITGraph.Sdk.Models.WindowsMinimumOperatingSystem.CreateFromDiscriminatorValue); } },
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
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.WindowsArchitecture>("applicableArchitectures", ApplicableArchitectures);
            writer.WriteStringValue("identityName", IdentityName);
            writer.WriteStringValue("identityPublisherHash", IdentityPublisherHash);
            writer.WriteStringValue("identityResourceIdentifier", IdentityResourceIdentifier);
            writer.WriteStringValue("identityVersion", IdentityVersion);
            writer.WriteBoolValue("isBundle", IsBundle);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.WindowsMinimumOperatingSystem>("minimumSupportedOperatingSystem", MinimumSupportedOperatingSystem);
        }
    }
}
#pragma warning restore CS0618
