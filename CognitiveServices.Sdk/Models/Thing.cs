// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace CognitiveServices.Sdk.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
    #pragma warning disable CS1591
    public partial class Thing : global::CognitiveServices.Sdk.Models.Response, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>An ID that uniquely identifies this item.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? BingId { get; private set; }
#nullable restore
#else
        public string BingId { get; private set; }
#endif
        /// <summary>A short description of the item.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; private set; }
#nullable restore
#else
        public string Description { get; private set; }
#endif
        /// <summary>Defines additional information about an entity such as type hints.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::CognitiveServices.Sdk.Models.EntitiesEntityPresentationInfo? EntityPresentationInfo { get; set; }
#nullable restore
#else
        public global::CognitiveServices.Sdk.Models.EntitiesEntityPresentationInfo EntityPresentationInfo { get; set; }
#endif
        /// <summary>Defines an image</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::CognitiveServices.Sdk.Models.ImageObject? Image { get; set; }
#nullable restore
#else
        public global::CognitiveServices.Sdk.Models.ImageObject Image { get; set; }
#endif
        /// <summary>The name of the thing represented by this object.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; private set; }
#nullable restore
#else
        public string Name { get; private set; }
#endif
        /// <summary>The URL to get more information about the thing represented by this object.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Url { get; private set; }
#nullable restore
#else
        public string Url { get; private set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::CognitiveServices.Sdk.Models.Thing"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::CognitiveServices.Sdk.Models.Thing CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            var mappingValue = parseNode.GetChildNode("_type")?.GetStringValue();
            return mappingValue switch
            {
                "Airport" => new global::CognitiveServices.Sdk.Models.Airport(),
                "CivicStructure" => new global::CognitiveServices.Sdk.Models.CivicStructure(),
                "CreativeWork" => new global::CognitiveServices.Sdk.Models.CreativeWork(),
                "EntertainmentBusiness" => new global::CognitiveServices.Sdk.Models.EntertainmentBusiness(),
                "FoodEstablishment" => new global::CognitiveServices.Sdk.Models.FoodEstablishment(),
                "Hotel" => new global::CognitiveServices.Sdk.Models.Hotel(),
                "ImageObject" => new global::CognitiveServices.Sdk.Models.ImageObject(),
                "License" => new global::CognitiveServices.Sdk.Models.License(),
                "LocalBusiness" => new global::CognitiveServices.Sdk.Models.LocalBusiness(),
                "LodgingBusiness" => new global::CognitiveServices.Sdk.Models.LodgingBusiness(),
                "MediaObject" => new global::CognitiveServices.Sdk.Models.MediaObject(),
                "MovieTheater" => new global::CognitiveServices.Sdk.Models.MovieTheater(),
                "Organization" => new global::CognitiveServices.Sdk.Models.Organization(),
                "Place" => new global::CognitiveServices.Sdk.Models.Place(),
                "PostalAddress" => new global::CognitiveServices.Sdk.Models.PostalAddress(),
                "Restaurant" => new global::CognitiveServices.Sdk.Models.Restaurant(),
                "StructuredValue" => new global::CognitiveServices.Sdk.Models.StructuredValue(),
                "TouristAttraction" => new global::CognitiveServices.Sdk.Models.TouristAttraction(),
                _ => new global::CognitiveServices.Sdk.Models.Thing(),
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
                { "bingId", n => { BingId = n.GetStringValue(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "entityPresentationInfo", n => { EntityPresentationInfo = n.GetObjectValue<global::CognitiveServices.Sdk.Models.EntitiesEntityPresentationInfo>(global::CognitiveServices.Sdk.Models.EntitiesEntityPresentationInfo.CreateFromDiscriminatorValue); } },
                { "image", n => { Image = n.GetObjectValue<global::CognitiveServices.Sdk.Models.ImageObject>(global::CognitiveServices.Sdk.Models.ImageObject.CreateFromDiscriminatorValue); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "url", n => { Url = n.GetStringValue(); } },
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
            writer.WriteObjectValue<global::CognitiveServices.Sdk.Models.EntitiesEntityPresentationInfo>("entityPresentationInfo", EntityPresentationInfo);
            writer.WriteObjectValue<global::CognitiveServices.Sdk.Models.ImageObject>("image", Image);
        }
    }
}
#pragma warning restore CS0618
