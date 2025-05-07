using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Florence.ClientAssembly.Graphics.Cameras;
using Florence.ClientAssembly.Graphics.Renderables;

namespace Florence.ClientAssembly.Graphics.GameObjects
{
    public abstract class AGameObject
    {
        public ARenderable Model => _model;
        public Vector3 Direction => _direction;
        public Vector3 Position => _position;
		public Vector3 Rotation => _rotation;

		public Vector3 Scale => _scale;

        protected ARenderable _model;
        protected Vector3 _direction;
        protected Vector3 _position;
		protected Vector3 _rotation;
		protected Vector3 _scale;


		protected float _speed;
                
        private static int GameObjectCounter;
        public readonly int GameObjectNumber;

		protected Matrix4 _modelView;


		public AGameObject(ARenderable model, Vector3 position, Vector3 direction, Vector3 rotation, float velocity)
        {
            _model = model;
            _direction = direction;
            _position = position;

            _scale = new Vector3(1);
            _rotation = new Vector3(0f, 0f, 0f);
            _speed = velocity;
            
            GameObjectNumber = GameObjectCounter++;
        }

        public virtual void Update(double time, double delta)
        {
            _position += _direction * (_speed * (float)delta);
        }

        public virtual void Render(ICamera camera)
        {
            _model.Bind();
            var t2 = Matrix4.CreateTranslation(_position.X, _position.Y, _position.Z);
            var r1 = Matrix4.CreateRotationX(_rotation.X);
            var r2 = Matrix4.CreateRotationY(_rotation.Y);
            var r3 = Matrix4.CreateRotationZ(_rotation.Z);
            var s = Matrix4.CreateScale(_scale);
            _modelView = r1 * r2 * r3 * s * t2 * camera.LookAtMatrix;
            GL.UniformMatrix4(21, false, ref _modelView);
            _model.Render();
        }

 

//GET
        public Vector3 Get_position()
        {
            return _position;
        }
        public Vector3 Get_direction()
        {
            return _direction;
        }
		public Vector3 Get_rotation()
		{
			return _rotation;
		}
		public float Get_speed()
        {
            return _speed;
        }
//SET
        public void Set_Scale(Vector3 scale)
        {
            _scale = scale;
        }
        public void Set_Position(Vector3 position)
        {
            _position = position;
        }
        public void Set_Direction(Vector3 position)
        {
            _direction = position;
        }

        public void Set_Rotation(Vector3 value)
        {
            value.Normalized();
            _rotation = value;

            if (_rotation.X > System.Math.PI)
            {
                while (_rotation.X > System.Math.PI)
                {
                    _rotation.X = _rotation.X - (float)(2 * System.Math.PI);
                }
            }
            else if (_rotation.X <= -System.Math.PI)
            {
                while (_rotation.X <= -System.Math.PI)
                {
                    _rotation.X = _rotation.X + (float)(2 * System.Math.PI);
                }
            }

            _rotation.Y = _rotation.Y + value.Y;
            if (_rotation.X > System.Math.PI)
            {
                while (_rotation.Y > System.Math.PI)
                {
                    _rotation.Y = _rotation.Y - (float)(2 * System.Math.PI);
                }
            }
            else if (_rotation.X <= -System.Math.PI)
            {
                while (_rotation.Y <= -System.Math.PI)
                {
                    _rotation.Y = _rotation.Y + (float)(2 * System.Math.PI);
                }
            }

            _rotation.Z = _rotation.Z + value.Z;
            if (_rotation.Z > System.Math.PI)
            {
                while (_rotation.Z > System.Math.PI)
                {
                    _rotation.Z = _rotation.Z - (float)(2 * System.Math.PI);
                }
            }
            else if (_rotation.Z <= -System.Math.PI)
            {
                while (_rotation.Z <= -System.Math.PI)
                {
                    _rotation.Z = _rotation.Z + (float)(2 * System.Math.PI);
                }
            }
        }
    }
}