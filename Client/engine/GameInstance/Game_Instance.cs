using Florence.ClientAssembly.Graphics.Renderables;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Florence.ClientAssembly
{
    public class Game_Instance
    {
        static private Florence.ClientAssembly.game_Instance.Arena arena;
        static private Florence.ClientAssembly.Graphics.GameObjectFactory _gameObjectFactory;
        static private List<Florence.ClientAssembly.Graphics.GameObjects.AGameObject> _gameObjects;
        static private Dictionary<string, ARenderable> models;
        static private Florence.ClientAssembly.Graphics.ShaderProgram _solidProgram;
        static private Florence.ClientAssembly.Graphics.ShaderProgram _texturedProgram;

        /// <summary>
        /// ID of our program on the graphics card
        /// </summary>
        int pgmID;

        /// <summary>
        /// Address of the vertex shader
        /// </summary>
        int vsID;

        /// <summary>
        /// Address of the fragment shader
        /// </summary>
        int fsID;

        /// <summary>
        /// Address of the color parameter
        /// </summary>
        int attribute_vcol;

        /// <summary>
        /// Address of the position parameter
        /// </summary>
        int attribute_vpos;

        /// <summary>
        /// Address of the modelview matrix uniform
        /// </summary>
        int uniform_mview;

        /// <summary>
        /// Address of the Vertex Buffer Object for our position parameter
        /// </summary>
        int vbo_position;

        /// <summary>
        /// Address of the Vertex Buffer Object for our color parameter
        /// </summary>
        int vbo_color;

        /// <summary>
        /// Address of the Vertex Buffer Object for our modelview matrix
        /// </summary>
        int vbo_mview;

        /// <summary>
        /// Index Buffer Object
        /// </summary>
        int ibo_elements;

        /// <summary>
        /// Array of our vertex positions
        /// </summary>
        Vector3[] vertdata;

        /// <summary>
        /// Array of our vertex colors
        /// </summary>
        Vector3[] coldata;

        /// <summary>
        /// Array of our indices
        /// </summary>
        int[] indicedata;


        /// <summary>
        /// List of all the Volumes to be drawn
        /// </summary>
        //List<Volume> objects = new List<Volume>();

        public Game_Instance()
        {
            arena = new Florence.ClientAssembly.game_Instance.Arena();
            while (arena == null) { /* Wait while is created */ }



            _gameObjects = new List<Florence.ClientAssembly.Graphics.GameObjects.AGameObject>(1);
            while (_gameObjects == null) { /* Wait while is created */ }

        }
        public void Initalise_Graphics()
        {
            Florence.ClientAssembly.Framework.GetClient().GetExecute().Create_And_Run_Graphics();
        }
        public void initProgram()
        {
            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Create_FloorForMap();

            /** In this function, we'll start with a call to the GL.CreateProgram() function,
             * which returns the ID for a new program object, which we'll store in pgmID. */
            pgmID = GL.CreateProgram();

            loadShader("..\\..\\..\\..\\graphics\\Shaders\\1Vert\\cubeVert.glsl", ShaderType.VertexShader, pgmID, out vsID);
            loadShader("..\\..\\..\\..\\graphics\\Shaders\\5Frag\\cubeFrag.glsl", ShaderType.FragmentShader, pgmID, out fsID);

            /** Now that the shaders are added, the program needs to be linked.
             * Like C code, the code is first compiled, then linked, so that it goes
             * from human-readable code to the machine language needed. */
            GL.LinkProgram(pgmID);
            Console.WriteLine(GL.GetProgramInfoLog(pgmID));

            /** We have multiple inputs on our vertex shader, so we need to get
            * their addresses to give the shader position and color information for our vertices.
            * 
            * To get the addresses for each variable, we use the 
            * GL.GetAttribLocation and GL.GetUniformLocation functions.
            * Each takes the program's ID and the name of the variable in the shader. */
            attribute_vpos = GL.GetAttribLocation(pgmID, "vPosition");
            attribute_vcol = GL.GetAttribLocation(pgmID, "vColor");
            uniform_mview = GL.GetUniformLocation(pgmID, "modelview");

            /** Now our shaders and program are set up, but we need to give them something to draw.
             * To do this, we'll be using a Vertex Buffer Object (VBO).
             * When you use a VBO, first you need to have the graphics card create
             * one, then bind to it and send your information. 
             * Then, when the DrawArrays function is called, the information in
             * the buffers will be sent to the shaders and drawn to the screen. */
            GL.GenBuffers(1, out vbo_position);
            GL.GenBuffers(1, out vbo_color);
            GL.GenBuffers(1, out vbo_mview);

            /** We'll need to get another buffer object to put our indice data into.  */
            GL.GenBuffers(1, out ibo_elements);
        }
        private void loadShader(String filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            using (StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        public void Create_FloorForMap()
        {
            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Create_FloorForMap("FloorOfGrass", new Vector3(0, 0, 0));
            _gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_FloorForMap());
        }
        public void Create_gameObjectFactory()
        {
            _gameObjectFactory = new Florence.ClientAssembly.Graphics.GameObjectFactory(models);
            while (_gameObjectFactory == null) { /* Wait while is created */ }
        }

        public void Create_gameObjects()
        {
            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Create_Player();
            _gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player());
            _gameObjects.RemoveAt(0);

            //_gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().CreateEarth("Earth", new Vector3(0, 0, 0)));
            _gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().CreateAsteroid("Asteroid", new Vector3(-50, 0, 50)));
            _gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().CreateAsteroid("Asteroid", new Vector3(50, 0, 50)));
            _gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().CreateAsteroid("Golden", new Vector3(0, -50, 50)));
            _gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().CreateAsteroid("Golden", new Vector3(0, 50, 50)));
            _gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().CreateAsteroid("Wooden", new Vector3(0, 0, -60)));
            _gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().CreateAsteroid("Wooden", new Vector3(0, 0, 60)));
        }

        public void Load_Sphere_Solid()
        {
            _solidProgram = new Florence.ClientAssembly.Graphics.ShaderProgram();
            _solidProgram.AddShader(ShaderType.VertexShader, "..\\..\\..\\..\\graphics\\Shaders\\1Vert\\simplePipeVert.c");
            _solidProgram.AddShader(ShaderType.FragmentShader, "..\\..\\..\\..\\graphics\\Shaders\\5Frag\\simplePipeFrag.c");
            _solidProgram.Link();
        }

        public void Load_Sphere_Textures()
        {
            _texturedProgram = new Florence.ClientAssembly.Graphics.ShaderProgram();
            _texturedProgram.AddShader(ShaderType.VertexShader, "..\\..\\..\\..\\graphics\\Shaders\\1Vert\\simplePipeTexVert.c");
            _texturedProgram.AddShader(ShaderType.FragmentShader, "..\\..\\..\\..\\graphics\\Shaders\\5Frag\\simplePipeTexFrag.c");
            _texturedProgram.Link();
        }
        public void Load_Models()
        {
            models = new Dictionary<string, ARenderable>();
            while (models == null) { /* Wait while is created */ }
            models.Add("Earth", new MipMapGeneratedRenderObject(new Florence.ClientAssembly.Graphics.IcoSphereFactory().Create(3), _texturedProgram.Id, "..\\..\\..\\..\\graphics\\Textures\\grass.jpeg", 8));
            models.Add("Wooden", new MipMapGeneratedRenderObject(new Florence.ClientAssembly.Graphics.IcoSphereFactory().Create(3), _texturedProgram.Id, "..\\..\\..\\..\\graphics\\Textures\\wooden.png", 8));
            models.Add("Golden", new MipMapGeneratedRenderObject(new Florence.ClientAssembly.Graphics.IcoSphereFactory().Create(3), _texturedProgram.Id, "..\\..\\..\\..\\graphics\\Textures\\golden.bmp", 8));
            models.Add("Asteroid", new MipMapGeneratedRenderObject(new Florence.ClientAssembly.Graphics.IcoSphereFactory().Create(3), _texturedProgram.Id, "..\\..\\..\\..\\graphics\\Textures\\moonmap1k.jpg", 8));
            models.Add("Floor", new MipMapGeneratedRenderObject(Florence.ClientAssembly.Graphics.RenderObjectFactory.CreateTexturedCube6(1, 1, 1), _texturedProgram.Id, "..\\..\\..\\..\\graphics\\Textures\\grass.jpeg", 8));
            //models.Add("Gameover", new MipMapGeneratedRenderObject(Florence.ClientAssembly.Graphics.RenderObjectFactory.CreateTexturedCube6(1, 1, 1), _texturedProgram.Id, "..\\..\\..\\..\\graphics\\Textures\\gameover.png", 8));
            models.Add("Player", new MipMapGeneratedRenderObject(new Florence.ClientAssembly.Graphics.IcoSphereFactory().Create(3), _texturedProgram.Id, "..\\..\\..\\..\\graphics\\Textures\\dotted.png", 8));
        }

        public Florence.ClientAssembly.game_Instance.Arena Get_Arena()
        {
            return arena;
        }

        public List<Florence.ClientAssembly.Graphics.GameObjects.AGameObject> Get_GameObjects()
        {
            return _gameObjects;
        }
        public Florence.ClientAssembly.Graphics.GameObjectFactory Get_gameObjectFactory()
        {
            return _gameObjectFactory;
        }

        public Dictionary<string, ARenderable> Get_models()
        {
            return models;
        }



        public Florence.ClientAssembly.Graphics.ShaderProgram Get_texturedProgram()
        {
            return _texturedProgram;
        }

        public Florence.ClientAssembly.Graphics.ShaderProgram Get_solidProgram()
        {
            return _solidProgram;
        }


    }
}
