// <auto-generated/>
using System.Runtime.Serialization;
using System;
namespace AITGraph.Sdk.Models
{
    /// <summary>Possible values for LocalSecurityOptionsStandardUserElevationPromptBehavior</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public enum LocalSecurityOptionsStandardUserElevationPromptBehaviorType
    {
        /// <summary>Not Configured</summary>
        [EnumMember(Value = "notConfigured")]
        NotConfigured,
        /// <summary>Automatically deny elevation requests</summary>
        [EnumMember(Value = "automaticallyDenyElevationRequests")]
        AutomaticallyDenyElevationRequests,
        /// <summary>Prompt for credentials on the secure desktop</summary>
        [EnumMember(Value = "promptForCredentialsOnTheSecureDesktop")]
        PromptForCredentialsOnTheSecureDesktop,
        /// <summary>Prompt for credentials</summary>
        [EnumMember(Value = "promptForCredentials")]
        PromptForCredentials,
    }
}
