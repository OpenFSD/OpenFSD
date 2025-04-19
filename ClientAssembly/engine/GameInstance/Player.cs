using OpenTK;
using Florence.ServerAssembly.Graphics.Cameras;
using Florence.ServerAssembly.Graphics.GameObjects;
using Florence.ServerAssembly.Graphics.Renderables;

namespace Florence.ClientAssembly.game_Instance
{
    public class Player : AGameObject
    {
        private bool _firstMove;
        private bool _firstMouseMove;
        private bool isPlayerMoved;
        private bool isMouseChanged;

        private Vector2 mousePos;

        private FirstPersonCamera _cameraFP;
        private ThirdPersonCamera _cameraTP;
        private bool cameraSelector;

        public Player(ARenderable model, Vector3 position, Vector3 direction, Vector3 rotation, float velocity)
            : base(model, position, direction, rotation, velocity)
        {
            _firstMove = true;
            _firstMouseMove = true;
            cameraSelector = true;
             mousePos = new Vector2(1920/2, 1080/2);
            _last_position = position;

            _cameraFP = null;
            _cameraTP = null;
        }

        public void Align_PlayerGyro()
        {
            Set_Position(Get_position().Normalized() * 101f);

            Set_fowards(Get_position() + (Get_position() - Get_last_position()));
            Set_fowards(Get_fowards().Normalized());

            Set_up(Get_position() + Get_position().Normalized());

            Set_right(Vector3.Cross(Get_fowards(),Get_up()));
            Set_right(Get_right().Normalized());
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

//GET
        public FirstPersonCamera Get_Camera_FP()
        {
            return _cameraFP;
        }
        public ThirdPersonCamera Get_Camera_TP()
        {
            return _cameraTP;
        }
        public bool Get_IsFirstMove()
        {
            return _firstMove;
        }
        public bool Get_IsFirstMouseMove()
        {
            return _firstMouseMove;
        }
        public bool Get_cameraSelector()
        {
            return cameraSelector;
        }
        public Vector2 Get_MousePos()
        {
            return mousePos;
        }

        
//SET
        public void Set_IsFirstMove(bool value)
        {
            _firstMove = value;
        }
        public void Set_IsFirstMouseMove(bool value)
        {
            _firstMouseMove = value;
        }
        public void Set_cameraSelector(bool value)
        {
            cameraSelector = value;
        }
        public void Set_MousePos(Vector2 pos)
        {
            mousePos = pos;
        }

    }
}

