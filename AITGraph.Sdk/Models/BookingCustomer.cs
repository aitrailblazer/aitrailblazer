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
    /// Represents a customer of the business.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class BookingCustomer : global::AITGraph.Sdk.Models.BookingPerson, IParsable
    {
        /// <summary>Addresses associated with the customer, including home, business and other addresses.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.PhysicalAddress>? Addresses { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.PhysicalAddress> Addresses { get; set; }
#endif
        /// <summary>Phone numbers associated with the customer, including home, business and mobile numbers.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.Phone>? Phones { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.Phone> Phones { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.BookingCustomer"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.BookingCustomer CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.BookingCustomer();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "addresses", n => { Addresses = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.PhysicalAddress>(global::AITGraph.Sdk.Models.PhysicalAddress.CreateFromDiscriminatorValue)?.AsList(); } },
                { "phones", n => { Phones = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.Phone>(global::AITGraph.Sdk.Models.Phone.CreateFromDiscriminatorValue)?.AsList(); } },
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
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.PhysicalAddress>("addresses", Addresses);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.Phone>("phones", Phones);
        }
    }
}
#pragma warning restore CS0618
