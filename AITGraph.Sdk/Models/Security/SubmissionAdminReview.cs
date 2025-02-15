// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace AITGraph.Sdk.Models.Security
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class SubmissionAdminReview : IAdditionalDataHolder, IParsable
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
        /// <summary>Specifies who reviewed the email. The identification is an email ID or other identity strings.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ReviewBy { get; set; }
#nullable restore
#else
        public string ReviewBy { get; set; }
#endif
        /// <summary>Specifies the date time when the review occurred.</summary>
        public DateTimeOffset? ReviewDateTime { get; set; }
        /// <summary>Specifies what the review result was. The possible values are: notJunk, spam, phishing, malware, allowedByPolicy, blockedByPolicy, spoof, unknown, noResultAvailable, and unknownFutureValue.</summary>
        public global::AITGraph.Sdk.Models.Security.SubmissionResultCategory? ReviewResult { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.Security.SubmissionAdminReview"/> and sets the default values.
        /// </summary>
        public SubmissionAdminReview()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.Security.SubmissionAdminReview"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::AITGraph.Sdk.Models.Security.SubmissionAdminReview CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.Security.SubmissionAdminReview();
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
                { "reviewBy", n => { ReviewBy = n.GetStringValue(); } },
                { "reviewDateTime", n => { ReviewDateTime = n.GetDateTimeOffsetValue(); } },
                { "reviewResult", n => { ReviewResult = n.GetEnumValue<global::AITGraph.Sdk.Models.Security.SubmissionResultCategory>(); } },
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
            writer.WriteStringValue("reviewBy", ReviewBy);
            writer.WriteDateTimeOffsetValue("reviewDateTime", ReviewDateTime);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.Security.SubmissionResultCategory>("reviewResult", ReviewResult);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
