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
    public partial class Restaurant : global::CognitiveServices.Sdk.Models.FoodEstablishment, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The acceptsReservations property</summary>
        public bool? AcceptsReservations { get; private set; }
        /// <summary>The menuUrl property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? MenuUrl { get; private set; }
#nullable restore
#else
        public string MenuUrl { get; private set; }
#endif
        /// <summary>The reservationUrl property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ReservationUrl { get; private set; }
#nullable restore
#else
        public string ReservationUrl { get; private set; }
#endif
        /// <summary>The servesCuisine property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? ServesCuisine { get; private set; }
#nullable restore
#else
        public List<string> ServesCuisine { get; private set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::CognitiveServices.Sdk.Models.Restaurant"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::CognitiveServices.Sdk.Models.Restaurant CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::CognitiveServices.Sdk.Models.Restaurant();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "acceptsReservations", n => { AcceptsReservations = n.GetBoolValue(); } },
                { "menuUrl", n => { MenuUrl = n.GetStringValue(); } },
                { "reservationUrl", n => { ReservationUrl = n.GetStringValue(); } },
                { "servesCuisine", n => { ServesCuisine = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
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
        }
    }
}
#pragma warning restore CS0618
