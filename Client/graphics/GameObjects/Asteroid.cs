using System;
using OpenTK;
using Florence.ServerAssembly.Graphics.Renderables;

namespace Florence.ServerAssembly.Graphics.GameObjects
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


        public override void Update(double time, double delta)
        {
            /*
            _rotation_In_World.X = (float)Math.Sin((time + GameObjectNumber) * 0.3);
            _rotation_In_World.Y = (float)Math.Cos((time + GameObjectNumber) * 0.5);
            _rotation_In_World.Z = (float)Math.Cos((time + GameObjectNumber) * 0.2);
            var d = new Vector4(_rotation_In_World.X, _rotation_In_World.Y, 0, 0);
            d.Normalize();
            _fowards = d;
            if (_lockedBullet != null && _lockedBullet.ToBeRemoved)
            {
                _lockedBullet = null;
                _model = _original;
            }
            */
            base.Update(time, delta);
        }
    }
}