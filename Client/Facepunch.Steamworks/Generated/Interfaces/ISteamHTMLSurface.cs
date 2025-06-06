using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe partial class ISteamHTMLSurface : SteamInterface
	{
		public const string Version = "STEAMHTMLSURFACE_INTERFACE_VERSION_005";
		
		internal ISteamHTMLSurface( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_SteamHTMLSurface_v005", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamHTMLSurface_v005();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamHTMLSurface_v005();
		
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_Init", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _Init( IntPtr self );
		
		#endregion
		internal bool Init()
		{
			var returnValue = _Init( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_Shutdown", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _Shutdown( IntPtr self );
		
		#endregion
		internal bool Shutdown()
		{
			var returnValue = _Shutdown( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_CreateBrowser", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _CreateBrowser( IntPtr self, IntPtr pchUserAgent, IntPtr pchUserCSS );
		
		#endregion
		internal CallResult<HTML_BrowserReady_t> CreateBrowser( string pchUserAgent, string pchUserCSS )
		{
			using var str__pchUserAgent = new Utf8StringToNative( pchUserAgent );
			using var str__pchUserCSS = new Utf8StringToNative( pchUserCSS );
			var returnValue = _CreateBrowser( Self, str__pchUserAgent.Pointer, str__pchUserCSS.Pointer );
			return new CallResult<HTML_BrowserReady_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_RemoveBrowser", CallingConvention = Platform.CC)]
		private static extern void _RemoveBrowser( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void RemoveBrowser( HHTMLBrowser unBrowserHandle )
		{
			_RemoveBrowser( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_LoadURL", CallingConvention = Platform.CC)]
		private static extern void _LoadURL( IntPtr self, HHTMLBrowser unBrowserHandle, IntPtr pchURL, IntPtr pchPostData );
		
		#endregion
		internal void LoadURL( HHTMLBrowser unBrowserHandle, string pchURL, string pchPostData )
		{
			using var str__pchURL = new Utf8StringToNative( pchURL );
			using var str__pchPostData = new Utf8StringToNative( pchPostData );
			_LoadURL( Self, unBrowserHandle, str__pchURL.Pointer, str__pchPostData.Pointer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_SetSize", CallingConvention = Platform.CC)]
		private static extern void _SetSize( IntPtr self, HHTMLBrowser unBrowserHandle, uint unWidth, uint unHeight );
		
		#endregion
		internal void SetSize( HHTMLBrowser unBrowserHandle, uint unWidth, uint unHeight )
		{
			_SetSize( Self, unBrowserHandle, unWidth, unHeight );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_StopLoad", CallingConvention = Platform.CC)]
		private static extern void _StopLoad( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void StopLoad( HHTMLBrowser unBrowserHandle )
		{
			_StopLoad( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_Reload", CallingConvention = Platform.CC)]
		private static extern void _Reload( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void Reload( HHTMLBrowser unBrowserHandle )
		{
			_Reload( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_GoBack", CallingConvention = Platform.CC)]
		private static extern void _GoBack( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void GoBack( HHTMLBrowser unBrowserHandle )
		{
			_GoBack( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_GoForward", CallingConvention = Platform.CC)]
		private static extern void _GoForward( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void GoForward( HHTMLBrowser unBrowserHandle )
		{
			_GoForward( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_AddHeader", CallingConvention = Platform.CC)]
		private static extern void _AddHeader( IntPtr self, HHTMLBrowser unBrowserHandle, IntPtr pchKey, IntPtr pchValue );
		
		#endregion
		internal void AddHeader( HHTMLBrowser unBrowserHandle, string pchKey, string pchValue )
		{
			using var str__pchKey = new Utf8StringToNative( pchKey );
			using var str__pchValue = new Utf8StringToNative( pchValue );
			_AddHeader( Self, unBrowserHandle, str__pchKey.Pointer, str__pchValue.Pointer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_ExecuteJavascript", CallingConvention = Platform.CC)]
		private static extern void _ExecuteJavascript( IntPtr self, HHTMLBrowser unBrowserHandle, IntPtr pchScript );
		
		#endregion
		internal void ExecuteJavascript( HHTMLBrowser unBrowserHandle, string pchScript )
		{
			using var str__pchScript = new Utf8StringToNative( pchScript );
			_ExecuteJavascript( Self, unBrowserHandle, str__pchScript.Pointer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseUp", CallingConvention = Platform.CC)]
		private static extern void _MouseUp( IntPtr self, HHTMLBrowser unBrowserHandle, IntPtr eMouseButton );
		
		#endregion
		internal void MouseUp( HHTMLBrowser unBrowserHandle, IntPtr eMouseButton )
		{
			_MouseUp( Self, unBrowserHandle, eMouseButton );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseDown", CallingConvention = Platform.CC)]
		private static extern void _MouseDown( IntPtr self, HHTMLBrowser unBrowserHandle, IntPtr eMouseButton );
		
		#endregion
		internal void MouseDown( HHTMLBrowser unBrowserHandle, IntPtr eMouseButton )
		{
			_MouseDown( Self, unBrowserHandle, eMouseButton );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseDoubleClick", CallingConvention = Platform.CC)]
		private static extern void _MouseDoubleClick( IntPtr self, HHTMLBrowser unBrowserHandle, IntPtr eMouseButton );
		
		#endregion
		internal void MouseDoubleClick( HHTMLBrowser unBrowserHandle, IntPtr eMouseButton )
		{
			_MouseDoubleClick( Self, unBrowserHandle, eMouseButton );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseMove", CallingConvention = Platform.CC)]
		private static extern void _MouseMove( IntPtr self, HHTMLBrowser unBrowserHandle, int x, int y );
		
		#endregion
		internal void MouseMove( HHTMLBrowser unBrowserHandle, int x, int y )
		{
			_MouseMove( Self, unBrowserHandle, x, y );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseWheel", CallingConvention = Platform.CC)]
		private static extern void _MouseWheel( IntPtr self, HHTMLBrowser unBrowserHandle, int nDelta );
		
		#endregion
		internal void MouseWheel( HHTMLBrowser unBrowserHandle, int nDelta )
		{
			_MouseWheel( Self, unBrowserHandle, nDelta );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyDown", CallingConvention = Platform.CC)]
		private static extern void _KeyDown( IntPtr self, HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, IntPtr eHTMLKeyModifiers, [MarshalAs( UnmanagedType.U1 )] bool bIsSystemKey );
		
		#endregion
		internal void KeyDown( HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, IntPtr eHTMLKeyModifiers, [MarshalAs( UnmanagedType.U1 )] bool bIsSystemKey )
		{
			_KeyDown( Self, unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers, bIsSystemKey );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyUp", CallingConvention = Platform.CC)]
		private static extern void _KeyUp( IntPtr self, HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, IntPtr eHTMLKeyModifiers );
		
		#endregion
		internal void KeyUp( HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, IntPtr eHTMLKeyModifiers )
		{
			_KeyUp( Self, unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyChar", CallingConvention = Platform.CC)]
		private static extern void _KeyChar( IntPtr self, HHTMLBrowser unBrowserHandle, uint cUnicodeChar, IntPtr eHTMLKeyModifiers );
		
		#endregion
		internal void KeyChar( HHTMLBrowser unBrowserHandle, uint cUnicodeChar, IntPtr eHTMLKeyModifiers )
		{
			_KeyChar( Self, unBrowserHandle, cUnicodeChar, eHTMLKeyModifiers );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_SetHorizontalScroll", CallingConvention = Platform.CC)]
		private static extern void _SetHorizontalScroll( IntPtr self, HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll );
		
		#endregion
		internal void SetHorizontalScroll( HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll )
		{
			_SetHorizontalScroll( Self, unBrowserHandle, nAbsolutePixelScroll );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_SetVerticalScroll", CallingConvention = Platform.CC)]
		private static extern void _SetVerticalScroll( IntPtr self, HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll );
		
		#endregion
		internal void SetVerticalScroll( HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll )
		{
			_SetVerticalScroll( Self, unBrowserHandle, nAbsolutePixelScroll );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_SetKeyFocus", CallingConvention = Platform.CC)]
		private static extern void _SetKeyFocus( IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bHasKeyFocus );
		
		#endregion
		internal void SetKeyFocus( HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bHasKeyFocus )
		{
			_SetKeyFocus( Self, unBrowserHandle, bHasKeyFocus );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_ViewSource", CallingConvention = Platform.CC)]
		private static extern void _ViewSource( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void ViewSource( HHTMLBrowser unBrowserHandle )
		{
			_ViewSource( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_CopyToClipboard", CallingConvention = Platform.CC)]
		private static extern void _CopyToClipboard( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void CopyToClipboard( HHTMLBrowser unBrowserHandle )
		{
			_CopyToClipboard( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_PasteFromClipboard", CallingConvention = Platform.CC)]
		private static extern void _PasteFromClipboard( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void PasteFromClipboard( HHTMLBrowser unBrowserHandle )
		{
			_PasteFromClipboard( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_Find", CallingConvention = Platform.CC)]
		private static extern void _Find( IntPtr self, HHTMLBrowser unBrowserHandle, IntPtr pchSearchStr, [MarshalAs( UnmanagedType.U1 )] bool bCurrentlyInFind, [MarshalAs( UnmanagedType.U1 )] bool bReverse );
		
		#endregion
		internal void Find( HHTMLBrowser unBrowserHandle, string pchSearchStr, [MarshalAs( UnmanagedType.U1 )] bool bCurrentlyInFind, [MarshalAs( UnmanagedType.U1 )] bool bReverse )
		{
			using var str__pchSearchStr = new Utf8StringToNative( pchSearchStr );
			_Find( Self, unBrowserHandle, str__pchSearchStr.Pointer, bCurrentlyInFind, bReverse );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_StopFind", CallingConvention = Platform.CC)]
		private static extern void _StopFind( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void StopFind( HHTMLBrowser unBrowserHandle )
		{
			_StopFind( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_GetLinkAtPosition", CallingConvention = Platform.CC)]
		private static extern void _GetLinkAtPosition( IntPtr self, HHTMLBrowser unBrowserHandle, int x, int y );
		
		#endregion
		internal void GetLinkAtPosition( HHTMLBrowser unBrowserHandle, int x, int y )
		{
			_GetLinkAtPosition( Self, unBrowserHandle, x, y );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_SetCookie", CallingConvention = Platform.CC)]
		private static extern void _SetCookie( IntPtr self, IntPtr pchHostname, IntPtr pchKey, IntPtr pchValue, IntPtr pchPath, RTime32 nExpires, [MarshalAs( UnmanagedType.U1 )] bool bSecure, [MarshalAs( UnmanagedType.U1 )] bool bHTTPOnly );
		
		#endregion
		internal void SetCookie( string pchHostname, string pchKey, string pchValue, string pchPath, RTime32 nExpires, [MarshalAs( UnmanagedType.U1 )] bool bSecure, [MarshalAs( UnmanagedType.U1 )] bool bHTTPOnly )
		{
			using var str__pchHostname = new Utf8StringToNative( pchHostname );
			using var str__pchKey = new Utf8StringToNative( pchKey );
			using var str__pchValue = new Utf8StringToNative( pchValue );
			using var str__pchPath = new Utf8StringToNative( pchPath );
			_SetCookie( Self, str__pchHostname.Pointer, str__pchKey.Pointer, str__pchValue.Pointer, str__pchPath.Pointer, nExpires, bSecure, bHTTPOnly );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_SetPageScaleFactor", CallingConvention = Platform.CC)]
		private static extern void _SetPageScaleFactor( IntPtr self, HHTMLBrowser unBrowserHandle, float flZoom, int nPointX, int nPointY );
		
		#endregion
		internal void SetPageScaleFactor( HHTMLBrowser unBrowserHandle, float flZoom, int nPointX, int nPointY )
		{
			_SetPageScaleFactor( Self, unBrowserHandle, flZoom, nPointX, nPointY );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_SetBackgroundMode", CallingConvention = Platform.CC)]
		private static extern void _SetBackgroundMode( IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bBackgroundMode );
		
		#endregion
		internal void SetBackgroundMode( HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bBackgroundMode )
		{
			_SetBackgroundMode( Self, unBrowserHandle, bBackgroundMode );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_SetDPIScalingFactor", CallingConvention = Platform.CC)]
		private static extern void _SetDPIScalingFactor( IntPtr self, HHTMLBrowser unBrowserHandle, float flDPIScaling );
		
		#endregion
		internal void SetDPIScalingFactor( HHTMLBrowser unBrowserHandle, float flDPIScaling )
		{
			_SetDPIScalingFactor( Self, unBrowserHandle, flDPIScaling );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_OpenDeveloperTools", CallingConvention = Platform.CC)]
		private static extern void _OpenDeveloperTools( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void OpenDeveloperTools( HHTMLBrowser unBrowserHandle )
		{
			_OpenDeveloperTools( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_AllowStartRequest", CallingConvention = Platform.CC)]
		private static extern void _AllowStartRequest( IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bAllowed );
		
		#endregion
		internal void AllowStartRequest( HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bAllowed )
		{
			_AllowStartRequest( Self, unBrowserHandle, bAllowed );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_JSDialogResponse", CallingConvention = Platform.CC)]
		private static extern void _JSDialogResponse( IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bResult );
		
		#endregion
		internal void JSDialogResponse( HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bResult )
		{
			_JSDialogResponse( Self, unBrowserHandle, bResult );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamHTMLSurface_FileLoadDialogResponse", CallingConvention = Platform.CC)]
		private static extern void _FileLoadDialogResponse( IntPtr self, HHTMLBrowser unBrowserHandle, IntPtr pchSelectedFiles );
		
		#endregion
		internal void FileLoadDialogResponse( HHTMLBrowser unBrowserHandle, string pchSelectedFiles )
		{
			using var str__pchSelectedFiles = new Utf8StringToNative( pchSelectedFiles );
			_FileLoadDialogResponse( Self, unBrowserHandle, str__pchSelectedFiles.Pointer );
		}
		
	}
}
