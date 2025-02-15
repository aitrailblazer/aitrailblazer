// <auto-generated/>
using System.Runtime.Serialization;
using System;
namespace AITGraph.Sdk.Models
{
    /// <summary>Device enrollment join type.</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public enum JoinType
    {
        /// <summary>Unknown enrollment join type.</summary>
        [EnumMember(Value = "unknown")]
        Unknown,
        /// <summary>The device is joined by Azure AD.</summary>
        [EnumMember(Value = "azureADJoined")]
        AzureADJoined,
        /// <summary>The device is registered by Azure AD.</summary>
        [EnumMember(Value = "azureADRegistered")]
        AzureADRegistered,
        /// <summary>The device is joined by hybrid Azure AD.</summary>
        [EnumMember(Value = "hybridAzureADJoined")]
        HybridAzureADJoined,
    }
}
