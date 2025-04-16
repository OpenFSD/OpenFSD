using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Florence.ServerAssembly.Graphics.Cameras;
using Florence.ServerAssembly.Graphics.Renderables;

namespace Florence.ServerAssembly.Graphics.GameObjects
{
    public abstract class AGameObject
    {
        public ARenderable Model => _model;
        public Vector4 Position => _position;
        public Vector4 Direction => _direction;
        public Vector3 Scale => _scale;
        private static int GameObjectCounter;
        public readonly int GameObjectNumber;
        protected ARenderable _model;
        protected Vector4 _position;
        protected Vector4 _direction;
        protected Vector4 _rotation;
        protected float _velocity;
        protected Matrix4 _modelView;
        protected Vector3 _scale;
        public bool ToBeRemoved { get; set; }

        public AGameObject(ARenderable model, Vector4 position, Vector4 direction, Vector4 rotation, float velocity)
        {
            _model = model;
            _position = position;
            _direction = direction;
            _rotation = rotation;
            _velocity = velocity;
            _scale = new Vector3(1);
            GameObjectNumber = GameObjectCounter++;
        }

        public virtual void Update(double time, double delta)
        {
            _position += _direction*(_velocity*(float) delta);
        }

        public virtual void Render(ICamera camera)
        {
            _model.Bind();
            var t2 = Matrix4.CreateTranslation(_position.X, _position.Y, _position.Z);
            var r1 = Matrix4.CreateRotationX(_rotation.X);
            var r2 = Matrix4.CreateRotationY(_rotation.Y);
            var r3 = Matrix4.CreateRotationZ(_rotation.Z);
            var s = Matrix4.CreateScale(_scale);
            _modelView = r1*r2*r3*s*t2*camera.LookAtMatrix;
            GL.UniformMatrix4(21, false, ref _modelView);
            _model.Render();
        }
//GET
        public Vector4 Get_direction()
        {
            return _direction;
        }
        public Vector4 Get_position()
        {
            return _position;
        }
        public Vector4 Get_rotation()
        {
            return _rotation;
        }
        public float Get_pitch()
        {
            return _rotation.X;
        }

        public float Get_yaw()
        {
            return _rotation.Y;
        }

        public float Get_roll()
        {
            return _rotation.Z;
        }
        
//SET
        public void Set_direction(Vector4 value)
        {
            _direction = value;
        }
        public void Set_Scale(Vector3 scale)
        {
            _scale = scale;
        }
        public void Set_Position(Vector4 position)
        {
            _position = position;
        }
        public void Set_Rotation(Vector4 position)
        {
            _rotation = position;
        }

        public void Set_pitch(float value)
        {
            _rotation.X = value;
        }

        public void Set_yaw(float value)
        {
            _rotation.Y = value;
        }

        public void Set_roll(float value)
        {
            _rotation.Z = value;
        }
    }
}