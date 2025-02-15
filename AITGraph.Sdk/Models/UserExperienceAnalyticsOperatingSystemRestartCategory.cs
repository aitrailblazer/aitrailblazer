// <auto-generated/>
using System.Runtime.Serialization;
using System;
namespace AITGraph.Sdk.Models
{
    /// <summary>Operating System restart category</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public enum UserExperienceAnalyticsOperatingSystemRestartCategory
    {
        /// <summary>Unknown</summary>
        [EnumMember(Value = "unknown")]
        Unknown,
        /// <summary>Restart with update</summary>
        [EnumMember(Value = "restartWithUpdate")]
        RestartWithUpdate,
        /// <summary>Restart without update</summary>
        [EnumMember(Value = "restartWithoutUpdate")]
        RestartWithoutUpdate,
        /// <summary>Blue screen restart</summary>
        [EnumMember(Value = "blueScreen")]
        BlueScreen,
        /// <summary>Shutdown with update</summary>
        [EnumMember(Value = "shutdownWithUpdate")]
        ShutdownWithUpdate,
        /// <summary>Shutdown without update</summary>
        [EnumMember(Value = "shutdownWithoutUpdate")]
        ShutdownWithoutUpdate,
        /// <summary>Long power button press</summary>
        [EnumMember(Value = "longPowerButtonPress")]
        LongPowerButtonPress,
        /// <summary>Boot error</summary>
        [EnumMember(Value = "bootError")]
        BootError,
        /// <summary>Update</summary>
        [EnumMember(Value = "update")]
        Update,
    }
}
