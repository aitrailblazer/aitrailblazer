// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace AITGraph.Sdk.Models.TermStore
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class Store : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Default language of the term store.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DefaultLanguageTag { get; set; }
#nullable restore
#else
        public string DefaultLanguageTag { get; set; }
#endif
        /// <summary>Collection of all groups available in the term store.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.TermStore.Group>? Groups { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.TermStore.Group> Groups { get; set; }
#endif
        /// <summary>List of languages for the term store.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? LanguageTags { get; set; }
#nullable restore
#else
        public List<string> LanguageTags { get; set; }
#endif
        /// <summary>Collection of all sets available in the term store.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.TermStore.Set>? Sets { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.TermStore.Set> Sets { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.TermStore.Store"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.TermStore.Store CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.TermStore.Store();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "defaultLanguageTag", n => { DefaultLanguageTag = n.GetStringValue(); } },
                { "groups", n => { Groups = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.TermStore.Group>(global::AITGraph.Sdk.Models.TermStore.Group.CreateFromDiscriminatorValue)?.AsList(); } },
                { "languageTags", n => { LanguageTags = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "sets", n => { Sets = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.TermStore.Set>(global::AITGraph.Sdk.Models.TermStore.Set.CreateFromDiscriminatorValue)?.AsList(); } },
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
            writer.WriteStringValue("defaultLanguageTag", DefaultLanguageTag);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.TermStore.Group>("groups", Groups);
            writer.WriteCollectionOfPrimitiveValues<string>("languageTags", LanguageTags);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.TermStore.Set>("sets", Sets);
        }
    }
}
#pragma warning restore CS0618
