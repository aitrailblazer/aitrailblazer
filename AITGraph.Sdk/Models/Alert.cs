// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace AITGraph.Sdk.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class Alert : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Name or alias of the activity group (attacker) this alert is attributed to.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ActivityGroupName { get; set; }
#nullable restore
#else
        public string ActivityGroupName { get; set; }
#endif
        /// <summary>The alertDetections property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.AlertDetection>? AlertDetections { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.AlertDetection> AlertDetections { get; set; }
#endif
        /// <summary>Name of the analyst the alert is assigned to for triage, investigation, or remediation (supports update).</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AssignedTo { get; set; }
#nullable restore
#else
        public string AssignedTo { get; set; }
#endif
        /// <summary>Azure subscription ID, present if this alert is related to an Azure resource.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AzureSubscriptionId { get; set; }
#nullable restore
#else
        public string AzureSubscriptionId { get; set; }
#endif
        /// <summary>Azure Active Directory tenant ID. Required.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AzureTenantId { get; set; }
#nullable restore
#else
        public string AzureTenantId { get; set; }
#endif
        /// <summary>Category of the alert (for example, credentialTheft, ransomware, etc.).</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Category { get; set; }
#nullable restore
#else
        public string Category { get; set; }
#endif
        /// <summary>Time at which the alert was closed. The Timestamp type represents date and time information using ISO 8601 format and is always in UTC time. For example, midnight UTC on Jan 1, 2014 is 2014-01-01T00:00:00Z (supports update).</summary>
        public DateTimeOffset? ClosedDateTime { get; set; }
        /// <summary>Security-related stateful information generated by the provider about the cloud application/s related to this alert.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.CloudAppSecurityState>? CloudAppStates { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.CloudAppSecurityState> CloudAppStates { get; set; }
#endif
        /// <summary>Customer-provided comments on alert (for customer alert management) (supports update).</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Comments { get; set; }
#nullable restore
#else
        public List<string> Comments { get; set; }
#endif
        /// <summary>Confidence of the detection logic (percentage between 1-100).</summary>
        public int? Confidence { get; set; }
        /// <summary>Time at which the alert was created by the alert provider. The Timestamp type represents date and time information using ISO 8601 format and is always in UTC time. For example, midnight UTC on Jan 1, 2014 is 2014-01-01T00:00:00Z. Required.</summary>
        public DateTimeOffset? CreatedDateTime { get; set; }
        /// <summary>Alert description.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>Set of alerts related to this alert entity (each alert is pushed to the SIEM as a separate record).</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? DetectionIds { get; set; }
#nullable restore
#else
        public List<string> DetectionIds { get; set; }
#endif
        /// <summary>Time at which the event(s) that served as the trigger(s) to generate the alert occurred. The Timestamp type represents date and time information using ISO 8601 format and is always in UTC time. For example, midnight UTC on Jan 1, 2014 is 2014-01-01T00:00:00Z. Required.</summary>
        public DateTimeOffset? EventDateTime { get; set; }
        /// <summary>Analyst feedback on the alert. Possible values are: unknown, truePositive, falsePositive, benignPositive. (supports update)</summary>
        public global::AITGraph.Sdk.Models.AlertFeedback? Feedback { get; set; }
        /// <summary>Security-related stateful information generated by the provider about the file(s) related to this alert.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.FileSecurityState>? FileStates { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.FileSecurityState> FileStates { get; set; }
#endif
        /// <summary>A collection of alertHistoryStates comprising an audit log of all updates made to an alert.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.AlertHistoryState>? HistoryStates { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.AlertHistoryState> HistoryStates { get; set; }
#endif
        /// <summary>Security-related stateful information generated by the provider about the host(s) related to this alert.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.HostSecurityState>? HostStates { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.HostSecurityState> HostStates { get; set; }
#endif
        /// <summary>IDs of incidents related to current alert.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? IncidentIds { get; set; }
#nullable restore
#else
        public List<string> IncidentIds { get; set; }
