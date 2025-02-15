// <auto-generated/>
using System.Runtime.Serialization;
using System;
namespace AITGraph.Sdk.Models
{
    /// <summary>Connectors name for connector status</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public enum ConnectorName
    {
        /// <summary>Indicates the expiration date/time for the Apple MDM Push Certificate.</summary>
        [EnumMember(Value = "applePushNotificationServiceExpirationDateTime")]
        ApplePushNotificationServiceExpirationDateTime,
        /// <summary>Indicates the expiration date/time for Vpp Token.</summary>
        [EnumMember(Value = "vppTokenExpirationDateTime")]
        VppTokenExpirationDateTime,
        /// <summary>Indicate the last sync data/time that the Vpp Token performed a sync.</summary>
        [EnumMember(Value = "vppTokenLastSyncDateTime")]
        VppTokenLastSyncDateTime,
        /// <summary>Indicate the last sync date/time that the Windows Autopilot performed a sync.</summary>
        [EnumMember(Value = "windowsAutopilotLastSyncDateTime")]
        WindowsAutopilotLastSyncDateTime,
        /// <summary>Indicates the last sync date/time that the Windows Store for Business performed a sync.</summary>
        [EnumMember(Value = "windowsStoreForBusinessLastSyncDateTime")]
        WindowsStoreForBusinessLastSyncDateTime,
        /// <summary>Indicates the last sync date/time that the JAMF connector performed a sync.</summary>
        [EnumMember(Value = "jamfLastSyncDateTime")]
        JamfLastSyncDateTime,
        /// <summary>Indicates the last sync date/time that the NDES connector performed a sync.</summary>
        [EnumMember(Value = "ndesConnectorLastConnectionDateTime")]
        NdesConnectorLastConnectionDateTime,
        /// <summary>Indicates the expiration date/time for the Apple Enrollment Program token.</summary>
        [EnumMember(Value = "appleDepExpirationDateTime")]
        AppleDepExpirationDateTime,
        /// <summary>Indicates the last sync date/time that the Apple Enrollment Program token performed a sync.</summary>
        [EnumMember(Value = "appleDepLastSyncDateTime")]
        AppleDepLastSyncDateTime,
        /// <summary>Indicates the last sync date/time that the Exchange ActiveSync connector performed a sync.</summary>
        [EnumMember(Value = "onPremConnectorLastSyncDateTime")]
        OnPremConnectorLastSyncDateTime,
        /// <summary>Indicates the last sync date/time that the Google Play App performed a sync.</summary>
        [EnumMember(Value = "googlePlayAppLastSyncDateTime")]
        GooglePlayAppLastSyncDateTime,
        /// <summary>Indicates the last modified date / time that the Google Play connector was updated.</summary>
        [EnumMember(Value = "googlePlayConnectorLastModifiedDateTime")]
        GooglePlayConnectorLastModifiedDateTime,
        /// <summary>Indicates the last heartbeat date/time that the Windows Defender ATP connector was contacted.</summary>
        [EnumMember(Value = "windowsDefenderATPConnectorLastHeartbeatDateTime")]
        WindowsDefenderATPConnectorLastHeartbeatDateTime,
        /// <summary>Indicates the last heartbeat date/time that the Mobile Threat Defence connector was contacted.</summary>
        [EnumMember(Value = "mobileThreatDefenceConnectorLastHeartbeatDateTime")]
        MobileThreatDefenceConnectorLastHeartbeatDateTime,
        /// <summary>Indicates the last sync date/time that the Chrombook Last Directory performed a sync.</summary>
        [EnumMember(Value = "chromebookLastDirectorySyncDateTime")]
        ChromebookLastDirectorySyncDateTime,
        /// <summary>Future use</summary>
        [EnumMember(Value = "futureValue")]
        FutureValue,
    }
}
