// <auto-generated/>
using System.Runtime.Serialization;
using System;
namespace AITGraph.Sdk.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public enum DeviceManagementConfigurationStringFormat
    #pragma warning restore CS1591
    {
        /// <summary>Indicates a string with no well-defined format expected.</summary>
        [EnumMember(Value = "none")]
        None,
        /// <summary>Indicates a string that is expected to be a valid email address.</summary>
        [EnumMember(Value = "email")]
        Email,
        /// <summary>Indicates a string that is expected to be a valid GUID.</summary>
        [EnumMember(Value = "guid")]
        Guid,
        /// <summary>Indicates a string that is expected to be a valid IP address.</summary>
        [EnumMember(Value = "ip")]
        Ip,
        /// <summary>Indicates a string that is base64 encoded.</summary>
        [EnumMember(Value = "base64")]
        Base64,
        /// <summary>Indicates a string that is expected to be a valid URL.</summary>
        [EnumMember(Value = "url")]
        Url,
        /// <summary>Indicates a string that should refer to a version.</summary>
        [EnumMember(Value = "version")]
        Version,
        /// <summary>Indicates a string that is expected to be a valid XML.</summary>
        [EnumMember(Value = "xml")]
        Xml,
        /// <summary>Indicates a string that is expected to be a valid date.</summary>
        [EnumMember(Value = "date")]
        Date,
        /// <summary>Indicates a string that is expected to be a valid time.</summary>
        [EnumMember(Value = "time")]
        Time,
        [EnumMember(Value = "binary")]
        #pragma warning disable CS1591
        Binary,
        #pragma warning restore CS1591
        /// <summary>Indicates a string that is expected to be a valid Regex string.</summary>
        [EnumMember(Value = "regEx")]
        RegEx,
        /// <summary>Indicates a string that is expected to be a valid JSON string.</summary>
        [EnumMember(Value = "json")]
        Json,
        /// <summary>Indicates a string that is expected to be a valid Datetime.</summary>
        [EnumMember(Value = "dateTime")]
        DateTime,
        [EnumMember(Value = "surfaceHub")]
        #pragma warning disable CS1591
        SurfaceHub,
        #pragma warning restore CS1591
        /// <summary>String whose value is a bash script</summary>
        [EnumMember(Value = "bashScript")]
        BashScript,
        /// <summary>Sentinel member for cases where the client cannot handle the new enum values.</summary>
        [EnumMember(Value = "unknownFutureValue")]
        UnknownFutureValue,
    }
}
