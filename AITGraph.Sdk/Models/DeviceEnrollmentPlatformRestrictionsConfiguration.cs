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
    /// Device Enrollment Configuration that restricts the types of devices a user can enroll
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class DeviceEnrollmentPlatformRestrictionsConfiguration : global::AITGraph.Sdk.Models.DeviceEnrollmentConfiguration, IParsable
    {
        /// <summary>Android for work restrictions based on platform, platform operating system version, and device ownership</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction? AndroidForWorkRestriction { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction AndroidForWorkRestriction { get; set; }
#endif
        /// <summary>Android restrictions based on platform, platform operating system version, and device ownership</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction? AndroidRestriction { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction AndroidRestriction { get; set; }
#endif
        /// <summary>Ios restrictions based on platform, platform operating system version, and device ownership</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction? IosRestriction { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction IosRestriction { get; set; }
#endif
        /// <summary>Mac restrictions based on platform, platform operating system version, and device ownership</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction? MacOSRestriction { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction MacOSRestriction { get; set; }
#endif
        /// <summary>Mac restrictions based on platform, platform operating system version, and device ownership</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction? MacRestriction { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction MacRestriction { get; set; }
#endif
        /// <summary>Windows Home Sku restrictions based on platform, platform operating system version, and device ownership</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction? WindowsHomeSkuRestriction { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction WindowsHomeSkuRestriction { get; set; }
#endif
        /// <summary>Windows mobile restrictions based on platform, platform operating system version, and device ownership</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction? WindowsMobileRestriction { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction WindowsMobileRestriction { get; set; }
#endif
        /// <summary>Windows restrictions based on platform, platform operating system version, and device ownership</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction? WindowsRestriction { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction WindowsRestriction { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestrictionsConfiguration"/> and sets the default values.
        /// </summary>
        public DeviceEnrollmentPlatformRestrictionsConfiguration() : base()
        {
            OdataType = "#microsoft.graph.deviceEnrollmentPlatformRestrictionsConfiguration";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestrictionsConfiguration"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestrictionsConfiguration CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestrictionsConfiguration();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "androidForWorkRestriction", n => { AndroidForWorkRestriction = n.GetObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>(global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction.CreateFromDiscriminatorValue); } },
                { "androidRestriction", n => { AndroidRestriction = n.GetObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>(global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction.CreateFromDiscriminatorValue); } },
                { "iosRestriction", n => { IosRestriction = n.GetObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>(global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction.CreateFromDiscriminatorValue); } },
                { "macOSRestriction", n => { MacOSRestriction = n.GetObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>(global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction.CreateFromDiscriminatorValue); } },
                { "macRestriction", n => { MacRestriction = n.GetObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>(global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction.CreateFromDiscriminatorValue); } },
                { "windowsHomeSkuRestriction", n => { WindowsHomeSkuRestriction = n.GetObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>(global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction.CreateFromDiscriminatorValue); } },
                { "windowsMobileRestriction", n => { WindowsMobileRestriction = n.GetObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>(global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction.CreateFromDiscriminatorValue); } },
                { "windowsRestriction", n => { WindowsRestriction = n.GetObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>(global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction.CreateFromDiscriminatorValue); } },
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
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>("androidForWorkRestriction", AndroidForWorkRestriction);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>("androidRestriction", AndroidRestriction);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>("iosRestriction", IosRestriction);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>("macOSRestriction", MacOSRestriction);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>("macRestriction", MacRestriction);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>("windowsHomeSkuRestriction", WindowsHomeSkuRestriction);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>("windowsMobileRestriction", WindowsMobileRestriction);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.DeviceEnrollmentPlatformRestriction>("windowsRestriction", WindowsRestriction);
        }
    }
}
#pragma warning restore CS0618
