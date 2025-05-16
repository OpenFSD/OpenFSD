using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florence.ServerAssembly.GameInstance
{
    public class Settings
    {
        private float _fov = 45f;
        private static int _refreshRate = 144;
        private static bool _systemInitialised = false;
        private char _screenSize_X;
        private char _screenSize_Y;

        public Settings()
        {
            Console.WriteLine("Florence.ClientAssembly: Settings");
            set_ScreenSize_X((char)1920);
            set_ScreenSize_Y((char)1080);
            Set_fov(90f);
        }
        //GET
        public float Get_fov()
        {
            return _fov;
        }
        public int Get_refreshRate()
        {
            return _refreshRate;
        }
        public char Get_ScreenSize_X()
        {
            return _screenSize_X;
        }
        public char Get_ScreenSize_Y()
        {
            return _screenSize_Y;
        }

        //SET
        public void Set_fov(float value)
        {
            _fov = value;
        }
        public void Set_refreshRate(int value)
        {
            _refreshRate = value;
        }
        public void set_ScreenSize_X(char value)
        {
            _screenSize_X = value;
        }
        public void set_ScreenSize_Y(char value)
        {
            _screenSize_Y = value;
        }
        public static bool Get_systemInitialised()
        {
            return _systemInitialised;

        }
        public static void Set_systemInitialised(bool value)
        {
            _systemInitialised = value;
        }
    }
}