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
    public partial class NotifyUserAction : global::AITGraph.Sdk.Models.DlpActionInfo, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The actionLastModifiedDateTime property</summary>
        public DateTimeOffset? ActionLastModifiedDateTime { get; set; }
        /// <summary>The emailText property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? EmailText { get; set; }
#nullable restore
#else
        public string EmailText { get; set; }
#endif
        /// <summary>The policyTip property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PolicyTip { get; set; }
#nullable restore
#else
        public string PolicyTip { get; set; }
#endif
        /// <summary>The recipients property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Recipients { get; set; }
#nullable restore
#else
        public List<string> Recipients { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.NotifyUserAction"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.NotifyUserAction CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.NotifyUserAction();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "actionLastModifiedDateTime", n => { ActionLastModifiedDateTime = n.GetDateTimeOffsetValue(); } },
                { "emailText", n => { EmailText = n.GetStringValue(); } },
                { "policyTip", n => { PolicyTip = n.GetStringValue(); } },
                { "recipients", n => { Recipients = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
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
            writer.WriteDateTimeOffsetValue("actionLastModifiedDateTime", ActionLastModifiedDateTime);
            writer.WriteStringValue("emailText", EmailText);
            writer.WriteStringValue("policyTip", PolicyTip);
            writer.WriteCollectionOfPrimitiveValues<string>("recipients", Recipients);
        }
    }
}
#pragma warning restore CS0618
