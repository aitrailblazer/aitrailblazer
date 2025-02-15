// <auto-generated/>
using System.Runtime.Serialization;
using System;
namespace AITGraph.Sdk.Models
{
    /// <summary>Device health monitoring scope</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public enum WindowsHealthMonitoringScope
    {
        /// <summary>Undefined</summary>
        [EnumMember(Value = "undefined")]
        Undefined,
        /// <summary>Basic events for windows device health monitoring</summary>
        [EnumMember(Value = "healthMonitoring")]
        HealthMonitoring,
        /// <summary>Boot performance events</summary>
        [EnumMember(Value = "bootPerformance")]
        BootPerformance,
        /// <summary>Windows updates events</summary>
        [EnumMember(Value = "windowsUpdates")]
        WindowsUpdates,
        /// <summary>PrivilegeManagement</summary>
        [EnumMember(Value = "privilegeManagement")]
        PrivilegeManagement,
    }
}
