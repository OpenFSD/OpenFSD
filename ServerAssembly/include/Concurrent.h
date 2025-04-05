#pragma once
#include "Concurrent_Control.h"
#include "Framework_Server.h"
#include "Praise0_Algorithm.h"
#include "Praise0_Input.h"
#include "Praise0_Output.h"
#include "Praise1_Algorithm.h"
#include "Praise1_Input.h"
#include "Praise1_Output.h"
#include "Praise2_Algorithm.h"
#include "Praise2_Input.h"
#include "Praise2_Output.h"


namespace Server_Library
{
    class Concurrent
    {
    public:
        Concurrent();
        virtual ~Concurrent();

        void Initialise_Control();
        static void Thread_Concurrency(
            __int8 concurrent_coreId,
            __int8 number_Implemented_Cores
        );
        void Do_Concurrent_Algorithm_For_PraiseEventId(
            __int16 ptr_praiseEventId,
            class Object* ptr_Algorithm_Subset,
            class Object* ptr_Input_Subset,
            class Object* ptr_Output_Subset
        );

        class Object* Get_Algorithm_Subset();
        class Concurrent_Control* Get_Concurrent_Control();

        void Set_Algorithm_Subset(class Object* value_algorithm);

    protected:

    private:
        static class Concurrent_Control* ptr_Concurrent_Control;
        static class Object* ptr_Algorithms_Subset;

    };
}