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
    public partial class ReferenceAttachment : global::AITGraph.Sdk.Models.Attachment, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Specifies whether the attachment is a link to a folder. Must set this to true if sourceUrl is a link to a folder. Optional.</summary>
        public bool? IsFolder { get; set; }
        /// <summary>Specifies the permissions granted for the attachment by the type of provider in providerType. Possible values are: other, view, edit, anonymousView, anonymousEdit, organizationView, organizationEdit. Optional.</summary>
        public global::AITGraph.Sdk.Models.ReferenceAttachmentPermission? Permission { get; set; }
        /// <summary>Applies to only a reference attachment of an image - URL to get a preview image. Use thumbnailUrl and previewUrl only when sourceUrl identifies an image file. Optional.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PreviewUrl { get; set; }
#nullable restore
#else
        public string PreviewUrl { get; set; }
#endif
        /// <summary>The type of provider that supports an attachment of this contentType. Possible values are: other, oneDriveBusiness, oneDriveConsumer, dropbox. Optional.</summary>
        public global::AITGraph.Sdk.Models.ReferenceAttachmentProvider? ProviderType { get; set; }
        /// <summary>URL to get the attachment content. If this is a URL to a folder, then for the folder to be displayed correctly in Outlook or Outlook on the web, set isFolder to true. Required.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SourceUrl { get; set; }
#nullable restore
#else
        public string SourceUrl { get; set; }
#endif
        /// <summary>Applies to only a reference attachment of an image - URL to get a thumbnail image. Use thumbnailUrl and previewUrl only when sourceUrl identifies an image file. Optional.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ThumbnailUrl { get; set; }
#nullable restore
#else
        public string ThumbnailUrl { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.ReferenceAttachment"/> and sets the default values.
        /// </summary>
        public ReferenceAttachment() : base()
        {
            OdataType = "#microsoft.graph.referenceAttachment";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.ReferenceAttachment"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.ReferenceAttachment CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.ReferenceAttachment();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "isFolder", n => { IsFolder = n.GetBoolValue(); } },
                { "permission", n => { Permission = n.GetEnumValue<global::AITGraph.Sdk.Models.ReferenceAttachmentPermission>(); } },
                { "previewUrl", n => { PreviewUrl = n.GetStringValue(); } },
                { "providerType", n => { ProviderType = n.GetEnumValue<global::AITGraph.Sdk.Models.ReferenceAttachmentProvider>(); } },
                { "sourceUrl", n => { SourceUrl = n.GetStringValue(); } },
                { "thumbnailUrl", n => { ThumbnailUrl = n.GetStringValue(); } },
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
            writer.WriteBoolValue("isFolder", IsFolder);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.ReferenceAttachmentPermission>("permission", Permission);
            writer.WriteStringValue("previewUrl", PreviewUrl);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.ReferenceAttachmentProvider>("providerType", ProviderType);
            writer.WriteStringValue("sourceUrl", SourceUrl);
            writer.WriteStringValue("thumbnailUrl", ThumbnailUrl);
        }
    }
}
#pragma warning restore CS0618
