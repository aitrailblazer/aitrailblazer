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
    public partial class AccessPackageResourceAttribute : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Information about how to set the attribute, currently a accessPackageUserDirectoryAttributeStore object type.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.AccessPackageResourceAttributeDestination? AttributeDestination { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.AccessPackageResourceAttributeDestination AttributeDestination { get; set; }
#endif
        /// <summary>The name of the attribute in the end system. If the destination is accessPackageUserDirectoryAttributeStore, then a user property such as jobTitle or a directory schema extension for the user object type, such as extension_2b676109c7c74ae2b41549205f1947ed_personalTitle.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AttributeName { get; set; }
#nullable restore
#else
        public string AttributeName { get; set; }
#endif
        /// <summary>Information about how to populate the attribute value when an accessPackageAssignmentRequest is being fulfilled, currently a accessPackageResourceAttributeQuestion object type.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.AccessPackageResourceAttributeSource? AttributeSource { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.AccessPackageResourceAttributeSource AttributeSource { get; set; }
#endif
        /// <summary>Unique identifier for the attribute on the access package resource. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>Specifies whether or not an existing attribute value can be edited by the requester.</summary>
        public bool? IsEditable { get; set; }
        /// <summary>Specifies whether the attribute will remain in the end system after an assignment ends.</summary>
        public bool? IsPersistedOnAssignmentRemoval { get; set; }
        /// <summary>The OdataType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OdataType { get; set; }
#nullable restore
#else
        public string OdataType { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.AccessPackageResourceAttribute"/> and sets the default values.
        /// </summary>
        public AccessPackageResourceAttribute()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.AccessPackageResourceAttribute"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::AITGraph.Sdk.Models.AccessPackageResourceAttribute CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.AccessPackageResourceAttribute();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "attributeDestination", n => { AttributeDestination = n.GetObjectValue<global::AITGraph.Sdk.Models.AccessPackageResourceAttributeDestination>(global::AITGraph.Sdk.Models.AccessPackageResourceAttributeDestination.CreateFromDiscriminatorValue); } },
                { "attributeName", n => { AttributeName = n.GetStringValue(); } },
                { "attributeSource", n => { AttributeSource = n.GetObjectValue<global::AITGraph.Sdk.Models.AccessPackageResourceAttributeSource>(global::AITGraph.Sdk.Models.AccessPackageResourceAttributeSource.CreateFromDiscriminatorValue); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "isEditable", n => { IsEditable = n.GetBoolValue(); } },
                { "isPersistedOnAssignmentRemoval", n => { IsPersistedOnAssignmentRemoval = n.GetBoolValue(); } },
                { "@odata.type", n => { OdataType = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.AccessPackageResourceAttributeDestination>("attributeDestination", AttributeDestination);
            writer.WriteStringValue("attributeName", AttributeName);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.AccessPackageResourceAttributeSource>("attributeSource", AttributeSource);
            writer.WriteStringValue("id", Id);
            writer.WriteBoolValue("isEditable", IsEditable);
            writer.WriteBoolValue("isPersistedOnAssignmentRemoval", IsPersistedOnAssignmentRemoval);
            writer.WriteStringValue("@odata.type", OdataType);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
