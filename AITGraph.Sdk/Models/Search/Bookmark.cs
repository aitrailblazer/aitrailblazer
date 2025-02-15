// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace AITGraph.Sdk.Models.Search
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class Bookmark : global::AITGraph.Sdk.Models.Search.SearchAnswer, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Timestamp of when the bookmark will stop to appear as a search result. Set as null for always available.</summary>
        public DateTimeOffset? AvailabilityEndDateTime { get; set; }
        /// <summary>Timestamp of when the bookmark will start to appear as a search result. Set as null for always available.</summary>
        public DateTimeOffset? AvailabilityStartDateTime { get; set; }
        /// <summary>Categories commonly used to describe this bookmark. For example, IT and HR.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Categories { get; set; }
#nullable restore
#else
        public List<string> Categories { get; set; }
#endif
        /// <summary>List of security groups able to view this bookmark.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? GroupIds { get; set; }
#nullable restore
#else
        public List<string> GroupIds { get; set; }
#endif
        /// <summary>True if this bookmark was suggested to the admin by a user or was mined and suggested by Microsoft. Read-only.</summary>
        public bool? IsSuggested { get; set; }
        /// <summary>Keywords that trigger this bookmark to appear in search results.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.Search.AnswerKeyword? Keywords { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.Search.AnswerKeyword Keywords { get; set; }
#endif
        /// <summary>A list of language names that are geographically specific and that this bookmark can be viewed in. Each language tag value follows the pattern {language}-{region}. As an example, en-us is English as used in the United States. See supported language tags for the list of possible values.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? LanguageTags { get; set; }
#nullable restore
#else
        public List<string> LanguageTags { get; set; }
#endif
        /// <summary>List of devices and operating systems able to view this bookmark. Possible values are: unknown, android, androidForWork, ios, macOS, windowsPhone81, windowsPhone81AndLater, windows10AndLater, androidWorkProfile, androidASOP.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.DevicePlatformType?>? Platforms { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.DevicePlatformType?> Platforms { get; set; }
#endif
        /// <summary>List of Power Apps associated with this bookmark. If users add existing Power Apps to a bookmark, they can complete tasks, such as to enter vacation time or to report expenses on the search results page.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? PowerAppIds { get; set; }
#nullable restore
#else
        public List<string> PowerAppIds { get; set; }
#endif
        /// <summary>The state property</summary>
        public global::AITGraph.Sdk.Models.Search.AnswerState? State { get; set; }
        /// <summary>Variations of a bookmark for different countries or devices. Use when you need to show different content to users based on their device, country/region, or both. The date and group settings will apply to all variations.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Search.AnswerVariant>? TargetedVariations { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Search.AnswerVariant> TargetedVariations { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.Search.Bookmark"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.Search.Bookmark CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.Search.Bookmark();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "availabilityEndDateTime", n => { AvailabilityEndDateTime = n.GetDateTimeOffsetValue(); } },
                { "availabilityStartDateTime", n => { AvailabilityStartDateTime = n.GetDateTimeOffsetValue(); } },
                { "categories", n => { Categories = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "groupIds", n => { GroupIds = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "isSuggested", n => { IsSuggested = n.GetBoolValue(); } },
                { "keywords", n => { Keywords = n.GetObjectValue<global::AITGraph.Sdk.Models.Search.AnswerKeyword>(global::AITGraph.Sdk.Models.Search.AnswerKeyword.CreateFromDiscriminatorValue); } },
                { "languageTags", n => { LanguageTags = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "platforms", n => { Platforms = n.GetCollectionOfEnumValues<global::AITGraph.Sdk.Models.DevicePlatformType>()?.AsList(); } },
                { "powerAppIds", n => { PowerAppIds = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "state", n => { State = n.GetEnumValue<global::AITGraph.Sdk.Models.Search.AnswerState>(); } },
                { "targetedVariations", n => { TargetedVariations = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Search.AnswerVariant>(global::AITGraph.Sdk.Models.Search.AnswerVariant.CreateFromDiscriminatorValue)?.AsList(); } },
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
            writer.WriteDateTimeOffsetValue("availabilityEndDateTime", AvailabilityEndDateTime);
            writer.WriteDateTimeOffsetValue("availabilityStartDateTime", AvailabilityStartDateTime);
            writer.WriteCollectionOfPrimitiveValues<string>("categories", Categories);
            writer.WriteCollectionOfPrimitiveValues<string>("groupIds", GroupIds);
            writer.WriteBoolValue("isSuggested", IsSuggested);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.Search.AnswerKeyword>("keywords", Keywords);
            writer.WriteCollectionOfPrimitiveValues<string>("languageTags", LanguageTags);
            writer.WriteCollectionOfEnumValues<global::AITGraph.Sdk.Models.DevicePlatformType>("platforms", Platforms);
            writer.WriteCollectionOfPrimitiveValues<string>("powerAppIds", PowerAppIds);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.Search.AnswerState>("state", State);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Search.AnswerVariant>("targetedVariations", TargetedVariations);
        }
    }
}
#pragma warning restore CS0618
