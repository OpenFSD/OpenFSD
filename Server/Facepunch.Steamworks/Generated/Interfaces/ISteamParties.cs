using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe partial class ISteamParties : SteamInterface
	{
		public const string Version = "SteamParties002";
		
		internal ISteamParties( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_SteamParties_v002", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamParties_v002();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamParties_v002();
		
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamParties_GetNumActiveBeacons", CallingConvention = Platform.CC)]
		private static extern uint _GetNumActiveBeacons( IntPtr self );
		
		#endregion
		internal uint GetNumActiveBeacons()
		{
			var returnValue = _GetNumActiveBeacons( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamParties_GetBeaconByIndex", CallingConvention = Platform.CC)]
		private static extern PartyBeaconID_t _GetBeaconByIndex( IntPtr self, uint unIndex );
		
		#endregion
		internal PartyBeaconID_t GetBeaconByIndex( uint unIndex )
		{
			var returnValue = _GetBeaconByIndex( Self, unIndex );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamParties_GetBeaconDetails", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetBeaconDetails( IntPtr self, PartyBeaconID_t ulBeaconID, ref SteamId pSteamIDBeaconOwner, ref SteamPartyBeaconLocation_t pLocation, IntPtr pchMetadata, int cchMetadata );
		
		#endregion
		internal bool GetBeaconDetails( PartyBeaconID_t ulBeaconID, ref SteamId pSteamIDBeaconOwner, ref SteamPartyBeaconLocation_t pLocation, out string pchMetadata )
		{
			using var mem__pchMetadata = Helpers.TakeMemory();
			var returnValue = _GetBeaconDetails( Self, ulBeaconID, ref pSteamIDBeaconOwner, ref pLocation, mem__pchMetadata, (1024 * 32) );
			pchMetadata = Helpers.MemoryToString( mem__pchMetadata );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamParties_JoinParty", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _JoinParty( IntPtr self, PartyBeaconID_t ulBeaconID );
		
		#endregion
		internal CallResult<JoinPartyCallback_t> JoinParty( PartyBeaconID_t ulBeaconID )
		{
			var returnValue = _JoinParty( Self, ulBeaconID );
			return new CallResult<JoinPartyCallback_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamParties_GetNumAvailableBeaconLocations", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetNumAvailableBeaconLocations( IntPtr self, ref uint puNumLocations );
		
		#endregion
		internal bool GetNumAvailableBeaconLocations( ref uint puNumLocations )
		{
			var returnValue = _GetNumAvailableBeaconLocations( Self, ref puNumLocations );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamParties_GetAvailableBeaconLocations", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetAvailableBeaconLocations( IntPtr self, ref SteamPartyBeaconLocation_t pLocationList, uint uMaxNumLocations );
		
		#endregion
		internal bool GetAvailableBeaconLocations( ref SteamPartyBeaconLocation_t pLocationList, uint uMaxNumLocations )
		{
			var returnValue = _GetAvailableBeaconLocations( Self, ref pLocationList, uMaxNumLocations );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamParties_CreateBeacon", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _CreateBeacon( IntPtr self, uint unOpenSlots, ref SteamPartyBeaconLocation_t pBeaconLocation, IntPtr pchConnectString, IntPtr pchMetadata );
		
		#endregion
		internal CallResult<CreateBeaconCallback_t> CreateBeacon( uint unOpenSlots,  /* ref */ SteamPartyBeaconLocation_t pBeaconLocation, string pchConnectString, string pchMetadata )
		{
			using var str__pchConnectString = new Utf8StringToNative( pchConnectString );
			using var str__pchMetadata = new Utf8StringToNative( pchMetadata );
			var returnValue = _CreateBeacon( Self, unOpenSlots, ref pBeaconLocation, str__pchConnectString.Pointer, str__pchMetadata.Pointer );
			return new CallResult<CreateBeaconCallback_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamParties_OnReservationCompleted", CallingConvention = Platform.CC)]
		private static extern void _OnReservationCompleted( IntPtr self, PartyBeaconID_t ulBeacon, SteamId steamIDUser );
		
		#endregion
		internal void OnReservationCompleted( PartyBeaconID_t ulBeacon, SteamId steamIDUser )
		{
			_OnReservationCompleted( Self, ulBeacon, steamIDUser );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamParties_CancelReservation", CallingConvention = Platform.CC)]
		private static extern void _CancelReservation( IntPtr self, PartyBeaconID_t ulBeacon, SteamId steamIDUser );
		
		#endregion
		internal void CancelReservation( PartyBeaconID_t ulBeacon, SteamId steamIDUser )
		{
			_CancelReservation( Self, ulBeacon, steamIDUser );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamParties_ChangeNumOpenSlots", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _ChangeNumOpenSlots( IntPtr self, PartyBeaconID_t ulBeacon, uint unOpenSlots );
		
		#endregion
		internal CallResult<ChangeNumOpenSlotsCallback_t> ChangeNumOpenSlots( PartyBeaconID_t ulBeacon, uint unOpenSlots )
		{
			var returnValue = _ChangeNumOpenSlots( Self, ulBeacon, unOpenSlots );
			return new CallResult<ChangeNumOpenSlotsCallback_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamParties_DestroyBeacon", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _DestroyBeacon( IntPtr self, PartyBeaconID_t ulBeacon );
		
		#endregion
		internal bool DestroyBeacon( PartyBeaconID_t ulBeacon )
		{
			var returnValue = _DestroyBeacon( Self, ulBeacon );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamParties_GetBeaconLocationData", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetBeaconLocationData( IntPtr self, SteamPartyBeaconLocation_t BeaconLocation, SteamPartyBeaconLocationData eData, IntPtr pchDataStringOut, int cchDataStringOut );
		
		#endregion
		internal bool GetBeaconLocationData( SteamPartyBeaconLocation_t BeaconLocation, SteamPartyBeaconLocationData eData, out string pchDataStringOut )
		{
			using var mem__pchDataStringOut = Helpers.TakeMemory();
			var returnValue = _GetBeaconLocationData( Self, BeaconLocation, eData, mem__pchDataStringOut, (1024 * 32) );
			pchDataStringOut = Helpers.MemoryToString( mem__pchDataStringOut );
			return returnValue;
		}
		
	}
}
