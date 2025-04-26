using OpenTK;
using Florence.ServerAssembly.Graphics.GameObjects;

namespace Florence.ServerAssembly.Graphics.Cameras
{
    public class FirstPersonCamera : ICamera
    {
        public Matrix4 LookAtMatrix { get; private set; }
        private readonly AGameObject _target;
        private readonly Vector3 _offset;
        private float cameraSpeed;
        private float sensitivity;

        public FirstPersonCamera(AGameObject target)
            : this(target, Vector3.Zero)
        {
        
        }
        public FirstPersonCamera(AGameObject target, Vector3 offset)
        {
            _target = target;
            _offset = offset;
            cameraSpeed = 1;
            sensitivity = 1/60;
        }

        public void Update(double time, double delta)
        {
            LookAtMatrix = Matrix4.LookAt(
                _target.Position + _offset,  
                _target.Position + _target.Fowards + _offset,
                _target.Up
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
    }
}