#pragma once
#include <vector>

namespace Server_Library
{
    class Player
    {
    public:
        Player();
        virtual ~Player();

        static bool Get_isMouseChanged();
        static bool Get_isFirstMove();
        static std::vector<float> Get_Front();
        static std::vector<float> Get_Right();
        static std::vector<float> Get_Up();
        static std::vector<float> Get_MousePos();
        static std::vector<float> Get_PlayerPosition();

        static float Get_CameraSpeed();
        static float Get_Sensativity();

        static void Set_isFirstMove(bool value);
        static void Set_MousePos(std::vector<float> value);
        static void Set_PlayerPosition(std::vector<float> value);

    protected:

    private:
        static bool _firstMove;

        static bool isPlayerMoved;
        static std::vector<float> player_Position;
        static std::vector<float> _Front;
        static std::vector<float> _Right;
        static std::vector<float> _Up;

        static bool isMouseChanged;
        static std::vector<float> mouse_Position;

        static float cameraSpeed;
        static float sensitivity;
    };
}