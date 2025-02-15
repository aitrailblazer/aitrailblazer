// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace AITGraph.Sdk.Models
{
    /// <summary>
    /// The category entity stores the category of a group policy definition
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class GroupPolicyCategory : global::AITGraph.Sdk.Models.Entity, IParsable
    {
        /// <summary>The children categories</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.GroupPolicyCategory>? Children { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.GroupPolicyCategory> Children { get; set; }
#endif
        /// <summary>The id of the definition file the category came from</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.GroupPolicyDefinitionFile? DefinitionFile { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.GroupPolicyDefinitionFile DefinitionFile { get; set; }
#endif
        /// <summary>The immediate GroupPolicyDefinition children of the category</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.GroupPolicyDefinition>? Definitions { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.GroupPolicyDefinition> Definitions { get; set; }
#endif
        /// <summary>The string id of the category&apos;s display name</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DisplayName { get; set; }
#nullable restore
#else
        public string DisplayName { get; set; }
#endif
        /// <summary>Category Ingestion source</summary>
        public global::AITGraph.Sdk.Models.IngestionSource? IngestionSource { get; set; }
        /// <summary>Defines if the category is a root category</summary>
        public bool? IsRoot { get; set; }
        /// <summary>The date and time the entity was last modified.</summary>
        public DateTimeOffset? LastModifiedDateTime { get; set; }
        /// <summary>The parent category</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.GroupPolicyCategory? Parent { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.GroupPolicyCategory Parent { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.GroupPolicyCategory"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.GroupPolicyCategory CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.GroupPolicyCategory();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "children", n => { Children = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.GroupPolicyCategory>(global::AITGraph.Sdk.Models.GroupPolicyCategory.CreateFromDiscriminatorValue)?.AsList(); } },
                { "definitionFile", n => { DefinitionFile = n.GetObjectValue<global::AITGraph.Sdk.Models.GroupPolicyDefinitionFile>(global::AITGraph.Sdk.Models.GroupPolicyDefinitionFile.CreateFromDiscriminatorValue); } },
                { "definitions", n => { Definitions = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.GroupPolicyDefinition>(global::AITGraph.Sdk.Models.GroupPolicyDefinition.CreateFromDiscriminatorValue)?.AsList(); } },
                { "displayName", n => { DisplayName = n.GetStringValue(); } },
                { "ingestionSource", n => { IngestionSource = n.GetEnumValue<global::AITGraph.Sdk.Models.IngestionSource>(); } },
                { "isRoot", n => { IsRoot = n.GetBoolValue(); } },
                { "lastModifiedDateTime", n => { LastModifiedDateTime = n.GetDateTimeOffsetValue(); } },
                { "parent", n => { Parent = n.GetObjectValue<global::AITGraph.Sdk.Models.GroupPolicyCategory>(global::AITGraph.Sdk.Models.GroupPolicyCategory.CreateFromDiscriminatorValue); } },
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
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.GroupPolicyCategory>("children", Children);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.GroupPolicyDefinitionFile>("definitionFile", DefinitionFile);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.GroupPolicyDefinition>("definitions", Definitions);
            writer.WriteStringValue("displayName", DisplayName);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.IngestionSource>("ingestionSource", IngestionSource);
            writer.WriteBoolValue("isRoot", IsRoot);
            writer.WriteDateTimeOffsetValue("lastModifiedDateTime", LastModifiedDateTime);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.GroupPolicyCategory>("parent", Parent);
        }
    }
}
#pragma warning restore CS0618
