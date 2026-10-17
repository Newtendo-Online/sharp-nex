using System;
using System.Collections.Generic;

// https://github.com/kinnay/NintendoClients/blob/8d3da6c9f647b02835551086c44cff65267230ae/nintendo/nex/errors.py
namespace nex
{
    public static class Constants
    {
        public const uint ERROR_MASK = 1u << 31;
    }

    public class nexError : Exception
    {
        public nexError() : base() { }
        public nexError(string message) : base(message) { }
        public nexError(string message, Exception innerException) : base(message, innerException) { }
    }

    public static class Core
    {
        public const uint Unknown = 0x00010001;
        public const uint NotImplemented = 0x00010002;
        public const uint InvalidPointer = 0x00010003;
        public const uint OperationAborted = 0x00010004;
        public const uint Exception = 0x00010005;
        public const uint AccessDenied = 0x00010006;
        public const uint InvalidHandle = 0x00010007;
        public const uint InvalidIndex = 0x00010008;
        public const uint OutOfMemory = 0x00010009;
        public const uint InvalidArgument = 0x0001000A;
        public const uint Timeout = 0x0001000B;
        public const uint InitializationFailure = 0x0001000C;
        public const uint CallInitiationFailure = 0x0001000D;
        public const uint RegistrationError = 0x0001000E;
        public const uint BufferOverflow = 0x0001000F;
        public const uint InvalidLockState = 0x00010010;
        public const uint InvalidSequence = 0x00010011;
        public const uint SystemError = 0x00010012;
        public const uint Cancelled = 0x00010013;
    }

    public static class DDL
    {
        public const uint InvalidSignature = 0x00020001;
        public const uint IncorrectVersion = 0x00020002;
    }

    public static class RendezVous
    {
        public const uint ConnectionFailure = 0x00030001;
        public const uint NotAuthenticated = 0x00030002;
        public const uint InvalidUsername = 0x00030064;
        public const uint InvalidPassword = 0x00030065;
        public const uint UsernameAlreadyExists = 0x00030066;
        public const uint AccountDisabled = 0x00030067;
        public const uint AccountExpired = 0x00030068;
        public const uint ConcurrentLoginDenied = 0x00030069;
        public const uint EncryptionFailure = 0x0003006A;
        public const uint InvalidPID = 0x0003006B;
        public const uint MaxConnectionsReached = 0x0003006C;
        public const uint InvalidGID = 0x0003006D;
        public const uint InvalidControlScriptID = 0x0003006E;
        public const uint InvalidOperationInLiveEnvironment = 0x0003006F;
        public const uint DuplicateEntry = 0x00030070;
        public const uint ControlScriptFailure = 0x00030071;
        public const uint ClassNotFound = 0x00030072;
        public const uint SessionVoid = 0x00030073;
        public const uint DDLMismatch = 0x00030075;
        public const uint InvalidConfiguration = 0x00030076;
        public const uint SessionFull = 0x000300C8;
        public const uint InvalidGatheringPassword = 0x000300C9;
        public const uint WithoutParticipationPeriod = 0x000300CA;
        public const uint PersistentGatheringCreationMax = 0x000300CB;
        public const uint PersistentGatheringParticipationMax = 0x000300CC;
        public const uint DeniedByParticipants = 0x000300CD;
        public const uint ParticipantInBlackList = 0x000300CE;
        public const uint GameServerMaintenance = 0x000300CF;
        public const uint OperationPostpone = 0x000300D0;
        public const uint OutOfRatingRange = 0x000300D1;
        public const uint ConnectionDisconnected = 0x000300D2;
        public const uint InvalidOperation = 0x000300D3;
        public const uint NotParticipatedGathering = 0x000300D4;
        public const uint MatchmakeSessionUserPasswordUnmatch = 0x000300D5;
        public const uint MatchmakeSessionSystemPasswordUnmatch = 0x000300D6;
        public const uint UserIsOffline = 0x000300D7;
        public const uint AlreadyParticipatedGathering = 0x000300D8;
        public const uint PermissionDenied = 0x000300D9;
        public const uint NotFriend = 0x000300DA;
        public const uint SessionClosed = 0x000300DB;
        public const uint DatabaseTemporarilyUnavailable = 0x000300DC;
        public const uint InvalidUniqueId = 0x000300DD;
        public const uint MatchmakingWithdrawn = 0x000300DE;
        public const uint LimitExceeded = 0x000300DF;
        public const uint AccountTemporarilyDisabled = 0x000300E0;
        public const uint PartiallyServiceClosed = 0x000300E1;
        public const uint ConnectionDisconnectedForConcurrentLogin = 0x000300E2;
    }

    public static class PythonCore
    {
        public const uint Exception = 0x00040001;
        public const uint TypeError = 0x00040002;
        public const uint IndexError = 0x00040003;
        public const uint InvalidReference = 0x00040004;
        public const uint CallFailure = 0x00040005;
        public const uint MemoryError = 0x00040006;
        public const uint KeyError = 0x00040007;
        public const uint OperationError = 0x00040008;
        public const uint ConversionError = 0x00040009;
        public const uint ValidationError = 0x0004000A;
    }

