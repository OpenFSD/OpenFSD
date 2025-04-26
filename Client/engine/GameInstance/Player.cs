using OpenTK;
using Florence.ServerAssembly.Graphics.Cameras;
using Florence.ServerAssembly.Graphics.GameObjects;
using Florence.ServerAssembly.Graphics.Renderables;

namespace Florence.ClientAssembly.game_Instance
{
    public class Player : AGameObject
    {
        private Vector2 mousePos;
        private float _player_Yaw_radians;
        private float _player_Pitch_radians;
        private Vector3 player_axis_X;
        private Vector3 player_axis_Y;
        private Vector3 player_axis_Z;
        private FirstPersonCamera _cameraFP;
        private ThirdPersonCamera _cameraTP;
        private bool cameraSelector;

        public Player(ARenderable model, Vector3 position, Vector3 direction, Vector3 rotation, float velocity)
            : base(model, position, direction, rotation, velocity)
        {
            mousePos = new Vector2((char)Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_X() / 2, (char)Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_Y() / 2);
            _player_Yaw_radians = 0;
            _player_Pitch_radians = 0;
            _cameraFP = null;
            _cameraTP = null;
            cameraSelector = false;
        }

        public void Initialise_Player()
        {
            _last_position = Position;
            player_axis_X = this.Fowards;
            player_axis_Y = this.Right;
            player_axis_Z = this.Up;
        }
        public void Add_deltaX_To_player_Yaw_radians(float deltaX)
        {
            var sensitivity = Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_Camera_FP().Get_sensitivity();
            float deltaX_radians = (float)((System.Math.PI / 180) * (deltaX * sensitivity));
            if ((deltaX_radians < (float)System.Math.PI) && (deltaX_radians >= (float)-System.Math.PI))
            {
                deltaX_radians += deltaX;
            }
            else if (deltaX_radians >= (float)(System.Math.PI))
            {
                deltaX_radians -= (float)(2 * System.Math.PI);
            }
            else if (deltaX_radians < (float)(-System.Math.PI))
            {
                deltaX_radians += (float)(2 * System.Math.PI);
            }
            Set_player_Yaw_radians(Get_player_Yaw_radians() + deltaX_radians);
        }
        public void Add_deltaY_To_player_Pitch_radians(float deltaY)
        {
            var sensitivity = Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_Camera_FP().Get_sensitivity();
            float deltaY_radians = (float)((System.Math.PI / 180) * (deltaY * sensitivity));
            if ((deltaY_radians < (float)System.Math.PI) && (deltaY_radians >= (float)-System.Math.PI))
            {
                deltaY_radians += (float)((System.Math.PI / 180) * deltaY);
            }
            else if (deltaY_radians >= (float)(System.Math.PI))
            {
                deltaY_radians -= (float)(2 * System.Math.PI);
            }
            else if (deltaY_radians < (float)(-System.Math.PI))
            {
                deltaY_radians += (float)(2 * System.Math.PI);
            }
            Set_player_Pitch_radians(Get_player_Pitch_radians() + deltaY_radians);
        }
        public Vector3 Calculate_Rotations(Vector3 gyroPosition)
        {
            float angleFrom_X = (float)System.Math.Asin((Vector3.Cross(gyroPosition, Get_player_axis_X())).Length / (Get_player_axis_X().Length * gyroPosition.Length));
            float angleFrom_Y = (float)System.Math.Asin((Vector3.Cross(gyroPosition, Get_player_axis_Y())).Length / (Get_player_axis_Y().Length * gyroPosition.Length));
            float angleFrom_Z = (float)System.Math.Asin((Vector3.Cross(gyroPosition, Get_player_axis_Z())).Length / (Get_player_axis_Z().Length * gyroPosition.Length));

            if ((gyroPosition.X >= 0) && (gyroPosition.Y >= 0) && (gyroPosition.Z >= 0))
            {
                angleFrom_X = 0 - angleFrom_X;
                //angleTo_Y = +ve
                //angleTo_Z = +ve
            }
            else if ((gyroPosition.X >= 0) && (gyroPosition.Y >= 0) && (gyroPosition.Z < 0))
            {
                //angleFrom_X = 0 + angleFrom_X
                //angleFrom_Y = 0 + angleFrom_Y
                angleFrom_Z = 0 - angleFrom_Z;
            }
            else if ((gyroPosition.X >= 0) && (gyroPosition.Y < 0) && (gyroPosition.Z >= 0))
            {
                angleFrom_X = 0 - angleFrom_X;
                angleFrom_Y = 0 - angleFrom_Y;
                //angleFrom_Z = 0 + angleFrom_Z;        
            }
            else if ((gyroPosition.X >= 0) && (gyroPosition.Y < 0) && (gyroPosition.Z < 0))
            {
                //angleFrom_X = 0 + angleFrom_X;
                //angleFrom_Y = 0 + angleFrom_Y;
                //angleFrom_Z = 0 + angleFrom_Z;
            }
            else if ((gyroPosition.X < 0) && (gyroPosition.Y >= 0) && (gyroPosition.Z >= 0))
            {
                angleFrom_X = 0 - angleFrom_X;
                angleFrom_Y = 0 - angleFrom_Y;
                angleFrom_Z = 0 - angleFrom_Z;
            }
            else if ((gyroPosition.X < 0) && (gyroPosition.Y >= 0) && (gyroPosition.Z < 0))
            {
                //angleFrom_X = 0 + angleFrom_X;
                //angleFrom_Y = 0 + angleFrom_Y;
                angleFrom_Z = 0 - angleFrom_Z;
            }
            else if ((gyroPosition.X < 0) && (gyroPosition.Y < 0) && (gyroPosition.Z >= 0))
            {
                angleFrom_X = 0 - angleFrom_X;
                angleFrom_Y = 0 - angleFrom_Y;
                //angleFrom_Z = 0 + angleFrom_Z;        
            }
            else if ((gyroPosition.X < 0) && (gyroPosition.Y < 0) && (gyroPosition.Z < 0))
            {
                //angleFrom_X = 0 + angleFrom_X;
                //angleFrom_Y = 0 + angleFrom_Y;
                angleFrom_Z = 0 - angleFrom_Z;
            }
            System.Console.WriteLine("TESTBENCH => delta_angleFrom_X = " + angleFrom_X + "  delta_angleFrom_Y = " + angleFrom_Y + "  delta_angleFrom_Z = " + angleFrom_Z);
            return new Vector3(angleFrom_X, angleFrom_Y, angleFrom_Z);
        }
        public void Create_Cameras()
        {
            Vector3 temp = -_fowards + _up;
            _cameraTP = new Florence.ServerAssembly.Graphics.Cameras.ThirdPersonCamera(
                Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player(),
                temp
            ); ;
            while (_cameraTP == null) { }

            _cameraFP = new Florence.ServerAssembly.Graphics.Cameras.FirstPersonCamera(
                Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player()
            );
            while (_cameraFP == null) { }
        }
        public void Tweek_Player_Gyro()
        {
            _position = _position.Normalized() * 103f;
        }
//GET
        public FirstPersonCamera Get_Camera_FP()
        {
            return _cameraFP;
        }
        public ThirdPersonCamera Get_Camera_TP()
        {
            return _cameraTP;
        }
        public bool Get_cameraSelector()
        {
            return cameraSelector;
        }
        public Vector2 Get_MousePos()
        {
            return mousePos;
        }
        public float Get_player_Yaw_radians()
        {
            return _player_Yaw_radians;
        }
        public float Get_player_Pitch_radians()
        {
            return _player_Pitch_radians;
        }
        public Vector3 Get_player_axis_X()
        {
            return player_axis_X;
        }
        public Vector3 Get_player_axis_Y()
        {
            return player_axis_Y;
        }
        public Vector3 Get_player_axis_Z()
        {
            return player_axis_Z;
        }

