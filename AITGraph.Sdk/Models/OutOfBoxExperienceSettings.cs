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
    /// Out of box experience setting
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class OutOfBoxExperienceSettings : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The deviceUsageType property</summary>
        public global::AITGraph.Sdk.Models.WindowsDeviceUsageType? DeviceUsageType { get; set; }
        /// <summary>If set to true, then the user can&apos;t start over with different account, on company sign-in</summary>
        public bool? HideEscapeLink { get; set; }
        /// <summary>Show or hide EULA to user</summary>
        public bool? HideEULA { get; set; }
        /// <summary>Show or hide privacy settings to user</summary>
        public bool? HidePrivacySettings { get; set; }
        /// <summary>The OdataType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OdataType { get; set; }
#nullable restore
#else
        public string OdataType { get; set; }
#endif
        /// <summary>If set, then skip the keyboard selection page if Language and Region are set</summary>
        public bool? SkipKeyboardSelectionPage { get; set; }
        /// <summary>The userType property</summary>
        public global::AITGraph.Sdk.Models.WindowsUserType? UserType { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.OutOfBoxExperienceSettings"/> and sets the default values.
        /// </summary>
        public OutOfBoxExperienceSettings()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.OutOfBoxExperienceSettings"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::AITGraph.Sdk.Models.OutOfBoxExperienceSettings CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.OutOfBoxExperienceSettings();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "deviceUsageType", n => { DeviceUsageType = n.GetEnumValue<global::AITGraph.Sdk.Models.WindowsDeviceUsageType>(); } },
                { "hideEULA", n => { HideEULA = n.GetBoolValue(); } },
                { "hideEscapeLink", n => { HideEscapeLink = n.GetBoolValue(); } },
                { "hidePrivacySettings", n => { HidePrivacySettings = n.GetBoolValue(); } },
                { "@odata.type", n => { OdataType = n.GetStringValue(); } },
                { "skipKeyboardSelectionPage", n => { SkipKeyboardSelectionPage = n.GetBoolValue(); } },
                { "userType", n => { UserType = n.GetEnumValue<global::AITGraph.Sdk.Models.WindowsUserType>(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.WindowsDeviceUsageType>("deviceUsageType", DeviceUsageType);
            writer.WriteBoolValue("hideEscapeLink", HideEscapeLink);
            writer.WriteBoolValue("hideEULA", HideEULA);
            writer.WriteBoolValue("hidePrivacySettings", HidePrivacySettings);
            writer.WriteStringValue("@odata.type", OdataType);
            writer.WriteBoolValue("skipKeyboardSelectionPage", SkipKeyboardSelectionPage);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.WindowsUserType>("userType", UserType);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
