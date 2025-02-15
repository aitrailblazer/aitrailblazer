// <auto-generated/>
using System.Runtime.Serialization;
using System;
namespace AITGraph.Sdk.Models
{
    /// <summary>Type of start menu app list visibility.</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public enum WindowsStartMenuAppListVisibilityType
    {
        /// <summary>User defined. Default value.</summary>
        [EnumMember(Value = "userDefined")]
        UserDefined,
        /// <summary>Collapse the app list on the start menu.</summary>
        [EnumMember(Value = "collapse")]
        Collapse,
        /// <summary>Removes the app list entirely from the start menu.</summary>
        [EnumMember(Value = "remove")]
        Remove,
        /// <summary>Disables the corresponding toggle (Collapse or Remove) in the Settings app.</summary>
        [EnumMember(Value = "disableSettingsApp")]
        DisableSettingsApp,
    }
}