    public static class Transport
    {
        public const uint Unknown = 0x00050001;
        public const uint ConnectionFailure = 0x00050002;
        public const uint InvalidUrl = 0x00050003;
        public const uint InvalidKey = 0x00050004;
        public const uint InvalidURLType = 0x00050005;
        public const uint DuplicateEndpoint = 0x00050006;
        public const uint IOError = 0x00050007;
        public const uint Timeout = 0x00050008;
        public const uint ConnectionReset = 0x00050009;
        public const uint IncorrectRemoteAuthentication = 0x0005000A;
        public const uint ServerRequestError = 0x0005000B;
        public const uint DecompressionFailure = 0x0005000C;
        public const uint ReliableSendBufferFullFatal = 0x0005000D;
        public const uint UPnPCannotInit = 0x0005000E;
        public const uint UPnPCannotAddMapping = 0x0005000F;
        public const uint NatPMPCannotInit = 0x00050010;
        public const uint NatPMPCannotAddMapping = 0x00050011;
        public const uint UnsupportedNAT = 0x00050013;
        public const uint DnsError = 0x00050014;
        public const uint ProxyError = 0x00050015;
        public const uint DataRemaining = 0x00050016;
        public const uint NoBuffer = 0x00050017;
        public const uint NotFound = 0x00050018;
        public const uint TemporaryServerError = 0x00050019;
        public const uint PermanentServerError = 0x0005001A;
        public const uint ServiceUnavailable = 0x0005001B;
        public const uint ReliableSendBufferFull = 0x0005001C;
        public const uint InvalidStation = 0x0005001D;
        public const uint InvalidSubStreamID = 0x0005001E;
        public const uint PacketBufferFull = 0x0005001F;
        public const uint NatTraversalError = 0x00050020;
        public const uint NatCheckError = 0x00050021;
    }

    public static class DOCore
    {
        public const uint StationNotReached = 0x00060001;
        public const uint TargetStationDisconnect = 0x00060002;
        public const uint LocalStationLeaving = 0x00060003;
        public const uint ObjectNotFound = 0x00060004;
        public const uint InvalidRole = 0x00060005;
        public const uint CallTimeout = 0x00060006;
        public const uint RMCDispatchFailed = 0x00060007;
        public const uint MigrationInProgress = 0x00060008;
        public const uint NoAuthority = 0x00060009;
        public const uint NoTargetStationSpecified = 0x0006000A;
        public const uint JoinFailed = 0x0006000B;
        public const uint JoinDenied = 0x0006000C;
        public const uint ConnectivityTestFailed = 0x0006000D;
        public const uint Unknown = 0x0006000E;
        public const uint UnfreedReferences = 0x0006000F;
        public const uint JobTerminationFailed = 0x00060010;
        public const uint InvalidState = 0x00060011;
        public const uint FaultRecoveryFatal = 0x00060012;
        public const uint FaultRecoveryJobProcessFailed = 0x00060013;
        public const uint StationInconsitency = 0x00060014;
        public const uint AbnormalMasterState = 0x00060015;
        public const uint VersionMismatch = 0x00060016;
    }

    public static class FPD
    {
        public const uint NotInitialized = 0x00650000;
        public const uint AlreadyInitialized = 0x00650001;
        public const uint NotConnected = 0x00650002;
        public const uint Connected = 0x00650003;
        public const uint InitializationFailure = 0x00650004;
        public const uint OutOfMemory = 0x00650005;
        public const uint RmcFailed = 0x00650006;
        public const uint InvalidArgument = 0x00650007;
        public const uint InvalidLocalAccountID = 0x00650008;
        public const uint InvalidPrincipalID = 0x00650009;
        public const uint InvalidLocalFriendCode = 0x0065000A;
        public const uint LocalAccountNotExists = 0x0065000B;
        public const uint LocalAccountNotLoaded = 0x0065000C;
        public const uint LocalAccountAlreadyLoaded = 0x0065000D;
        public const uint FriendAlreadyExists = 0x0065000E;
        public const uint FriendNotExists = 0x0065000F;
        public const uint FriendNumMax = 0x00650010;
        public const uint NotFriend = 0x00650011;
        public const uint FileIO = 0x00650012;
        public const uint P2PInternetProhibited = 0x00650013;
        public const uint Unknown = 0x00650014;
        public const uint InvalidState = 0x00650015;
        public const uint AddFriendProhibited = 0x00650017;
        public const uint InvalidAccount = 0x00650019;
        public const uint BlacklistedByMe = 0x0065001A;
        public const uint FriendAlreadyAdded = 0x0065001C;
        public const uint MyFriendListLimitExceed = 0x0065001D;
        public const uint RequestLimitExceed = 0x0065001E;
        public const uint InvalidMessageID = 0x0065001F;
        public const uint MessageIsNotMine = 0x00650020;
        public const uint MessageIsNotForMe = 0x00650021;
        public const uint FriendRequestBlocked = 0x00650022;
        public const uint NotInMyFriendList = 0x00650023;
        public const uint FriendListedByMe = 0x00650024;
        public const uint NotInMyBlacklist = 0x00650025;
        public const uint IncompatibleAccount = 0x00650026;
        public const uint BlockSettingChangeNotAllowed = 0x00650027;
        public const uint SizeLimitExceeded = 0x00650028;
        public const uint OperationNotAllowed = 0x00650029;
        public const uint NotNetworkAccount = 0x0065002A;
        public const uint NotificationNotFound = 0x0065002B;
        public const uint PreferenceNotInitialized = 0x0065002C;
        public const uint FriendRequestNotAllowed = 0x0065002D;
    }

