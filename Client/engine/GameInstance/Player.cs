using OpenTK;
using Florence.ClientAssembly.Graphics.Cameras;
using Florence.ClientAssembly.Graphics.GameObjects;
using Florence.ClientAssembly.Graphics.Renderables;

namespace Florence.ClientAssembly.game_Instance
{
    public class Player : AGameObject
    {
        private Vector2 _last_mousePos;
        private float _player_Yaw_radians;
        private float _player_Pitch_radians;
        //private Vector3 player_axis_X;
        //private Vector3 player_axis_Y;
        //private Vector3 player_axis_Z;
        private FirstPersonCamera _cameraFP;
        private ThirdPersonCamera _cameraTP;
        private bool cameraSelector;
        private bool isFirstMove;

        public Player(ARenderable model, Vector3 position, Vector3 direction, Vector3 rotation, float velocity)
            : base(model, position, direction, rotation, velocity)
        {
            _last_mousePos = new Vector2((char)Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_X() / 2, (char)Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_Y() / 2);
            _player_Yaw_radians = 0;
            _player_Pitch_radians = 0;
            _cameraFP = null;
            _cameraTP = null;
            cameraSelector = true;
            isFirstMove = true;
        }

        public void Initialise_Player()
        {
            //player_axis_X = this.Fowards;
            //player_axis_Y = this.Up;
            //player_axis_Z = this.Right;
        }
        public void Create_Cameras()
        {
            _cameraTP = new Florence.ClientAssembly.Graphics.Cameras.ThirdPersonCamera(
                Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player()
            );
            while (_cameraTP == null) { }

            _cameraFP = new Florence.ClientAssembly.Graphics.Cameras.FirstPersonCamera(
                Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player()
            );
            while (_cameraFP == null) { }
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
        public bool Get_isFirstMove()
        {
            return isFirstMove;
        }
        public Vector2 Get_Last_MousePos()
        {
            return _last_mousePos;
        }
        public float Get_player_Yaw_radians()
        {
            return _player_Yaw_radians;
        }
        public float Get_player_Pitch_radians()
        {
            return _player_Pitch_radians;
        }
        /*
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
        */
        //SET
        public void Set_cameraSelector(bool value)
        {
            cameraSelector = value;
        }
        public void Set_isFirstMove(bool value)
        {
            isFirstMove = value;
        }
        public void Set_Last_MousePos(Vector2 pos)
        {
            _last_mousePos = pos;
        }
        public void Set_player_Yaw_radians(float value)
        {
            _player_Yaw_radians = value;
        }
        public void Set_player_Pitch_radians(float value)
        {
            _player_Pitch_radians = value;
        }
        /*
        public void Set_player_rot_axis_X(Vector3 value)
        {
            player_axis_X = value;
        }
        public void Set_player_rot_axis_Y(Vector3 value)
        {
            player_axis_Y = value;
        }
        public void Set_player_rot_axis_Z(Vector3 value)
        {
            player_axis_Z = value;
        }
        */
    }
}

