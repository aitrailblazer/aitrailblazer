// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System;
namespace AITGraph.Sdk.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PersonAward : global::AITGraph.Sdk.Models.ItemFacet, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Descpription of the award or honor.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>Name of the award or honor.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DisplayName { get; set; }
#nullable restore
#else
        public string DisplayName { get; set; }
#endif
        /// <summary>The date that the award or honor was granted.</summary>
        public Date? IssuedDate { get; set; }
        /// <summary>Authority which granted the award or honor.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IssuingAuthority { get; set; }
#nullable restore
#else
        public string IssuingAuthority { get; set; }
#endif
        /// <summary>URL referencing a thumbnail of the award or honor.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ThumbnailUrl { get; set; }
#nullable restore
#else
        public string ThumbnailUrl { get; set; }
#endif
        /// <summary>URL referencing the award or honor.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? WebUrl { get; set; }
#nullable restore
#else
        public string WebUrl { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.PersonAward"/> and sets the default values.
        /// </summary>
        public PersonAward() : base()
        {
            OdataType = "#microsoft.graph.personAward";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.PersonAward"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.PersonAward CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.PersonAward();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "description", n => { Description = n.GetStringValue(); } },
                { "displayName", n => { DisplayName = n.GetStringValue(); } },
                { "issuedDate", n => { IssuedDate = n.GetDateValue(); } },
                { "issuingAuthority", n => { IssuingAuthority = n.GetStringValue(); } },
                { "thumbnailUrl", n => { ThumbnailUrl = n.GetStringValue(); } },
                { "webUrl", n => { WebUrl = n.GetStringValue(); } },
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
            writer.WriteStringValue("description", Description);
            writer.WriteStringValue("displayName", DisplayName);
            writer.WriteDateValue("issuedDate", IssuedDate);
            writer.WriteStringValue("issuingAuthority", IssuingAuthority);
            writer.WriteStringValue("thumbnailUrl", ThumbnailUrl);
            writer.WriteStringValue("webUrl", WebUrl);
        }
    }
}
#pragma warning restore CS0618
