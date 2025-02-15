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
    public partial class WorkbookWorksheet : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Returns collection of charts that are part of the worksheet. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.WorkbookChart>? Charts { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.WorkbookChart> Charts { get; set; }
#endif
        /// <summary>The display name of the worksheet.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>Returns collection of names that are associated with the worksheet. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.WorkbookNamedItem>? Names { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.WorkbookNamedItem> Names { get; set; }
#endif
        /// <summary>Collection of PivotTables that are part of the worksheet.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.WorkbookPivotTable>? PivotTables { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.WorkbookPivotTable> PivotTables { get; set; }
#endif
        /// <summary>The zero-based position of the worksheet within the workbook.</summary>
        public int? Position { get; set; }
        /// <summary>Returns sheet protection object for a worksheet. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.WorkbookWorksheetProtection? Protection { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.WorkbookWorksheetProtection Protection { get; set; }
#endif
        /// <summary>Collection of tables that are part of the worksheet. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.WorkbookTable>? Tables { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.WorkbookTable> Tables { get; set; }
#endif
        /// <summary>The Visibility of the worksheet. The possible values are: Visible, Hidden, VeryHidden.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Visibility { get; set; }
#nullable restore
#else
        public string Visibility { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.WorkbookWorksheet"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.WorkbookWorksheet CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.WorkbookWorksheet();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "charts", n => { Charts = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.WorkbookChart>(global::AITGraph.Sdk.Models.WorkbookChart.CreateFromDiscriminatorValue)?.AsList(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "names", n => { Names = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.WorkbookNamedItem>(global::AITGraph.Sdk.Models.WorkbookNamedItem.CreateFromDiscriminatorValue)?.AsList(); } },
                { "pivotTables", n => { PivotTables = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.WorkbookPivotTable>(global::AITGraph.Sdk.Models.WorkbookPivotTable.CreateFromDiscriminatorValue)?.AsList(); } },
                { "position", n => { Position = n.GetIntValue(); } },
                { "protection", n => { Protection = n.GetObjectValue<global::AITGraph.Sdk.Models.WorkbookWorksheetProtection>(global::AITGraph.Sdk.Models.WorkbookWorksheetProtection.CreateFromDiscriminatorValue); } },
                { "tables", n => { Tables = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.WorkbookTable>(global::AITGraph.Sdk.Models.WorkbookTable.CreateFromDiscriminatorValue)?.AsList(); } },
                { "visibility", n => { Visibility = n.GetStringValue(); } },
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
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.WorkbookChart>("charts", Charts);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.WorkbookNamedItem>("names", Names);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.WorkbookPivotTable>("pivotTables", PivotTables);
            writer.WriteIntValue("position", Position);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.WorkbookWorksheetProtection>("protection", Protection);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.WorkbookTable>("tables", Tables);
            writer.WriteStringValue("visibility", Visibility);
        }
    }
}
#pragma warning restore CS0618
