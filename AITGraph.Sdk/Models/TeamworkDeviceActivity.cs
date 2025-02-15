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
    public partial class TeamworkDeviceActivity : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The active peripheral devices attached to the device.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.TeamworkActivePeripherals? ActivePeripherals { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.TeamworkActivePeripherals ActivePeripherals { get; set; }
#endif
        /// <summary>Identity of the user who created the device activity document.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.IdentitySet? CreatedBy { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.IdentitySet CreatedBy { get; set; }
#endif
        /// <summary>The UTC date and time when the device activity document was created.</summary>
        public DateTimeOffset? CreatedDateTime { get; set; }
        /// <summary>Identity of the user who last modified the device activity details.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.IdentitySet? LastModifiedBy { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.IdentitySet LastModifiedBy { get; set; }
#endif
        /// <summary>The UTC date and time when the device activity detail was last modified.</summary>
        public DateTimeOffset? LastModifiedDateTime { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.TeamworkDeviceActivity"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.TeamworkDeviceActivity CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.TeamworkDeviceActivity();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "activePeripherals", n => { ActivePeripherals = n.GetObjectValue<global::AITGraph.Sdk.Models.TeamworkActivePeripherals>(global::AITGraph.Sdk.Models.TeamworkActivePeripherals.CreateFromDiscriminatorValue); } },
                { "createdBy", n => { CreatedBy = n.GetObjectValue<global::AITGraph.Sdk.Models.IdentitySet>(global::AITGraph.Sdk.Models.IdentitySet.CreateFromDiscriminatorValue); } },
                { "createdDateTime", n => { CreatedDateTime = n.GetDateTimeOffsetValue(); } },
                { "lastModifiedBy", n => { LastModifiedBy = n.GetObjectValue<global::AITGraph.Sdk.Models.IdentitySet>(global::AITGraph.Sdk.Models.IdentitySet.CreateFromDiscriminatorValue); } },
                { "lastModifiedDateTime", n => { LastModifiedDateTime = n.GetDateTimeOffsetValue(); } },
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
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.TeamworkActivePeripherals>("activePeripherals", ActivePeripherals);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.IdentitySet>("createdBy", CreatedBy);
            writer.WriteDateTimeOffsetValue("createdDateTime", CreatedDateTime);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.IdentitySet>("lastModifiedBy", LastModifiedBy);
            writer.WriteDateTimeOffsetValue("lastModifiedDateTime", LastModifiedDateTime);
        }
    }
}
#pragma warning restore CS0618
