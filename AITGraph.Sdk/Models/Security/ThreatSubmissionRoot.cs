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
    public partial class ThreatSubmissionRoot : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The emailThreats property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Security.EmailThreatSubmission>? EmailThreats { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Security.EmailThreatSubmission> EmailThreats { get; set; }
#endif
        /// <summary>The emailThreatSubmissionPolicies property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Security.EmailThreatSubmissionPolicy>? EmailThreatSubmissionPolicies { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Security.EmailThreatSubmissionPolicy> EmailThreatSubmissionPolicies { get; set; }
#endif
        /// <summary>The fileThreats property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Security.FileThreatSubmission>? FileThreats { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Security.FileThreatSubmission> FileThreats { get; set; }
#endif
        /// <summary>The urlThreats property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Security.UrlThreatSubmission>? UrlThreats { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Security.UrlThreatSubmission> UrlThreats { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.Security.ThreatSubmissionRoot"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.Security.ThreatSubmissionRoot CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.Security.ThreatSubmissionRoot();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "emailThreatSubmissionPolicies", n => { EmailThreatSubmissionPolicies = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Security.EmailThreatSubmissionPolicy>(global::AITGraph.Sdk.Models.Security.EmailThreatSubmissionPolicy.CreateFromDiscriminatorValue)?.AsList(); } },
                { "emailThreats", n => { EmailThreats = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Security.EmailThreatSubmission>(global::AITGraph.Sdk.Models.Security.EmailThreatSubmission.CreateFromDiscriminatorValue)?.AsList(); } },
                { "fileThreats", n => { FileThreats = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Security.FileThreatSubmission>(global::AITGraph.Sdk.Models.Security.FileThreatSubmission.CreateFromDiscriminatorValue)?.AsList(); } },
                { "urlThreats", n => { UrlThreats = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Security.UrlThreatSubmission>(global::AITGraph.Sdk.Models.Security.UrlThreatSubmission.CreateFromDiscriminatorValue)?.AsList(); } },
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
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Security.EmailThreatSubmission>("emailThreats", EmailThreats);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Security.EmailThreatSubmissionPolicy>("emailThreatSubmissionPolicies", EmailThreatSubmissionPolicies);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Security.FileThreatSubmission>("fileThreats", FileThreats);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Security.UrlThreatSubmission>("urlThreats", UrlThreats);
        }
    }
}
#pragma warning restore CS0618