        //SET
        public void Set_cameraSelector(bool value)
        {
            cameraSelector = value;
        }
        public void Set_MousePos(Vector2 pos)
        {
            mousePos = pos;
        }
        public void Set_player_Yaw_radians(float value)
        {
            _player_Yaw_radians = value;

            if (_player_Yaw_radians >= System.Math.PI)
            {
                while (_player_Yaw_radians >= System.Math.PI)
                {
                    _player_Yaw_radians = _player_Yaw_radians - (float)(2 * System.Math.PI);
                }
            }
            else if (_player_Yaw_radians < -System.Math.PI)
            {
                while (_player_Yaw_radians < -System.Math.PI)
                {
                    _player_Yaw_radians = _player_Yaw_radians + (float)(2 * System.Math.PI);
                }
            }
        }
        public void Set_player_Pitch_radians(float value)
        {
            _player_Pitch_radians = value;

            if (_player_Pitch_radians >= System.Math.PI)
            {
                while (_player_Pitch_radians >= System.Math.PI)
                {
                    _player_Pitch_radians = _player_Pitch_radians - (float)(2 * System.Math.PI);
                }
            }
            else if (_player_Pitch_radians < -System.Math.PI)
            {
                while (_player_Pitch_radians < -System.Math.PI)
                {
                    _player_Pitch_radians = _player_Pitch_radians + (float)(2 * System.Math.PI);
                }
            }
        }
        public void Set_player_axis_X(Vector3 value)
        {
            player_axis_X = value;
        }
        public void Set_player_axis_Y(Vector3 value)
        {
            player_axis_Y = value;
        }
        public void Set_player_axis_Z(Vector3 value)
        {
            player_axis_Z = value;
        }
    }
}

