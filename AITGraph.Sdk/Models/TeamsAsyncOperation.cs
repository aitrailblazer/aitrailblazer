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
    public partial class TeamsAsyncOperation : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Number of times the operation was attempted before being marked successful or failed.</summary>
        public int? AttemptsCount { get; set; }
        /// <summary>Time when the operation was created.</summary>
        public DateTimeOffset? CreatedDateTime { get; set; }
        /// <summary>Any error that causes the async operation to fail.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.OperationError? Error { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.OperationError Error { get; set; }
#endif
        /// <summary>Time when the async operation was last updated.</summary>
        public DateTimeOffset? LastActionDateTime { get; set; }
        /// <summary>The operationType property</summary>
        public global::AITGraph.Sdk.Models.TeamsAsyncOperationType? OperationType { get; set; }
        /// <summary>The status property</summary>
        public global::AITGraph.Sdk.Models.TeamsAsyncOperationStatus? Status { get; set; }
        /// <summary>The ID of the object that&apos;s created or modified as result of this async operation, typically a team.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? TargetResourceId { get; set; }
#nullable restore
#else
        public string TargetResourceId { get; set; }
#endif
        /// <summary>The location of the object that&apos;s created or modified as result of this async operation. This URL should be treated as an opaque value and not parsed into its component paths.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? TargetResourceLocation { get; set; }
#nullable restore
#else
        public string TargetResourceLocation { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.TeamsAsyncOperation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.TeamsAsyncOperation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.TeamsAsyncOperation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "attemptsCount", n => { AttemptsCount = n.GetIntValue(); } },
                { "createdDateTime", n => { CreatedDateTime = n.GetDateTimeOffsetValue(); } },
                { "error", n => { Error = n.GetObjectValue<global::AITGraph.Sdk.Models.OperationError>(global::AITGraph.Sdk.Models.OperationError.CreateFromDiscriminatorValue); } },
                { "lastActionDateTime", n => { LastActionDateTime = n.GetDateTimeOffsetValue(); } },
                { "operationType", n => { OperationType = n.GetEnumValue<global::AITGraph.Sdk.Models.TeamsAsyncOperationType>(); } },
                { "status", n => { Status = n.GetEnumValue<global::AITGraph.Sdk.Models.TeamsAsyncOperationStatus>(); } },
                { "targetResourceId", n => { TargetResourceId = n.GetStringValue(); } },
                { "targetResourceLocation", n => { TargetResourceLocation = n.GetStringValue(); } },
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
            writer.WriteIntValue("attemptsCount", AttemptsCount);
            writer.WriteDateTimeOffsetValue("createdDateTime", CreatedDateTime);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.OperationError>("error", Error);
            writer.WriteDateTimeOffsetValue("lastActionDateTime", LastActionDateTime);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.TeamsAsyncOperationType>("operationType", OperationType);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.TeamsAsyncOperationStatus>("status", Status);
            writer.WriteStringValue("targetResourceId", TargetResourceId);
            writer.WriteStringValue("targetResourceLocation", TargetResourceLocation);
        }
    }
}
#pragma warning restore CS0618
