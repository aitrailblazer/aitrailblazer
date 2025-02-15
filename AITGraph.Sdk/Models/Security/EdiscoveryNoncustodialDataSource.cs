// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace AITGraph.Sdk.Models.Security
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class EdiscoveryNoncustodialDataSource : global::AITGraph.Sdk.Models.Security.DataSourceContainer, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>User source or SharePoint site data source as non-custodial data source.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.Security.DataSource? DataSource { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.Security.DataSource DataSource { get; set; }
#endif
        /// <summary>Operation entity that represents the latest indexing for the non-custodial data source.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.Security.EdiscoveryIndexOperation? LastIndexOperation { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.Security.EdiscoveryIndexOperation LastIndexOperation { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.Security.EdiscoveryNoncustodialDataSource"/> and sets the default values.
        /// </summary>
        public EdiscoveryNoncustodialDataSource() : base()
        {
            OdataType = "#microsoft.graph.security.ediscoveryNoncustodialDataSource";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.Security.EdiscoveryNoncustodialDataSource"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.Security.EdiscoveryNoncustodialDataSource CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.Security.EdiscoveryNoncustodialDataSource();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "dataSource", n => { DataSource = n.GetObjectValue<global::AITGraph.Sdk.Models.Security.DataSource>(global::AITGraph.Sdk.Models.Security.DataSource.CreateFromDiscriminatorValue); } },
                { "lastIndexOperation", n => { LastIndexOperation = n.GetObjectValue<global::AITGraph.Sdk.Models.Security.EdiscoveryIndexOperation>(global::AITGraph.Sdk.Models.Security.EdiscoveryIndexOperation.CreateFromDiscriminatorValue); } },
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
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.Security.DataSource>("dataSource", DataSource);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.Security.EdiscoveryIndexOperation>("lastIndexOperation", LastIndexOperation);
        }
    }
}
#pragma warning restore CS0618
