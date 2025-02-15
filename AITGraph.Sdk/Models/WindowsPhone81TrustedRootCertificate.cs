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
    /// Windows Phone 8.1+ Trusted Root Certificate configuration profile
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WindowsPhone81TrustedRootCertificate : global::AITGraph.Sdk.Models.DeviceConfiguration, IParsable
    {
        /// <summary>File name to display in UI.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CertFileName { get; set; }
#nullable restore
#else
        public string CertFileName { get; set; }
#endif
        /// <summary>Trusted Root Certificate</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public byte[]? TrustedRootCertificate { get; set; }
#nullable restore
#else
        public byte[] TrustedRootCertificate { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.WindowsPhone81TrustedRootCertificate"/> and sets the default values.
        /// </summary>
        public WindowsPhone81TrustedRootCertificate() : base()
        {
            OdataType = "#microsoft.graph.windowsPhone81TrustedRootCertificate";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.WindowsPhone81TrustedRootCertificate"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.WindowsPhone81TrustedRootCertificate CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.WindowsPhone81TrustedRootCertificate();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "certFileName", n => { CertFileName = n.GetStringValue(); } },
                { "trustedRootCertificate", n => { TrustedRootCertificate = n.GetByteArrayValue(); } },
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
            writer.WriteStringValue("certFileName", CertFileName);
            writer.WriteByteArrayValue("trustedRootCertificate", TrustedRootCertificate);
        }
    }
}
#pragma warning restore CS0618
