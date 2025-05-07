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
        protected Vector3 _fowards;
        protected Vector3 _right;
        protected Vector3 _up;
        
        private float cameraSpeed;
        private float sensitivity;

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
    }
}