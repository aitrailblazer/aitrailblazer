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
    public partial class CallStartedEventMessageDetail : global::AITGraph.Sdk.Models.EventMessageDetail, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Represents the call event type. Possible values are: call, meeting, screenShare, unknownFutureValue.</summary>
        public global::AITGraph.Sdk.Models.TeamworkCallEventType? CallEventType { get; set; }
        /// <summary>Unique identifier of the call.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CallId { get; set; }
#nullable restore
#else
        public string CallId { get; set; }
#endif
        /// <summary>Initiator of the event.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.IdentitySet? Initiator { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.IdentitySet Initiator { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.CallStartedEventMessageDetail"/> and sets the default values.
        /// </summary>
        public CallStartedEventMessageDetail() : base()
        {
            OdataType = "#microsoft.graph.callStartedEventMessageDetail";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.CallStartedEventMessageDetail"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.CallStartedEventMessageDetail CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.CallStartedEventMessageDetail();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "callEventType", n => { CallEventType = n.GetEnumValue<global::AITGraph.Sdk.Models.TeamworkCallEventType>(); } },
                { "callId", n => { CallId = n.GetStringValue(); } },
                { "initiator", n => { Initiator = n.GetObjectValue<global::AITGraph.Sdk.Models.IdentitySet>(global::AITGraph.Sdk.Models.IdentitySet.CreateFromDiscriminatorValue); } },
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
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.TeamworkCallEventType>("callEventType", CallEventType);
            writer.WriteStringValue("callId", CallId);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.IdentitySet>("initiator", Initiator);
        }
    }
}
#pragma warning restore CS0618
