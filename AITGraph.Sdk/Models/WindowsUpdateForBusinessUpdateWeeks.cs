// <auto-generated/>
using System.Runtime.Serialization;
using System;
namespace AITGraph.Sdk.Models
{
    /// <summary>Scheduled the update installation on the weeks of the month</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public enum WindowsUpdateForBusinessUpdateWeeks
    {
        /// <summary>Allow the user to set.</summary>
        [EnumMember(Value = "userDefined")]
        UserDefined,
        /// <summary>Scheduled the update installation on the first week of the month</summary>
        [EnumMember(Value = "firstWeek")]
        FirstWeek,
        /// <summary>Scheduled the update installation on the second week of the month</summary>
        [EnumMember(Value = "secondWeek")]
        SecondWeek,
        /// <summary>Scheduled the update installation on the third week of the month</summary>
        [EnumMember(Value = "thirdWeek")]
        ThirdWeek,
        /// <summary>Scheduled the update installation on the fourth week of the month</summary>
        [EnumMember(Value = "fourthWeek")]
        FourthWeek,
        /// <summary>Scheduled the update installation on every week of the month</summary>
        [EnumMember(Value = "everyWeek")]
        EveryWeek,
        /// <summary>Evolvable enum member</summary>
        [EnumMember(Value = "unknownFutureValue")]
        UnknownFutureValue,
    }
}