#endif
        /// <summary>The investigationSecurityStates property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.InvestigationSecurityState>? InvestigationSecurityStates { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.InvestigationSecurityState> InvestigationSecurityStates { get; set; }
#endif
        /// <summary>The lastEventDateTime property</summary>
        public DateTimeOffset? LastEventDateTime { get; set; }
        /// <summary>Time at which the alert entity was last modified. The Timestamp type represents date and time information using ISO 8601 format and is always in UTC time. For example, midnight UTC on Jan 1, 2014 is 2014-01-01T00:00:00Z.</summary>
        public DateTimeOffset? LastModifiedDateTime { get; set; }
        /// <summary>Threat Intelligence pertaining to malware related to this alert.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.MalwareState>? MalwareStates { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.MalwareState> MalwareStates { get; set; }
#endif
        /// <summary>The messageSecurityStates property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.MessageSecurityState>? MessageSecurityStates { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.MessageSecurityState> MessageSecurityStates { get; set; }
#endif
        /// <summary>Security-related stateful information generated by the provider about the network connection(s) related to this alert.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.NetworkConnection>? NetworkConnections { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.NetworkConnection> NetworkConnections { get; set; }
#endif
        /// <summary>Security-related stateful information generated by the provider about the process or processes related to this alert.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Process>? Processes { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Process> Processes { get; set; }
#endif
        /// <summary>Vendor/provider recommended action(s) to take as a result of the alert (for example, isolate machine, enforce2FA, reimage host).</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? RecommendedActions { get; set; }
#nullable restore
#else
        public List<string> RecommendedActions { get; set; }
#endif
        /// <summary>Security-related stateful information generated by the provider about the registry keys related to this alert.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.RegistryKeyState>? RegistryKeyStates { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.RegistryKeyState> RegistryKeyStates { get; set; }
#endif
        /// <summary>Resources related to current alert. For example, for some alerts this can have the Azure Resource value.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.SecurityResource>? SecurityResources { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.SecurityResource> SecurityResources { get; set; }
#endif
        /// <summary>The severity property</summary>
        public global::AITGraph.Sdk.Models.AlertSeverity? Severity { get; set; }
        /// <summary>Hyperlinks (URIs) to the source material related to the alert, for example, provider&apos;s user interface for alerts or log search, etc.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? SourceMaterials { get; set; }
#nullable restore
#else
        public List<string> SourceMaterials { get; set; }
#endif
        /// <summary>The status property</summary>
        public global::AITGraph.Sdk.Models.AlertStatus? Status { get; set; }
        /// <summary>User-definable labels that can be applied to an alert and can serve as filter conditions (for example &apos;HVA&apos;, &apos;SAW&apos;, etc.) (supports update).</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Tags { get; set; }
#nullable restore
#else
        public List<string> Tags { get; set; }
#endif
        /// <summary>Alert title. Required.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Title { get; set; }
#nullable restore
#else
        public string Title { get; set; }
#endif
        /// <summary>Security-related information about the specific properties that triggered the alert (properties appearing in the alert). Alerts might contain information about multiple users, hosts, files, ip addresses. This field indicates which properties triggered the alert generation.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.AlertTrigger>? Triggers { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.AlertTrigger> Triggers { get; set; }
#endif
        /// <summary>The uriClickSecurityStates property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.UriClickSecurityState>? UriClickSecurityStates { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.UriClickSecurityState> UriClickSecurityStates { get; set; }
#endif
        /// <summary>Security-related stateful information generated by the provider about the user accounts related to this alert.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.UserSecurityState>? UserStates { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.UserSecurityState> UserStates { get; set; }
#endif
        /// <summary>Complex type containing details about the security product/service vendor, provider, and subprovider (for example, vendor=Microsoft; provider=Windows Defender ATP; subProvider=AppLocker). Required.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.SecurityVendorInformation? VendorInformation { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.SecurityVendorInformation VendorInformation { get; set; }
