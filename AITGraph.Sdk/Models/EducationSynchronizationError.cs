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
    public partial class EducationSynchronizationError : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Represents the sync entity (school, section, student, teacher).</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? EntryType { get; set; }
#nullable restore
#else
        public string EntryType { get; set; }
#endif
        /// <summary>Represents the error code for this error.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ErrorCode { get; set; }
#nullable restore
#else
        public string ErrorCode { get; set; }
#endif
        /// <summary>Contains a description of the error.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ErrorMessage { get; set; }
#nullable restore
#else
        public string ErrorMessage { get; set; }
#endif
        /// <summary>The unique identifier for the entry.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? JoiningValue { get; set; }
#nullable restore
#else
        public string JoiningValue { get; set; }
#endif
        /// <summary>The time of occurrence of this error.</summary>
        public DateTimeOffset? RecordedDateTime { get; set; }
        /// <summary>The identifier of this error entry.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ReportableIdentifier { get; set; }
#nullable restore
#else
        public string ReportableIdentifier { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.EducationSynchronizationError"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.EducationSynchronizationError CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.EducationSynchronizationError();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "entryType", n => { EntryType = n.GetStringValue(); } },
                { "errorCode", n => { ErrorCode = n.GetStringValue(); } },
                { "errorMessage", n => { ErrorMessage = n.GetStringValue(); } },
                { "joiningValue", n => { JoiningValue = n.GetStringValue(); } },
                { "recordedDateTime", n => { RecordedDateTime = n.GetDateTimeOffsetValue(); } },
                { "reportableIdentifier", n => { ReportableIdentifier = n.GetStringValue(); } },
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
            writer.WriteStringValue("entryType", EntryType);
            writer.WriteStringValue("errorCode", ErrorCode);
            writer.WriteStringValue("errorMessage", ErrorMessage);
            writer.WriteStringValue("joiningValue", JoiningValue);
            writer.WriteDateTimeOffsetValue("recordedDateTime", RecordedDateTime);
            writer.WriteStringValue("reportableIdentifier", ReportableIdentifier);
        }
    }
}
#pragma warning restore CS0618
