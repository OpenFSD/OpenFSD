/*
*  Managed C# wrapper for FLORENCE Concurrent Que library by Jasper Assembly Pty Ltd.
*  Copyright (c) 2022 - 2025 Jasper Assembly Pty Ltd.  
*  All rights reserved.
*/

using System.Runtime.InteropServices;
using System.Security;


namespace Florence.Concurrency
{
    [SuppressUnmanagedCodeSecurity]
    public static class ConcurrentQue_Client
    {
        [DllImport("WaitLaunch_Client_ConcurrentThread_lib.dll", CharSet = CharSet.Unicode)]
        public static extern void Create_ConcurrentQue();

        [DllImport("WaitLaunch_Client_ConcurrentThread_lib.dll", CharSet = CharSet.Unicode)]
        public static extern void Request_Wait_Launch();

        [DllImport("WaitLaunch_Client_ConcurrentThread_lib.dll", CharSet = CharSet.Unicode)]
        public static extern void Thread_End();

        [DllImport("WaitLaunch_Client_ConcurrentThread_lib.dll", CharSet = CharSet.Unicode)]
        public static extern void Get_coreId_To_Launch();

        [DllImport("WaitLaunch_Client_ConcurrentThread_lib.dll", CharSet = CharSet.Unicode)]
        public static extern void Get_Flag_Active();

        [DllImport("WaitLaunch_Client_ConcurrentThread_lib.dll", CharSet = CharSet.Unicode)]
        public static extern void Get_Flag_Idle();

        [DllImport("WaitLaunch_Client_ConcurrentThread_lib.dll", CharSet = CharSet.Unicode)]
        public static extern void Get_State_LaunchBit();
    }

    [SuppressUnmanagedCodeSecurity]
    public static class ConcurrentQue_Server
    {
        [DllImport("WaitLaunch_Server_ConcurrentThread_lib.dll", CharSet = CharSet.Unicode)]
        public static extern void Create_ConcurrentQue();

        [DllImport("WaitLaunch_Server_ConcurrentThread_lib.dll", CharSet = CharSet.Unicode)]
        public static extern void Request_Wait_Launch();

        [DllImport("WaitLaunch_Server_ConcurrentThread_lib.dll", CharSet = CharSet.Unicode)]
        public static extern void Thread_End();

        [DllImport("WaitLaunch_Server_ConcurrentThread_lib.dll", CharSet = CharSet.Unicode)]
        public static extern void Get_coreId_To_Launch();

        [DllImport("WaitLaunch_Server_ConcurrentThread_lib.dll", CharSet = CharSet.Unicode)]
        public static extern void Get_Flag_Active();

        [DllImport("WaitLaunch_Server_ConcurrentThread_lib.dll", CharSet = CharSet.Unicode)]
        public static extern void Get_Flag_Idle();

        [DllImport("WaitLaunch_Server_ConcurrentThread_lib.dll", CharSet = CharSet.Unicode)]
        public static extern void Get_State_LaunchBit();
    }
}