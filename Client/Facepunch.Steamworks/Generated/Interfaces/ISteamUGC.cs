using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe partial class ISteamUGC : SteamInterface
	{
		public const string Version = "STEAMUGC_INTERFACE_VERSION020";
		
		internal ISteamUGC( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_SteamUGC_v020", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamUGC_v020();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamUGC_v020();
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_SteamGameServerUGC_v020", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamGameServerUGC_v020();
		public override IntPtr GetServerInterfacePointer() => SteamAPI_SteamGameServerUGC_v020();
		
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_CreateQueryUserUGCRequest", CallingConvention = Platform.CC)]
		private static extern UGCQueryHandle_t _CreateQueryUserUGCRequest( IntPtr self, AccountID_t unAccountID, UserUGCList eListType, UgcType eMatchingUGCType, UserUGCListSortOrder eSortOrder, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage );
		
		#endregion
		internal UGCQueryHandle_t CreateQueryUserUGCRequest( AccountID_t unAccountID, UserUGCList eListType, UgcType eMatchingUGCType, UserUGCListSortOrder eSortOrder, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage )
		{
			var returnValue = _CreateQueryUserUGCRequest( Self, unAccountID, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID, nConsumerAppID, unPage );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_CreateQueryAllUGCRequestPage", CallingConvention = Platform.CC)]
		private static extern UGCQueryHandle_t _CreateQueryAllUGCRequest( IntPtr self, UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage );
		
		#endregion
		internal UGCQueryHandle_t CreateQueryAllUGCRequest( UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage )
		{
			var returnValue = _CreateQueryAllUGCRequest( Self, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, unPage );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_CreateQueryAllUGCRequestCursor", CallingConvention = Platform.CC)]
		private static extern UGCQueryHandle_t _CreateQueryAllUGCRequest( IntPtr self, UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, IntPtr pchCursor );
		
		#endregion
		internal UGCQueryHandle_t CreateQueryAllUGCRequest( UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, string pchCursor )
		{
			using var str__pchCursor = new Utf8StringToNative( pchCursor );
			var returnValue = _CreateQueryAllUGCRequest( Self, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, str__pchCursor.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_CreateQueryUGCDetailsRequest", CallingConvention = Platform.CC)]
		private static extern UGCQueryHandle_t _CreateQueryUGCDetailsRequest( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs );
		
		#endregion
		internal UGCQueryHandle_t CreateQueryUGCDetailsRequest( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs )
		{
			var returnValue = _CreateQueryUGCDetailsRequest( Self, pvecPublishedFileID, unNumPublishedFileIDs );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SendQueryUGCRequest", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _SendQueryUGCRequest( IntPtr self, UGCQueryHandle_t handle );
		
		#endregion
		internal CallResult<SteamUGCQueryCompleted_t> SendQueryUGCRequest( UGCQueryHandle_t handle )
		{
			var returnValue = _SendQueryUGCRequest( Self, handle );
			return new CallResult<SteamUGCQueryCompleted_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCResult", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCResult( IntPtr self, UGCQueryHandle_t handle, uint index, ref SteamUGCDetails_t pDetails );
		
		#endregion
		internal bool GetQueryUGCResult( UGCQueryHandle_t handle, uint index, ref SteamUGCDetails_t pDetails )
		{
			var returnValue = _GetQueryUGCResult( Self, handle, index, ref pDetails );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCNumTags", CallingConvention = Platform.CC)]
		private static extern uint _GetQueryUGCNumTags( IntPtr self, UGCQueryHandle_t handle, uint index );
		
		#endregion
		internal uint GetQueryUGCNumTags( UGCQueryHandle_t handle, uint index )
		{
			var returnValue = _GetQueryUGCNumTags( Self, handle, index );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCTag", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCTag( IntPtr self, UGCQueryHandle_t handle, uint index, uint indexTag, IntPtr pchValue, uint cchValueSize );
		
		#endregion
		internal bool GetQueryUGCTag( UGCQueryHandle_t handle, uint index, uint indexTag, out string pchValue )
		{
			using var mem__pchValue = Helpers.TakeMemory();
			var returnValue = _GetQueryUGCTag( Self, handle, index, indexTag, mem__pchValue, (1024 * 32) );
			pchValue = Helpers.MemoryToString( mem__pchValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCTagDisplayName", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCTagDisplayName( IntPtr self, UGCQueryHandle_t handle, uint index, uint indexTag, IntPtr pchValue, uint cchValueSize );
		
		#endregion
		internal bool GetQueryUGCTagDisplayName( UGCQueryHandle_t handle, uint index, uint indexTag, out string pchValue )
		{
			using var mem__pchValue = Helpers.TakeMemory();
			var returnValue = _GetQueryUGCTagDisplayName( Self, handle, index, indexTag, mem__pchValue, (1024 * 32) );
			pchValue = Helpers.MemoryToString( mem__pchValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCPreviewURL", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCPreviewURL( IntPtr self, UGCQueryHandle_t handle, uint index, IntPtr pchURL, uint cchURLSize );
		
		#endregion
		internal bool GetQueryUGCPreviewURL( UGCQueryHandle_t handle, uint index, out string pchURL )
		{
			using var mem__pchURL = Helpers.TakeMemory();
			var returnValue = _GetQueryUGCPreviewURL( Self, handle, index, mem__pchURL, (1024 * 32) );
			pchURL = Helpers.MemoryToString( mem__pchURL );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCMetadata", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCMetadata( IntPtr self, UGCQueryHandle_t handle, uint index, IntPtr pchMetadata, uint cchMetadatasize );
		
		#endregion
		internal bool GetQueryUGCMetadata( UGCQueryHandle_t handle, uint index, out string pchMetadata )
		{
			using var mem__pchMetadata = Helpers.TakeMemory();
			var returnValue = _GetQueryUGCMetadata( Self, handle, index, mem__pchMetadata, (1024 * 32) );
			pchMetadata = Helpers.MemoryToString( mem__pchMetadata );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCChildren", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCChildren( IntPtr self, UGCQueryHandle_t handle, uint index, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries );
		
		#endregion
		internal bool GetQueryUGCChildren( UGCQueryHandle_t handle, uint index, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries )
		{
			var returnValue = _GetQueryUGCChildren( Self, handle, index, pvecPublishedFileID, cMaxEntries );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCStatistic", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCStatistic( IntPtr self, UGCQueryHandle_t handle, uint index, ItemStatistic eStatType, ref ulong pStatValue );
		
		#endregion
		internal bool GetQueryUGCStatistic( UGCQueryHandle_t handle, uint index, ItemStatistic eStatType, ref ulong pStatValue )
		{
			var returnValue = _GetQueryUGCStatistic( Self, handle, index, eStatType, ref pStatValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCNumAdditionalPreviews", CallingConvention = Platform.CC)]
		private static extern uint _GetQueryUGCNumAdditionalPreviews( IntPtr self, UGCQueryHandle_t handle, uint index );
		
		#endregion
		internal uint GetQueryUGCNumAdditionalPreviews( UGCQueryHandle_t handle, uint index )
		{
			var returnValue = _GetQueryUGCNumAdditionalPreviews( Self, handle, index );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCAdditionalPreview", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCAdditionalPreview( IntPtr self, UGCQueryHandle_t handle, uint index, uint previewIndex, IntPtr pchURLOrVideoID, uint cchURLSize, IntPtr pchOriginalFileName, uint cchOriginalFileNameSize, ref ItemPreviewType pPreviewType );
		
		#endregion
		internal bool GetQueryUGCAdditionalPreview( UGCQueryHandle_t handle, uint index, uint previewIndex, out string pchURLOrVideoID, out string pchOriginalFileName, ref ItemPreviewType pPreviewType )
		{
			using var mem__pchURLOrVideoID = Helpers.TakeMemory();
			using var mem__pchOriginalFileName = Helpers.TakeMemory();
			var returnValue = _GetQueryUGCAdditionalPreview( Self, handle, index, previewIndex, mem__pchURLOrVideoID, (1024 * 32), mem__pchOriginalFileName, (1024 * 32), ref pPreviewType );
			pchURLOrVideoID = Helpers.MemoryToString( mem__pchURLOrVideoID );
			pchOriginalFileName = Helpers.MemoryToString( mem__pchOriginalFileName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCNumKeyValueTags", CallingConvention = Platform.CC)]
		private static extern uint _GetQueryUGCNumKeyValueTags( IntPtr self, UGCQueryHandle_t handle, uint index );
		
		#endregion
		internal uint GetQueryUGCNumKeyValueTags( UGCQueryHandle_t handle, uint index )
		{
			var returnValue = _GetQueryUGCNumKeyValueTags( Self, handle, index );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCKeyValueTag", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCKeyValueTag( IntPtr self, UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, IntPtr pchKey, uint cchKeySize, IntPtr pchValue, uint cchValueSize );
		
		#endregion
		internal bool GetQueryUGCKeyValueTag( UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, out string pchKey, out string pchValue )
		{
			using var mem__pchKey = Helpers.TakeMemory();
			using var mem__pchValue = Helpers.TakeMemory();
			var returnValue = _GetQueryUGCKeyValueTag( Self, handle, index, keyValueTagIndex, mem__pchKey, (1024 * 32), mem__pchValue, (1024 * 32) );
			pchKey = Helpers.MemoryToString( mem__pchKey );
			pchValue = Helpers.MemoryToString( mem__pchValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetQueryFirstUGCKeyValueTag", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCKeyValueTag( IntPtr self, UGCQueryHandle_t handle, uint index, IntPtr pchKey, IntPtr pchValue, uint cchValueSize );
		
		#endregion
		internal bool GetQueryUGCKeyValueTag( UGCQueryHandle_t handle, uint index, string pchKey, out string pchValue )
		{
			using var str__pchKey = new Utf8StringToNative( pchKey );
			using var mem__pchValue = Helpers.TakeMemory();
			var returnValue = _GetQueryUGCKeyValueTag( Self, handle, index, str__pchKey.Pointer, mem__pchValue, (1024 * 32) );
			pchValue = Helpers.MemoryToString( mem__pchValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetNumSupportedGameVersions", CallingConvention = Platform.CC)]
		private static extern uint _GetNumSupportedGameVersions( IntPtr self, UGCQueryHandle_t handle, uint index );
		
		#endregion
		internal uint GetNumSupportedGameVersions( UGCQueryHandle_t handle, uint index )
		{
			var returnValue = _GetNumSupportedGameVersions( Self, handle, index );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetSupportedGameVersionData", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetSupportedGameVersionData( IntPtr self, UGCQueryHandle_t handle, uint index, uint versionIndex, IntPtr pchGameBranchMin, IntPtr pchGameBranchMax, uint cchGameBranchSize );
		
		#endregion
		internal bool GetSupportedGameVersionData( UGCQueryHandle_t handle, uint index, uint versionIndex, out string pchGameBranchMin, out string pchGameBranchMax )
		{
			using var mem__pchGameBranchMin = Helpers.TakeMemory();
			using var mem__pchGameBranchMax = Helpers.TakeMemory();
			var returnValue = _GetSupportedGameVersionData( Self, handle, index, versionIndex, mem__pchGameBranchMin, mem__pchGameBranchMax, (1024 * 32) );
			pchGameBranchMin = Helpers.MemoryToString( mem__pchGameBranchMin );
			pchGameBranchMax = Helpers.MemoryToString( mem__pchGameBranchMax );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCContentDescriptors", CallingConvention = Platform.CC)]
		private static extern uint _GetQueryUGCContentDescriptors( IntPtr self, UGCQueryHandle_t handle, uint index, [In,Out] UGCContentDescriptorID[]  pvecDescriptors, uint cMaxEntries );
		
		#endregion
		internal uint GetQueryUGCContentDescriptors( UGCQueryHandle_t handle, uint index, [In,Out] UGCContentDescriptorID[]  pvecDescriptors, uint cMaxEntries )
		{
			var returnValue = _GetQueryUGCContentDescriptors( Self, handle, index, pvecDescriptors, cMaxEntries );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_ReleaseQueryUGCRequest", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ReleaseQueryUGCRequest( IntPtr self, UGCQueryHandle_t handle );
		
		#endregion
		internal bool ReleaseQueryUGCRequest( UGCQueryHandle_t handle )
		{
			var returnValue = _ReleaseQueryUGCRequest( Self, handle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_AddRequiredTag", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddRequiredTag( IntPtr self, UGCQueryHandle_t handle, IntPtr pTagName );
		
		#endregion
		internal bool AddRequiredTag( UGCQueryHandle_t handle, string pTagName )
		{
			using var str__pTagName = new Utf8StringToNative( pTagName );
			var returnValue = _AddRequiredTag( Self, handle, str__pTagName.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_AddRequiredTagGroup", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddRequiredTagGroup( IntPtr self, UGCQueryHandle_t handle, ref SteamParamStringArray_t pTagGroups );
		
		#endregion
		internal bool AddRequiredTagGroup( UGCQueryHandle_t handle, ref SteamParamStringArray_t pTagGroups )
		{
			var returnValue = _AddRequiredTagGroup( Self, handle, ref pTagGroups );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_AddExcludedTag", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddExcludedTag( IntPtr self, UGCQueryHandle_t handle, IntPtr pTagName );
		
		#endregion
		internal bool AddExcludedTag( UGCQueryHandle_t handle, string pTagName )
		{
			using var str__pTagName = new Utf8StringToNative( pTagName );
			var returnValue = _AddExcludedTag( Self, handle, str__pTagName.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetReturnOnlyIDs", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnOnlyIDs( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnOnlyIDs );
		
		#endregion
		internal bool SetReturnOnlyIDs( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnOnlyIDs )
		{
			var returnValue = _SetReturnOnlyIDs( Self, handle, bReturnOnlyIDs );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetReturnKeyValueTags", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnKeyValueTags( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnKeyValueTags );
		
		#endregion
		internal bool SetReturnKeyValueTags( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnKeyValueTags )
		{
			var returnValue = _SetReturnKeyValueTags( Self, handle, bReturnKeyValueTags );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetReturnLongDescription", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnLongDescription( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnLongDescription );
		
		#endregion
		internal bool SetReturnLongDescription( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnLongDescription )
		{
			var returnValue = _SetReturnLongDescription( Self, handle, bReturnLongDescription );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetReturnMetadata", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnMetadata( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnMetadata );
		
		#endregion
		internal bool SetReturnMetadata( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnMetadata )
		{
			var returnValue = _SetReturnMetadata( Self, handle, bReturnMetadata );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetReturnChildren", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnChildren( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnChildren );
		
		#endregion
		internal bool SetReturnChildren( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnChildren )
		{
			var returnValue = _SetReturnChildren( Self, handle, bReturnChildren );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetReturnAdditionalPreviews", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnAdditionalPreviews( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnAdditionalPreviews );
		
		#endregion
		internal bool SetReturnAdditionalPreviews( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnAdditionalPreviews )
		{
			var returnValue = _SetReturnAdditionalPreviews( Self, handle, bReturnAdditionalPreviews );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetReturnTotalOnly", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnTotalOnly( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnTotalOnly );
		
		#endregion
		internal bool SetReturnTotalOnly( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnTotalOnly )
		{
			var returnValue = _SetReturnTotalOnly( Self, handle, bReturnTotalOnly );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetReturnPlaytimeStats", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnPlaytimeStats( IntPtr self, UGCQueryHandle_t handle, uint unDays );
		
		#endregion
		internal bool SetReturnPlaytimeStats( UGCQueryHandle_t handle, uint unDays )
		{
			var returnValue = _SetReturnPlaytimeStats( Self, handle, unDays );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetLanguage", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetLanguage( IntPtr self, UGCQueryHandle_t handle, IntPtr pchLanguage );
		
		#endregion
		internal bool SetLanguage( UGCQueryHandle_t handle, string pchLanguage )
		{
			using var str__pchLanguage = new Utf8StringToNative( pchLanguage );
			var returnValue = _SetLanguage( Self, handle, str__pchLanguage.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetAllowCachedResponse", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetAllowCachedResponse( IntPtr self, UGCQueryHandle_t handle, uint unMaxAgeSeconds );
		
		#endregion
		internal bool SetAllowCachedResponse( UGCQueryHandle_t handle, uint unMaxAgeSeconds )
		{
			var returnValue = _SetAllowCachedResponse( Self, handle, unMaxAgeSeconds );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetAdminQuery", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetAdminQuery( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bAdminQuery );
		
		#endregion
		internal bool SetAdminQuery( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bAdminQuery )
		{
			var returnValue = _SetAdminQuery( Self, handle, bAdminQuery );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetCloudFileNameFilter", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetCloudFileNameFilter( IntPtr self, UGCQueryHandle_t handle, IntPtr pMatchCloudFileName );
		
		#endregion
		internal bool SetCloudFileNameFilter( UGCQueryHandle_t handle, string pMatchCloudFileName )
		{
			using var str__pMatchCloudFileName = new Utf8StringToNative( pMatchCloudFileName );
			var returnValue = _SetCloudFileNameFilter( Self, handle, str__pMatchCloudFileName.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetMatchAnyTag", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetMatchAnyTag( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bMatchAnyTag );
		
		#endregion
		internal bool SetMatchAnyTag( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bMatchAnyTag )
		{
			var returnValue = _SetMatchAnyTag( Self, handle, bMatchAnyTag );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetSearchText", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetSearchText( IntPtr self, UGCQueryHandle_t handle, IntPtr pSearchText );
		
		#endregion
		internal bool SetSearchText( UGCQueryHandle_t handle, string pSearchText )
		{
			using var str__pSearchText = new Utf8StringToNative( pSearchText );
			var returnValue = _SetSearchText( Self, handle, str__pSearchText.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetRankedByTrendDays", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetRankedByTrendDays( IntPtr self, UGCQueryHandle_t handle, uint unDays );
		
		#endregion
		internal bool SetRankedByTrendDays( UGCQueryHandle_t handle, uint unDays )
		{
			var returnValue = _SetRankedByTrendDays( Self, handle, unDays );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetTimeCreatedDateRange", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetTimeCreatedDateRange( IntPtr self, UGCQueryHandle_t handle, RTime32 rtStart, RTime32 rtEnd );
		
		#endregion
		internal bool SetTimeCreatedDateRange( UGCQueryHandle_t handle, RTime32 rtStart, RTime32 rtEnd )
		{
			var returnValue = _SetTimeCreatedDateRange( Self, handle, rtStart, rtEnd );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetTimeUpdatedDateRange", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetTimeUpdatedDateRange( IntPtr self, UGCQueryHandle_t handle, RTime32 rtStart, RTime32 rtEnd );
		
		#endregion
		internal bool SetTimeUpdatedDateRange( UGCQueryHandle_t handle, RTime32 rtStart, RTime32 rtEnd )
		{
			var returnValue = _SetTimeUpdatedDateRange( Self, handle, rtStart, rtEnd );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_AddRequiredKeyValueTag", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddRequiredKeyValueTag( IntPtr self, UGCQueryHandle_t handle, IntPtr pKey, IntPtr pValue );
		
		#endregion
		internal bool AddRequiredKeyValueTag( UGCQueryHandle_t handle, string pKey, string pValue )
		{
			using var str__pKey = new Utf8StringToNative( pKey );
			using var str__pValue = new Utf8StringToNative( pValue );
			var returnValue = _AddRequiredKeyValueTag( Self, handle, str__pKey.Pointer, str__pValue.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_CreateItem", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _CreateItem( IntPtr self, AppId nConsumerAppId, WorkshopFileType eFileType );
		
		#endregion
		internal CallResult<CreateItemResult_t> CreateItem( AppId nConsumerAppId, WorkshopFileType eFileType )
		{
			var returnValue = _CreateItem( Self, nConsumerAppId, eFileType );
			return new CallResult<CreateItemResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_StartItemUpdate", CallingConvention = Platform.CC)]
		private static extern UGCUpdateHandle_t _StartItemUpdate( IntPtr self, AppId nConsumerAppId, PublishedFileId nPublishedFileID );
		
		#endregion
		internal UGCUpdateHandle_t StartItemUpdate( AppId nConsumerAppId, PublishedFileId nPublishedFileID )
		{
			var returnValue = _StartItemUpdate( Self, nConsumerAppId, nPublishedFileID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetItemTitle", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemTitle( IntPtr self, UGCUpdateHandle_t handle, IntPtr pchTitle );
		
		#endregion
		internal bool SetItemTitle( UGCUpdateHandle_t handle, string pchTitle )
		{
			using var str__pchTitle = new Utf8StringToNative( pchTitle );
			var returnValue = _SetItemTitle( Self, handle, str__pchTitle.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetItemDescription", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemDescription( IntPtr self, UGCUpdateHandle_t handle, IntPtr pchDescription );
		
		#endregion
		internal bool SetItemDescription( UGCUpdateHandle_t handle, string pchDescription )
		{
			using var str__pchDescription = new Utf8StringToNative( pchDescription );
			var returnValue = _SetItemDescription( Self, handle, str__pchDescription.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetItemUpdateLanguage", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemUpdateLanguage( IntPtr self, UGCUpdateHandle_t handle, IntPtr pchLanguage );
		
		#endregion
		internal bool SetItemUpdateLanguage( UGCUpdateHandle_t handle, string pchLanguage )
		{
			using var str__pchLanguage = new Utf8StringToNative( pchLanguage );
			var returnValue = _SetItemUpdateLanguage( Self, handle, str__pchLanguage.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetItemMetadata", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemMetadata( IntPtr self, UGCUpdateHandle_t handle, IntPtr pchMetaData );
		
		#endregion
		internal bool SetItemMetadata( UGCUpdateHandle_t handle, string pchMetaData )
		{
			using var str__pchMetaData = new Utf8StringToNative( pchMetaData );
			var returnValue = _SetItemMetadata( Self, handle, str__pchMetaData.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetItemVisibility", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemVisibility( IntPtr self, UGCUpdateHandle_t handle, RemoteStoragePublishedFileVisibility eVisibility );
		
		#endregion
		internal bool SetItemVisibility( UGCUpdateHandle_t handle, RemoteStoragePublishedFileVisibility eVisibility )
		{
			var returnValue = _SetItemVisibility( Self, handle, eVisibility );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetItemTags", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemTags( IntPtr self, UGCUpdateHandle_t updateHandle, ref SteamParamStringArray_t pTags, [MarshalAs( UnmanagedType.U1 )] bool bAllowAdminTags );
		
		#endregion
		internal bool SetItemTags( UGCUpdateHandle_t updateHandle, ref SteamParamStringArray_t pTags, [MarshalAs( UnmanagedType.U1 )] bool bAllowAdminTags )
		{
			var returnValue = _SetItemTags( Self, updateHandle, ref pTags, bAllowAdminTags );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetItemContent", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemContent( IntPtr self, UGCUpdateHandle_t handle, IntPtr pszContentFolder );
		
		#endregion
		internal bool SetItemContent( UGCUpdateHandle_t handle, string pszContentFolder )
		{
			using var str__pszContentFolder = new Utf8StringToNative( pszContentFolder );
			var returnValue = _SetItemContent( Self, handle, str__pszContentFolder.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetItemPreview", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemPreview( IntPtr self, UGCUpdateHandle_t handle, IntPtr pszPreviewFile );
		
		#endregion
		internal bool SetItemPreview( UGCUpdateHandle_t handle, string pszPreviewFile )
		{
			using var str__pszPreviewFile = new Utf8StringToNative( pszPreviewFile );
			var returnValue = _SetItemPreview( Self, handle, str__pszPreviewFile.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetAllowLegacyUpload", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetAllowLegacyUpload( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bAllowLegacyUpload );
		
		#endregion
		internal bool SetAllowLegacyUpload( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bAllowLegacyUpload )
		{
			var returnValue = _SetAllowLegacyUpload( Self, handle, bAllowLegacyUpload );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_RemoveAllItemKeyValueTags", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _RemoveAllItemKeyValueTags( IntPtr self, UGCUpdateHandle_t handle );
		
		#endregion
		internal bool RemoveAllItemKeyValueTags( UGCUpdateHandle_t handle )
		{
			var returnValue = _RemoveAllItemKeyValueTags( Self, handle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_RemoveItemKeyValueTags", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _RemoveItemKeyValueTags( IntPtr self, UGCUpdateHandle_t handle, IntPtr pchKey );
		
		#endregion
		internal bool RemoveItemKeyValueTags( UGCUpdateHandle_t handle, string pchKey )
		{
			using var str__pchKey = new Utf8StringToNative( pchKey );
			var returnValue = _RemoveItemKeyValueTags( Self, handle, str__pchKey.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_AddItemKeyValueTag", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddItemKeyValueTag( IntPtr self, UGCUpdateHandle_t handle, IntPtr pchKey, IntPtr pchValue );
		
		#endregion
		internal bool AddItemKeyValueTag( UGCUpdateHandle_t handle, string pchKey, string pchValue )
		{
			using var str__pchKey = new Utf8StringToNative( pchKey );
			using var str__pchValue = new Utf8StringToNative( pchValue );
			var returnValue = _AddItemKeyValueTag( Self, handle, str__pchKey.Pointer, str__pchValue.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_AddItemPreviewFile", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddItemPreviewFile( IntPtr self, UGCUpdateHandle_t handle, IntPtr pszPreviewFile, ItemPreviewType type );
		
		#endregion
		internal bool AddItemPreviewFile( UGCUpdateHandle_t handle, string pszPreviewFile, ItemPreviewType type )
		{
			using var str__pszPreviewFile = new Utf8StringToNative( pszPreviewFile );
			var returnValue = _AddItemPreviewFile( Self, handle, str__pszPreviewFile.Pointer, type );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_AddItemPreviewVideo", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddItemPreviewVideo( IntPtr self, UGCUpdateHandle_t handle, IntPtr pszVideoID );
		
		#endregion
		internal bool AddItemPreviewVideo( UGCUpdateHandle_t handle, string pszVideoID )
		{
			using var str__pszVideoID = new Utf8StringToNative( pszVideoID );
			var returnValue = _AddItemPreviewVideo( Self, handle, str__pszVideoID.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_UpdateItemPreviewFile", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _UpdateItemPreviewFile( IntPtr self, UGCUpdateHandle_t handle, uint index, IntPtr pszPreviewFile );
		
		#endregion
		internal bool UpdateItemPreviewFile( UGCUpdateHandle_t handle, uint index, string pszPreviewFile )
		{
			using var str__pszPreviewFile = new Utf8StringToNative( pszPreviewFile );
			var returnValue = _UpdateItemPreviewFile( Self, handle, index, str__pszPreviewFile.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_UpdateItemPreviewVideo", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _UpdateItemPreviewVideo( IntPtr self, UGCUpdateHandle_t handle, uint index, IntPtr pszVideoID );
		
		#endregion
		internal bool UpdateItemPreviewVideo( UGCUpdateHandle_t handle, uint index, string pszVideoID )
		{
			using var str__pszVideoID = new Utf8StringToNative( pszVideoID );
			var returnValue = _UpdateItemPreviewVideo( Self, handle, index, str__pszVideoID.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_RemoveItemPreview", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _RemoveItemPreview( IntPtr self, UGCUpdateHandle_t handle, uint index );
		
		#endregion
		internal bool RemoveItemPreview( UGCUpdateHandle_t handle, uint index )
		{
			var returnValue = _RemoveItemPreview( Self, handle, index );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_AddContentDescriptor", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddContentDescriptor( IntPtr self, UGCUpdateHandle_t handle, UGCContentDescriptorID descid );
		
		#endregion
		internal bool AddContentDescriptor( UGCUpdateHandle_t handle, UGCContentDescriptorID descid )
		{
			var returnValue = _AddContentDescriptor( Self, handle, descid );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_RemoveContentDescriptor", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _RemoveContentDescriptor( IntPtr self, UGCUpdateHandle_t handle, UGCContentDescriptorID descid );
		
		#endregion
		internal bool RemoveContentDescriptor( UGCUpdateHandle_t handle, UGCContentDescriptorID descid )
		{
			var returnValue = _RemoveContentDescriptor( Self, handle, descid );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetRequiredGameVersions", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetRequiredGameVersions( IntPtr self, UGCUpdateHandle_t handle, IntPtr pszGameBranchMin, IntPtr pszGameBranchMax );
		
		#endregion
		internal bool SetRequiredGameVersions( UGCUpdateHandle_t handle, string pszGameBranchMin, string pszGameBranchMax )
		{
			using var str__pszGameBranchMin = new Utf8StringToNative( pszGameBranchMin );
			using var str__pszGameBranchMax = new Utf8StringToNative( pszGameBranchMax );
			var returnValue = _SetRequiredGameVersions( Self, handle, str__pszGameBranchMin.Pointer, str__pszGameBranchMax.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SubmitItemUpdate", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _SubmitItemUpdate( IntPtr self, UGCUpdateHandle_t handle, IntPtr pchChangeNote );
		
		#endregion
		internal CallResult<SubmitItemUpdateResult_t> SubmitItemUpdate( UGCUpdateHandle_t handle, string pchChangeNote )
		{
			using var str__pchChangeNote = new Utf8StringToNative( pchChangeNote );
			var returnValue = _SubmitItemUpdate( Self, handle, str__pchChangeNote.Pointer );
			return new CallResult<SubmitItemUpdateResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetItemUpdateProgress", CallingConvention = Platform.CC)]
		private static extern ItemUpdateStatus _GetItemUpdateProgress( IntPtr self, UGCUpdateHandle_t handle, ref ulong punBytesProcessed, ref ulong punBytesTotal );
		
		#endregion
		internal ItemUpdateStatus GetItemUpdateProgress( UGCUpdateHandle_t handle, ref ulong punBytesProcessed, ref ulong punBytesTotal )
		{
			var returnValue = _GetItemUpdateProgress( Self, handle, ref punBytesProcessed, ref punBytesTotal );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SetUserItemVote", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _SetUserItemVote( IntPtr self, PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bVoteUp );
		
		#endregion
		internal CallResult<SetUserItemVoteResult_t> SetUserItemVote( PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bVoteUp )
		{
			var returnValue = _SetUserItemVote( Self, nPublishedFileID, bVoteUp );
			return new CallResult<SetUserItemVoteResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetUserItemVote", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _GetUserItemVote( IntPtr self, PublishedFileId nPublishedFileID );
		
		#endregion
		internal CallResult<GetUserItemVoteResult_t> GetUserItemVote( PublishedFileId nPublishedFileID )
		{
			var returnValue = _GetUserItemVote( Self, nPublishedFileID );
			return new CallResult<GetUserItemVoteResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_AddItemToFavorites", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _AddItemToFavorites( IntPtr self, AppId nAppId, PublishedFileId nPublishedFileID );
		
		#endregion
		internal CallResult<UserFavoriteItemsListChanged_t> AddItemToFavorites( AppId nAppId, PublishedFileId nPublishedFileID )
		{
			var returnValue = _AddItemToFavorites( Self, nAppId, nPublishedFileID );
			return new CallResult<UserFavoriteItemsListChanged_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_RemoveItemFromFavorites", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _RemoveItemFromFavorites( IntPtr self, AppId nAppId, PublishedFileId nPublishedFileID );
		
		#endregion
		internal CallResult<UserFavoriteItemsListChanged_t> RemoveItemFromFavorites( AppId nAppId, PublishedFileId nPublishedFileID )
		{
			var returnValue = _RemoveItemFromFavorites( Self, nAppId, nPublishedFileID );
			return new CallResult<UserFavoriteItemsListChanged_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SubscribeItem", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _SubscribeItem( IntPtr self, PublishedFileId nPublishedFileID );
		
		#endregion
		internal CallResult<RemoteStorageSubscribePublishedFileResult_t> SubscribeItem( PublishedFileId nPublishedFileID )
		{
			var returnValue = _SubscribeItem( Self, nPublishedFileID );
			return new CallResult<RemoteStorageSubscribePublishedFileResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_UnsubscribeItem", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _UnsubscribeItem( IntPtr self, PublishedFileId nPublishedFileID );
		
		#endregion
		internal CallResult<RemoteStorageUnsubscribePublishedFileResult_t> UnsubscribeItem( PublishedFileId nPublishedFileID )
		{
			var returnValue = _UnsubscribeItem( Self, nPublishedFileID );
			return new CallResult<RemoteStorageUnsubscribePublishedFileResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetNumSubscribedItems", CallingConvention = Platform.CC)]
		private static extern uint _GetNumSubscribedItems( IntPtr self );
		
		#endregion
		internal uint GetNumSubscribedItems()
		{
			var returnValue = _GetNumSubscribedItems( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetSubscribedItems", CallingConvention = Platform.CC)]
		private static extern uint _GetSubscribedItems( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries );
		
		#endregion
		internal uint GetSubscribedItems( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries )
		{
			var returnValue = _GetSubscribedItems( Self, pvecPublishedFileID, cMaxEntries );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetItemState", CallingConvention = Platform.CC)]
		private static extern uint _GetItemState( IntPtr self, PublishedFileId nPublishedFileID );
		
		#endregion
		internal uint GetItemState( PublishedFileId nPublishedFileID )
		{
			var returnValue = _GetItemState( Self, nPublishedFileID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetItemInstallInfo", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetItemInstallInfo( IntPtr self, PublishedFileId nPublishedFileID, ref ulong punSizeOnDisk, IntPtr pchFolder, uint cchFolderSize, ref uint punTimeStamp );
		
		#endregion
		internal bool GetItemInstallInfo( PublishedFileId nPublishedFileID, ref ulong punSizeOnDisk, out string pchFolder, ref uint punTimeStamp )
		{
			using var mem__pchFolder = Helpers.TakeMemory();
			var returnValue = _GetItemInstallInfo( Self, nPublishedFileID, ref punSizeOnDisk, mem__pchFolder, (1024 * 32), ref punTimeStamp );
			pchFolder = Helpers.MemoryToString( mem__pchFolder );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetItemDownloadInfo", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetItemDownloadInfo( IntPtr self, PublishedFileId nPublishedFileID, ref ulong punBytesDownloaded, ref ulong punBytesTotal );
		
		#endregion
		internal bool GetItemDownloadInfo( PublishedFileId nPublishedFileID, ref ulong punBytesDownloaded, ref ulong punBytesTotal )
		{
			var returnValue = _GetItemDownloadInfo( Self, nPublishedFileID, ref punBytesDownloaded, ref punBytesTotal );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_DownloadItem", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _DownloadItem( IntPtr self, PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bHighPriority );
		
		#endregion
		internal bool DownloadItem( PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bHighPriority )
		{
			var returnValue = _DownloadItem( Self, nPublishedFileID, bHighPriority );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_BInitWorkshopForGameServer", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BInitWorkshopForGameServer( IntPtr self, DepotId_t unWorkshopDepotID, IntPtr pszFolder );
		
		#endregion
		internal bool BInitWorkshopForGameServer( DepotId_t unWorkshopDepotID, string pszFolder )
		{
			using var str__pszFolder = new Utf8StringToNative( pszFolder );
			var returnValue = _BInitWorkshopForGameServer( Self, unWorkshopDepotID, str__pszFolder.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_SuspendDownloads", CallingConvention = Platform.CC)]
		private static extern void _SuspendDownloads( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bSuspend );
		
		#endregion
		internal void SuspendDownloads( [MarshalAs( UnmanagedType.U1 )] bool bSuspend )
		{
			_SuspendDownloads( Self, bSuspend );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_StartPlaytimeTracking", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _StartPlaytimeTracking( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs );
		
		#endregion
		internal CallResult<StartPlaytimeTrackingResult_t> StartPlaytimeTracking( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs )
		{
			var returnValue = _StartPlaytimeTracking( Self, pvecPublishedFileID, unNumPublishedFileIDs );
			return new CallResult<StartPlaytimeTrackingResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_StopPlaytimeTracking", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _StopPlaytimeTracking( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs );
		
		#endregion
		internal CallResult<StopPlaytimeTrackingResult_t> StopPlaytimeTracking( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs )
		{
			var returnValue = _StopPlaytimeTracking( Self, pvecPublishedFileID, unNumPublishedFileIDs );
			return new CallResult<StopPlaytimeTrackingResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_StopPlaytimeTrackingForAllItems", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _StopPlaytimeTrackingForAllItems( IntPtr self );
		
		#endregion
		internal CallResult<StopPlaytimeTrackingResult_t> StopPlaytimeTrackingForAllItems()
		{
			var returnValue = _StopPlaytimeTrackingForAllItems( Self );
			return new CallResult<StopPlaytimeTrackingResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_AddDependency", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _AddDependency( IntPtr self, PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID );
		
		#endregion
		internal CallResult<AddUGCDependencyResult_t> AddDependency( PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID )
		{
			var returnValue = _AddDependency( Self, nParentPublishedFileID, nChildPublishedFileID );
			return new CallResult<AddUGCDependencyResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_RemoveDependency", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _RemoveDependency( IntPtr self, PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID );
		
		#endregion
		internal CallResult<RemoveUGCDependencyResult_t> RemoveDependency( PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID )
		{
			var returnValue = _RemoveDependency( Self, nParentPublishedFileID, nChildPublishedFileID );
			return new CallResult<RemoveUGCDependencyResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_AddAppDependency", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _AddAppDependency( IntPtr self, PublishedFileId nPublishedFileID, AppId nAppID );
		
		#endregion
		internal CallResult<AddAppDependencyResult_t> AddAppDependency( PublishedFileId nPublishedFileID, AppId nAppID )
		{
			var returnValue = _AddAppDependency( Self, nPublishedFileID, nAppID );
			return new CallResult<AddAppDependencyResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_RemoveAppDependency", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _RemoveAppDependency( IntPtr self, PublishedFileId nPublishedFileID, AppId nAppID );
		
		#endregion
		internal CallResult<RemoveAppDependencyResult_t> RemoveAppDependency( PublishedFileId nPublishedFileID, AppId nAppID )
		{
			var returnValue = _RemoveAppDependency( Self, nPublishedFileID, nAppID );
			return new CallResult<RemoveAppDependencyResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetAppDependencies", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _GetAppDependencies( IntPtr self, PublishedFileId nPublishedFileID );
		
		#endregion
		internal CallResult<GetAppDependenciesResult_t> GetAppDependencies( PublishedFileId nPublishedFileID )
		{
			var returnValue = _GetAppDependencies( Self, nPublishedFileID );
			return new CallResult<GetAppDependenciesResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_DeleteItem", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _DeleteItem( IntPtr self, PublishedFileId nPublishedFileID );
		
		#endregion
		internal CallResult<DeleteItemResult_t> DeleteItem( PublishedFileId nPublishedFileID )
		{
			var returnValue = _DeleteItem( Self, nPublishedFileID );
			return new CallResult<DeleteItemResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_ShowWorkshopEULA", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ShowWorkshopEULA( IntPtr self );
		
		#endregion
		internal bool ShowWorkshopEULA()
		{
			var returnValue = _ShowWorkshopEULA( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetWorkshopEULAStatus", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _GetWorkshopEULAStatus( IntPtr self );
		
		#endregion
		internal CallResult<WorkshopEULAStatus_t> GetWorkshopEULAStatus()
		{
			var returnValue = _GetWorkshopEULAStatus( Self );
			return new CallResult<WorkshopEULAStatus_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( "steam_api.dll", EntryPoint = "SteamAPI_ISteamUGC_GetUserContentDescriptorPreferences", CallingConvention = Platform.CC)]
		private static extern uint _GetUserContentDescriptorPreferences( IntPtr self, [In,Out] UGCContentDescriptorID[]  pvecDescriptors, uint cMaxEntries );
		
		#endregion
		internal uint GetUserContentDescriptorPreferences( [In,Out] UGCContentDescriptorID[]  pvecDescriptors, uint cMaxEntries )
		{
			var returnValue = _GetUserContentDescriptorPreferences( Self, pvecDescriptors, cMaxEntries );
			return returnValue;
		}
		
	}
}
