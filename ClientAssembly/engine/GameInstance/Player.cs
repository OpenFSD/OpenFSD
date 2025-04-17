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
        private ThirdPersonCamera _camera;
        private float cameraSpeed;
        private float sensitivity;

        public Player(ARenderable model, Vector4 position, Vector4 direction, Vector4 rotation, float velocity)
            : base(model, position, direction, rotation, velocity)
        {
            _firstMove = true;
            _firstMouseMove = true;
            isPlayerMoved = false;
            isMouseChanged = false;
            mousePos = new Vector2(1920/2, 1080/2);
            _camera = null;
            cameraSpeed = 1.5f;
            sensitivity = 0.01f;
        }

        public void Align_PlayerGyro()
        {
            Set_Position(Get_position().Normalized() * 110f);

            Vector3 v_cross = Vector3.Cross(_last_position.Xyz, _position.Xyz);
            double theta = System.Math.Asin(v_cross.Length / (_last_position.Xyz.Length * _position.Xyz.Length));
/*
            v_A = new Vector3(0, _last_position.Y, 0);
            v_B = new Vector3(0, _position.Y, 0);
            Vector3 vY_cross = Vector3.Cross(v_A, v_B);
            double thetaY = System.Math.Asin(vY_cross.Length / (v_A.Length * v_B.Length));

            v_A = new Vector3(0, 0, _last_position.Z);
            v_B = new Vector3(0, 0, _position.Z);
            Vector3 vZ_cross = Vector3.Cross(v_A, v_B);
            double thetaZ = System.Math.Asin(vZ_cross.Length / (v_A.Length * v_B.Length));

            Set_Rotation(new Vector4((float)thetaX, (float)thetaY, (float)thetaZ, 0));

            Matrix4 modelMatrix = Matrix4.CreateRotationX(_rotation.X) * Matrix4.CreateRotationY(_rotation.Y) * Matrix4.CreateRotationZ(_rotation.Z) * Matrix4.CreateTranslation(_direction.X, _direction.Y, _direction.Z);
            Quaternion q = modelMatrix.ExtractRotation();
            _direction = new Vector4(q.Xyz, 0);
            _direction.Normalize();
            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_Player().Set_direction(_direction);
*/
        }

        public ThirdPersonCamera Get_Camera()
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

        public float Get_cameraSpeed()
        {
            return cameraSpeed;
        }

        public float Get_sensitivity()
        {
            return sensitivity;
        }

        public void Set_Camera()
        {
            _camera = new Florence.ServerAssembly.Graphics.Cameras.ThirdPersonCamera(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player());
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
            mousePos = pos;
        }
    }
}

