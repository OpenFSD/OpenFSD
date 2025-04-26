#pragma once
#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers

#include "ConcurrentQue_Client_LaunchConcurrency.h"

namespace ConcurrentQue
{
    class ConcurrentQue_Client_Framework
    {
    public:
        ConcurrentQue_Client_Framework();
        virtual ~ConcurrentQue_Client_Framework();
        static void Create_ConcurrentQue();
        static void Request_Wait_Launch(unsigned char concurrent_CoreId);
        static void Thread_End(unsigned char concurrent_CoreId);
        
        static unsigned char Get_coreId_To_Launch();
        static bool Get_Flag_Active();
        static bool Get_Flag_Idle();
        static bool Get_State_LaunchBit();

        static class ConcurrentQue_Client_LaunchConcurrency* Get_LaunchConcurrency();

    protected:

    private:
        static class ConcurrentQue_Client_LaunchConcurrency* ptr_LaunchConcurrency;
    };
}