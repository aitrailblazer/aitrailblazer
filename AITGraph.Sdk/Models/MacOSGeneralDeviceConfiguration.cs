// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace AITGraph.Sdk.Models
{
    /// <summary>
    /// This topic provides descriptions of the declared methods, properties and relationships exposed by the macOSGeneralDeviceConfiguration resource.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class MacOSGeneralDeviceConfiguration : global::AITGraph.Sdk.Models.DeviceConfiguration, IParsable
    {
        /// <summary>Yes prevents users from adding friends to Game Center. Available for devices running macOS versions 10.13 and later.</summary>
        public bool? AddingGameCenterFriendsBlocked { get; set; }
        /// <summary>Indicates whether or not to allow AirDrop.</summary>
        public bool? AirDropBlocked { get; set; }
        /// <summary>Indicates whether or to block users from unlocking their Mac with Apple Watch.</summary>
        public bool? AppleWatchBlockAutoUnlock { get; set; }
        /// <summary>Indicates whether or not to block the user from accessing the camera of the device.</summary>
        public bool? CameraBlocked { get; set; }
        /// <summary>Indicates whether or not to allow remote screen observation by Classroom app. Requires MDM enrollment via Apple School Manager or Apple Business Manager.</summary>
        public bool? ClassroomAppBlockRemoteScreenObservation { get; set; }
        /// <summary>Indicates whether or not to automatically give permission to the teacher of a managed course on the Classroom app to view a student&apos;s screen without prompting. Requires MDM enrollment via Apple School Manager or Apple Business Manager.</summary>
        public bool? ClassroomAppForceUnpromptedScreenObservation { get; set; }
        /// <summary>Indicates whether or not to automatically give permission to the teacher&apos;s requests, without prompting the student. Requires MDM enrollment via Apple School Manager or Apple Business Manager.</summary>
        public bool? ClassroomForceAutomaticallyJoinClasses { get; set; }
        /// <summary>Indicates whether a student enrolled in an unmanaged course via Classroom will be required to request permission from the teacher when attempting to leave the course. Requires MDM enrollment via Apple School Manager or Apple Business Manager.</summary>
        public bool? ClassroomForceRequestPermissionToLeaveClasses { get; set; }
        /// <summary>Indicates whether or not to allow the teacher to lock apps or the device without prompting the student. Requires MDM enrollment via Apple School Manager or Apple Business Manager.</summary>
        public bool? ClassroomForceUnpromptedAppAndDeviceLock { get; set; }
        /// <summary>Possible values of the compliance app list.</summary>
        public global::AITGraph.Sdk.Models.AppListType? CompliantAppListType { get; set; }
        /// <summary>List of apps in the compliance (either allow list or block list, controlled by CompliantAppListType). This collection can contain a maximum of 10000 elements.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.AppListItem>? CompliantAppsList { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.AppListItem> CompliantAppsList { get; set; }
#endif
        /// <summary>Indicates whether or not to allow content caching.</summary>
        public bool? ContentCachingBlocked { get; set; }
        /// <summary>Indicates whether or not to block definition lookup.</summary>
        public bool? DefinitionLookupBlocked { get; set; }
        /// <summary>An email address lacking a suffix that matches any of these strings will be considered out-of-domain.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? EmailInDomainSuffixes { get; set; }
#nullable restore
#else
        public List<string> EmailInDomainSuffixes { get; set; }
#endif
        /// <summary>TRUE disables the reset option on supervised devices. FALSE enables the reset option on supervised devices. Available for devices running macOS versions 12.0 and later.</summary>
        public bool? EraseContentAndSettingsBlocked { get; set; }
        /// <summary>Yes disables Game Center, and the Game Center icon is removed from the Home screen. Available for devices running macOS versions 10.13 and later.</summary>
        public bool? GameCenterBlocked { get; set; }
        /// <summary>Indicates whether or not to block the user from continuing work that they started on a MacOS device on another iOS or MacOS device (MacOS 10.15 or later).</summary>
        public bool? ICloudBlockActivityContinuation { get; set; }
        /// <summary>Indicates whether or not to block iCloud from syncing contacts.</summary>
        public bool? ICloudBlockAddressBook { get; set; }
        /// <summary>Indicates whether or not to block iCloud from syncing bookmarks.</summary>
        public bool? ICloudBlockBookmarks { get; set; }
        /// <summary>Indicates whether or not to block iCloud from syncing calendars.</summary>
        public bool? ICloudBlockCalendar { get; set; }
        /// <summary>Indicates whether or not to block iCloud document sync.</summary>
        public bool? ICloudBlockDocumentSync { get; set; }
        /// <summary>Indicates whether or not to block iCloud from syncing mail.</summary>
        public bool? ICloudBlockMail { get; set; }
        /// <summary>Indicates whether or not to block iCloud from syncing notes.</summary>
        public bool? ICloudBlockNotes { get; set; }
        /// <summary>Indicates whether or not to block iCloud Photo Library.</summary>
        public bool? ICloudBlockPhotoLibrary { get; set; }
        /// <summary>Indicates whether or not to block iCloud from syncing reminders.</summary>
        public bool? ICloudBlockReminders { get; set; }
        /// <summary>When TRUE the synchronization of cloud desktop and documents is blocked. When FALSE, synchronization of the cloud desktop and documents are allowed. Available for devices running macOS 10.12.4 and later.</summary>
        public bool? ICloudDesktopAndDocumentsBlocked { get; set; }
        /// <summary>iCloud private relay is an iCloud+ service that prevents networks and servers from monitoring a person&apos;s activity across the internet. By blocking iCloud private relay, Apple will not encrypt the traffic leaving the device. Available for devices running macOS 12 and later.</summary>
        public bool? ICloudPrivateRelayBlocked { get; set; }
        /// <summary>Indicates whether or not to block files from being transferred using iTunes.</summary>
        public bool? ITunesBlockFileSharing { get; set; }
        /// <summary>Indicates whether or not to block Music service and revert Music app to classic mode.</summary>
        public bool? ITunesBlockMusicService { get; set; }
        /// <summary>Indicates whether or not to block the user from using dictation input.</summary>
        public bool? KeyboardBlockDictation { get; set; }
        /// <summary>Indicates whether or not iCloud keychain synchronization is blocked (macOS 10.12 and later).</summary>
        public bool? KeychainBlockCloudSync { get; set; }
        /// <summary>TRUE prevents multiplayer gaming when using Game Center. FALSE allows multiplayer gaming when using Game Center. Available for devices running macOS versions 10.13 and later.</summary>
        public bool? MultiplayerGamingBlocked { get; set; }
        /// <summary>Indicates whether or not to block sharing passwords with the AirDrop passwords feature.</summary>
        public bool? PasswordBlockAirDropSharing { get; set; }
        /// <summary>Indicates whether or not to block the AutoFill Passwords feature.</summary>
        public bool? PasswordBlockAutoFill { get; set; }
        /// <summary>Indicates whether or not to block fingerprint unlock.</summary>
        public bool? PasswordBlockFingerprintUnlock { get; set; }
        /// <summary>Indicates whether or not to allow passcode modification.</summary>
        public bool? PasswordBlockModification { get; set; }
        /// <summary>Indicates whether or not to block requesting passwords from nearby devices.</summary>
        public bool? PasswordBlockProximityRequests { get; set; }
        /// <summary>Block simple passwords.</summary>
        public bool? PasswordBlockSimple { get; set; }
        /// <summary>Number of days before the password expires.</summary>
        public int? PasswordExpirationDays { get; set; }
        /// <summary>The number of allowed failed attempts to enter the passcode at the device&apos;s lock screen. Valid values 2 to 11</summary>
        public int? PasswordMaximumAttemptCount { get; set; }
        /// <summary>Number of character sets a password must contain. Valid values 0 to 4</summary>
        public int? PasswordMinimumCharacterSetCount { get; set; }
        /// <summary>Minimum length of passwords.</summary>
        public int? PasswordMinimumLength { get; set; }
        /// <summary>Minutes of inactivity required before a password is required.</summary>
        public int? PasswordMinutesOfInactivityBeforeLock { get; set; }
        /// <summary>Minutes of inactivity required before the screen times out.</summary>
        public int? PasswordMinutesOfInactivityBeforeScreenTimeout { get; set; }
        /// <summary>The number of minutes before the login is reset after the maximum number of unsuccessful login attempts is reached.</summary>
        public int? PasswordMinutesUntilFailedLoginReset { get; set; }
        /// <summary>Number of previous passwords to block.</summary>
        public int? PasswordPreviousPasswordBlockCount { get; set; }
        /// <summary>Whether or not to require a password.</summary>
        public bool? PasswordRequired { get; set; }
        /// <summary>Possible values of required passwords.</summary>
        public global::AITGraph.Sdk.Models.RequiredPasswordType? PasswordRequiredType { get; set; }
        /// <summary>List of privacy preference policy controls. This collection can contain a maximum of 10000 elements.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::AITGraph.Sdk.Models.MacOSPrivacyAccessControlItem>? PrivacyAccessControls { get; set; }
#nullable restore
#else
        public List<global::AITGraph.Sdk.Models.MacOSPrivacyAccessControlItem> PrivacyAccessControls { get; set; }
#endif
        /// <summary>Indicates whether or not to block the user from using Auto fill in Safari.</summary>
        public bool? SafariBlockAutofill { get; set; }
        /// <summary>Indicates whether or not to block the user from taking Screenshots.</summary>
        public bool? ScreenCaptureBlocked { get; set; }
        /// <summary>Specify the number of days (1-90) to delay visibility of major OS software updates. Available for devices running macOS versions 11.3 and later. Valid values 0 to 90</summary>
        public int? SoftwareUpdateMajorOSDeferredInstallDelayInDays { get; set; }
        /// <summary>Specify the number of days (1-90) to delay visibility of minor OS software updates. Available for devices running macOS versions 11.3 and later. Valid values 0 to 90</summary>
        public int? SoftwareUpdateMinorOSDeferredInstallDelayInDays { get; set; }
        /// <summary>Specify the number of days (1-90) to delay visibility of non-OS software updates. Available for devices running macOS versions 11.3 and later. Valid values 0 to 90</summary>
        public int? SoftwareUpdateNonOSDeferredInstallDelayInDays { get; set; }
        /// <summary>Sets how many days a software update will be delyed for a supervised device. Valid values 0 to 90</summary>
        public int? SoftwareUpdatesEnforcedDelayInDays { get; set; }
        /// <summary>Indicates whether or not to block Spotlight from returning any results from an Internet search.</summary>
        public bool? SpotlightBlockInternetResults { get; set; }
        /// <summary>Maximum hours after which the user must enter their password to unlock the device instead of using Touch ID. Available for devices running macOS 12 and later. Valid values 0 to 2147483647</summary>
        public int? TouchIdTimeoutInHours { get; set; }
        /// <summary>Determines whether to delay OS and/or app updates for macOS. Possible values are: none, delayOSUpdateVisibility, delayAppUpdateVisibility, unknownFutureValue, delayMajorOsUpdateVisibility.</summary>
        public global::AITGraph.Sdk.Models.MacOSSoftwareUpdateDelayPolicy? UpdateDelayPolicy { get; set; }
        /// <summary>TRUE prevents the wallpaper from being changed. FALSE allows the wallpaper to be changed. Available for devices running macOS versions 10.13 and later.</summary>
        public bool? WallpaperModificationBlocked { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Models.MacOSGeneralDeviceConfiguration"/> and sets the default values.
        /// </summary>
        public MacOSGeneralDeviceConfiguration() : base()
        {
            OdataType = "#microsoft.graph.macOSGeneralDeviceConfiguration";
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.MacOSGeneralDeviceConfiguration"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::AITGraph.Sdk.Models.MacOSGeneralDeviceConfiguration CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::AITGraph.Sdk.Models.MacOSGeneralDeviceConfiguration();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "addingGameCenterFriendsBlocked", n => { AddingGameCenterFriendsBlocked = n.GetBoolValue(); } },
                { "airDropBlocked", n => { AirDropBlocked = n.GetBoolValue(); } },
                { "appleWatchBlockAutoUnlock", n => { AppleWatchBlockAutoUnlock = n.GetBoolValue(); } },
                { "cameraBlocked", n => { CameraBlocked = n.GetBoolValue(); } },
                { "classroomAppBlockRemoteScreenObservation", n => { ClassroomAppBlockRemoteScreenObservation = n.GetBoolValue(); } },
                { "classroomAppForceUnpromptedScreenObservation", n => { ClassroomAppForceUnpromptedScreenObservation = n.GetBoolValue(); } },
                { "classroomForceAutomaticallyJoinClasses", n => { ClassroomForceAutomaticallyJoinClasses = n.GetBoolValue(); } },
                { "classroomForceRequestPermissionToLeaveClasses", n => { ClassroomForceRequestPermissionToLeaveClasses = n.GetBoolValue(); } },
                { "classroomForceUnpromptedAppAndDeviceLock", n => { ClassroomForceUnpromptedAppAndDeviceLock = n.GetBoolValue(); } },
                { "compliantAppListType", n => { CompliantAppListType = n.GetEnumValue<global::AITGraph.Sdk.Models.AppListType>(); } },
                { "compliantAppsList", n => { CompliantAppsList = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.AppListItem>(global::AITGraph.Sdk.Models.AppListItem.CreateFromDiscriminatorValue)?.AsList(); } },
                { "contentCachingBlocked", n => { ContentCachingBlocked = n.GetBoolValue(); } },
                { "definitionLookupBlocked", n => { DefinitionLookupBlocked = n.GetBoolValue(); } },
                { "emailInDomainSuffixes", n => { EmailInDomainSuffixes = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "eraseContentAndSettingsBlocked", n => { EraseContentAndSettingsBlocked = n.GetBoolValue(); } },
                { "gameCenterBlocked", n => { GameCenterBlocked = n.GetBoolValue(); } },
                { "iCloudBlockActivityContinuation", n => { ICloudBlockActivityContinuation = n.GetBoolValue(); } },
                { "iCloudBlockAddressBook", n => { ICloudBlockAddressBook = n.GetBoolValue(); } },
                { "iCloudBlockBookmarks", n => { ICloudBlockBookmarks = n.GetBoolValue(); } },
                { "iCloudBlockCalendar", n => { ICloudBlockCalendar = n.GetBoolValue(); } },
                { "iCloudBlockDocumentSync", n => { ICloudBlockDocumentSync = n.GetBoolValue(); } },
                { "iCloudBlockMail", n => { ICloudBlockMail = n.GetBoolValue(); } },
                { "iCloudBlockNotes", n => { ICloudBlockNotes = n.GetBoolValue(); } },
                { "iCloudBlockPhotoLibrary", n => { ICloudBlockPhotoLibrary = n.GetBoolValue(); } },
                { "iCloudBlockReminders", n => { ICloudBlockReminders = n.GetBoolValue(); } },
                { "iCloudDesktopAndDocumentsBlocked", n => { ICloudDesktopAndDocumentsBlocked = n.GetBoolValue(); } },
                { "iCloudPrivateRelayBlocked", n => { ICloudPrivateRelayBlocked = n.GetBoolValue(); } },
                { "iTunesBlockFileSharing", n => { ITunesBlockFileSharing = n.GetBoolValue(); } },
                { "iTunesBlockMusicService", n => { ITunesBlockMusicService = n.GetBoolValue(); } },
                { "keyboardBlockDictation", n => { KeyboardBlockDictation = n.GetBoolValue(); } },
                { "keychainBlockCloudSync", n => { KeychainBlockCloudSync = n.GetBoolValue(); } },
                { "multiplayerGamingBlocked", n => { MultiplayerGamingBlocked = n.GetBoolValue(); } },
                { "passwordBlockAirDropSharing", n => { PasswordBlockAirDropSharing = n.GetBoolValue(); } },
                { "passwordBlockAutoFill", n => { PasswordBlockAutoFill = n.GetBoolValue(); } },
                { "passwordBlockFingerprintUnlock", n => { PasswordBlockFingerprintUnlock = n.GetBoolValue(); } },
                { "passwordBlockModification", n => { PasswordBlockModification = n.GetBoolValue(); } },
                { "passwordBlockProximityRequests", n => { PasswordBlockProximityRequests = n.GetBoolValue(); } },
                { "passwordBlockSimple", n => { PasswordBlockSimple = n.GetBoolValue(); } },
                { "passwordExpirationDays", n => { PasswordExpirationDays = n.GetIntValue(); } },
                { "passwordMaximumAttemptCount", n => { PasswordMaximumAttemptCount = n.GetIntValue(); } },
                { "passwordMinimumCharacterSetCount", n => { PasswordMinimumCharacterSetCount = n.GetIntValue(); } },
                { "passwordMinimumLength", n => { PasswordMinimumLength = n.GetIntValue(); } },
                { "passwordMinutesOfInactivityBeforeLock", n => { PasswordMinutesOfInactivityBeforeLock = n.GetIntValue(); } },
                { "passwordMinutesOfInactivityBeforeScreenTimeout", n => { PasswordMinutesOfInactivityBeforeScreenTimeout = n.GetIntValue(); } },
                { "passwordMinutesUntilFailedLoginReset", n => { PasswordMinutesUntilFailedLoginReset = n.GetIntValue(); } },
                { "passwordPreviousPasswordBlockCount", n => { PasswordPreviousPasswordBlockCount = n.GetIntValue(); } },
                { "passwordRequired", n => { PasswordRequired = n.GetBoolValue(); } },
                { "passwordRequiredType", n => { PasswordRequiredType = n.GetEnumValue<global::AITGraph.Sdk.Models.RequiredPasswordType>(); } },
                { "privacyAccessControls", n => { PrivacyAccessControls = n.GetCollectionOfObjectValues<global::AITGraph.Sdk.Models.MacOSPrivacyAccessControlItem>(global::AITGraph.Sdk.Models.MacOSPrivacyAccessControlItem.CreateFromDiscriminatorValue)?.AsList(); } },
                { "safariBlockAutofill", n => { SafariBlockAutofill = n.GetBoolValue(); } },
                { "screenCaptureBlocked", n => { ScreenCaptureBlocked = n.GetBoolValue(); } },
                { "softwareUpdateMajorOSDeferredInstallDelayInDays", n => { SoftwareUpdateMajorOSDeferredInstallDelayInDays = n.GetIntValue(); } },
                { "softwareUpdateMinorOSDeferredInstallDelayInDays", n => { SoftwareUpdateMinorOSDeferredInstallDelayInDays = n.GetIntValue(); } },
                { "softwareUpdateNonOSDeferredInstallDelayInDays", n => { SoftwareUpdateNonOSDeferredInstallDelayInDays = n.GetIntValue(); } },
                { "softwareUpdatesEnforcedDelayInDays", n => { SoftwareUpdatesEnforcedDelayInDays = n.GetIntValue(); } },
                { "spotlightBlockInternetResults", n => { SpotlightBlockInternetResults = n.GetBoolValue(); } },
                { "touchIdTimeoutInHours", n => { TouchIdTimeoutInHours = n.GetIntValue(); } },
                { "updateDelayPolicy", n => { UpdateDelayPolicy = n.GetEnumValue<global::AITGraph.Sdk.Models.MacOSSoftwareUpdateDelayPolicy>(); } },
                { "wallpaperModificationBlocked", n => { WallpaperModificationBlocked = n.GetBoolValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public override void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            base.Serialize(writer);
            writer.WriteBoolValue("addingGameCenterFriendsBlocked", AddingGameCenterFriendsBlocked);
            writer.WriteBoolValue("airDropBlocked", AirDropBlocked);
            writer.WriteBoolValue("appleWatchBlockAutoUnlock", AppleWatchBlockAutoUnlock);
            writer.WriteBoolValue("cameraBlocked", CameraBlocked);
            writer.WriteBoolValue("classroomAppBlockRemoteScreenObservation", ClassroomAppBlockRemoteScreenObservation);
            writer.WriteBoolValue("classroomAppForceUnpromptedScreenObservation", ClassroomAppForceUnpromptedScreenObservation);
            writer.WriteBoolValue("classroomForceAutomaticallyJoinClasses", ClassroomForceAutomaticallyJoinClasses);
            writer.WriteBoolValue("classroomForceRequestPermissionToLeaveClasses", ClassroomForceRequestPermissionToLeaveClasses);
            writer.WriteBoolValue("classroomForceUnpromptedAppAndDeviceLock", ClassroomForceUnpromptedAppAndDeviceLock);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.AppListType>("compliantAppListType", CompliantAppListType);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.AppListItem>("compliantAppsList", CompliantAppsList);
            writer.WriteBoolValue("contentCachingBlocked", ContentCachingBlocked);
            writer.WriteBoolValue("definitionLookupBlocked", DefinitionLookupBlocked);
            writer.WriteCollectionOfPrimitiveValues<string>("emailInDomainSuffixes", EmailInDomainSuffixes);
            writer.WriteBoolValue("eraseContentAndSettingsBlocked", EraseContentAndSettingsBlocked);
            writer.WriteBoolValue("gameCenterBlocked", GameCenterBlocked);
            writer.WriteBoolValue("iCloudBlockActivityContinuation", ICloudBlockActivityContinuation);
            writer.WriteBoolValue("iCloudBlockAddressBook", ICloudBlockAddressBook);
            writer.WriteBoolValue("iCloudBlockBookmarks", ICloudBlockBookmarks);
            writer.WriteBoolValue("iCloudBlockCalendar", ICloudBlockCalendar);
            writer.WriteBoolValue("iCloudBlockDocumentSync", ICloudBlockDocumentSync);
            writer.WriteBoolValue("iCloudBlockMail", ICloudBlockMail);
            writer.WriteBoolValue("iCloudBlockNotes", ICloudBlockNotes);
            writer.WriteBoolValue("iCloudBlockPhotoLibrary", ICloudBlockPhotoLibrary);
            writer.WriteBoolValue("iCloudBlockReminders", ICloudBlockReminders);
            writer.WriteBoolValue("iCloudDesktopAndDocumentsBlocked", ICloudDesktopAndDocumentsBlocked);
            writer.WriteBoolValue("iCloudPrivateRelayBlocked", ICloudPrivateRelayBlocked);
            writer.WriteBoolValue("iTunesBlockFileSharing", ITunesBlockFileSharing);
            writer.WriteBoolValue("iTunesBlockMusicService", ITunesBlockMusicService);
            writer.WriteBoolValue("keyboardBlockDictation", KeyboardBlockDictation);
            writer.WriteBoolValue("keychainBlockCloudSync", KeychainBlockCloudSync);
            writer.WriteBoolValue("multiplayerGamingBlocked", MultiplayerGamingBlocked);
            writer.WriteBoolValue("passwordBlockAirDropSharing", PasswordBlockAirDropSharing);
            writer.WriteBoolValue("passwordBlockAutoFill", PasswordBlockAutoFill);
            writer.WriteBoolValue("passwordBlockFingerprintUnlock", PasswordBlockFingerprintUnlock);
            writer.WriteBoolValue("passwordBlockModification", PasswordBlockModification);
            writer.WriteBoolValue("passwordBlockProximityRequests", PasswordBlockProximityRequests);
            writer.WriteBoolValue("passwordBlockSimple", PasswordBlockSimple);
            writer.WriteIntValue("passwordExpirationDays", PasswordExpirationDays);
            writer.WriteIntValue("passwordMaximumAttemptCount", PasswordMaximumAttemptCount);
            writer.WriteIntValue("passwordMinimumCharacterSetCount", PasswordMinimumCharacterSetCount);
            writer.WriteIntValue("passwordMinimumLength", PasswordMinimumLength);
            writer.WriteIntValue("passwordMinutesOfInactivityBeforeLock", PasswordMinutesOfInactivityBeforeLock);
            writer.WriteIntValue("passwordMinutesOfInactivityBeforeScreenTimeout", PasswordMinutesOfInactivityBeforeScreenTimeout);
            writer.WriteIntValue("passwordMinutesUntilFailedLoginReset", PasswordMinutesUntilFailedLoginReset);
            writer.WriteIntValue("passwordPreviousPasswordBlockCount", PasswordPreviousPasswordBlockCount);
            writer.WriteBoolValue("passwordRequired", PasswordRequired);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.RequiredPasswordType>("passwordRequiredType", PasswordRequiredType);
            writer.WriteCollectionOfObjectValues<global::AITGraph.Sdk.Models.MacOSPrivacyAccessControlItem>("privacyAccessControls", PrivacyAccessControls);
            writer.WriteBoolValue("safariBlockAutofill", SafariBlockAutofill);
            writer.WriteBoolValue("screenCaptureBlocked", ScreenCaptureBlocked);
            writer.WriteIntValue("softwareUpdateMajorOSDeferredInstallDelayInDays", SoftwareUpdateMajorOSDeferredInstallDelayInDays);
            writer.WriteIntValue("softwareUpdateMinorOSDeferredInstallDelayInDays", SoftwareUpdateMinorOSDeferredInstallDelayInDays);
            writer.WriteIntValue("softwareUpdateNonOSDeferredInstallDelayInDays", SoftwareUpdateNonOSDeferredInstallDelayInDays);
            writer.WriteIntValue("softwareUpdatesEnforcedDelayInDays", SoftwareUpdatesEnforcedDelayInDays);
            writer.WriteBoolValue("spotlightBlockInternetResults", SpotlightBlockInternetResults);
            writer.WriteIntValue("touchIdTimeoutInHours", TouchIdTimeoutInHours);
            writer.WriteEnumValue<global::AITGraph.Sdk.Models.MacOSSoftwareUpdateDelayPolicy>("updateDelayPolicy", UpdateDelayPolicy);
            writer.WriteBoolValue("wallpaperModificationBlocked", WallpaperModificationBlocked);
        }
    }
}
#pragma warning restore CS0618
