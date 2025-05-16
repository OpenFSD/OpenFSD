using OpenTK;
using Florence.ServerAssembly.Graphics.Cameras;
using Florence.ServerAssembly.Graphics.GameObjects;
using Florence.ServerAssembly.Graphics.Renderables;

namespace ServerAssembly.GameInstance
{
    public class Player : AGameObject
    {
        private bool _firstMove;
        private bool _firstMouseMove;
        private Vector2 mousePos;
        private FirstPersonCamera _cameraFP;
        private ThirdPersonCamera _cameraTP;
        private float cameraSpeed;
        private float sensitivity;

        public Player(ARenderable model, Vector3 position, Vector3 direction, Vector3 rotation, float velocity)
            : base(model, position, direction, rotation, velocity)
        {
            _firstMove = true;
            _firstMouseMove = true;
            mousePos = new Vector2(0, 0);
            _cameraFP = null;
            _cameraTP = null;
            cameraSpeed = 1.5f;
            sensitivity = 1f;
        }
        public void Create_Cameras()
        {
            _cameraFP = new FirstPersonCamera(
                this
            );
            while (_cameraFP == null) { }

            _cameraTP = new ThirdPersonCamera(
                this
            );
            while (_cameraTP == null) { }
        }

        public FirstPersonCamera Get_CameraFP()
        {
            return _cameraFP;
        }
        public ThirdPersonCamera Get_CameraTP()
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
        public Vector2 Get_MousePos()
        {
            return mousePos;
        }

        public float Get_cameraSpeed()
        {
            return cameraSpeed;
        }

        public float Get_sensitivity()
        {
            return sensitivity;
        }



        public void Set_IsFirstMove(bool value)
        {
            _firstMove = value;
        }
        public void Set_IsFirstMouseMove(bool value)
        {
            _firstMouseMove = value;
        }

        public void Set_MousePos(Vector2 pos)
        {
            mousePos = pos;
        }


    }
}

