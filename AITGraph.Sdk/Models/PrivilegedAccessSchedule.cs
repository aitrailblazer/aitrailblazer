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
    public partial class PrivilegedAccessSchedule : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The createdDateTime property</summary>
        public DateTimeOffset? CreatedDateTime { get; set; }
        /// <summary>The createdUsing property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CreatedUsing { get; set; }
#nullable restore
#else
        public string CreatedUsing { get; set; }
#endif
        /// <summary>The modifiedDateTime property</summary>
        public DateTimeOffset? ModifiedDateTime { get; set; }
        /// <summary>The scheduleInfo property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.RequestSchedule? ScheduleInfo { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.RequestSchedule ScheduleInfo { get; set; }
#endif
        /// <summary>The status property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Status { get; set; }
#nullable restore
#else
        public string Status { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.PrivilegedAccessSchedule"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.PrivilegedAccessSchedule CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            var mappingValue = parseNode.GetChildNode("@odata.type")?.GetStringValue();
            return mappingValue switch
            {
                "#microsoft.graph.privilegedAccessGroupAssignmentSchedule" => new global::AITGraph.Sdk.Models.PrivilegedAccessGroupAssignmentSchedule(),
                "#microsoft.graph.privilegedAccessGroupEligibilitySchedule" => new global::AITGraph.Sdk.Models.PrivilegedAccessGroupEligibilitySchedule(),
                _ => new global::AITGraph.Sdk.Models.PrivilegedAccessSchedule(),
            };
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "createdDateTime", n => { CreatedDateTime = n.GetDateTimeOffsetValue(); } },
                { "createdUsing", n => { CreatedUsing = n.GetStringValue(); } },
                { "modifiedDateTime", n => { ModifiedDateTime = n.GetDateTimeOffsetValue(); } },
                { "scheduleInfo", n => { ScheduleInfo = n.GetObjectValue<global::AITGraph.Sdk.Models.RequestSchedule>(global::AITGraph.Sdk.Models.RequestSchedule.CreateFromDiscriminatorValue); } },
                { "status", n => { Status = n.GetStringValue(); } },
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
            writer.WriteDateTimeOffsetValue("createdDateTime", CreatedDateTime);
            writer.WriteStringValue("createdUsing", CreatedUsing);
            writer.WriteDateTimeOffsetValue("modifiedDateTime", ModifiedDateTime);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.RequestSchedule>("scheduleInfo", ScheduleInfo);
            writer.WriteStringValue("status", Status);
        }
    }
}
#pragma warning restore CS0618