#endif
        /// <summary>Threat intelligence pertaining to one or more vulnerabilities related to this alert.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.VulnerabilityState>? VulnerabilityStates { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.VulnerabilityState> VulnerabilityStates { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.Alert"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.Alert CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.Alert();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "activityGroupName", n => { ActivityGroupName = n.GetStringValue(); } },
                { "alertDetections", n => { AlertDetections = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.AlertDetection>(global::AITGraph.Sdk.Models.AlertDetection.CreateFromDiscriminatorValue)?.AsList(); } },
                { "assignedTo", n => { AssignedTo = n.GetStringValue(); } },
                { "azureSubscriptionId", n => { AzureSubscriptionId = n.GetStringValue(); } },
                { "azureTenantId", n => { AzureTenantId = n.GetStringValue(); } },
                { "category", n => { Category = n.GetStringValue(); } },
                { "closedDateTime", n => { ClosedDateTime = n.GetDateTimeOffsetValue(); } },
                { "cloudAppStates", n => { CloudAppStates = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.CloudAppSecurityState>(global::AITGraph.Sdk.Models.CloudAppSecurityState.CreateFromDiscriminatorValue)?.AsList(); } },
                { "comments", n => { Comments = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "confidence", n => { Confidence = n.GetIntValue(); } },
                { "createdDateTime", n => { CreatedDateTime = n.GetDateTimeOffsetValue(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "detectionIds", n => { DetectionIds = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "eventDateTime", n => { EventDateTime = n.GetDateTimeOffsetValue(); } },
                { "feedback", n => { Feedback = n.GetEnumValue<global::AITGraph.Sdk.Models.AlertFeedback>(); } },
                { "fileStates", n => { FileStates = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.FileSecurityState>(global::AITGraph.Sdk.Models.FileSecurityState.CreateFromDiscriminatorValue)?.AsList(); } },
                { "historyStates", n => { HistoryStates = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.AlertHistoryState>(global::AITGraph.Sdk.Models.AlertHistoryState.CreateFromDiscriminatorValue)?.AsList(); } },
                { "hostStates", n => { HostStates = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.HostSecurityState>(global::AITGraph.Sdk.Models.HostSecurityState.CreateFromDiscriminatorValue)?.AsList(); } },
                { "incidentIds", n => { IncidentIds = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "investigationSecurityStates", n => { InvestigationSecurityStates = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.InvestigationSecurityState>(global::AITGraph.Sdk.Models.InvestigationSecurityState.CreateFromDiscriminatorValue)?.AsList(); } },
                { "lastEventDateTime", n => { LastEventDateTime = n.GetDateTimeOffsetValue(); } },
                { "lastModifiedDateTime", n => { LastModifiedDateTime = n.GetDateTimeOffsetValue(); } },
                { "malwareStates", n => { MalwareStates = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.MalwareState>(global::AITGraph.Sdk.Models.MalwareState.CreateFromDiscriminatorValue)?.AsList(); } },
                { "messageSecurityStates", n => { MessageSecurityStates = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.MessageSecurityState>(global::AITGraph.Sdk.Models.MessageSecurityState.CreateFromDiscriminatorValue)?.AsList(); } },
                { "networkConnections", n => { NetworkConnections = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.NetworkConnection>(global::AITGraph.Sdk.Models.NetworkConnection.CreateFromDiscriminatorValue)?.AsList(); } },
                { "processes", n => { Processes = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Process>(global::AITGraph.Sdk.Models.Process.CreateFromDiscriminatorValue)?.AsList(); } },
                { "recommendedActions", n => { RecommendedActions = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "registryKeyStates", n => { RegistryKeyStates = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.RegistryKeyState>(global::AITGraph.Sdk.Models.RegistryKeyState.CreateFromDiscriminatorValue)?.AsList(); } },
                { "securityResources", n => { SecurityResources = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.SecurityResource>(global::AITGraph.Sdk.Models.SecurityResource.CreateFromDiscriminatorValue)?.AsList(); } },
                { "severity", n => { Severity = n.GetEnumValue<global::AITGraph.Sdk.Models.AlertSeverity>(); } },
                { "sourceMaterials", n => { SourceMaterials = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "status", n => { Status = n.GetEnumValue<global::AITGraph.Sdk.Models.AlertStatus>(); } },
                { "tags", n => { Tags = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "title", n => { Title = n.GetStringValue(); } },
                { "triggers", n => { Triggers = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.AlertTrigger>(global::AITGraph.Sdk.Models.AlertTrigger.CreateFromDiscriminatorValue)?.AsList(); } },
                { "uriClickSecurityStates", n => { UriClickSecurityStates = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.UriClickSecurityState>(global::AITGraph.Sdk.Models.UriClickSecurityState.CreateFromDiscriminatorValue)?.AsList(); } },
                { "userStates", n => { UserStates = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.UserSecurityState>(global::AITGraph.Sdk.Models.UserSecurityState.CreateFromDiscriminatorValue)?.AsList(); } },
                { "vendorInformation", n => { VendorInformation = n.GetObjectValue<global::AITGraph.Sdk.Models.SecurityVendorInformation>(global::AITGraph.Sdk.Models.SecurityVendorInformation.CreateFromDiscriminatorValue); } },
                { "vulnerabilityStates", n => { VulnerabilityStates = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.VulnerabilityState>(global::AITGraph.Sdk.Models.VulnerabilityState.CreateFromDiscriminatorValue)?.AsList(); } },
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
            writer.WriteStringValue("activityGroupName", ActivityGroupName);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.AlertDetection>("alertDetections", AlertDetections);
            writer.WriteStringValue("assignedTo", AssignedTo);
            writer.WriteStringValue("azureSubscriptionId", AzureSubscriptionId);
            writer.WriteStringValue("azureTenantId", AzureTenantId);
            writer.WriteStringValue("category", Category);
            writer.WriteDateTimeOffsetValue("closedDateTime", ClosedDateTime);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.CloudAppSecurityState>("cloudAppStates", CloudAppStates);
            writer.WriteCollectionOfPrimitiveValues<string>("comments", Comments);
            writer.WriteIntValue("confidence", Confidence);
            writer.WriteDateTimeOffsetValue("createdDateTime", CreatedDateTime);
            writer.WriteStringValue("description", Description);
            writer.WriteCollectionOfPrimitiveValues<string>("detectionIds", DetectionIds);
            writer.WriteDateTimeOffsetValue("eventDateTime", EventDateTime);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.AlertFeedback>("feedback", Feedback);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.FileSecurityState>("fileStates", FileStates);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.AlertHistoryState>("historyStates", HistoryStates);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.HostSecurityState>("hostStates", HostStates);
            writer.WriteCollectionOfPrimitiveValues<string>("incidentIds", IncidentIds);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.InvestigationSecurityState>("investigationSecurityStates", InvestigationSecurityStates);
            writer.WriteDateTimeOffsetValue("lastEventDateTime", LastEventDateTime);
            writer.WriteDateTimeOffsetValue("lastModifiedDateTime", LastModifiedDateTime);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.MalwareState>("malwareStates", MalwareStates);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.MessageSecurityState>("messageSecurityStates", MessageSecurityStates);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.NetworkConnection>("networkConnections", NetworkConnections);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Process>("processes", Processes);
            writer.WriteCollectionOfPrimitiveValues<string>("recommendedActions", RecommendedActions);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.RegistryKeyState>("registryKeyStates", RegistryKeyStates);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.SecurityResource>("securityResources", SecurityResources);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.AlertSeverity>("severity", Severity);
            writer.WriteCollectionOfPrimitiveValues<string>("sourceMaterials", SourceMaterials);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.AlertStatus>("status", Status);
            writer.WriteCollectionOfPrimitiveValues<string>("tags", Tags);
            writer.WriteStringValue("title", Title);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.AlertTrigger>("triggers", Triggers);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.UriClickSecurityState>("uriClickSecurityStates", UriClickSecurityStates);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.UserSecurityState>("userStates", UserStates);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.SecurityVendorInformation>("vendorInformation", VendorInformation);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.VulnerabilityState>("vulnerabilityStates", VulnerabilityStates);
        }
    }
}
#pragma warning restore CS0618
