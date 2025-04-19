using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Florence.ServerAssembly.Graphics.Cameras;
using Florence.ServerAssembly.Graphics.Renderables;

namespace Florence.ServerAssembly.Graphics.GameObjects
{
    public abstract class AGameObject
    {
        public ARenderable Model => _model;
        public Vector3 Position => _position;
        public Vector3 Direction => _direction;
        public Vector3 Fowards => _fowards;
        public Vector3 Right => _right;
        public Vector3 Up => _up;
        public Vector3 Scale => _scale;
        private static int GameObjectCounter;
        public readonly int GameObjectNumber;
        protected ARenderable _model;
        protected Vector3 _position;
        protected Vector3 _last_position;
        protected Vector3 _fowards;
        protected Vector3 _direction;
        protected Vector3 _right;
        protected Vector3 _up;
        protected Vector3 _rotation;
        protected short[] _angleX;
        protected short[] _angleY;
        protected float _velocity;
        protected Matrix4 _modelView;
        protected Vector3 _scale;
        
        public AGameObject(ARenderable model, Vector3 position, Vector3 direction, Vector3 rotation, float velocity)
        {
            _model = model;
            _position = position;
            _last_position = _position;
            _direction = direction;
            _fowards = new Vector3(1f, 0f, 0f);
            _up = position + position.Normalized();
            _right = Vector3.Cross(_up, _fowards);
            _rotation = new Vector3((float)-(System.Math.PI/2), 0f, 0f);
            _angleX = new short[3];
            _angleY = new short[3];
            _velocity = velocity;
            _scale = new Vector3(1);
            GameObjectNumber = GameObjectCounter++;
        }

        public virtual void Update(double time, double delta)
        {
            _position += _direction * (_velocity * (float)delta);
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
        public void Add_deltaX(short deltaX)
        {
            _angleX[2] += deltaX;
            if(_angleX[2] < (short)0)
            {
                _angleX[2] = (short)(60 - _angleX[2]);
                _angleX[1] -= (short)1;
                if(_angleX[1] < 0)
                {
                    _angleX[1] = (short)(60 - _angleX[1]);
                    _angleX[0] -= (short)1;
                    if (_angleX[0] < 0)
                    {
                        _angleX[0] = (short)(360 - _angleX[0]);
                    }
                }
            }
            else if (_angleX[2] > (short)60)
            {
                _angleX[2] = (short)(_angleX[2] - 60);
                _angleX[1] += (short)1;
                if (_angleX[1] > 60)
                {
                    _angleX[1] = (short)(_angleX[1] - 60);
                    _angleX[0] -= (short)1;
                    if (_angleX[0] > 360)
                    {
                        _angleX[0] = (short)(_angleX[0] - 360);
                    }
                }
            }
        }
        public void Add_deltaY(short deltaX)
        {
            _angleY[2] += deltaX;
            if (_angleY[2] < (short)0)
            {
                _angleY[2] = (short)(60 - _angleY[2]);
                _angleY[1] -= (short)1;
                if (_angleY[1] < 0)
                {
                    _angleY[1] = (short)(60 - _angleY[1]);
                    _angleY[0] -= (short)1;
                    if (_angleY[0] < 0)
                    {
                        _angleY[0] = (short)(360 - _angleY[0]);
                    }
                }
            }
            else if (_angleY[2] > (short)60)
            {
                _angleY[2] = (short)(_angleY[2] - 60);
                _angleY[1] += (short)1;
                if (_angleY[1] > 60)
                {
                    _angleY[1] = (short)(_angleY[1] - 60);
                    _angleY[01] -= (short)1;
                    if (_angleY[0] > 360)
                    {
                        _angleY[0] = (short)(_angleY[0] - 360);
                    }
                }
            }
        }
        public float Get_deltaX()
        {
            return (float)(System.Math.PI / 180) * (_angleX[0] + (_angleX[1] / 60) + (_angleX[2] / (60 * 60)));
        }
        public float Get_deltaY()
        {
            return (float)(System.Math.PI / 180) * (_angleY[0] + (_angleY[1] / 60) + (_angleY[2] / (60 * 60)));
        }

        //GET
        public Vector3 Get_position()
        {
            return _position;
        }
        public Vector3 Get_last_position()
        {
            return _last_position;
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
            return _rotation;
        }
        public float Get_world_pitch()
        {
            return _rotation.X;
        }

        public float Get_world_yaw()
        {
            return _rotation.Y;
        }

        public float Get_world_roll()
        {
            return _rotation.Z;
        }
        public float Get_velocity()
        {
            return _velocity;
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
        public void Set_last_Position(Vector3 position)
        {
            _last_position = position;
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
        public void Set_Rotation(Vector3 position)
        {
            _rotation = position;
        }

        public void Set_world_pitch(float value)
        {
            _rotation.X = value;
        }

        public void Set_world_yaw(float value)
        {
            _rotation.Y = value;
        }

        public void Set_world_roll(float value)
        {
            _rotation.Z = value;
        }
    }
}