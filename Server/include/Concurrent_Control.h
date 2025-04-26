#pragma once
#include "Framework_Server.h"
#include "User_Alg.h"
#include "Praise0_Algorithm.h"
#include "Praise1_Algorithm.h"
#include "Praise2_Algorithm.h"

namespace Server_Library
{
    class Concurrent_Control
    {
    public:
        Concurrent_Control();
        virtual ~Concurrent_Control();

        void SelectSet_Algorithm_Subset(
            __int8 ptr_praiseEventId,
            __int8 concurrent_coreId
        );

    protected:

    private:

    };
}