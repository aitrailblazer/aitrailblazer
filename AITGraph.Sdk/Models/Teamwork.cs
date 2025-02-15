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
    public partial class Teamwork : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>A collection of deleted teams.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.DeletedTeam>? DeletedTeams { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.DeletedTeam> DeletedTeams { get; set; }
#endif
        /// <summary>The Teams devices provisioned for the tenant.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.TeamworkDevice>? Devices { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.TeamworkDevice> Devices { get; set; }
#endif
        /// <summary>Represents tenant-wide settings for all Teams apps in the tenant.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.TeamsAppSettings? TeamsAppSettings { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.TeamsAppSettings TeamsAppSettings { get; set; }
#endif
        /// <summary>The templates associated with a team.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.TeamTemplate>? TeamTemplates { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.TeamTemplate> TeamTemplates { get; set; }
#endif
        /// <summary>A workforce integration with shifts.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.WorkforceIntegration>? WorkforceIntegrations { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.WorkforceIntegration> WorkforceIntegrations { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.Teamwork"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.Teamwork CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.Teamwork();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "deletedTeams", n => { DeletedTeams = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.DeletedTeam>(global::AITGraph.Sdk.Models.DeletedTeam.CreateFromDiscriminatorValue)?.AsList(); } },
                { "devices", n => { Devices = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.TeamworkDevice>(global::AITGraph.Sdk.Models.TeamworkDevice.CreateFromDiscriminatorValue)?.AsList(); } },
                { "teamTemplates", n => { TeamTemplates = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.TeamTemplate>(global::AITGraph.Sdk.Models.TeamTemplate.CreateFromDiscriminatorValue)?.AsList(); } },
                { "teamsAppSettings", n => { TeamsAppSettings = n.GetObjectValue<global::AITGraph.Sdk.Models.TeamsAppSettings>(global::AITGraph.Sdk.Models.TeamsAppSettings.CreateFromDiscriminatorValue); } },
                { "workforceIntegrations", n => { WorkforceIntegrations = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.WorkforceIntegration>(global::AITGraph.Sdk.Models.WorkforceIntegration.CreateFromDiscriminatorValue)?.AsList(); } },
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
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.DeletedTeam>("deletedTeams", DeletedTeams);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.TeamworkDevice>("devices", Devices);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.TeamsAppSettings>("teamsAppSettings", TeamsAppSettings);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.TeamTemplate>("teamTemplates", TeamTemplates);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.WorkforceIntegration>("workforceIntegrations", WorkforceIntegrations);
        }
    }
}
#pragma warning restore CS0618
