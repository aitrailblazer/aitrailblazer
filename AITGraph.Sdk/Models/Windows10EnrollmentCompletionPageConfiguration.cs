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
    /// Windows 10 Enrollment Status Page Configuration
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class Windows10EnrollmentCompletionPageConfiguration : global::AITGraph.Sdk.Models.DeviceEnrollmentConfiguration, IParsable
    {
        /// <summary>Allow or block device reset on installation failure</summary>
        public bool? AllowDeviceResetOnInstallFailure { get; set; }
        /// <summary>Allow the user to continue using the device on installation failure</summary>
        public bool? AllowDeviceUseOnInstallFailure { get; set; }
        /// <summary>Allow or block log collection on installation failure</summary>
        public bool? AllowLogCollectionOnInstallFailure { get; set; }
        /// <summary>Install all required apps as non blocking apps during white glove</summary>
        public bool? AllowNonBlockingAppInstallation { get; set; }
        /// <summary>Allow the user to retry the setup on installation failure</summary>
        public bool? BlockDeviceSetupRetryByUser { get; set; }
        /// <summary>Set custom error message to show upon installation failure</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CustomErrorMessage { get; set; }
#nullable restore
#else
        public string CustomErrorMessage { get; set; }
#endif
        /// <summary>Only show installation progress for first user post enrollment</summary>
        public bool? DisableUserStatusTrackingAfterFirstUser { get; set; }
        /// <summary>Set installation progress timeout in minutes</summary>
        public int? InstallProgressTimeoutInMinutes { get; set; }
        /// <summary>Allows quality updates installation during OOBE</summary>
        public bool? InstallQualityUpdates { get; set; }
        /// <summary>Selected applications to track the installation status</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? SelectedMobileAppIds { get; set; }
#nullable restore
#else
        public List<string> SelectedMobileAppIds { get; set; }
#endif
        /// <summary>Show or hide installation progress to user</summary>
        public bool? ShowInstallationProgress { get; set; }
        /// <summary>Only show installation progress for Autopilot enrollment scenarios</summary>
        public bool? TrackInstallProgressForAutopilotOnly { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.Windows10EnrollmentCompletionPageConfiguration"/> and sets the default values.
        /// </summary>
        public Windows10EnrollmentCompletionPageConfiguration() : base()
        {
            OdataType = "#microsoft.graph.windows10EnrollmentCompletionPageConfiguration";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.Windows10EnrollmentCompletionPageConfiguration"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.Windows10EnrollmentCompletionPageConfiguration CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.Windows10EnrollmentCompletionPageConfiguration();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "allowDeviceResetOnInstallFailure", n => { AllowDeviceResetOnInstallFailure = n.GetBoolValue(); } },
                { "allowDeviceUseOnInstallFailure", n => { AllowDeviceUseOnInstallFailure = n.GetBoolValue(); } },
                { "allowLogCollectionOnInstallFailure", n => { AllowLogCollectionOnInstallFailure = n.GetBoolValue(); } },
                { "allowNonBlockingAppInstallation", n => { AllowNonBlockingAppInstallation = n.GetBoolValue(); } },
                { "blockDeviceSetupRetryByUser", n => { BlockDeviceSetupRetryByUser = n.GetBoolValue(); } },
                { "customErrorMessage", n => { CustomErrorMessage = n.GetStringValue(); } },
                { "disableUserStatusTrackingAfterFirstUser", n => { DisableUserStatusTrackingAfterFirstUser = n.GetBoolValue(); } },
                { "installProgressTimeoutInMinutes", n => { InstallProgressTimeoutInMinutes = n.GetIntValue(); } },
                { "installQualityUpdates", n => { InstallQualityUpdates = n.GetBoolValue(); } },
                { "selectedMobileAppIds", n => { SelectedMobileAppIds = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "showInstallationProgress", n => { ShowInstallationProgress = n.GetBoolValue(); } },
                { "trackInstallProgressForAutopilotOnly", n => { TrackInstallProgressForAutopilotOnly = n.GetBoolValue(); } },
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
            writer.WriteBoolValue("allowDeviceResetOnInstallFailure", AllowDeviceResetOnInstallFailure);
            writer.WriteBoolValue("allowDeviceUseOnInstallFailure", AllowDeviceUseOnInstallFailure);
            writer.WriteBoolValue("allowLogCollectionOnInstallFailure", AllowLogCollectionOnInstallFailure);
            writer.WriteBoolValue("allowNonBlockingAppInstallation", AllowNonBlockingAppInstallation);
            writer.WriteBoolValue("blockDeviceSetupRetryByUser", BlockDeviceSetupRetryByUser);
            writer.WriteStringValue("customErrorMessage", CustomErrorMessage);
            writer.WriteBoolValue("disableUserStatusTrackingAfterFirstUser", DisableUserStatusTrackingAfterFirstUser);
            writer.WriteIntValue("installProgressTimeoutInMinutes", InstallProgressTimeoutInMinutes);
            writer.WriteBoolValue("installQualityUpdates", InstallQualityUpdates);
            writer.WriteCollectionOfPrimitiveValues<string>("selectedMobileAppIds", SelectedMobileAppIds);
            writer.WriteBoolValue("showInstallationProgress", ShowInstallationProgress);
            writer.WriteBoolValue("trackInstallProgressForAutopilotOnly", TrackInstallProgressForAutopilotOnly);
        }
    }
}
#pragma warning restore CS0618
