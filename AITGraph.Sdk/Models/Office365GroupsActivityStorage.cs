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
    public partial class Office365GroupsActivityStorage : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The storage used in group mailbox.</summary>
        public long? MailboxStorageUsedInBytes { get; set; }
        /// <summary>The snapshot date for Exchange and SharePoint used storage.</summary>
        public Date? ReportDate { get; set; }
        /// <summary>The number of days the report covers.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ReportPeriod { get; set; }
#nullable restore
#else
        public string ReportPeriod { get; set; }
#endif
        /// <summary>The latest date of the content.</summary>
        public Date? ReportRefreshDate { get; set; }
        /// <summary>The storage used in SharePoint document library.</summary>
        public long? SiteStorageUsedInBytes { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.Office365GroupsActivityStorage"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.Office365GroupsActivityStorage CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.Office365GroupsActivityStorage();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "mailboxStorageUsedInBytes", n => { MailboxStorageUsedInBytes = n.GetLongValue(); } },
                { "reportDate", n => { ReportDate = n.GetDateValue(); } },
                { "reportPeriod", n => { ReportPeriod = n.GetStringValue(); } },
                { "reportRefreshDate", n => { ReportRefreshDate = n.GetDateValue(); } },
                { "siteStorageUsedInBytes", n => { SiteStorageUsedInBytes = n.GetLongValue(); } },
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
            writer.WriteLongValue("mailboxStorageUsedInBytes", MailboxStorageUsedInBytes);
            writer.WriteDateValue("reportDate", ReportDate);
            writer.WriteStringValue("reportPeriod", ReportPeriod);
            writer.WriteDateValue("reportRefreshDate", ReportRefreshDate);
            writer.WriteLongValue("siteStorageUsedInBytes", SiteStorageUsedInBytes);
        }
    }
}
#pragma warning restore CS0618