    public static class Ranking
    {
        public const uint NotInitialized = 0x00670001;
        public const uint InvalidArgument = 0x00670002;
        public const uint RegistrationError = 0x00670003;
        public const uint NotFound = 0x00670005;
        public const uint InvalidScore = 0x00670006;
        public const uint InvalidDataSize = 0x00670007;
        public const uint PermissionDenied = 0x00670009;
        public const uint Unknown = 0x0067000A;
        public const uint NotImplemented = 0x0067000B;
    }

    public static class Authentication
    {
        public const uint NASAuthenticateError = 0x00680001;
        public const uint TokenParseError = 0x00680002;
        public const uint HttpConnectionError = 0x00680003;
        public const uint HttpDNSError = 0x00680004;
        public const uint HttpGetProxySetting = 0x00680005;
        public const uint TokenExpired = 0x00680006;
        public const uint ValidationFailed = 0x00680007;
        public const uint InvalidParam = 0x00680008;
        public const uint PrincipalIdUnmatched = 0x00680009;
        public const uint MoveCountUnmatch = 0x0068000A;
        public const uint UnderMaintenance = 0x0068000B;
        public const uint UnsupportedVersion = 0x0068000C;
        public const uint ServerVersionIsOld = 0x0068000D;
        public const uint Unknown = 0x0068000E;
        public const uint ClientVersionIsOld = 0x0068000F;
        public const uint AccountLibraryError = 0x00680010;
        public const uint ServiceNoLongerAvailable = 0x00680011;
        public const uint UnknownApplication = 0x00680012;
        public const uint ApplicationVersionIsOld = 0x00680013;
        public const uint OutOfService = 0x00680014;
        public const uint NetworkServiceLicenseRequired = 0x00680015;
        public const uint NetworkServiceLicenseSystemError = 0x00680016;
        public const uint NetworkServiceLicenseError3 = 0x00680017;
        public const uint NetworkServiceLicenseError4 = 0x00680018;
    }

    public static class DataStore
    {
        public const uint Unknown = 0x00690001;
        public const uint InvalidArgument = 0x00690002;
        public const uint PermissionDenied = 0x00690003;
        public const uint NotFound = 0x00690004;
        public const uint AlreadyLocked = 0x00690005;
        public const uint UnderReviewing = 0x00690006;
        public const uint Expired = 0x00690007;
        public const uint InvalidCheckToken = 0x00690008;
        public const uint SystemFileError = 0x00690009;
        public const uint OverCapacity = 0x0069000A;
        public const uint OperationNotAllowed = 0x0069000B;
        public const uint InvalidPassword = 0x0069000C;
        public const uint ValueNotEqual = 0x0069000D;
    }

    public static class ServiceItem
    {
        public const uint Unknown = 0x006C0001;
        public const uint InvalidArgument = 0x006C0002;
        public const uint EShopUnknownHttpError = 0x006C0003;
        public const uint EShopResponseParseError = 0x006C0004;
        public const uint NotOwned = 0x006C0005;
        public const uint InvalidLimitationType = 0x006C0006;
        public const uint ConsumptionRightShortage = 0x006C0007;
    }

    public static class MatchmakeReferee
    {
        public const uint Unknown = 0x006F0001;
        public const uint InvalidArgument = 0x006F0002;
        public const uint AlreadyExists = 0x006F0003;
        public const uint NotParticipatedGathering = 0x006F0004;
        public const uint NotParticipatedRound = 0x006F0005;
        public const uint StatsNotFound = 0x006F0006;
        public const uint RoundNotFound = 0x006F0007;
        public const uint RoundArbitrated = 0x006F0008;
        public const uint RoundNotArbitrated = 0x006F0009;
    }

    public static class Subscriber
    {
        public const uint Unknown = 0x00700001;
        public const uint InvalidArgument = 0x00700002;
        public const uint OverLimit = 0x00700003;
        public const uint PermissionDenied = 0x00700004;
    }

    public static class Ranking2
    {
        public const uint Unknown = 0x00710001;
        public const uint InvalidArgument = 0x00710002;
        public const uint InvalidScore = 0x00710003;
    }

