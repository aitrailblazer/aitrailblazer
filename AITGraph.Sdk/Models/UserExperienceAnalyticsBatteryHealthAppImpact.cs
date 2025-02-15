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
    /// The user experience analytics battery health app impact entity contains battery usage related information at an app level for the tenant.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class UserExperienceAnalyticsBatteryHealthAppImpact : global::AITGraph.Sdk.Models.Entity, IParsable
    {
        /// <summary>Number of active devices for using that app over a 14-day period. Valid values -2147483648 to 2147483647</summary>
        public int? ActiveDevices { get; set; }
        /// <summary>User friendly display name for the app. Eg: Outlook</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AppDisplayName { get; set; }
#nullable restore
#else
        public string AppDisplayName { get; set; }
#endif
        /// <summary>App name. Eg: oltk.exe</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AppName { get; set; }
#nullable restore
#else
        public string AppName { get; set; }
#endif
        /// <summary>App publisher. Eg: Microsoft Corporation</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AppPublisher { get; set; }
#nullable restore
#else
        public string AppPublisher { get; set; }
#endif
        /// <summary>The percent of total battery power used by this application when the device was not plugged into AC power, over 14 days computed across all devices in the tenant. Unit in percentage. Valid values -1.79769313486232E+308 to 1.79769313486232E+308</summary>
        public double? BatteryUsagePercentage { get; set; }
        /// <summary>true if the user had active interaction with the app.</summary>
        public bool? IsForegroundApp { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.UserExperienceAnalyticsBatteryHealthAppImpact"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.UserExperienceAnalyticsBatteryHealthAppImpact CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.UserExperienceAnalyticsBatteryHealthAppImpact();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "activeDevices", n => { ActiveDevices = n.GetIntValue(); } },
                { "appDisplayName", n => { AppDisplayName = n.GetStringValue(); } },
                { "appName", n => { AppName = n.GetStringValue(); } },
                { "appPublisher", n => { AppPublisher = n.GetStringValue(); } },
                { "batteryUsagePercentage", n => { BatteryUsagePercentage = n.GetDoubleValue(); } },
                { "isForegroundApp", n => { IsForegroundApp = n.GetBoolValue(); } },
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
            writer.WriteIntValue("activeDevices", ActiveDevices);
            writer.WriteStringValue("appDisplayName", AppDisplayName);
            writer.WriteStringValue("appName", AppName);
            writer.WriteStringValue("appPublisher", AppPublisher);
            writer.WriteDoubleValue("batteryUsagePercentage", BatteryUsagePercentage);
            writer.WriteBoolValue("isForegroundApp", IsForegroundApp);
        }
    }
}
#pragma warning restore CS0618
