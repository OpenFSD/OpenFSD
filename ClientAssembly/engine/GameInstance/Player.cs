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
        private ICamera _camera;
        private float cameraSpeed;
        private float sensitivity;

        public Player(ARenderable model, Vector4 position, Vector4 direction, Vector4 rotation, float velocity)
            : base(model, position, direction, rotation, velocity)
        {
            _firstMove = true;
            _firstMouseMove = true;
            isPlayerMoved = false;
            isMouseChanged = false;
            mousePos = new Vector2(0, 0);
            _camera = new StaticCamera();
            cameraSpeed = 1.5f;
            sensitivity = 0.2f;

            Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 target = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
            //_camera = new Florence.ServerAssembly.Graphics.Cameras.FirstPersonCamera(this, target, up);
        }

        public ICamera Get_Camera()
        {
            return _camera;
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

        public void Set_Camera()
        {
            _camera = new Florence.ServerAssembly.Graphics.Cameras.StaticCamera();
            while (_camera == null) { }
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
            Framework.GetClient().GetData().GetGame_Instance().Get_Player().Set_MousePos(pos);
        }
    }
}

