using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Florence.ServerAssembly.Graphics.Cameras;
using Florence.ServerAssembly.Graphics.Renderables;

namespace Florence.ServerAssembly.Graphics.GameObjects
{
    public abstract class AGameObject
    {
        public ARenderable Model => _model;
        public Vector3 Direction => _direction;
        public Vector3 Position => _position;
        public Vector3 Last_Position => _last_position;
        public Vector3 Fowards => _fowards;
        public Vector3 Up => _up;
        public Vector3 Right => _right;
        public Vector3 Scale => _scale;
        protected ARenderable _model;
        protected Vector3 _direction;
        protected Vector3 _position;
        protected Vector3 _last_position;
        protected Vector3 _fowards;
        protected Vector3 _right;
        protected Vector3 _up;
        protected Vector3 _rotation_In_World;
        protected float _speed;
        protected Matrix4 _modelView;
        protected Vector3 _scale;
        private static int GameObjectCounter;
        public readonly int GameObjectNumber;


        public AGameObject(ARenderable model, Vector3 position, Vector3 direction, Vector3 rotation, float velocity)
        {
            _model = model;
            _direction = direction;
            _position = position;
            _last_position = position;
            _fowards = new Vector3(1f, 0f, 0f);
            _up = position + position.Normalized();
            _right = Vector3.Cross(_up, _fowards);
            _scale = new Vector3(1);
            _rotation_In_World = new Vector3((float)-(System.Math.PI/2), 0f, 0f);
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
            var r1 = Matrix4.CreateRotationX(_rotation_In_World.X);
            var r2 = Matrix4.CreateRotationY(_rotation_In_World.Y);
            var r3 = Matrix4.CreateRotationZ(_rotation_In_World.Z);
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
        public Vector3 Get_fowards()
        {
            return _fowards;
        }
        public Vector3 Get_right()
        {
            return _right;
        }
        public Vector3 Get_up()
        {
            return _up;
        }
        public Vector3 Get_rotation_In_World()
        {
            return _rotation_In_World;
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
        public void Set_fowards(Vector3 value)
        {
            _fowards = value;
        }
        public void Set_right(Vector3 value)
        {
            _right = value;
        }
        public void Set_up(Vector3 value)
        {
            _up = value;
        }
        public void Set_Rotation_In_World(Vector3 value)
        {
            value.Normalized();
            _rotation_In_World = value;

            if (_rotation_In_World.X >= System.Math.PI)
            {
                while (_rotation_In_World.X >= System.Math.PI)
                {
                    _rotation_In_World.X = _rotation_In_World.X - (float)(2 * System.Math.PI);
                }
            }
            else if (_rotation_In_World.X < -System.Math.PI)
            {
                while (_rotation_In_World.X < -System.Math.PI)
                {
                    _rotation_In_World.X = _rotation_In_World.X + (float)(2 * System.Math.PI);
                }
            }

            _rotation_In_World.Y = _rotation_In_World.Y + value.Y;
            if (_rotation_In_World.X >= System.Math.PI)
            {
                while (_rotation_In_World.Y >= System.Math.PI)
                {
                    _rotation_In_World.Y = _rotation_In_World.Y - (float)(2 * System.Math.PI);
                }
            }
            else if (_rotation_In_World.X < -System.Math.PI)
            {
                while (_rotation_In_World.Y < -System.Math.PI)
                {
                    _rotation_In_World.Y = _rotation_In_World.Y + (float)(2 * System.Math.PI);
                }
            }

            _rotation_In_World.Z = _rotation_In_World.Z + value.Z;
            if (_rotation_In_World.Z >= System.Math.PI)
            {
                while (_rotation_In_World.Z >= System.Math.PI)
                {
                    _rotation_In_World.Z = _rotation_In_World.Z - (float)(2 * System.Math.PI);
                }
            }
            else if (_rotation_In_World.Z < -System.Math.PI)
            {
                while (_rotation_In_World.Z < -System.Math.PI)
                {
                    _rotation_In_World.Z = _rotation_In_World.Z + (float)(2 * System.Math.PI);
                }
            }
        }
    }
}