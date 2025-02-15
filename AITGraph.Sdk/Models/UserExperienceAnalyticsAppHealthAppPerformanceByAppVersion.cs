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
    /// The user experience analytics application performance entity contains app performance details by app version.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class UserExperienceAnalyticsAppHealthAppPerformanceByAppVersion : global::AITGraph.Sdk.Models.Entity, IParsable
    {
        /// <summary>The number of crashes for the app. Valid values -2147483648 to 2147483647</summary>
        public int? AppCrashCount { get; set; }
        /// <summary>The friendly name of the application.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AppDisplayName { get; set; }
#nullable restore
#else
        public string AppDisplayName { get; set; }
#endif
        /// <summary>The name of the application.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AppName { get; set; }
#nullable restore
#else
        public string AppName { get; set; }
#endif
        /// <summary>The publisher of the application.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AppPublisher { get; set; }
#nullable restore
#else
        public string AppPublisher { get; set; }
#endif
        /// <summary>The total usage time of the application in minutes. Valid values -2147483648 to 2147483647</summary>
        public int? AppUsageDuration { get; set; }
        /// <summary>The version of the application.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AppVersion { get; set; }
#nullable restore
#else
        public string AppVersion { get; set; }
#endif
        /// <summary>The mean time to failure for the app in minutes. Valid values -2147483648 to 2147483647</summary>
        public int? MeanTimeToFailureInMinutes { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.UserExperienceAnalyticsAppHealthAppPerformanceByAppVersion"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.UserExperienceAnalyticsAppHealthAppPerformanceByAppVersion CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.UserExperienceAnalyticsAppHealthAppPerformanceByAppVersion();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "appCrashCount", n => { AppCrashCount = n.GetIntValue(); } },
                { "appDisplayName", n => { AppDisplayName = n.GetStringValue(); } },
                { "appName", n => { AppName = n.GetStringValue(); } },
                { "appPublisher", n => { AppPublisher = n.GetStringValue(); } },
                { "appUsageDuration", n => { AppUsageDuration = n.GetIntValue(); } },
                { "appVersion", n => { AppVersion = n.GetStringValue(); } },
                { "meanTimeToFailureInMinutes", n => { MeanTimeToFailureInMinutes = n.GetIntValue(); } },
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
            writer.WriteIntValue("appCrashCount", AppCrashCount);
            writer.WriteStringValue("appDisplayName", AppDisplayName);
            writer.WriteStringValue("appName", AppName);
            writer.WriteStringValue("appPublisher", AppPublisher);
            writer.WriteIntValue("appUsageDuration", AppUsageDuration);
            writer.WriteStringValue("appVersion", AppVersion);
            writer.WriteIntValue("meanTimeToFailureInMinutes", MeanTimeToFailureInMinutes);
        }
    }
}
#pragma warning restore CS0618
