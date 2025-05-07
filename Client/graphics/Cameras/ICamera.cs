using OpenTK;

namespace Florence.ClientAssembly.Graphics.Cameras
{
    public interface ICamera
    {
 
        Matrix4 LookAtMatrix { get; }
        void Update(double time, double delta);


    }
}