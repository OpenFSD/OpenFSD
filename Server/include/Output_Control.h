#pragma once
#include "Framework_Server.h"
#include "User_O.h"
#include "Praise0_Output.h"
#include "Praise1_Output.h"
#include "Praise2_Output.h"

namespace Server_Library
{
    class Output_Control
    {
    public:
        Output_Control();
        virtual ~Output_Control();

        void SelectSet_Output_Subset(__int8 ptr_praiseEventId, __int8 concurrent_coreId);

    protected:

    private:

    };
}