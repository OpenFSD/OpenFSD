using OpenTK;
using Florence.ClientAssembly.Graphics.GameObjects;

namespace Florence.ClientAssembly.Graphics.Cameras
{
    public class FirstPersonCamera : ICamera
    {
        public Matrix4 LookAtMatrix { get; private set; }
        
        private readonly AGameObject _target;
        private readonly Vector3 _cameraBoomArm;

        public Vector3 Fowards => _fowards;
        public Vector3 Up => _up;
        public Vector3 Right => _right;
        private Vector3 _fowards;
        private Vector3 _right;
        private Vector3 _up;
        private float _pitch;
        private float _yaw;
        private float _fov = MathHelper.PiOver2;
        private float cameraSpeed;
        private float sensitivity;

        private Vector2 _last_mousePos;
        private bool isFirstMove;

        public FirstPersonCamera(AGameObject target)
            : this(target, Vector3.Zero)
        {
        
        }
        public FirstPersonCamera(AGameObject target, Vector3 offset)
        {
            _target = target;
            _cameraBoomArm = offset;
            _fowards = new Vector3(1f, 0f, 0f);
            _up = Vector3.UnitY;
            _right = Vector3.Cross(_up, _fowards);
            cameraSpeed = 1;
            sensitivity = 1;
            _last_mousePos = new Vector2((char)Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_X() / 2, (char)Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_Y() / 2);
            isFirstMove = true;
        }

        //GET
        public float Get_Fov()
        {
            return _fov;
        }
        public float Get_Pitch()
        {
            return _pitch;
        }
        public float Get_Yaw()
        {
            return _yaw;
        }

//SET
        public void Set_Fov(float value)
        {
            _fov = value;
        }
        public void Set_Pitch(float value)
        {
            //var angle = MathHelper.Clamp(value, -MathHelper.DegreesToRadians(89f), MathHelper.DegreesToRadians(89f));
            _pitch = value;
            if (Get_Pitch() > (float)((System.Math.PI / 180) * 85f))
            {
                Set_Pitch((float)((System.Math.PI / 180) * 85f));
            }
            else if (Get_Pitch() <= (float)((System.Math.PI / 180) * -85f))
            {
                Set_Pitch((float)((System.Math.PI / 180) * -85f));
            }
            UpdateVectors();
        }
        public void Set_Yaw(float value)
        {
            _yaw = value;
            if (Get_Yaw() > System.Math.PI)
            {
                Set_Yaw(Get_Yaw() - (float)(2 * System.Math.PI));
            }
            else if (Get_Yaw() <= -System.Math.PI)
            {
                Set_Yaw(Get_Yaw() + (float)(2 * System.Math.PI));
            }
            UpdateVectors();
        }

        private void UpdateVectors()
        {
            Vector3 fowards = new Vector3(0, 0, 0);
            fowards.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
            fowards.Y = MathF.Sin(_pitch);
            fowards.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);

            float theta_X = Vector3.CalculateAngle(new Vector3(_fowards.X, 0, 0), new Vector3(fowards.X, 0, 0));
            float theta_Y = Vector3.CalculateAngle(new Vector3(0, _fowards.Y, 0), new Vector3(0, fowards.Y, 0));
            float theta_Z = Vector3.CalculateAngle(new Vector3(0, 0, _fowards.Z), new Vector3(0, 0, fowards.Z));

            _fowards = Vector3.Normalize(fowards);
            

            //Matrix4x4 rotationMatrix = Matrix4x4.CreateRotationY(angleInRadians);
            //Vector3 rotatedVector = Vector3.Transform(direction, rotationMatrix);

            _right = Vector3.Normalize(Vector3.Cross(_fowards, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.UnitY);
        }
        public void Update(double time, double delta)
        {
            LookAtMatrix = Matrix4.LookAt(
                _target.Position + _cameraBoomArm,  
                _target.Position + _fowards + _cameraBoomArm,
                _up
            );
            
        }

//GET
        public float Get_cameraSpeed()
        {
            return cameraSpeed;
        }
        public float Get_sensitivity()
        {
            return sensitivity;
        }
        public Vector3 Get_fowards()
        {
            return _fowards;
        }
        public Vector3 Get_right()
        {
            return _right;
        }
        public Vector3 Get_up()
        {
            return _up;
        }
        public Vector2 Get_Last_MousePos()
        {
            return _last_mousePos;
        }
        public bool Get_isFirstMove()
        {
            return isFirstMove;
        }
        //SET
        public void Set_fowards(Vector3 value)
        {
            _fowards = value;
        }
        public void Set_right(Vector3 value)
        {
            _right = value;
        }
        public void Set_up(Vector3 value)
        {
            _up = value;
        }
        public void Set_Last_MousePos(Vector2 pos)
        {
            _last_mousePos = pos;
        }
        public void Set_isFirstMove(bool value)
        {
            isFirstMove = value;
        }
    }
}