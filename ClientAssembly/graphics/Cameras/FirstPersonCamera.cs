using OpenTK;
using Florence.ServerAssembly.Graphics.GameObjects;

namespace Florence.ServerAssembly.Graphics.Cameras
{
    public class FirstPersonCamera : ICamera
    {
        public Matrix4 LookAtMatrix { get; private set; }
        private readonly AGameObject _target;
        private readonly Vector3 _offset;

        private Vector3 _front;
        private Vector3 _up;
        private Vector3 _right;

        private float _pitch;
        private float _yaw;

        private float _fov;

        public FirstPersonCamera(AGameObject target)
            : this(target, Vector3.Zero)
        {
        
        }
        public FirstPersonCamera(AGameObject target, Vector3 offset)
        {
            _target = target;
            _offset = offset;
            _front = -Vector3.UnitZ;
            _up = Vector3.UnitY;
            _right = Vector3.UnitX;
            _pitch = 0;
            _yaw = -MathHelper.PiOver2;
            _fov = MathHelper.PiOver2;
        }

        public void Update(double time, double delta)
        {
            LookAtMatrix = Matrix4.LookAt(
                new Vector3(_target.Position) + _offset,  
                new Vector3(_target.Position + _target.Direction) + _offset, 
                Vector3.UnitY);
        }

        public Vector3 Get_front()
        {
            return _front;
        }
        public Vector3 Get_up()
        {
            return _up;
        }
        public Vector3 Get_right()
        {
            return _right;
        }
        public float Get_pitch()
        {
            return _pitch;
        }
        public float Get_yaw()
        {
            return _yaw;
        }

        public void Set_front(Vector3 value)
        {
            _front = value;
        }
        public void Set_up(Vector3 value)
        {
            _up = value;
        }
        public void Set_right(Vector3 value)
        {
            _right = value;
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