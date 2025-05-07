using OpenTK;
using Florence.ClientAssembly.Graphics.GameObjects;

namespace Florence.ClientAssembly.Graphics.Cameras
{
    public class ThirdPersonCamera : ICamera
    {
        public Matrix4 LookAtMatrix { get; private set; }
        private readonly AGameObject _player;
        private readonly Vector3 _cameraBoomArm;
        public Vector3 Fowards => _fowards;
        public Vector3 Up => _up;
        public Vector3 Right => _right;
        protected Vector3 _fowards;
        protected Vector3 _right;
        protected Vector3 _up;

        private float cameraSpeed;
        private float sensitivity;

        public ThirdPersonCamera(AGameObject target)
            : this(target, Vector3.Zero)
        {

        }
        public ThirdPersonCamera(AGameObject target, Vector3 offset)
        {
            _player = target;
            _fowards = new Vector3(1f, 0f, 0f);
            _up = Vector3.UnitY;
            _right = Vector3.Cross(_up, _fowards);

            _cameraBoomArm = _up;
            cameraSpeed = 20f;
            sensitivity = 1f;
        }

        public void Update(double time, double delta)
        {
            LookAtMatrix = Matrix4.LookAt(
                _player.Position + _cameraBoomArm,
                _player.Position + _player.Direction,
                _up
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