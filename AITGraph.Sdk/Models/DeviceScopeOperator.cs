// <auto-generated/>
using System.Runtime.Serialization;
using System;
namespace AITGraph.Sdk.Models
{
    /// <summary>Device scope configuration query operator. Possible values are: equals, notEquals, contains, notContains, greaterThan, lessThan. Default value: equals.</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public enum DeviceScopeOperator
    {
        /// <summary>No operator set for the device scope configuration.</summary>
        [EnumMember(Value = "none")]
        None,
        /// <summary>Operator for the device configuration query to be used (Equals).</summary>
        [EnumMember(Value = "equals")]
        Equals,
        /// <summary>Placeholder value for future expansion enums such as notEquals, contains, notContains, greaterThan, lessThan.</summary>
        [EnumMember(Value = "unknownFutureValue")]
        UnknownFutureValue,
    }
}
