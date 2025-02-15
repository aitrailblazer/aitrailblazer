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
    public partial class AccessPackageResourceRequest : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The accessPackageResource property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.AccessPackageResource? AccessPackageResource { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.AccessPackageResource AccessPackageResource { get; set; }
#endif
        /// <summary>The unique ID of the access package catalog.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CatalogId { get; set; }
#nullable restore
#else
        public string CatalogId { get; set; }
#endif
        /// <summary>The executeImmediately property</summary>
        public bool? ExecuteImmediately { get; set; }
        /// <summary>The Timestamp type represents date and time information using ISO 8601 format and is always in UTC time. For example, midnight UTC on Jan 1, 2014 is 2014-01-01T00:00:00Z</summary>
        public DateTimeOffset? ExpirationDateTime { get; set; }
        /// <summary>If set, does not add the resource.</summary>
        public bool? IsValidationOnly { get; set; }
        /// <summary>The requestor&apos;s justification for adding or removing the resource.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Justification { get; set; }
#nullable restore
#else
        public string Justification { get; set; }
#endif
        /// <summary>Read-only. Nullable. Supports $expand.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.AccessPackageSubject? Requestor { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.AccessPackageSubject Requestor { get; set; }
#endif
        /// <summary>The outcome of whether the service was able to add the resource to the catalog.  The value is Delivered if the resource was added or removed. Read-Only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? RequestState { get; set; }
#nullable restore
#else
        public string RequestState { get; set; }
#endif
        /// <summary>The requestStatus property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? RequestStatus { get; set; }
#nullable restore
#else
        public string RequestStatus { get; set; }
#endif
        /// <summary>Use AdminAdd to add a resource, if the caller is an administrator or resource owner, AdminUpdate to update a resource, or AdminRemove to remove a resource.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? RequestType { get; set; }
#nullable restore
#else
        public string RequestType { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.AccessPackageResourceRequest"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.AccessPackageResourceRequest CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.AccessPackageResourceRequest();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "accessPackageResource", n => { AccessPackageResource = n.GetObjectValue<global::AITGraph.Sdk.Models.AccessPackageResource>(global::AITGraph.Sdk.Models.AccessPackageResource.CreateFromDiscriminatorValue); } },
                { "catalogId", n => { CatalogId = n.GetStringValue(); } },
                { "executeImmediately", n => { ExecuteImmediately = n.GetBoolValue(); } },
                { "expirationDateTime", n => { ExpirationDateTime = n.GetDateTimeOffsetValue(); } },
                { "isValidationOnly", n => { IsValidationOnly = n.GetBoolValue(); } },
                { "justification", n => { Justification = n.GetStringValue(); } },
                { "requestState", n => { RequestState = n.GetStringValue(); } },
                { "requestStatus", n => { RequestStatus = n.GetStringValue(); } },
                { "requestType", n => { RequestType = n.GetStringValue(); } },
                { "requestor", n => { Requestor = n.GetObjectValue<global::AITGraph.Sdk.Models.AccessPackageSubject>(global::AITGraph.Sdk.Models.AccessPackageSubject.CreateFromDiscriminatorValue); } },
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
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.AccessPackageResource>("accessPackageResource", AccessPackageResource);
            writer.WriteStringValue("catalogId", CatalogId);
            writer.WriteBoolValue("executeImmediately", ExecuteImmediately);
            writer.WriteDateTimeOffsetValue("expirationDateTime", ExpirationDateTime);
            writer.WriteBoolValue("isValidationOnly", IsValidationOnly);
            writer.WriteStringValue("justification", Justification);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.AccessPackageSubject>("requestor", Requestor);
            writer.WriteStringValue("requestState", RequestState);
            writer.WriteStringValue("requestStatus", RequestStatus);
            writer.WriteStringValue("requestType", RequestType);
        }
    }
}
#pragma warning restore CS0618
