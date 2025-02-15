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
    public partial class OrganizationSettings : global::AITGraph.Sdk.Models.Entity, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Contains the properties that are configured by an administrator as a tenant-level privacy control whether to identify duplicate contacts among a user&apos;s contacts list and suggest the user to merge those contacts to have a cleaner contacts list. List contactInsights returns the settings to display or return contact insights in an organization.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.InsightsSettings? ContactInsights { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.InsightsSettings ContactInsights { get; set; }
#endif
        /// <summary>Contains the properties that are configured by an administrator for the visibility of Microsoft Graph-derived insights, between a user and other items in Microsoft 365, such as documents or sites. List itemInsights returns the settings to display or return item insights in an organization.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.InsightsSettings? ItemInsights { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.InsightsSettings ItemInsights { get; set; }
#endif
        /// <summary>The microsoftApplicationDataAccess property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.MicrosoftApplicationDataAccessSettings? MicrosoftApplicationDataAccess { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.MicrosoftApplicationDataAccessSettings MicrosoftApplicationDataAccess { get; set; }
#endif
        /// <summary>Contains the properties that are configured by an administrator for the visibility of a list of people relevant and working with a user in Microsoft 365. List peopleInsights returns the settings to display or return people insights in an organization.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.InsightsSettings? PeopleInsights { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.InsightsSettings PeopleInsights { get; set; }
#endif
        /// <summary>Contains a collection of the properties an administrator has defined as visible on the Microsoft 365 profile card. Get organization settings returns the properties configured for profile cards for the organization.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.ProfileCardProperty>? ProfileCardProperties { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.ProfileCardProperty> ProfileCardProperties { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.OrganizationSettings"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.OrganizationSettings CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.OrganizationSettings();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "contactInsights", n => { ContactInsights = n.GetObjectValue<global::AITGraph.Sdk.Models.InsightsSettings>(global::AITGraph.Sdk.Models.InsightsSettings.CreateFromDiscriminatorValue); } },
                { "itemInsights", n => { ItemInsights = n.GetObjectValue<global::AITGraph.Sdk.Models.InsightsSettings>(global::AITGraph.Sdk.Models.InsightsSettings.CreateFromDiscriminatorValue); } },
                { "microsoftApplicationDataAccess", n => { MicrosoftApplicationDataAccess = n.GetObjectValue<global::AITGraph.Sdk.Models.MicrosoftApplicationDataAccessSettings>(global::AITGraph.Sdk.Models.MicrosoftApplicationDataAccessSettings.CreateFromDiscriminatorValue); } },
                { "peopleInsights", n => { PeopleInsights = n.GetObjectValue<global::AITGraph.Sdk.Models.InsightsSettings>(global::AITGraph.Sdk.Models.InsightsSettings.CreateFromDiscriminatorValue); } },
                { "profileCardProperties", n => { ProfileCardProperties = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.ProfileCardProperty>(global::AITGraph.Sdk.Models.ProfileCardProperty.CreateFromDiscriminatorValue)?.AsList(); } },
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
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.InsightsSettings>("contactInsights", ContactInsights);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.InsightsSettings>("itemInsights", ItemInsights);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.MicrosoftApplicationDataAccessSettings>("microsoftApplicationDataAccess", MicrosoftApplicationDataAccess);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.InsightsSettings>("peopleInsights", PeopleInsights);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.ProfileCardProperty>("profileCardProperties", ProfileCardProperties);
        }
    }
}
#pragma warning restore CS0618
