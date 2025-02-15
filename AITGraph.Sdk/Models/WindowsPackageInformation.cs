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
    /// Contains properties for the package information for a Windows line of business app. Used as property within windowsPhone81AppXBundle object, which is also being deprecated. This complex type will be deprecated in February 2023.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WindowsPackageInformation : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Contains properties for Windows architecture.</summary>
        public global::AITGraph.Sdk.Models.WindowsArchitecture? ApplicableArchitecture { get; set; }
        /// <summary>The Display Name.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DisplayName { get; set; }
#nullable restore
#else
        public string DisplayName { get; set; }
#endif
        /// <summary>The Identity Name.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IdentityName { get; set; }
#nullable restore
#else
        public string IdentityName { get; set; }
#endif
        /// <summary>The Identity Publisher.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IdentityPublisher { get; set; }
#nullable restore
#else
        public string IdentityPublisher { get; set; }
#endif
        /// <summary>The Identity Resource Identifier.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IdentityResourceIdentifier { get; set; }
#nullable restore
#else
        public string IdentityResourceIdentifier { get; set; }
#endif
        /// <summary>The Identity Version.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IdentityVersion { get; set; }
#nullable restore
#else
        public string IdentityVersion { get; set; }
#endif
        /// <summary>The value for the minimum applicable operating system.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.WindowsMinimumOperatingSystem? MinimumSupportedOperatingSystem { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.WindowsMinimumOperatingSystem MinimumSupportedOperatingSystem { get; set; }
#endif
        /// <summary>The OdataType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OdataType { get; set; }
#nullable restore
#else
        public string OdataType { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.WindowsPackageInformation"/> and sets the default values.
        /// </summary>
        public WindowsPackageInformation()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.WindowsPackageInformation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::AITGraph.Sdk.Models.WindowsPackageInformation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.WindowsPackageInformation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "applicableArchitecture", n => { ApplicableArchitecture = n.GetEnumValue<global::AITGraph.Sdk.Models.WindowsArchitecture>(); } },
                { "displayName", n => { DisplayName = n.GetStringValue(); } },
                { "identityName", n => { IdentityName = n.GetStringValue(); } },
                { "identityPublisher", n => { IdentityPublisher = n.GetStringValue(); } },
                { "identityResourceIdentifier", n => { IdentityResourceIdentifier = n.GetStringValue(); } },
                { "identityVersion", n => { IdentityVersion = n.GetStringValue(); } },
                { "minimumSupportedOperatingSystem", n => { MinimumSupportedOperatingSystem = n.GetObjectValue<global::AITGraph.Sdk.Models.WindowsMinimumOperatingSystem>(global::AITGraph.Sdk.Models.WindowsMinimumOperatingSystem.CreateFromDiscriminatorValue); } },
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
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.WindowsArchitecture>("applicableArchitecture", ApplicableArchitecture);
            writer.WriteStringValue("displayName", DisplayName);
            writer.WriteStringValue("identityName", IdentityName);
            writer.WriteStringValue("identityPublisher", IdentityPublisher);
            writer.WriteStringValue("identityResourceIdentifier", IdentityResourceIdentifier);
            writer.WriteStringValue("identityVersion", IdentityVersion);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.WindowsMinimumOperatingSystem>("minimumSupportedOperatingSystem", MinimumSupportedOperatingSystem);
            writer.WriteStringValue("@odata.type", OdataType);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
