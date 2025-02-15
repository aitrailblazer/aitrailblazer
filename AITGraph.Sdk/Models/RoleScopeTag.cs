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
    /// Role Scope Tag
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class RoleScopeTag : global::AITGraph.Sdk.Models.Entity, IParsable
    {
        /// <summary>The list of assignments for this Role Scope Tag.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.RoleScopeTagAutoAssignment>? Assignments { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.RoleScopeTagAutoAssignment> Assignments { get; set; }
#endif
        /// <summary>Description of the Role Scope Tag.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>The display or friendly name of the Role Scope Tag.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DisplayName { get; set; }
#nullable restore
#else
        public string DisplayName { get; set; }
#endif
        /// <summary>Description of the Role Scope Tag. This property is read-only.</summary>
        public bool? IsBuiltIn { get; private set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.RoleScopeTag"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.RoleScopeTag CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.RoleScopeTag();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "assignments", n => { Assignments = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.RoleScopeTagAutoAssignment>(global::AITGraph.Sdk.Models.RoleScopeTagAutoAssignment.CreateFromDiscriminatorValue)?.AsList(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "displayName", n => { DisplayName = n.GetStringValue(); } },
                { "isBuiltIn", n => { IsBuiltIn = n.GetBoolValue(); } },
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
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.RoleScopeTagAutoAssignment>("assignments", Assignments);
            writer.WriteStringValue("description", Description);
            writer.WriteStringValue("displayName", DisplayName);
        }
    }
}
#pragma warning restore CS0618
