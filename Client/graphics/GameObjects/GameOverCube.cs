using System;
using OpenTK;
using Florence.ClientAssembly.Graphics.Renderables;

namespace Florence.ClientAssembly.Graphics.GameObjects
{
    public class GameOverCube : AGameObject
    {
        public GameOverCube(ARenderable model, Vector3 position, Vector3 direction, Vector3 rotation, float velocity) 
            : base(model, position, direction, rotation, velocity)
        {
        }

        public override void Update(double time, double delta)
        {
            var k = (float)time * 0.4f;
            _rotation.X = k * 0.7f;
            _rotation.Y = k * 0.5f;
            _rotation.Z = (float)Math.Sin(k * 0.4f);
            base.Update(time, delta);
        }
        
    }
}