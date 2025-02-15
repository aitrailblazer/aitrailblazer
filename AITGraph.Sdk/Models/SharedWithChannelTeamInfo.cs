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
    public partial class SharedWithChannelTeamInfo : global::AITGraph.Sdk.Models.TeamInfo, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>A collection of team members who have access to the shared channel.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.ConversationMember>? AllowedMembers { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.ConversationMember> AllowedMembers { get; set; }
#endif
        /// <summary>Indicates whether the team is the host of the channel.</summary>
        public bool? IsHostTeam { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.SharedWithChannelTeamInfo"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.SharedWithChannelTeamInfo CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.SharedWithChannelTeamInfo();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "allowedMembers", n => { AllowedMembers = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.ConversationMember>(global::AITGraph.Sdk.Models.ConversationMember.CreateFromDiscriminatorValue)?.AsList(); } },
                { "isHostTeam", n => { IsHostTeam = n.GetBoolValue(); } },
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
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.ConversationMember>("allowedMembers", AllowedMembers);
            writer.WriteBoolValue("isHostTeam", IsHostTeam);
        }
    }
}
#pragma warning restore CS0618
