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
    public partial class SharepointIds : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The unique identifier (guid) for the item&apos;s list in SharePoint.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ListId { get; set; }
#nullable restore
#else
        public string ListId { get; set; }
#endif
        /// <summary>An integer identifier for the item within the containing list.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ListItemId { get; set; }
#nullable restore
#else
        public string ListItemId { get; set; }
#endif
        /// <summary>The unique identifier (guid) for the item within OneDrive for Business or a SharePoint site.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ListItemUniqueId { get; set; }
#nullable restore
#else
        public string ListItemUniqueId { get; set; }
#endif
        /// <summary>The OdataType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OdataType { get; set; }
#nullable restore
#else
        public string OdataType { get; set; }
#endif
        /// <summary>The unique identifier (guid) for the item&apos;s site collection (SPSite).</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SiteId { get; set; }
#nullable restore
#else
        public string SiteId { get; set; }
#endif
        /// <summary>The SharePoint URL for the site that contains the item.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SiteUrl { get; set; }
#nullable restore
#else
        public string SiteUrl { get; set; }
#endif
        /// <summary>The unique identifier (guid) for the tenancy.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? TenantId { get; set; }
#nullable restore
#else
        public string TenantId { get; set; }
#endif
        /// <summary>The unique identifier (guid) for the item&apos;s site (SPWeb).</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? WebId { get; set; }
#nullable restore
#else
        public string WebId { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.SharepointIds"/> and sets the default values.
        /// </summary>
        public SharepointIds()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.SharepointIds"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::AITGraph.Sdk.Models.SharepointIds CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.SharepointIds();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "listId", n => { ListId = n.GetStringValue(); } },
                { "listItemId", n => { ListItemId = n.GetStringValue(); } },
                { "listItemUniqueId", n => { ListItemUniqueId = n.GetStringValue(); } },
                { "@odata.type", n => { OdataType = n.GetStringValue(); } },
                { "siteId", n => { SiteId = n.GetStringValue(); } },
                { "siteUrl", n => { SiteUrl = n.GetStringValue(); } },
                { "tenantId", n => { TenantId = n.GetStringValue(); } },
                { "webId", n => { WebId = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("listId", ListId);
            writer.WriteStringValue("listItemId", ListItemId);
            writer.WriteStringValue("listItemUniqueId", ListItemUniqueId);
            writer.WriteStringValue("@odata.type", OdataType);
            writer.WriteStringValue("siteId", SiteId);
            writer.WriteStringValue("siteUrl", SiteUrl);
            writer.WriteStringValue("tenantId", TenantId);
            writer.WriteStringValue("webId", WebId);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
