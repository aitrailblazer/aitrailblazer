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
    public partial class ServicePrincipalCreationConditionSet : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The applicationIds property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? ApplicationIds { get; set; }
#nullable restore
#else
        public List<string> ApplicationIds { get; set; }
#endif
        /// <summary>The applicationPublisherIds property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? ApplicationPublisherIds { get; set; }
#nullable restore
#else
        public List<string> ApplicationPublisherIds { get; set; }
#endif
        /// <summary>The applicationsFromVerifiedPublisherOnly property</summary>
        public bool? ApplicationsFromVerifiedPublisherOnly { get; set; }
        /// <summary>The applicationTenantIds property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? ApplicationTenantIds { get; set; }
#nullable restore
#else
        public List<string> ApplicationTenantIds { get; set; }
#endif
        /// <summary>The certifiedApplicationsOnly property</summary>
        public bool? CertifiedApplicationsOnly { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.ServicePrincipalCreationConditionSet"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.ServicePrincipalCreationConditionSet CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.ServicePrincipalCreationConditionSet();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "applicationIds", n => { ApplicationIds = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "applicationPublisherIds", n => { ApplicationPublisherIds = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "applicationTenantIds", n => { ApplicationTenantIds = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "applicationsFromVerifiedPublisherOnly", n => { ApplicationsFromVerifiedPublisherOnly = n.GetBoolValue(); } },
                { "certifiedApplicationsOnly", n => { CertifiedApplicationsOnly = n.GetBoolValue(); } },
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
            writer.WriteCollectionOfPrimitiveValues<string>("applicationIds", ApplicationIds);
            writer.WriteCollectionOfPrimitiveValues<string>("applicationPublisherIds", ApplicationPublisherIds);
            writer.WriteBoolValue("applicationsFromVerifiedPublisherOnly", ApplicationsFromVerifiedPublisherOnly);
            writer.WriteCollectionOfPrimitiveValues<string>("applicationTenantIds", ApplicationTenantIds);
            writer.WriteBoolValue("certifiedApplicationsOnly", CertifiedApplicationsOnly);
        }
    }
}
#pragma warning restore CS0618
