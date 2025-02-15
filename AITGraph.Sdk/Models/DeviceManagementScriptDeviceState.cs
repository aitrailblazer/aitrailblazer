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
    /// Contains properties for device run state of the device management script.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class DeviceManagementScriptDeviceState : global::AITGraph.Sdk.Models.Entity, IParsable
    {
        /// <summary>Error code corresponding to erroneous execution of the device management script.</summary>
        public int? ErrorCode { get; set; }
        /// <summary>Error description corresponding to erroneous execution of the device management script.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ErrorDescription { get; set; }
#nullable restore
#else
        public string ErrorDescription { get; set; }
#endif
        /// <summary>Latest time the device management script executes.</summary>
        public DateTimeOffset? LastStateUpdateDateTime { get; set; }
        /// <summary>The managed devices that executes the device management script.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::AITGraph.Sdk.Models.ManagedDevice? ManagedDevice { get; set; }
#nullable restore
#else
        public global::AITGraph.Sdk.Models.ManagedDevice ManagedDevice { get; set; }
#endif
        /// <summary>Details of execution output.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ResultMessage { get; set; }
#nullable restore
#else
        public string ResultMessage { get; set; }
#endif
        /// <summary>Indicates the type of execution status of the device management script.</summary>
        public global::AITGraph.Sdk.Models.RunState? RunState { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.DeviceManagementScriptDeviceState"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.DeviceManagementScriptDeviceState CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.DeviceManagementScriptDeviceState();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "errorCode", n => { ErrorCode = n.GetIntValue(); } },
                { "errorDescription", n => { ErrorDescription = n.GetStringValue(); } },
                { "lastStateUpdateDateTime", n => { LastStateUpdateDateTime = n.GetDateTimeOffsetValue(); } },
                { "managedDevice", n => { ManagedDevice = n.GetObjectValue<global::AITGraph.Sdk.Models.ManagedDevice>(global::AITGraph.Sdk.Models.ManagedDevice.CreateFromDiscriminatorValue); } },
                { "resultMessage", n => { ResultMessage = n.GetStringValue(); } },
                { "runState", n => { RunState = n.GetEnumValue<global::AITGraph.Sdk.Models.RunState>(); } },
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
            writer.WriteIntValue("errorCode", ErrorCode);
            writer.WriteStringValue("errorDescription", ErrorDescription);
            writer.WriteDateTimeOffsetValue("lastStateUpdateDateTime", LastStateUpdateDateTime);
            writer.WriteObjectValue<global::AITGraph.Sdk.Models.ManagedDevice>("managedDevice", ManagedDevice);
            writer.WriteStringValue("resultMessage", ResultMessage);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.RunState>("runState", RunState);
        }
    }
}
#pragma warning restore CS0618