    public static class SmartDeviceVoiceChat
    {
        public const uint Unknown = 0x00720001;
        public const uint InvalidArgument = 0x00720002;
        public const uint InvalidResponse = 0x00720003;
        public const uint InvalidAccessToken = 0x00720004;
        public const uint Unauthorized = 0x00720005;
        public const uint AccessError = 0x00720006;
        public const uint UserNotFound = 0x00720007;
        public const uint RoomNotFound = 0x00720008;
        public const uint RoomNotActivated = 0x00720009;
        public const uint ApplicationNotSupported = 0x0072000A;
        public const uint InternalServerError = 0x0072000B;
        public const uint ServiceUnavailable = 0x0072000C;
        public const uint UnexpectedError = 0x0072000D;
        public const uint UnderMaintenance = 0x0072000E;
        public const uint ServiceNoLongerAvailable = 0x0072000F;
        public const uint AccountTemporarilyDisabled = 0x00720010;
        public const uint PermissionDenied = 0x00720011;
        public const uint NetworkServiceLicenseRequired = 0x00720012;
        public const uint AccountLibraryError = 0x00720013;
        public const uint GameModeNotFound = 0x00720014;
    }

    public static class Screening
    {
        public const uint Unknown = 0x00730001;
        public const uint InvalidArgument = 0x00730002;
        public const uint NotFound = 0x00730003;
    }

    public static class Custom
    {
        public const uint Unknown = 0x00740001;
    }

    public static class Ess
    {
        public const uint Unknown = 0x00750001;
        public const uint GameSessionError = 0x00750002;
        public const uint GameSessionMaintenance = 0x00750003;
    }

