// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace CognitiveServices.Sdk.Models
{
    /// <summary>
    /// Defines a video object that is relevant to the query.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
    public partial class VideoObject : global::CognitiveServices.Sdk.Models.MediaObject, IParsable
    {
        /// <summary>The allowHttpsEmbed property</summary>
        public bool? AllowHttpsEmbed { get; private set; }
        /// <summary>The allowMobileEmbed property</summary>
        public bool? AllowMobileEmbed { get; private set; }
        /// <summary>The embedHtml property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? EmbedHtml { get; private set; }
#nullable restore
#else
        public string EmbedHtml { get; private set; }
#endif
        /// <summary>The isSuperfresh property</summary>
        public bool? IsSuperfresh { get; private set; }
        /// <summary>The motionThumbnailId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? MotionThumbnailId { get; private set; }
#nullable restore
#else
        public string MotionThumbnailId { get; private set; }
#endif
        /// <summary>The motionThumbnailUrl property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? MotionThumbnailUrl { get; private set; }
#nullable restore
#else
        public string MotionThumbnailUrl { get; private set; }
#endif
        /// <summary>Defines an image</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::CognitiveServices.Sdk.Models.ImageObject? Thumbnail { get; set; }
#nullable restore
#else
        public global::CognitiveServices.Sdk.Models.ImageObject Thumbnail { get; set; }
#endif
        /// <summary>The videoId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? VideoId { get; private set; }
#nullable restore
#else
        public string VideoId { get; private set; }
#endif
        /// <summary>The viewCount property</summary>
        public int? ViewCount { get; private set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::CognitiveServices.Sdk.Models.VideoObject"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::CognitiveServices.Sdk.Models.VideoObject CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::CognitiveServices.Sdk.Models.VideoObject();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "allowHttpsEmbed", n => { AllowHttpsEmbed = n.GetBoolValue(); } },
                { "allowMobileEmbed", n => { AllowMobileEmbed = n.GetBoolValue(); } },
                { "embedHtml", n => { EmbedHtml = n.GetStringValue(); } },
                { "isSuperfresh", n => { IsSuperfresh = n.GetBoolValue(); } },
                { "motionThumbnailId", n => { MotionThumbnailId = n.GetStringValue(); } },
                { "motionThumbnailUrl", n => { MotionThumbnailUrl = n.GetStringValue(); } },
                { "thumbnail", n => { Thumbnail = n.GetObjectValue<global::CognitiveServices.Sdk.Models.ImageObject>(global::CognitiveServices.Sdk.Models.ImageObject.CreateFromDiscriminatorValue); } },
                { "videoId", n => { VideoId = n.GetStringValue(); } },
                { "viewCount", n => { ViewCount = n.GetIntValue(); } },
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
            writer.WriteObjectValue<global::CognitiveServices.Sdk.Models.ImageObject>("thumbnail", Thumbnail);
        }
    }
}
#pragma warning restore CS0618
