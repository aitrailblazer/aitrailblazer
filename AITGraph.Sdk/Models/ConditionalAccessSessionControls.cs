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
    public partial class ConditionalAccessSessionControls : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Session control to enforce application restrictions. Only Exchange Online and Sharepoint Online support this session control.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.ApplicationEnforcedRestrictionsSessionControl? ApplicationEnforcedRestrictions { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.ApplicationEnforcedRestrictionsSessionControl ApplicationEnforcedRestrictions { get; set; }
#endif
        /// <summary>Session control to apply cloud app security.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.CloudAppSecuritySessionControl? CloudAppSecurity { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.CloudAppSecuritySessionControl CloudAppSecurity { get; set; }
#endif
        /// <summary>Session control for continuous access evaluation settings.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.ContinuousAccessEvaluationSessionControl? ContinuousAccessEvaluation { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.ContinuousAccessEvaluationSessionControl ContinuousAccessEvaluation { get; set; }
#endif
        /// <summary>Session control that determines whether it is acceptable for Azure AD to extend existing sessions based on information collected prior to an outage or not.</summary>
        public bool? DisableResilienceDefaults { get; set; }
        /// <summary>The OdataType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OdataType { get; set; }
#nullable restore
#else
        public string OdataType { get; set; }
#endif
        /// <summary>Session control to define whether to persist cookies or not. All apps should be selected for this session control to work correctly.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.PersistentBrowserSessionControl? PersistentBrowser { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.PersistentBrowserSessionControl PersistentBrowser { get; set; }
#endif
        /// <summary>Session control to enforce signin frequency.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.SignInFrequencySessionControl? SignInFrequency { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.SignInFrequencySessionControl SignInFrequency { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.ConditionalAccessSessionControls"/> and sets the default values.
        /// </summary>
        public ConditionalAccessSessionControls()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.ConditionalAccessSessionControls"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::AITGraph.Sdk.Models.ConditionalAccessSessionControls CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.ConditionalAccessSessionControls();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "applicationEnforcedRestrictions", n => { ApplicationEnforcedRestrictions = n.GetObjectValue<global::AITGraph.Sdk.Models.ApplicationEnforcedRestrictionsSessionControl>(global::AITGraph.Sdk.Models.ApplicationEnforcedRestrictionsSessionControl.CreateFromDiscriminatorValue); } },
                { "cloudAppSecurity", n => { CloudAppSecurity = n.GetObjectValue<global::AITGraph.Sdk.Models.CloudAppSecuritySessionControl>(global::AITGraph.Sdk.Models.CloudAppSecuritySessionControl.CreateFromDiscriminatorValue); } },
                { "continuousAccessEvaluation", n => { ContinuousAccessEvaluation = n.GetObjectValue<global::AITGraph.Sdk.Models.ContinuousAccessEvaluationSessionControl>(global::AITGraph.Sdk.Models.ContinuousAccessEvaluationSessionControl.CreateFromDiscriminatorValue); } },
                { "disableResilienceDefaults", n => { DisableResilienceDefaults = n.GetBoolValue(); } },
                { "@odata.type", n => { OdataType = n.GetStringValue(); } },
                { "persistentBrowser", n => { PersistentBrowser = n.GetObjectValue<global::AITGraph.Sdk.Models.PersistentBrowserSessionControl>(global::AITGraph.Sdk.Models.PersistentBrowserSessionControl.CreateFromDiscriminatorValue); } },
                { "signInFrequency", n => { SignInFrequency = n.GetObjectValue<global::AITGraph.Sdk.Models.SignInFrequencySessionControl>(global::AITGraph.Sdk.Models.SignInFrequencySessionControl.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.ApplicationEnforcedRestrictionsSessionControl>("applicationEnforcedRestrictions", ApplicationEnforcedRestrictions);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.CloudAppSecuritySessionControl>("cloudAppSecurity", CloudAppSecurity);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.ContinuousAccessEvaluationSessionControl>("continuousAccessEvaluation", ContinuousAccessEvaluation);
            writer.WriteBoolValue("disableResilienceDefaults", DisableResilienceDefaults);
            writer.WriteStringValue("@odata.type", OdataType);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.PersistentBrowserSessionControl>("persistentBrowser", PersistentBrowser);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.SignInFrequencySessionControl>("signInFrequency", SignInFrequency);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
