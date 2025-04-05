#pragma once
#include "Framework_Server.h"
#include "User_I.h"
#include "Praise0_Input.h"
#include "Praise1_Input.h"

namespace Server_Library
{
    class Input_Control
    {
    public:
        Input_Control();
        virtual ~Input_Control();
        
        void LoadValuesInToInputSubset(__int8 concurrent_CoreId, int* praiseEventId);
        void SelectSetInputSubset(int* ptr_praiseEventId);

    protected:

    private:

    };
}