using OpenTK;
using Florence.ClientAssembly.Graphics.Renderables;

namespace Florence.ClientAssembly.Graphics.GameObjects
{
    public class Asteroid : AGameObject
    {
        public int Score { get; set; }
        private ARenderable _original;
        public Asteroid(ARenderable model, Vector3 position, Vector3 direction, Vector3 rotation, float velocity) 
            : base(model, position, direction, rotation, velocity)
        {
            _original = model;
        }
    }
}