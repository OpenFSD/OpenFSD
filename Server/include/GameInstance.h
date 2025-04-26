#pragma once
#include <vector>

namespace Server_Library
{
    class GameInstance
    {
    public:
        GameInstance();
        virtual ~GameInstance();

        static class Player* GetPlayer(__int8 playerId);

    protected:

    private:
        static std::vector<class Player*> player;
    };
}