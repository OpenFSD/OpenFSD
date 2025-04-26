#pragma once
#include "Framework_Server.h"
#include "User_I.h"
#include "Praise0_Input.h"
#include "Praise1_Input.h"
#include "Praise2_Input.h"

namespace Server_Library
{
    class Input_Control
    {
    public:
        Input_Control();
        virtual ~Input_Control();
        
        void SelectSet_Input_Subset(__int8 ptr_praiseEventId);

    protected:

    private:

    };
}