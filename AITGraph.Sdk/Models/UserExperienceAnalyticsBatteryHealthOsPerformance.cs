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
    /// The user experience analytics battery health os performance entity contains battery related information for all operating system versions in their organization.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class UserExperienceAnalyticsBatteryHealthOsPerformance : global::AITGraph.Sdk.Models.Entity, IParsable
    {
        /// <summary>Number of active devices for that os version. Valid values -2147483648 to 2147483647</summary>
        public int? ActiveDevices { get; set; }
        /// <summary>The mean of the battery age for all devices running a particular operating system version in a tenant. Unit in days. Valid values -2147483648 to 2147483647</summary>
        public int? AverageBatteryAgeInDays { get; set; }
        /// <summary>The mean of the estimated runtimes on full charge for all devices running a particular operating system version. Unit in minutes. Valid values -2147483648 to 2147483647</summary>
        public int? AverageEstimatedRuntimeInMinutes { get; set; }
        /// <summary>The mean of the maximum capacity for all devices running a particular operating system version. Maximum capacity measures the full charge vs. design capacity for a device’s batteries.. Valid values -2147483648 to 2147483647</summary>
        public int? AverageMaxCapacityPercentage { get; set; }
        /// <summary>Build number of the operating system.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OsBuildNumber { get; set; }
#nullable restore
#else
        public string OsBuildNumber { get; set; }
#endif
        /// <summary>Version of the operating system.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OsVersion { get; set; }
#nullable restore
#else
        public string OsVersion { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.UserExperienceAnalyticsBatteryHealthOsPerformance"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.UserExperienceAnalyticsBatteryHealthOsPerformance CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.UserExperienceAnalyticsBatteryHealthOsPerformance();
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
                { "averageBatteryAgeInDays", n => { AverageBatteryAgeInDays = n.GetIntValue(); } },
                { "averageEstimatedRuntimeInMinutes", n => { AverageEstimatedRuntimeInMinutes = n.GetIntValue(); } },
                { "averageMaxCapacityPercentage", n => { AverageMaxCapacityPercentage = n.GetIntValue(); } },
                { "osBuildNumber", n => { OsBuildNumber = n.GetStringValue(); } },
                { "osVersion", n => { OsVersion = n.GetStringValue(); } },
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
            writer.WriteIntValue("averageBatteryAgeInDays", AverageBatteryAgeInDays);
            writer.WriteIntValue("averageEstimatedRuntimeInMinutes", AverageEstimatedRuntimeInMinutes);
            writer.WriteIntValue("averageMaxCapacityPercentage", AverageMaxCapacityPercentage);
            writer.WriteStringValue("osBuildNumber", OsBuildNumber);
            writer.WriteStringValue("osVersion", OsVersion);
        }
    }
}
#pragma warning restore CS0618
