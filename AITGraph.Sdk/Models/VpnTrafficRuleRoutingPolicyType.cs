// <auto-generated/>
using System.Runtime.Serialization;
using System;
namespace AITGraph.Sdk.Models
{
    /// <summary>Specifies the routing policy for a VPN traffic rule.</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public enum VpnTrafficRuleRoutingPolicyType
    {
        /// <summary>No routing policy specified.</summary>
        [EnumMember(Value = "none")]
        None,
        /// <summary>Network traffic for the specified app will be routed through the VPN.</summary>
        [EnumMember(Value = "splitTunnel")]
        SplitTunnel,
        /// <summary>All network traffic will be routed through the VPN.</summary>
        [EnumMember(Value = "forceTunnel")]
        ForceTunnel,
    }
}
