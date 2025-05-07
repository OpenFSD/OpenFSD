using OpenTK;

namespace Florence.ClientAssembly.Graphics.Cameras
{
    public class StaticCamera : ICamera
    {
        public Matrix4 LookAtMatrix { get; }
        public StaticCamera()
        {
            Vector3 position;
            position.X = 0;
            position.Y = 0;
            position.Z = 2;
            LookAtMatrix = Matrix4.LookAt(position, -Vector3.UnitZ, Vector3.UnitY);
        }
        public StaticCamera(Vector3 position, Vector3 target)
        {
            LookAtMatrix = Matrix4.LookAt(position, target, Vector3.UnitY);
        }
        public void Update(double time, double delta)
        {}
    }
}