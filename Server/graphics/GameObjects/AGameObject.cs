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
        public Vector3 Scale => _scale;
        private static int GameObjectCounter;
        public readonly int GameObjectNumber;
        protected ARenderable _model;
        protected Vector3 _position;
        protected Vector3 _direction;
        protected Vector3 _rotation;
        protected float _velocity;
        protected Matrix4 _modelView;
        protected Vector3 _scale;
        public bool ToBeRemoved { get; set; }

        public AGameObject(ARenderable model, Vector3 position, Vector3 direction, Vector3 rotation, float velocity)
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

        public void Update_Rotation(Vector3 value)
        {
            float rot_X = Florence.ServerAssembly.Framework.GetGameServer().GetData().GetGame_Instance().Get_gameObjectFactory().Get_player().Get_Rotation().X + value.X;
            if (rot_X > (float)(Math.PI / 180) * 180)
            {
                rot_X = (rot_X - (float)(Math.PI * 2));
            }
            if (rot_X <= (Math.PI / 180) * -180)
            {
                rot_X = (rot_X + (float)(Math.PI * 2));
            }
            float rot_Y = Florence.ServerAssembly.Framework.GetGameServer().GetData().GetGame_Instance().Get_gameObjectFactory().Get_player().Get_Rotation().Y + value.Y;
            if (rot_Y > (float)(Math.PI / 180) * 180)
            {
                rot_Y = (rot_Y - (float)(Math.PI * 2));
            }
            if (rot_Y <= (Math.PI / 180) * -180)
            {
                rot_Y = (rot_Y + (float)(Math.PI * 2));
            }
            float rot_Z = Florence.ServerAssembly.Framework.GetGameServer().GetData().GetGame_Instance().Get_gameObjectFactory().Get_player().Get_Rotation().Z + value.Z;
            if (rot_Z > (float)(Math.PI / 180) * 180)
            {
                rot_Z = (rot_Z - (float)(Math.PI * 2));
            }
            if (rot_Z <= (Math.PI / 180) * -180)
            {
                rot_Z = (rot_Z + (float)(Math.PI * 2));
            }
            Florence.ServerAssembly.Framework.GetGameServer().GetData().GetGame_Instance().Get_gameObjectFactory().Get_player().Set_rotation(new Vector3(rot_X, rot_Y, rot_Z));
        }
//get
        public Vector3 Get_Rotation()
        {
            return _rotation;
        }
//set
        public void SetScale(Vector3 scale)
        {
            _scale = scale;
        }
        public void SetPosition(Vector3 position)
        {
            _position = position;
        }
        public void Set_rotation(Vector3 scale)
        {
            _rotation = scale;
        }
    }
}