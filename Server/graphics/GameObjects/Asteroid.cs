using System;
using OpenTK;
using Florence.ServerAssembly.Graphics.Renderables;

namespace Florence.ServerAssembly.Graphics.GameObjects
{
    public class Asteroid : AGameObject
    {
        public int Score { get; set; }
        private Bullet _lockedBullet;
        private ARenderable _original;
        public Asteroid(ARenderable model, Vector3 position, Vector3 direction, Vector3 rotation, float velocity) 
            : base(model, position, direction, rotation, velocity)
        {
            _original = model;
        }


        public override void Update(double time, double delta)
        {
            _rotation.X = (float)Math.Sin((time + GameObjectNumber) * 0.3);
            _rotation.Y = (float)Math.Cos((time + GameObjectNumber) * 0.5);
            _rotation.Z = (float)Math.Cos((time + GameObjectNumber) * 0.2);
            base.Update(time, delta);
        }
        
        public void LockBullet(Bullet bullet)
        {
            _lockedBullet = bullet;
            _model = bullet.Model;
        }
    }
}