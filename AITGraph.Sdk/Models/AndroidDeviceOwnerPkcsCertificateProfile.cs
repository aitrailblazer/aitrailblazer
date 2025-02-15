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
    /// Android Device Owner PKCS certificate profile
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class AndroidDeviceOwnerPkcsCertificateProfile : global::AITGraph.Sdk.Models.AndroidDeviceOwnerCertificateProfileBase, IParsable
    {
        /// <summary>Certificate access type. Possible values are: userApproval, specificApps, unknownFutureValue.</summary>
        public global::AITGraph.Sdk.Models.AndroidDeviceOwnerCertificateAccessType? CertificateAccessType { get; set; }
        /// <summary>CertificateStore types</summary>
        public global::AITGraph.Sdk.Models.CertificateStore? CertificateStore { get; set; }
        /// <summary>PKCS Certificate Template Name</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CertificateTemplateName { get; set; }
#nullable restore
#else
        public string CertificateTemplateName { get; set; }
#endif
        /// <summary>PKCS Certification Authority</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CertificationAuthority { get; set; }
#nullable restore
#else
        public string CertificationAuthority { get; set; }
#endif
        /// <summary>PKCS Certification Authority Name</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CertificationAuthorityName { get; set; }
#nullable restore
#else
        public string CertificationAuthorityName { get; set; }
#endif
        /// <summary>Device Management Certification Authority Types.</summary>
        public global::AITGraph.Sdk.Models.DeviceManagementCertificationAuthority? CertificationAuthorityType { get; set; }
        /// <summary>Custom Subject Alternative Name Settings. This collection can contain a maximum of 500 elements.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.CustomSubjectAlternativeName>? CustomSubjectAlternativeNames { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.CustomSubjectAlternativeName> CustomSubjectAlternativeNames { get; set; }
#endif
        /// <summary>Certificate state for devices. This collection can contain a maximum of 2147483647 elements.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.ManagedDeviceCertificateState>? ManagedDeviceCertificateStates { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.ManagedDeviceCertificateState> ManagedDeviceCertificateStates { get; set; }
#endif
        /// <summary>Certificate access information. This collection can contain a maximum of 50 elements.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.AndroidDeviceOwnerSilentCertificateAccess>? SilentCertificateAccessDetails { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.AndroidDeviceOwnerSilentCertificateAccess> SilentCertificateAccessDetails { get; set; }
#endif
        /// <summary>Custom String that defines the AAD Attribute.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SubjectAlternativeNameFormatString { get; set; }
#nullable restore
#else
        public string SubjectAlternativeNameFormatString { get; set; }
#endif
        /// <summary>Custom format to use with SubjectNameFormat = Custom. Example: CN={{EmailAddress}},E={{EmailAddress}},OU=Enterprise Users,O=Contoso Corporation,L=Redmond,ST=WA,C=US</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SubjectNameFormatString { get; set; }
#nullable restore
#else
        public string SubjectNameFormatString { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.AndroidDeviceOwnerPkcsCertificateProfile"/> and sets the default values.
        /// </summary>
        public AndroidDeviceOwnerPkcsCertificateProfile() : base()
        {
            OdataType = "#microsoft.graph.androidDeviceOwnerPkcsCertificateProfile";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.AndroidDeviceOwnerPkcsCertificateProfile"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.AndroidDeviceOwnerPkcsCertificateProfile CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.AndroidDeviceOwnerPkcsCertificateProfile();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "certificateAccessType", n => { CertificateAccessType = n.GetEnumValue<global::AITGraph.Sdk.Models.AndroidDeviceOwnerCertificateAccessType>(); } },
                { "certificateStore", n => { CertificateStore = n.GetEnumValue<global::AITGraph.Sdk.Models.CertificateStore>(); } },
                { "certificateTemplateName", n => { CertificateTemplateName = n.GetStringValue(); } },
                { "certificationAuthority", n => { CertificationAuthority = n.GetStringValue(); } },
                { "certificationAuthorityName", n => { CertificationAuthorityName = n.GetStringValue(); } },
                { "certificationAuthorityType", n => { CertificationAuthorityType = n.GetEnumValue<global::AITGraph.Sdk.Models.DeviceManagementCertificationAuthority>(); } },
                { "customSubjectAlternativeNames", n => { CustomSubjectAlternativeNames = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.CustomSubjectAlternativeName>(global::AITGraph.Sdk.Models.CustomSubjectAlternativeName.CreateFromDiscriminatorValue)?.AsList(); } },
                { "managedDeviceCertificateStates", n => { ManagedDeviceCertificateStates = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.ManagedDeviceCertificateState>(global::AITGraph.Sdk.Models.ManagedDeviceCertificateState.CreateFromDiscriminatorValue)?.AsList(); } },
                { "silentCertificateAccessDetails", n => { SilentCertificateAccessDetails = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.AndroidDeviceOwnerSilentCertificateAccess>(global::AITGraph.Sdk.Models.AndroidDeviceOwnerSilentCertificateAccess.CreateFromDiscriminatorValue)?.AsList(); } },
                { "subjectAlternativeNameFormatString", n => { SubjectAlternativeNameFormatString = n.GetStringValue(); } },
                { "subjectNameFormatString", n => { SubjectNameFormatString = n.GetStringValue(); } },
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
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.AndroidDeviceOwnerCertificateAccessType>("certificateAccessType", CertificateAccessType);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.CertificateStore>("certificateStore", CertificateStore);
            writer.WriteStringValue("certificateTemplateName", CertificateTemplateName);
            writer.WriteStringValue("certificationAuthority", CertificationAuthority);
            writer.WriteStringValue("certificationAuthorityName", CertificationAuthorityName);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.DeviceManagementCertificationAuthority>("certificationAuthorityType", CertificationAuthorityType);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.CustomSubjectAlternativeName>("customSubjectAlternativeNames", CustomSubjectAlternativeNames);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.ManagedDeviceCertificateState>("managedDeviceCertificateStates", ManagedDeviceCertificateStates);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.AndroidDeviceOwnerSilentCertificateAccess>("silentCertificateAccessDetails", SilentCertificateAccessDetails);
            writer.WriteStringValue("subjectAlternativeNameFormatString", SubjectAlternativeNameFormatString);
            writer.WriteStringValue("subjectNameFormatString", SubjectNameFormatString);
        }
    }
}
#pragma warning restore CS0618
