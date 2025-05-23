using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe partial class ISteamUtils : SteamInterface
	{
		public const string Version = "SteamUtils010";
		
		internal ISteamUtils( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_SteamUtils_v010", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamUtils_v010();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamUtils_v010();
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_SteamGameServerUtils_v010", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamGameServerUtils_v010();
		public override IntPtr GetServerInterfacePointer() => SteamAPI_SteamGameServerUtils_v010();
		
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetSecondsSinceAppActive", CallingConvention = Platform.CC)]
		private static extern uint _GetSecondsSinceAppActive( IntPtr self );
		
		#endregion
		internal uint GetSecondsSinceAppActive()
		{
			var returnValue = _GetSecondsSinceAppActive( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetSecondsSinceComputerActive", CallingConvention = Platform.CC)]
		private static extern uint _GetSecondsSinceComputerActive( IntPtr self );
		
		#endregion
		internal uint GetSecondsSinceComputerActive()
		{
			var returnValue = _GetSecondsSinceComputerActive( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetConnectedUniverse", CallingConvention = Platform.CC)]
		private static extern Universe _GetConnectedUniverse( IntPtr self );
		
		#endregion
		internal Universe GetConnectedUniverse()
		{
			var returnValue = _GetConnectedUniverse( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetServerRealTime", CallingConvention = Platform.CC)]
		private static extern uint _GetServerRealTime( IntPtr self );
		
		#endregion
		internal uint GetServerRealTime()
		{
			var returnValue = _GetServerRealTime( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetIPCountry", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetIPCountry( IntPtr self );
		
		#endregion
		internal string GetIPCountry()
		{
			var returnValue = _GetIPCountry( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetImageSize", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetImageSize( IntPtr self, int iImage, ref uint pnWidth, ref uint pnHeight );
		
		#endregion
		internal bool GetImageSize( int iImage, ref uint pnWidth, ref uint pnHeight )
		{
			var returnValue = _GetImageSize( Self, iImage, ref pnWidth, ref pnHeight );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetImageRGBA", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetImageRGBA( IntPtr self, int iImage, [In,Out] byte[]  pubDest, int nDestBufferSize );
		
		#endregion
		internal bool GetImageRGBA( int iImage, [In,Out] byte[]  pubDest, int nDestBufferSize )
		{
			var returnValue = _GetImageRGBA( Self, iImage, pubDest, nDestBufferSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetCurrentBatteryPower", CallingConvention = Platform.CC)]
		private static extern byte _GetCurrentBatteryPower( IntPtr self );
		
		#endregion
		internal byte GetCurrentBatteryPower()
		{
			var returnValue = _GetCurrentBatteryPower( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetAppID", CallingConvention = Platform.CC)]
		private static extern uint _GetAppID( IntPtr self );
		
		#endregion
		internal uint GetAppID()
		{
			var returnValue = _GetAppID( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_SetOverlayNotificationPosition", CallingConvention = Platform.CC)]
		private static extern void _SetOverlayNotificationPosition( IntPtr self, NotificationPosition eNotificationPosition );
		
		#endregion
		internal void SetOverlayNotificationPosition( NotificationPosition eNotificationPosition )
		{
			_SetOverlayNotificationPosition( Self, eNotificationPosition );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_IsAPICallCompleted", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsAPICallCompleted( IntPtr self, SteamAPICall_t hSteamAPICall, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed );
		
		#endregion
		internal bool IsAPICallCompleted( SteamAPICall_t hSteamAPICall, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed )
		{
			var returnValue = _IsAPICallCompleted( Self, hSteamAPICall, ref pbFailed );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetAPICallFailureReason", CallingConvention = Platform.CC)]
		private static extern SteamAPICallFailure _GetAPICallFailureReason( IntPtr self, SteamAPICall_t hSteamAPICall );
		
		#endregion
		internal SteamAPICallFailure GetAPICallFailureReason( SteamAPICall_t hSteamAPICall )
		{
			var returnValue = _GetAPICallFailureReason( Self, hSteamAPICall );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetAPICallResult", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetAPICallResult( IntPtr self, SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed );
		
		#endregion
		internal bool GetAPICallResult( SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed )
		{
			var returnValue = _GetAPICallResult( Self, hSteamAPICall, pCallback, cubCallback, iCallbackExpected, ref pbFailed );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetIPCCallCount", CallingConvention = Platform.CC)]
		private static extern uint _GetIPCCallCount( IntPtr self );
		
		#endregion
		internal uint GetIPCCallCount()
		{
			var returnValue = _GetIPCCallCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_SetWarningMessageHook", CallingConvention = Platform.CC)]
		private static extern void _SetWarningMessageHook( IntPtr self, IntPtr pFunction );
		
		#endregion
		internal void SetWarningMessageHook( IntPtr pFunction )
		{
			_SetWarningMessageHook( Self, pFunction );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_IsOverlayEnabled", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsOverlayEnabled( IntPtr self );
		
		#endregion
		internal bool IsOverlayEnabled()
		{
			var returnValue = _IsOverlayEnabled( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_BOverlayNeedsPresent", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BOverlayNeedsPresent( IntPtr self );
		
		#endregion
		internal bool BOverlayNeedsPresent()
		{
			var returnValue = _BOverlayNeedsPresent( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_CheckFileSignature", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _CheckFileSignature( IntPtr self, IntPtr szFileName );
		
		#endregion
		internal CallResult<CheckFileSignature_t> CheckFileSignature( string szFileName )
		{
			using var str__szFileName = new Utf8StringToNative( szFileName );
			var returnValue = _CheckFileSignature( Self, str__szFileName.Pointer );
			return new CallResult<CheckFileSignature_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_ShowGamepadTextInput", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ShowGamepadTextInput( IntPtr self, GamepadTextInputMode eInputMode, GamepadTextInputLineMode eLineInputMode, IntPtr pchDescription, uint unCharMax, IntPtr pchExistingText );
		
		#endregion
		internal bool ShowGamepadTextInput( GamepadTextInputMode eInputMode, GamepadTextInputLineMode eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText )
		{
			using var str__pchDescription = new Utf8StringToNative( pchDescription );
			using var str__pchExistingText = new Utf8StringToNative( pchExistingText );
			var returnValue = _ShowGamepadTextInput( Self, eInputMode, eLineInputMode, str__pchDescription.Pointer, unCharMax, str__pchExistingText.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetEnteredGamepadTextLength", CallingConvention = Platform.CC)]
		private static extern uint _GetEnteredGamepadTextLength( IntPtr self );
		
		#endregion
		internal uint GetEnteredGamepadTextLength()
		{
			var returnValue = _GetEnteredGamepadTextLength( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetEnteredGamepadTextInput", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetEnteredGamepadTextInput( IntPtr self, IntPtr pchText, uint cchText );
		
		#endregion
		internal bool GetEnteredGamepadTextInput( out string pchText )
		{
			using var mem__pchText = Helpers.TakeMemory();
			var returnValue = _GetEnteredGamepadTextInput( Self, mem__pchText, (1024 * 32) );
			pchText = Helpers.MemoryToString( mem__pchText );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetSteamUILanguage", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetSteamUILanguage( IntPtr self );
		
		#endregion
		internal string GetSteamUILanguage()
		{
			var returnValue = _GetSteamUILanguage( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_IsSteamRunningInVR", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsSteamRunningInVR( IntPtr self );
		
		#endregion
		internal bool IsSteamRunningInVR()
		{
			var returnValue = _IsSteamRunningInVR( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_SetOverlayNotificationInset", CallingConvention = Platform.CC)]
		private static extern void _SetOverlayNotificationInset( IntPtr self, int nHorizontalInset, int nVerticalInset );
		
		#endregion
		internal void SetOverlayNotificationInset( int nHorizontalInset, int nVerticalInset )
		{
			_SetOverlayNotificationInset( Self, nHorizontalInset, nVerticalInset );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_IsSteamInBigPictureMode", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsSteamInBigPictureMode( IntPtr self );
		
		#endregion
		internal bool IsSteamInBigPictureMode()
		{
			var returnValue = _IsSteamInBigPictureMode( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_StartVRDashboard", CallingConvention = Platform.CC)]
		private static extern void _StartVRDashboard( IntPtr self );
		
		#endregion
		internal void StartVRDashboard()
		{
			_StartVRDashboard( Self );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_IsVRHeadsetStreamingEnabled", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsVRHeadsetStreamingEnabled( IntPtr self );
		
		#endregion
		internal bool IsVRHeadsetStreamingEnabled()
		{
			var returnValue = _IsVRHeadsetStreamingEnabled( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_SetVRHeadsetStreamingEnabled", CallingConvention = Platform.CC)]
		private static extern void _SetVRHeadsetStreamingEnabled( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bEnabled );
		
		#endregion
		internal void SetVRHeadsetStreamingEnabled( [MarshalAs( UnmanagedType.U1 )] bool bEnabled )
		{
			_SetVRHeadsetStreamingEnabled( Self, bEnabled );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_IsSteamChinaLauncher", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsSteamChinaLauncher( IntPtr self );
		
		#endregion
		internal bool IsSteamChinaLauncher()
		{
			var returnValue = _IsSteamChinaLauncher( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_InitFilterText", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _InitFilterText( IntPtr self, uint unFilterOptions );
		
		#endregion
		internal bool InitFilterText( uint unFilterOptions )
		{
			var returnValue = _InitFilterText( Self, unFilterOptions );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_FilterText", CallingConvention = Platform.CC)]
		private static extern int _FilterText( IntPtr self, TextFilteringContext eContext, SteamId sourceSteamID, IntPtr pchInputMessage, IntPtr pchOutFilteredText, uint nByteSizeOutFilteredText );
		
		#endregion
		internal int FilterText( TextFilteringContext eContext, SteamId sourceSteamID, string pchInputMessage, out string pchOutFilteredText )
		{
			using var str__pchInputMessage = new Utf8StringToNative( pchInputMessage );
			using var mem__pchOutFilteredText = Helpers.TakeMemory();
			var returnValue = _FilterText( Self, eContext, sourceSteamID, str__pchInputMessage.Pointer, mem__pchOutFilteredText, (1024 * 32) );
			pchOutFilteredText = Helpers.MemoryToString( mem__pchOutFilteredText );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetIPv6ConnectivityState", CallingConvention = Platform.CC)]
		private static extern SteamIPv6ConnectivityState _GetIPv6ConnectivityState( IntPtr self, SteamIPv6ConnectivityProtocol eProtocol );
		
		#endregion
		internal SteamIPv6ConnectivityState GetIPv6ConnectivityState( SteamIPv6ConnectivityProtocol eProtocol )
		{
			var returnValue = _GetIPv6ConnectivityState( Self, eProtocol );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_IsSteamRunningOnSteamDeck", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsSteamRunningOnSteamDeck( IntPtr self );
		
		#endregion
		internal bool IsSteamRunningOnSteamDeck()
		{
			var returnValue = _IsSteamRunningOnSteamDeck( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_ShowFloatingGamepadTextInput", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ShowFloatingGamepadTextInput( IntPtr self, TextInputMode eKeyboardMode, int nTextFieldXPosition, int nTextFieldYPosition, int nTextFieldWidth, int nTextFieldHeight );
		
		#endregion
		internal bool ShowFloatingGamepadTextInput( TextInputMode eKeyboardMode, int nTextFieldXPosition, int nTextFieldYPosition, int nTextFieldWidth, int nTextFieldHeight )
		{
			var returnValue = _ShowFloatingGamepadTextInput( Self, eKeyboardMode, nTextFieldXPosition, nTextFieldYPosition, nTextFieldWidth, nTextFieldHeight );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_SetGameLauncherMode", CallingConvention = Platform.CC)]
		private static extern void _SetGameLauncherMode( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bLauncherMode );
		
		#endregion
		internal void SetGameLauncherMode( [MarshalAs( UnmanagedType.U1 )] bool bLauncherMode )
		{
			_SetGameLauncherMode( Self, bLauncherMode );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_DismissFloatingGamepadTextInput", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _DismissFloatingGamepadTextInput( IntPtr self );
		
		#endregion
		internal bool DismissFloatingGamepadTextInput()
		{
			var returnValue = _DismissFloatingGamepadTextInput( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_DismissGamepadTextInput", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _DismissGamepadTextInput( IntPtr self );
		
		#endregion
		internal bool DismissGamepadTextInput()
		{
			var returnValue = _DismissGamepadTextInput( Self );
			return returnValue;
		}
		
	}
}
