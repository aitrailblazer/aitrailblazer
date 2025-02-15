// <auto-generated/>
using System.Runtime.Serialization;
using System;
namespace AITGraph.Sdk.Models
{
    /// <summary>Possible values of AppLocker Application Control Types</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public enum AppLockerApplicationControlType
    {
        /// <summary>Device default value, no Application Control type selected.</summary>
        [EnumMember(Value = "notConfigured")]
        NotConfigured,
        /// <summary>Enforce Windows component and store apps.</summary>
        [EnumMember(Value = "enforceComponentsAndStoreApps")]
        EnforceComponentsAndStoreApps,
        /// <summary>Audit Windows component and store apps.</summary>
        [EnumMember(Value = "auditComponentsAndStoreApps")]
        AuditComponentsAndStoreApps,
        /// <summary>Enforce Windows components, store apps and smart locker.</summary>
        [EnumMember(Value = "enforceComponentsStoreAppsAndSmartlocker")]
        EnforceComponentsStoreAppsAndSmartlocker,
        /// <summary>Audit Windows components, store apps and smart locker​.</summary>
        [EnumMember(Value = "auditComponentsStoreAppsAndSmartlocker")]
        AuditComponentsStoreAppsAndSmartlocker,
    }
}
