/*
*  Managed C# wrapper for FLORENCE Server library by Jasper Assembly Pty Ltd.
*  Copyright (c) 2022 - 2025 Brenton James Maddocks BEng(CompSys).  
*  All rights reserved.
*/
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Florence.Server_IO
{
    [SuppressUnmanagedCodeSecurity]
    public static class ConcurrentQue_ServerSide
    {
        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Create_ConcurrentQue();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Request_Wait_Launch();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Thread_End();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Get_coreId_To_Launch();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Get_Flag_Active();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Get_Flag_Idle();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Get_State_LaunchBit();
    }

    [SuppressUnmanagedCodeSecurity]
    public static class Library
    {
        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Create_Hosting_Server();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Flip_InBufferToWrite();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Flip_OutBufferToWrite();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern bool Get_Flag_IsStackLoaded_Server_InputAction();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern bool Get_Flag_IsStackLoaded_Server_OutputRecieve();

                [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern bool GetFlag_Server_Library_Initialised();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Pop_Stack_Output();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Push_Stack_InputPraises();
    }


    [SuppressUnmanagedCodeSecurity]
    internal static class PraiseEvents
    {
        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern byte Get_PraiseEventId();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern float Get_Praise1_pitch();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern float Get_Praise1_yaw();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Set_PraiseEventId(byte value);

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Set_Praise1_mousePos_X(Int16 value);

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Set_Praise1_mousePos_Y(Int16 value);
    }

    [SuppressUnmanagedCodeSecurity]
    public static class WriteEnable_Stack_Server_OutputSend
    {
        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Create_Write_Stack_Server_OutputSend();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void End_Write_Stack_Stack_Server_OutputSend(byte coreId);

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Request_Write_Stack_Server_OutputSend(byte coreId);
    }

    [SuppressUnmanagedCodeSecurity]
    public static class WriteEnable_Stack_Server_InputAction
    {
        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Create_Write_Stack_Server_InputAction();

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void End_Write_Stack_Stack_Server_InputAction(byte coreId);

        [DllImport("ServerLibrary.dll", CharSet = CharSet.Unicode)]
        public static extern void Request_Write_Stack_Server_InputAction(byte coreId);
    }
}