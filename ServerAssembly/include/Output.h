#pragma once
#include "Output_Control.h"

namespace Server_Library
{
    class Output
    {
    public:
        Output();
        virtual ~Output();

        void Initialise_Control();

        class Output_Control* Get_Control_Of_Output();
        class Object* Get_OutputBuffer_Subset();
        __int8 GetPraiseEventId();

        void Set_OutputBuffer_Subset(class Object* value);
        void SetPraiseEventId(__int8 value);

    protected:

    private:
        static class Output_Control* control_Of_Output;
        static class Object* praiseOutputBuffer_Subset;
        static __int8 out_PraiseEventId;
    };
}