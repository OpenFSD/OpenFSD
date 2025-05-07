using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Florence.ClientAssembly.Graphics.Cameras;
using Florence.ClientAssembly.Graphics.Renderables;

namespace Florence.ClientAssembly.Graphics.GameObjects
{
    public class MapFloor : AGameObject
    {
        private static float[] _vertices =
        {
            // Position         Texture coordinates
             0.5f,  0.5f, 0.0f,  1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };

        private static uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3
        };
 
        public MapFloor(ARenderable model, Vector3 position, Vector3 direction, Vector3 rotation, float speed)
            : base(model, position, direction, rotation, speed)
        {
            _model = model;
            _direction = direction;
            _position = position;
            _rotation = new Vector3(0f, 0f, 0f);
            _speed = speed;
        
        // top right
            _vertices[0] = position.X + 0.5f;
            _vertices[1] = position.Y + 0.5f;
            _vertices[2] = position.Z + 0.0f;
            _vertices[3] = 1.0f;
            _vertices[4] = 1.0f;

        // bottom right
            _vertices[5] = position.X + 0.5f;
            _vertices[6] = position.Y - 0.5f;
            _vertices[7] = position.Z + 0.0f;
            _vertices[8] = 1.0f;
            _vertices[9] = 0.0f;

        // bottom left
            _vertices[10] = position.X - 0.5f;
            _vertices[11] = position.Y - 0.5f;
            _vertices[12] = position.Z + 0.0f;
            _vertices[13] = 0.0f;
            _vertices[14] = 0.0f;

        // top left
            _vertices[15] = position.X - 0.5f;
            _vertices[16] = position.Y + 0.5f;
            _vertices[17] = position.Z + 0.0f;
            _vertices[18] = 0.0f;
            _vertices[19] = 1.0f;
        }

//GET
        public float[] Get_Verticies()
        {
            return _vertices;
        }
        public uint[] Get_Indices()
        {
            return _indices;
        }
//SET
       
    }
}