    public static class Errors
    {
        public static readonly Dictionary<uint, string> ErrorNames = new()
        {
            // Core
            { Core.Unknown, "Core::Unknown" },
            { Core.NotImplemented, "Core::NotImplemented" },
            { Core.InvalidPointer, "Core::InvalidPointer" },
            { Core.OperationAborted, "Core::OperationAborted" },
            { Core.Exception, "Core::Exception" },
            { Core.AccessDenied, "Core::AccessDenied" },
            { Core.InvalidHandle, "Core::InvalidHandle" },
            { Core.InvalidIndex, "Core::InvalidIndex" },
            { Core.OutOfMemory, "Core::OutOfMemory" },
            { Core.InvalidArgument, "Core::InvalidArgument" },
            { Core.Timeout, "Core::Timeout" },
            { Core.InitializationFailure, "Core::InitializationFailure" },
            { Core.CallInitiationFailure, "Core::CallInitiationFailure" },
            { Core.RegistrationError, "Core::RegistrationError" },
            { Core.BufferOverflow, "Core::BufferOverflow" },
            { Core.InvalidLockState, "Core::InvalidLockState" },
            { Core.InvalidSequence, "Core::InvalidSequence" },
            { Core.SystemError, "Core::SystemError" },
            { Core.Cancelled, "Core::Cancelled" },

            // DDL
            { DDL.InvalidSignature, "DDL::InvalidSignature" },
            { DDL.IncorrectVersion, "DDL::IncorrectVersion" },

            // RendezVous
            { RendezVous.ConnectionFailure, "RendezVous::ConnectionFailure" },
            { RendezVous.NotAuthenticated, "RendezVous::NotAuthenticated" },
            { RendezVous.InvalidUsername, "RendezVous::InvalidUsername" },
            { RendezVous.InvalidPassword, "RendezVous::InvalidPassword" },
            { RendezVous.UsernameAlreadyExists, "RendezVous::UsernameAlreadyExists" },
            { RendezVous.AccountDisabled, "RendezVous::AccountDisabled" },
            { RendezVous.AccountExpired, "RendezVous::AccountExpired" },
            { RendezVous.ConcurrentLoginDenied, "RendezVous::ConcurrentLoginDenied" },
            { RendezVous.EncryptionFailure, "RendezVous::EncryptionFailure" },
            { RendezVous.InvalidPID, "RendezVous::InvalidPID" },
            { RendezVous.MaxConnectionsReached, "RendezVous::MaxConnectionsReached" },
            { RendezVous.InvalidGID, "RendezVous::InvalidGID" },
            { RendezVous.InvalidControlScriptID, "RendezVous::InvalidControlScriptID" },
            { RendezVous.InvalidOperationInLiveEnvironment, "RendezVous::InvalidOperationInLiveEnvironment" },
            { RendezVous.DuplicateEntry, "RendezVous::DuplicateEntry" },
            { RendezVous.ControlScriptFailure, "RendezVous::ControlScriptFailure" },
            { RendezVous.ClassNotFound, "RendezVous::ClassNotFound" },
            { RendezVous.SessionVoid, "RendezVous::SessionVoid" },
            { RendezVous.DDLMismatch, "RendezVous::DDLMismatch" },
            { RendezVous.InvalidConfiguration, "RendezVous::InvalidConfiguration" },
            { RendezVous.SessionFull, "RendezVous::SessionFull" },
            { RendezVous.InvalidGatheringPassword, "RendezVous::InvalidGatheringPassword" },
            { RendezVous.WithoutParticipationPeriod, "RendezVous::WithoutParticipationPeriod" },
            { RendezVous.PersistentGatheringCreationMax, "RendezVous::PersistentGatheringCreationMax" },
            { RendezVous.PersistentGatheringParticipationMax, "RendezVous::PersistentGatheringParticipationMax" },
            { RendezVous.DeniedByParticipants, "RendezVous::DeniedByParticipants" },
            { RendezVous.ParticipantInBlackList, "RendezVous::ParticipantInBlackList" },
            { RendezVous.GameServerMaintenance, "RendezVous::GameServerMaintenance" },
            { RendezVous.OperationPostpone, "RendezVous::OperationPostpone" },
            { RendezVous.OutOfRatingRange, "RendezVous::OutOfRatingRange" },
            { RendezVous.ConnectionDisconnected, "RendezVous::ConnectionDisconnected" },
            { RendezVous.InvalidOperation, "RendezVous::InvalidOperation" },
            { RendezVous.NotParticipatedGathering, "RendezVous::NotParticipatedGathering" },
            { RendezVous.MatchmakeSessionUserPasswordUnmatch, "RendezVous::MatchmakeSessionUserPasswordUnmatch" },
            { RendezVous.MatchmakeSessionSystemPasswordUnmatch, "RendezVous::MatchmakeSessionSystemPasswordUnmatch" },
            { RendezVous.UserIsOffline, "RendezVous::UserIsOffline" },
            { RendezVous.AlreadyParticipatedGathering, "RendezVous::AlreadyParticipatedGathering" },
            { RendezVous.PermissionDenied, "RendezVous::PermissionDenied" },
            { RendezVous.NotFriend, "RendezVous::NotFriend" },
            { RendezVous.SessionClosed, "RendezVous::SessionClosed" },
            { RendezVous.DatabaseTemporarilyUnavailable, "RendezVous::DatabaseTemporarilyUnavailable" },
            { RendezVous.InvalidUniqueId, "RendezVous::InvalidUniqueId" },
            { RendezVous.MatchmakingWithdrawn, "RendezVous::MatchmakingWithdrawn" },
            { RendezVous.LimitExceeded, "RendezVous::LimitExceeded" },
            { RendezVous.AccountTemporarilyDisabled, "RendezVous::AccountTemporarilyDisabled" },
            { RendezVous.PartiallyServiceClosed, "RendezVous::PartiallyServiceClosed" },
            { RendezVous.ConnectionDisconnectedForConcurrentLogin, "RendezVous::ConnectionDisconnectedForConcurrentLogin" },

            // PythonCore
            { PythonCore.Exception, "PythonCore::Exception" },
            { PythonCore.TypeError, "PythonCore::TypeError" },
            { PythonCore.IndexError, "PythonCore::IndexError" },
            { PythonCore.InvalidReference, "PythonCore::InvalidReference" },
            { PythonCore.CallFailure, "PythonCore::CallFailure" },
            { PythonCore.MemoryError, "PythonCore::MemoryError" },
            { PythonCore.KeyError, "PythonCore::KeyError" },
            { PythonCore.OperationError, "PythonCore::OperationError" },
            { PythonCore.ConversionError, "PythonCore::ConversionError" },
            { PythonCore.ValidationError, "PythonCore::ValidationError" },

            // Transport
            { Transport.Unknown, "Transport::Unknown" },
            { Transport.ConnectionFailure, "Transport::ConnectionFailure" },
            { Transport.InvalidUrl, "Transport::InvalidUrl" },
            { Transport.InvalidKey, "Transport::InvalidKey" },
            { Transport.InvalidURLType, "Transport::InvalidURLType" },
            { Transport.DuplicateEndpoint, "Transport::DuplicateEndpoint" },
            { Transport.IOError, "Transport::IOError" },
            { Transport.Timeout, "Transport::Timeout" },
            { Transport.ConnectionReset, "Transport::ConnectionReset" },
            { Transport.IncorrectRemoteAuthentication, "Transport::IncorrectRemoteAuthentication" },
            { Transport.ServerRequestError, "Transport::ServerRequestError" },
            { Transport.DecompressionFailure, "Transport::DecompressionFailure" },
            { Transport.ReliableSendBufferFullFatal, "Transport::ReliableSendBufferFullFatal" },
            { Transport.UPnPCannotInit, "Transport::UPnPCannotInit" },
            { Transport.UPnPCannotAddMapping, "Transport::UPnPCannotAddMapping" },
            { Transport.NatPMPCannotInit, "Transport::NatPMPCannotInit" },
            { Transport.NatPMPCannotAddMapping, "Transport::NatPMPCannotAddMapping" },
            { Transport.UnsupportedNAT, "Transport::UnsupportedNAT" },
            { Transport.DnsError, "Transport::DnsError" },
            { Transport.ProxyError, "Transport::ProxyError" },
            { Transport.DataRemaining, "Transport::DataRemaining" },
            { Transport.NoBuffer, "Transport::NoBuffer" },
            { Transport.NotFound, "Transport::NotFound" },
            { Transport.TemporaryServerError, "Transport::TemporaryServerError" },
            { Transport.PermanentServerError, "Transport::PermanentServerError" },
            { Transport.ServiceUnavailable, "Transport::ServiceUnavailable" },
            { Transport.ReliableSendBufferFull, "Transport::ReliableSendBufferFull" },
            { Transport.InvalidStation, "Transport::InvalidStation" },
            { Transport.InvalidSubStreamID, "Transport::InvalidSubStreamID" },
            { Transport.PacketBufferFull, "Transport::PacketBufferFull" },
            { Transport.NatTraversalError, "Transport::NatTraversalError" },
            { Transport.NatCheckError, "Transport::NatCheckError" },

            // DOCore
            { DOCore.StationNotReached, "DOCore::StationNotReached" },
            { DOCore.TargetStationDisconnect, "DOCore::TargetStationDisconnect" },
            { DOCore.LocalStationLeaving, "DOCore::LocalStationLeaving" },
            { DOCore.ObjectNotFound, "DOCore::ObjectNotFound" },
            { DOCore.InvalidRole, "DOCore::InvalidRole" },
            { DOCore.CallTimeout, "DOCore::CallTimeout" },
            { DOCore.RMCDispatchFailed, "DOCore::RMCDispatchFailed" },
            { DOCore.MigrationInProgress, "DOCore::MigrationInProgress" },
            { DOCore.NoAuthority, "DOCore::NoAuthority" },
            { DOCore.NoTargetStationSpecified, "DOCore::NoTargetStationSpecified" },
            { DOCore.JoinFailed, "DOCore::JoinFailed" },
            { DOCore.JoinDenied, "DOCore::JoinDenied" },
            { DOCore.ConnectivityTestFailed, "DOCore::ConnectivityTestFailed" },
            { DOCore.Unknown, "DOCore::Unknown" },
            { DOCore.UnfreedReferences, "DOCore::UnfreedReferences" },
            { DOCore.JobTerminationFailed, "DOCore::JobTerminationFailed" },
            { DOCore.InvalidState, "DOCore::InvalidState" },
            { DOCore.FaultRecoveryFatal, "DOCore::FaultRecoveryFatal" },
            { DOCore.FaultRecoveryJobProcessFailed, "DOCore::FaultRecoveryJobProcessFailed" },
            { DOCore.StationInconsitency, "DOCore::StationInconsitency" },
            { DOCore.AbnormalMasterState, "DOCore::AbnormalMasterState" },
            { DOCore.VersionMismatch, "DOCore::VersionMismatch" },

            // FPD
            { FPD.NotInitialized, "FPD::NotInitialized" },
            { FPD.AlreadyInitialized, "FPD::AlreadyInitialized" },
            { FPD.NotConnected, "FPD::NotConnected" },
            { FPD.Connected, "FPD::Connected" },
            { FPD.InitializationFailure, "FPD::InitializationFailure" },
            { FPD.OutOfMemory, "FPD::OutOfMemory" },
            { FPD.RmcFailed, "FPD::RmcFailed" },
            { FPD.InvalidArgument, "FPD::InvalidArgument" },
            { FPD.InvalidLocalAccountID, "FPD::InvalidLocalAccountID" },
            { FPD.InvalidPrincipalID, "FPD::InvalidPrincipalID" },
            { FPD.InvalidLocalFriendCode, "FPD::InvalidLocalFriendCode" },
            { FPD.LocalAccountNotExists, "FPD::LocalAccountNotExists" },
            { FPD.LocalAccountNotLoaded, "FPD::LocalAccountNotLoaded" },
            { FPD.LocalAccountAlreadyLoaded, "FPD::LocalAccountAlreadyLoaded" },
            { FPD.FriendAlreadyExists, "FPD::FriendAlreadyExists" },
            { FPD.FriendNotExists, "FPD::FriendNotExists" },
            { FPD.FriendNumMax, "FPD::FriendNumMax" },
            { FPD.NotFriend, "FPD::NotFriend" },
            { FPD.FileIO, "FPD::FileIO" },
            { FPD.P2PInternetProhibited, "FPD::P2PInternetProhibited" },
            { FPD.Unknown, "FPD::Unknown" },
            { FPD.InvalidState, "FPD::InvalidState" },
            { FPD.AddFriendProhibited, "FPD::AddFriendProhibited" },
            { FPD.InvalidAccount, "FPD::InvalidAccount" },
            { FPD.BlacklistedByMe, "FPD::BlacklistedByMe" },
            { FPD.FriendAlreadyAdded, "FPD::FriendAlreadyAdded" },
            { FPD.MyFriendListLimitExceed, "FPD::MyFriendListLimitExceed" },
            { FPD.RequestLimitExceed, "FPD::RequestLimitExceed" },
            { FPD.InvalidMessageID, "FPD::InvalidMessageID" },
            { FPD.MessageIsNotMine, "FPD::MessageIsNotMine" },
            { FPD.MessageIsNotForMe, "FPD::MessageIsNotForMe" },
            { FPD.FriendRequestBlocked, "FPD::FriendRequestBlocked" },
            { FPD.NotInMyFriendList, "FPD::NotInMyFriendList" },
            { FPD.FriendListedByMe, "FPD::FriendListedByMe" },
            { FPD.NotInMyBlacklist, "FPD::NotInMyBlacklist" },
            { FPD.IncompatibleAccount, "FPD::IncompatibleAccount" },
            { FPD.BlockSettingChangeNotAllowed, "FPD::BlockSettingChangeNotAllowed" },
            { FPD.SizeLimitExceeded, "FPD::SizeLimitExceeded" },
            { FPD.OperationNotAllowed, "FPD::OperationNotAllowed" },
            { FPD.NotNetworkAccount, "FPD::NotNetworkAccount" },
            { FPD.NotificationNotFound, "FPD::NotificationNotFound" },
            { FPD.PreferenceNotInitialized, "FPD::PreferenceNotInitialized" },
            { FPD.FriendRequestNotAllowed, "FPD::FriendRequestNotAllowed" },

            // Ranking
            { Ranking.NotInitialized, "Ranking::NotInitialized" },
            { Ranking.InvalidArgument, "Ranking::InvalidArgument" },
            { Ranking.RegistrationError, "Ranking::RegistrationError" },
            { Ranking.NotFound, "Ranking::NotFound" },
            { Ranking.InvalidScore, "Ranking::InvalidScore" },
            { Ranking.InvalidDataSize, "Ranking::InvalidDataSize" },
            { Ranking.PermissionDenied, "Ranking::PermissionDenied" },
            { Ranking.Unknown, "Ranking::Unknown" },
            { Ranking.NotImplemented, "Ranking::NotImplemented" },

            // Authentication
            { Authentication.NASAuthenticateError, "Authentication::NASAuthenticateError" },
            { Authentication.TokenParseError, "Authentication::TokenParseError" },
            { Authentication.HttpConnectionError, "Authentication::HttpConnectionError" },
            { Authentication.HttpDNSError, "Authentication::HttpDNSError" },
            { Authentication.HttpGetProxySetting, "Authentication::HttpGetProxySetting" },
            { Authentication.TokenExpired, "Authentication::TokenExpired" },
            { Authentication.ValidationFailed, "Authentication::ValidationFailed" },
            { Authentication.InvalidParam, "Authentication::InvalidParam" },
            { Authentication.PrincipalIdUnmatched, "Authentication::PrincipalIdUnmatched" },
            { Authentication.MoveCountUnmatch, "Authentication::MoveCountUnmatch" },
            { Authentication.UnderMaintenance, "Authentication::UnderMaintenance" },
            { Authentication.UnsupportedVersion, "Authentication::UnsupportedVersion" },
            { Authentication.ServerVersionIsOld, "Authentication::ServerVersionIsOld" },
            { Authentication.Unknown, "Authentication::Unknown" },
            { Authentication.ClientVersionIsOld, "Authentication::ClientVersionIsOld" },
            { Authentication.AccountLibraryError, "Authentication::AccountLibraryError" },
            { Authentication.ServiceNoLongerAvailable, "Authentication::ServiceNoLongerAvailable" },
            { Authentication.UnknownApplication, "Authentication::UnknownApplication" },
            { Authentication.ApplicationVersionIsOld, "Authentication::ApplicationVersionIsOld" },
            { Authentication.OutOfService, "Authentication::OutOfService" },
            { Authentication.NetworkServiceLicenseRequired, "Authentication::NetworkServiceLicenseRequired" },
            { Authentication.NetworkServiceLicenseSystemError, "Authentication::NetworkServiceLicenseSystemError" },
            { Authentication.NetworkServiceLicenseError3, "Authentication::NetworkServiceLicenseError3" },
            { Authentication.NetworkServiceLicenseError4, "Authentication::NetworkServiceLicenseError4" },

            // DataStore
            { DataStore.Unknown, "DataStore::Unknown" },
            { DataStore.InvalidArgument, "DataStore::InvalidArgument" },
            { DataStore.PermissionDenied, "DataStore::PermissionDenied" },
            { DataStore.NotFound, "DataStore::NotFound" },
            { DataStore.AlreadyLocked, "DataStore::AlreadyLocked" },
            { DataStore.UnderReviewing, "DataStore::UnderReviewing" },
            { DataStore.Expired, "DataStore::Expired" },
            { DataStore.InvalidCheckToken, "DataStore::InvalidCheckToken" },
            { DataStore.SystemFileError, "DataStore::SystemFileError" },
            { DataStore.OverCapacity, "DataStore::OverCapacity" },
            { DataStore.OperationNotAllowed, "DataStore::OperationNotAllowed" },
            { DataStore.InvalidPassword, "DataStore::InvalidPassword" },
            { DataStore.ValueNotEqual, "DataStore::ValueNotEqual" },

            // ServiceItem
            { ServiceItem.Unknown, "ServiceItem::Unknown" },
            { ServiceItem.InvalidArgument, "ServiceItem::InvalidArgument" },
            { ServiceItem.EShopUnknownHttpError, "ServiceItem::EShopUnknownHttpError" },
            { ServiceItem.EShopResponseParseError, "ServiceItem::EShopResponseParseError" },
            { ServiceItem.NotOwned, "ServiceItem::NotOwned" },
            { ServiceItem.InvalidLimitationType, "ServiceItem::InvalidLimitationType" },
            { ServiceItem.ConsumptionRightShortage, "ServiceItem::ConsumptionRightShortage" },

            // MatchmakeReferee
            { MatchmakeReferee.Unknown, "MatchmakeReferee::Unknown" },
            { MatchmakeReferee.InvalidArgument, "MatchmakeReferee::InvalidArgument" },
            { MatchmakeReferee.AlreadyExists, "MatchmakeReferee::AlreadyExists" },
            { MatchmakeReferee.NotParticipatedGathering, "MatchmakeReferee::NotParticipatedGathering" },
            { MatchmakeReferee.NotParticipatedRound, "MatchmakeReferee::NotParticipatedRound" },
            { MatchmakeReferee.StatsNotFound, "MatchmakeReferee::StatsNotFound" },
            { MatchmakeReferee.RoundNotFound, "MatchmakeReferee::RoundNotFound" },
            { MatchmakeReferee.RoundArbitrated, "MatchmakeReferee::RoundArbitrated" },
            { MatchmakeReferee.RoundNotArbitrated, "MatchmakeReferee::RoundNotArbitrated" },

            // Subscriber
            { Subscriber.Unknown, "Subscriber::Unknown" },
            { Subscriber.InvalidArgument, "Subscriber::InvalidArgument" },
            { Subscriber.OverLimit, "Subscriber::OverLimit" },
            { Subscriber.PermissionDenied, "Subscriber::PermissionDenied" },

            // Ranking2
            { Ranking2.Unknown, "Ranking2::Unknown" },
            { Ranking2.InvalidArgument, "Ranking2::InvalidArgument" },
            { Ranking2.InvalidScore, "Ranking2::InvalidScore" },

            // SmartDeviceVoiceChat
            { SmartDeviceVoiceChat.Unknown, "SmartDeviceVoiceChat::Unknown" },
            { SmartDeviceVoiceChat.InvalidArgument, "SmartDeviceVoiceChat::InvalidArgument" },
            { SmartDeviceVoiceChat.InvalidResponse, "SmartDeviceVoiceChat::InvalidResponse" },
            { SmartDeviceVoiceChat.InvalidAccessToken, "SmartDeviceVoiceChat::InvalidAccessToken" },
            { SmartDeviceVoiceChat.Unauthorized, "SmartDeviceVoiceChat::Unauthorized" },
            { SmartDeviceVoiceChat.AccessError, "SmartDeviceVoiceChat::AccessError" },
            { SmartDeviceVoiceChat.UserNotFound, "SmartDeviceVoiceChat::UserNotFound" },
            { SmartDeviceVoiceChat.RoomNotFound, "SmartDeviceVoiceChat::RoomNotFound" },
            { SmartDeviceVoiceChat.RoomNotActivated, "SmartDeviceVoiceChat::RoomNotActivated" },
            { SmartDeviceVoiceChat.ApplicationNotSupported, "SmartDeviceVoiceChat::ApplicationNotSupported" },
            { SmartDeviceVoiceChat.InternalServerError, "SmartDeviceVoiceChat::InternalServerError" },
            { SmartDeviceVoiceChat.ServiceUnavailable, "SmartDeviceVoiceChat::ServiceUnavailable" },
            { SmartDeviceVoiceChat.UnexpectedError, "SmartDeviceVoiceChat::UnexpectedError" },
            { SmartDeviceVoiceChat.UnderMaintenance, "SmartDeviceVoiceChat::UnderMaintenance" },
            { SmartDeviceVoiceChat.ServiceNoLongerAvailable, "SmartDeviceVoiceChat::ServiceNoLongerAvailable" },
            { SmartDeviceVoiceChat.AccountTemporarilyDisabled, "SmartDeviceVoiceChat::AccountTemporarilyDisabled" },
            { SmartDeviceVoiceChat.PermissionDenied, "SmartDeviceVoiceChat::PermissionDenied" },
            { SmartDeviceVoiceChat.NetworkServiceLicenseRequired, "SmartDeviceVoiceChat::NetworkServiceLicenseRequired" },
            { SmartDeviceVoiceChat.AccountLibraryError, "SmartDeviceVoiceChat::AccountLibraryError" },
            { SmartDeviceVoiceChat.GameModeNotFound, "SmartDeviceVoiceChat::GameModeNotFound" },

            // Screening
            { Screening.Unknown, "Screening::Unknown" },
            { Screening.InvalidArgument, "Screening::InvalidArgument" },
            { Screening.NotFound, "Screening::NotFound" },

            // Custom
            { Custom.Unknown, "Custom::Unknown" },

            // Ess
            { Ess.Unknown, "Ess::Unknown" },
            { Ess.GameSessionError, "Ess::GameSessionError" },
            { Ess.GameSessionMaintenance, "Ess::GameSessionMaintenance" }
        };

        public static string ErrorNameFromCode(uint errorCode)
        {
            if (ErrorNames.TryGetValue(errorCode, out var name))
            {
                return name;
            }
            return "Invalid Error Code: " + errorCode;
        }
    }
}