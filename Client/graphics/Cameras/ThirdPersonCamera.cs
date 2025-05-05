using OpenTK;
using Florence.ServerAssembly.Graphics.GameObjects;

namespace Florence.ServerAssembly.Graphics.Cameras
{
    public class ThirdPersonCamera : ICamera
    {
        public Matrix4 LookAtMatrix { get; private set; }
        private readonly AGameObject _player;
        private readonly Vector3 _offset;
        private float cameraSpeed;
        private float sensitivity;

        public ThirdPersonCamera(AGameObject target)
            : this(target, Vector3.Zero)
        {}
        public ThirdPersonCamera(AGameObject target, Vector3 offset)
        {
            _player = target;
            _offset = offset;
            cameraSpeed = 20f;
            sensitivity = 1f;
        }

        public void Update(double time, double delta)
        {
            LookAtMatrix = Matrix4.LookAt(
                _player.Position + _offset,
                _player.Position + _player.Direction,
                _player.Up
            );
        }

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