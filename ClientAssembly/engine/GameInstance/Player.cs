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
        private Vector3 _foward;
        private Vector3 _right;
        private Vector3 _up;
        private float _pitch;
        private float _yaw;
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
            _foward = -Vector3.UnitZ;
            _up = Vector3.UnitY;
            _right = Vector3.Cross(_foward, _up);

            _camera = new StaticCamera();
            cameraSpeed = 1.5f;
            sensitivity = 0.2f;
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

        public Vector3 Get_foward()
        {
            return _foward;
        }

        public Vector3 Get_right()
        {
            return _right;
        }

        public Vector3 Get_up()
        {
            return _up;
        }
        
        public float Get_cameraSpeed()
        {
            return cameraSpeed;
        }

        public float Get_sensitivity()
        {
            return sensitivity;
        }

        public float Get_pitch()
        {
            return _pitch;
        }

        public float Get_yaw()
        {
            return _yaw;
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

        public void Set_foward(Vector3 value)
        {
            _foward = value;
        }

        public void Set_right(Vector3 value)
        {
            _right = value;
        }

        public void Set_up(Vector3 value)
        {
            _up = value;
        }

        public void Set_pitch(float value)
        {
            _pitch = value;
        }

        public void Set_yaw(float value)
        {
            _yaw = value;
        }
    }
}

