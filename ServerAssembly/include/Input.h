#pragma once
#include "Input_Control.h"

namespace Server_Library
{
    class Input
    {
    public:
        Input();
        virtual ~Input();
        void Initialise_Control();

        class Input_Control* Get_Input_Control();
        class Object* Get_InputBuffer_Subset();
        __int8 GetPraiseEventId();

        void Set_Subset_InputBuffer(class Praise0_Input* value);
        void Set_Subset_InputBuffer(class Praise1_Input* value);
        void Set_Subset_InputBuffer(class Praise2_Input* value);
        void SetPraiseEventId(__int8 value);

    protected:

    private:
        static class Input_Control* input_Control;
        static class Object* buffer_SubSet_InputPraise;
        static __int8 in_praiseEventId;

    };
}