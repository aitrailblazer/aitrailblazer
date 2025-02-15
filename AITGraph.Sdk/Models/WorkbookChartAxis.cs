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
    public partial class WorkbookChartAxis : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Represents the formatting of a chart object, which includes line and font formatting. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.WorkbookChartAxisFormat? Format { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.WorkbookChartAxisFormat Format { get; set; }
#endif
        /// <summary>Returns a gridlines object that represents the major gridlines for the specified axis. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.WorkbookChartGridlines? MajorGridlines { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.WorkbookChartGridlines MajorGridlines { get; set; }
#endif
        /// <summary>Represents the interval between two major tick marks. Can be set to a numeric value or an empty string.  The returned value is always a number.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.Json? MajorUnit { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.Json MajorUnit { get; set; }
#endif
        /// <summary>Represents the maximum value on the value axis.  Can be set to a numeric value or an empty string (for automatic axis values).  The returned value is always a number.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.Json? Maximum { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.Json Maximum { get; set; }
#endif
        /// <summary>Represents the minimum value on the value axis. Can be set to a numeric value or an empty string (for automatic axis values).  The returned value is always a number.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.Json? Minimum { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.Json Minimum { get; set; }
#endif
        /// <summary>Returns a Gridlines object that represents the minor gridlines for the specified axis. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.WorkbookChartGridlines? MinorGridlines { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.WorkbookChartGridlines MinorGridlines { get; set; }
#endif
        /// <summary>Represents the interval between two minor tick marks. &apos;Can be set to a numeric value or an empty string (for automatic axis values). The returned value is always a number.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.Json? MinorUnit { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.Json MinorUnit { get; set; }
#endif
        /// <summary>Represents the axis title. Read-only.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.WorkbookChartAxisTitle? Title { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.WorkbookChartAxisTitle Title { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.WorkbookChartAxis"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.WorkbookChartAxis CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.WorkbookChartAxis();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "format", n => { Format = n.GetObjectValue<global::AITGraph.Sdk.Models.WorkbookChartAxisFormat>(global::AITGraph.Sdk.Models.WorkbookChartAxisFormat.CreateFromDiscriminatorValue); } },
                { "majorGridlines", n => { MajorGridlines = n.GetObjectValue<global::AITGraph.Sdk.Models.WorkbookChartGridlines>(global::AITGraph.Sdk.Models.WorkbookChartGridlines.CreateFromDiscriminatorValue); } },
                { "majorUnit", n => { MajorUnit = n.GetObjectValue<global::AITGraph.Sdk.Models.Json>(global::AITGraph.Sdk.Models.Json.CreateFromDiscriminatorValue); } },
                { "maximum", n => { Maximum = n.GetObjectValue<global::AITGraph.Sdk.Models.Json>(global::AITGraph.Sdk.Models.Json.CreateFromDiscriminatorValue); } },
                { "minimum", n => { Minimum = n.GetObjectValue<global::AITGraph.Sdk.Models.Json>(global::AITGraph.Sdk.Models.Json.CreateFromDiscriminatorValue); } },
                { "minorGridlines", n => { MinorGridlines = n.GetObjectValue<global::AITGraph.Sdk.Models.WorkbookChartGridlines>(global::AITGraph.Sdk.Models.WorkbookChartGridlines.CreateFromDiscriminatorValue); } },
                { "minorUnit", n => { MinorUnit = n.GetObjectValue<global::AITGraph.Sdk.Models.Json>(global::AITGraph.Sdk.Models.Json.CreateFromDiscriminatorValue); } },
                { "title", n => { Title = n.GetObjectValue<global::AITGraph.Sdk.Models.WorkbookChartAxisTitle>(global::AITGraph.Sdk.Models.WorkbookChartAxisTitle.CreateFromDiscriminatorValue); } },
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
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.WorkbookChartAxisFormat>("format", Format);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.WorkbookChartGridlines>("majorGridlines", MajorGridlines);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.Json>("majorUnit", MajorUnit);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.Json>("maximum", Maximum);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.Json>("minimum", Minimum);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.WorkbookChartGridlines>("minorGridlines", MinorGridlines);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.Json>("minorUnit", MinorUnit);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.WorkbookChartAxisTitle>("title", Title);
        }
    }
}
#pragma warning restore CS0618
