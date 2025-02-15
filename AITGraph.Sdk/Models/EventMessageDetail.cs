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
    public partial class EventMessageDetail : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The OdataType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OdataType { get; set; }
#nullable restore
#else
        public string OdataType { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.EventMessageDetail"/> and sets the default values.
        /// </summary>
        public EventMessageDetail()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.EventMessageDetail"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::AITGraph.Sdk.Models.EventMessageDetail CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            var mappingValue = parseNode.GetChildNode("@odata.type")?.GetStringValue();
            return mappingValue switch
            {
                "#microsoft.graph.callEndedEventMessageDetail" => new global::AITGraph.Sdk.Models.CallEndedEventMessageDetail(),
                "#microsoft.graph.callRecordingEventMessageDetail" => new global::AITGraph.Sdk.Models.CallRecordingEventMessageDetail(),
                "#microsoft.graph.callStartedEventMessageDetail" => new global::AITGraph.Sdk.Models.CallStartedEventMessageDetail(),
                "#microsoft.graph.callTranscriptEventMessageDetail" => new global::AITGraph.Sdk.Models.CallTranscriptEventMessageDetail(),
                "#microsoft.graph.channelAddedEventMessageDetail" => new global::AITGraph.Sdk.Models.ChannelAddedEventMessageDetail(),
                "#microsoft.graph.channelDeletedEventMessageDetail" => new global::AITGraph.Sdk.Models.ChannelDeletedEventMessageDetail(),
                "#microsoft.graph.channelDescriptionUpdatedEventMessageDetail" => new global::AITGraph.Sdk.Models.ChannelDescriptionUpdatedEventMessageDetail(),
                "#microsoft.graph.channelRenamedEventMessageDetail" => new global::AITGraph.Sdk.Models.ChannelRenamedEventMessageDetail(),
                "#microsoft.graph.channelSetAsFavoriteByDefaultEventMessageDetail" => new global::AITGraph.Sdk.Models.ChannelSetAsFavoriteByDefaultEventMessageDetail(),
                "#microsoft.graph.channelUnsetAsFavoriteByDefaultEventMessageDetail" => new global::AITGraph.Sdk.Models.ChannelUnsetAsFavoriteByDefaultEventMessageDetail(),
                "#microsoft.graph.chatRenamedEventMessageDetail" => new global::AITGraph.Sdk.Models.ChatRenamedEventMessageDetail(),
                "#microsoft.graph.conversationMemberRoleUpdatedEventMessageDetail" => new global::AITGraph.Sdk.Models.ConversationMemberRoleUpdatedEventMessageDetail(),
                "#microsoft.graph.meetingPolicyUpdatedEventMessageDetail" => new global::AITGraph.Sdk.Models.MeetingPolicyUpdatedEventMessageDetail(),
                "#microsoft.graph.membersAddedEventMessageDetail" => new global::AITGraph.Sdk.Models.MembersAddedEventMessageDetail(),
                "#microsoft.graph.membersDeletedEventMessageDetail" => new global::AITGraph.Sdk.Models.MembersDeletedEventMessageDetail(),
                "#microsoft.graph.membersJoinedEventMessageDetail" => new global::AITGraph.Sdk.Models.MembersJoinedEventMessageDetail(),
                "#microsoft.graph.membersLeftEventMessageDetail" => new global::AITGraph.Sdk.Models.MembersLeftEventMessageDetail(),
                "#microsoft.graph.messagePinnedEventMessageDetail" => new global::AITGraph.Sdk.Models.MessagePinnedEventMessageDetail(),
                "#microsoft.graph.messageUnpinnedEventMessageDetail" => new global::AITGraph.Sdk.Models.MessageUnpinnedEventMessageDetail(),
                "#microsoft.graph.tabUpdatedEventMessageDetail" => new global::AITGraph.Sdk.Models.TabUpdatedEventMessageDetail(),
                "#microsoft.graph.teamArchivedEventMessageDetail" => new global::AITGraph.Sdk.Models.TeamArchivedEventMessageDetail(),
                "#microsoft.graph.teamCreatedEventMessageDetail" => new global::AITGraph.Sdk.Models.TeamCreatedEventMessageDetail(),
                "#microsoft.graph.teamDescriptionUpdatedEventMessageDetail" => new global::AITGraph.Sdk.Models.TeamDescriptionUpdatedEventMessageDetail(),
                "#microsoft.graph.teamJoiningDisabledEventMessageDetail" => new global::AITGraph.Sdk.Models.TeamJoiningDisabledEventMessageDetail(),
                "#microsoft.graph.teamJoiningEnabledEventMessageDetail" => new global::AITGraph.Sdk.Models.TeamJoiningEnabledEventMessageDetail(),
                "#microsoft.graph.teamRenamedEventMessageDetail" => new global::AITGraph.Sdk.Models.TeamRenamedEventMessageDetail(),
                "#microsoft.graph.teamsAppInstalledEventMessageDetail" => new global::AITGraph.Sdk.Models.TeamsAppInstalledEventMessageDetail(),
                "#microsoft.graph.teamsAppRemovedEventMessageDetail" => new global::AITGraph.Sdk.Models.TeamsAppRemovedEventMessageDetail(),
                "#microsoft.graph.teamsAppUpgradedEventMessageDetail" => new global::AITGraph.Sdk.Models.TeamsAppUpgradedEventMessageDetail(),
                "#microsoft.graph.teamUnarchivedEventMessageDetail" => new global::AITGraph.Sdk.Models.TeamUnarchivedEventMessageDetail(),
                _ => new global::AITGraph.Sdk.Models.EventMessageDetail(),
            };
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "@odata.type", n => { OdataType = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("@odata.type", OdataType);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
