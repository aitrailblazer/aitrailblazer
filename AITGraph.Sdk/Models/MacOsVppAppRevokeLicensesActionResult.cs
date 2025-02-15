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
    /// Defines results for actions on MacOS Vpp Apps, contains inherited properties for ActionResult.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class MacOsVppAppRevokeLicensesActionResult : IAdditionalDataHolder, IParsable
    {
        /// <summary>Possible types of reasons for an Apple Volume Purchase Program token action failure.</summary>
        public global::AITGraph.Sdk.Models.VppTokenActionFailureReason? ActionFailureReason { get; set; }
        /// <summary>Action name</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ActionName { get; set; }
#nullable restore
#else
        public string ActionName { get; set; }
#endif
        /// <summary>The actionState property</summary>
        public global::AITGraph.Sdk.Models.ActionState? ActionState { get; set; }
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>A count of the number of licenses for which revoke failed.</summary>
        public int? FailedLicensesCount { get; set; }
        /// <summary>Time the action state was last updated</summary>
        public DateTimeOffset? LastUpdatedDateTime { get; set; }
        /// <summary>DeviceId associated with the action.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ManagedDeviceId { get; set; }
#nullable restore
#else
        public string ManagedDeviceId { get; set; }
#endif
        /// <summary>The OdataType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OdataType { get; set; }
#nullable restore
#else
        public string OdataType { get; set; }
#endif
        /// <summary>Time the action was initiated</summary>
        public DateTimeOffset? StartDateTime { get; set; }
        /// <summary>A count of the number of licenses for which revoke was attempted.</summary>
        public int? TotalLicensesCount { get; set; }
        /// <summary>UserId associated with the action.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? UserId { get; set; }
#nullable restore
#else
        public string UserId { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.MacOsVppAppRevokeLicensesActionResult"/> and sets the default values.
        /// </summary>
        public MacOsVppAppRevokeLicensesActionResult()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.MacOsVppAppRevokeLicensesActionResult"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::AITGraph.Sdk.Models.MacOsVppAppRevokeLicensesActionResult CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.MacOsVppAppRevokeLicensesActionResult();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "actionFailureReason", n => { ActionFailureReason = n.GetEnumValue<global::AITGraph.Sdk.Models.VppTokenActionFailureReason>(); } },
                { "actionName", n => { ActionName = n.GetStringValue(); } },
                { "actionState", n => { ActionState = n.GetEnumValue<global::AITGraph.Sdk.Models.ActionState>(); } },
                { "failedLicensesCount", n => { FailedLicensesCount = n.GetIntValue(); } },
                { "lastUpdatedDateTime", n => { LastUpdatedDateTime = n.GetDateTimeOffsetValue(); } },
                { "managedDeviceId", n => { ManagedDeviceId = n.GetStringValue(); } },
                { "@odata.type", n => { OdataType = n.GetStringValue(); } },
                { "startDateTime", n => { StartDateTime = n.GetDateTimeOffsetValue(); } },
                { "totalLicensesCount", n => { TotalLicensesCount = n.GetIntValue(); } },
                { "userId", n => { UserId = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.VppTokenActionFailureReason>("actionFailureReason", ActionFailureReason);
            writer.WriteStringValue("actionName", ActionName);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.ActionState>("actionState", ActionState);
            writer.WriteIntValue("failedLicensesCount", FailedLicensesCount);
            writer.WriteDateTimeOffsetValue("lastUpdatedDateTime", LastUpdatedDateTime);
            writer.WriteStringValue("managedDeviceId", ManagedDeviceId);
            writer.WriteStringValue("@odata.type", OdataType);
            writer.WriteDateTimeOffsetValue("startDateTime", StartDateTime);
            writer.WriteIntValue("totalLicensesCount", TotalLicensesCount);
            writer.WriteStringValue("userId", UserId);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
