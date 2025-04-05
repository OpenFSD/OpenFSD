using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using Florence.ServerAssembly.Graphics.Cameras;
using Florence.ServerAssembly.Graphics.GameObjects;
using Florence.ServerAssembly.Graphics.Renderables;

namespace Florence.ServerAssembly.Graphics
{
    public sealed class MainWindow : GameWindow
    {
        private bool done_once;
        private readonly string _title;
        private GameObjectFactory _gameObjectFactory;
        private readonly List<AGameObject> _gameObjects = new List<AGameObject>();
        private double _time;
        private readonly Color4 _backColor = new Color4(0.1f, 0.1f, 0.3f, 1.0f);
        private Matrix4 _projectionMatrix;
        private float _fov = 45f;
        private ShaderProgram _texturedProgram;
        private ShaderProgram _solidProgram;
        private KeyboardState _lastKeyboardState;
        private Spacecraft _player;
        private int _score;
        private bool _gameOver;
        private Bullet.BulletType _bulletType;
        private Bullet _lastBullet;
        private bool _useFirstPerson = true;
        private ICamera _camera;

        public MainWindow()
            : base(750, // initial width
                500, // initial height
                GraphicsMode.Default,
                "",  // initial title
                GameWindowFlags.Fullscreen,
                DisplayDevice.Default,
                4, // OpenGL major version
                5, // OpenGL minor version
                GraphicsContextFlags.ForwardCompatible)
        {
            _title += "dreamstatecoding.blogspot.com: OpenGL Version: " + GL.GetString(StringName.Version);
        }
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            CreateProjection();
        }


        protected override void OnLoad(EventArgs e)
        {
            Debug.WriteLine("OnLoad");
            VSync = VSyncMode.Off;
            CreateProjection();
            _solidProgram = new ShaderProgram();
            _solidProgram.AddShader(ShaderType.VertexShader, "..\\..\\graphics\\Shaders\\1Vert\\simplePipeVert.c");
            _solidProgram.AddShader(ShaderType.FragmentShader, "..\\..\\graphics\\Shaders\\5Frag\\simplePipeFrag.c");
            _solidProgram.Link();

            _texturedProgram = new ShaderProgram();
            _texturedProgram.AddShader(ShaderType.VertexShader, "..\\..\\graphics\\Shaders\\1Vert\\simplePipeTexVert.c");
            _texturedProgram.AddShader(ShaderType.FragmentShader, "..\\..\\graphics\\Shaders\\5Frag\\simplePipeTexFrag.c");
            _texturedProgram.Link();
            
            var models = new Dictionary<string, ARenderable>();
            models.Add("Wooden", new MipMapGeneratedRenderObject(new IcoSphereFactory().Create(3), _texturedProgram.Id, "..\\..\\graphics\\Textures\\wooden.png", 8));
            models.Add("Golden", new MipMapGeneratedRenderObject(new IcoSphereFactory().Create(3), _texturedProgram.Id, "..\\..\\graphics\\Textures\\golden.bmp", 8));
            models.Add("Asteroid", new MipMapGeneratedRenderObject(new IcoSphereFactory().Create(3), _texturedProgram.Id, "..\\..\\graphics\\Textures\\moonmap1k.jpg", 8));
            models.Add("Spacecraft", new MipMapGeneratedRenderObject(RenderObjectFactory.CreateTexturedCube6(1, 1, 1), _texturedProgram.Id, "..\\..\\graphics\\Textures\\spacecraft.png", 8));
            models.Add("Gameover", new MipMapGeneratedRenderObject(RenderObjectFactory.CreateTexturedCube6(1, 1, 1), _texturedProgram.Id, "..\\..\\graphics\\Textures\\gameover.png", 8));
            models.Add("Bullet", new MipMapGeneratedRenderObject(new IcoSphereFactory().Create(3), _texturedProgram.Id, "..\\..\\graphics\\Textures\\dotted.png", 8));

            //models.Add("TestObject", new TexturedRenderObject(RenderObjectFactory.CreateTexturedCube(1, 1, 1), _texturedProgram.Id, "..\\..\\graphics\Textures\asteroid texture one side.jpg"));
            //models.Add("TestObjectGen", new MipMapGeneratedRenderObject(RenderObjectFactory.CreateTexturedCube(1, 1, 1), _texturedProgram.Id, "..\\..\\graphics\Textures\asteroid texture one side.jpg", 8));
            //models.Add("TestObjectPreGen", new MipMapManualRenderObject(RenderObjectFactory.CreateTexturedCube(1, 1, 1), _texturedProgram.Id, "..\\..\\graphics\Textures\asteroid texture one side mipmap levels 0 to 8.bmp", 9));

            _gameObjectFactory = new GameObjectFactory(models);

            _player = _gameObjectFactory.CreateSpacecraft();
            _gameObjects.Add(_player);
            _gameObjects.Add(_gameObjectFactory.CreateAsteroid());
            _gameObjects.Add(_gameObjectFactory.CreateGoldenAsteroid());
            _gameObjects.Add(_gameObjectFactory.CreateWoodenAsteroid());

            _camera = new StaticCamera();

            CursorVisible = true;

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
            GL.PointSize(3);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            Closed += OnClosed;
            Debug.WriteLine("OnLoad .. done");
        }
        
        private void OnClosed(object sender, EventArgs eventArgs)
        {
            Exit();
        }

        public override void Exit()
        {
            Debug.WriteLine("Exit called");
            _gameObjectFactory.Dispose();
            _solidProgram.Dispose();
            _texturedProgram.Dispose();
            base.Exit();
        }
        
        private void CreateProjection()
        {
            
            var aspectRatio = (float)Width/Height;
            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                _fov*((float) Math.PI/180f), // field of view angle, in radians
                aspectRatio,                // current window aspect ratio
                0.1f,                       // near plane
                4000f);                     // far plane
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            _time += e.Time;
            var remove = new HashSet<AGameObject>();
            var view = new Vector4(0, 0, -2.4f, 0);
            int removedAsteroids = 0;
            int outOfBoundsAsteroids = 0;
            foreach (var item in _gameObjects)
            {
                item.Update(_time, e.Time);
                if (item.ToBeRemoved)
                    remove.Add(item);

                if (item.GetType() == typeof (Bullet))
                {
                    var collide = ((Bullet) item).CheckCollision(_gameObjects);
                    if (collide != null)
                    {
                        remove.Add(item);
                        if (remove.Add(collide))
                        {
                            _score += ((Asteroid)collide).Score;
                            removedAsteroids++;
                        }
                    }
                }
                if (item.GetType() == typeof(Spacecraft))
                {
                    var collide = ((Spacecraft)item).CheckCollision(_gameObjects);
                    if (collide != null)
                    {
                        foreach (var x in _gameObjects)
                            remove.Add(x);
                        _gameObjects.Add(_gameObjectFactory.CreateGameOver());
                        _gameOver = true;
                        removedAsteroids = 0;
                        break;
                    }
                }
            }
            foreach (var r in remove)
            {
                r.ToBeRemoved = true;
                _gameObjects.Remove(r);
            }
            for (int i = 0; i < removedAsteroids; i++)
            {
                _gameObjects.Add(_gameObjectFactory.CreateRandomAsteroid());
            }
            for (int i = 0; i < outOfBoundsAsteroids; i++)
            {
                _gameObjects.Add(_gameObjectFactory.CreateRandomAsteroid());
            }
            if (_lastBullet == null || _lastBullet.ToBeRemoved)
            {
                _camera = new StaticCamera();
            }
            _camera.Update(_time, e.Time);
            HandleKeyboard(e.Time);
        }

        private void HandleKeyboard(double dt)
        {
            var KeyboardState = Keyboard.GetState();

            if (done_once == true)
            {
                Florence.ClientAssembly.Framework.GetClient().GetExecute().GetExecute_Control().SetFlag_ThreadInitialised(0, false);
                System.Console.WriteLine("Thread Initalised => Thread_OnUpdateFrame()");//TestBench
                done_once = false;
                System.Console.WriteLine("Thread Starting => Thread_OnUpdateFrame()");//TestBench
            }

            float period = (float)dt;
            Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().SetBuffer_Input(Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetEmptyInput());

            if (KeyboardState.IsKeyDown(Key.Escape))
            {
                this.Close();
            }
            if (Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(0) == false)
            {
                if (KeyboardState.IsKeyDown(Key.Enter))//ping
                {
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().SetIsPraiseEvent(0, true);
                    // Framework.GetClient().GetData().GetInput_Instnace().Get_Transmit_InputBuffer().SetPraiseEventId(0);
                    // Framework.GetClient().GetData().GetInput_Instnace().Get_Transmit_InputBuffer().GetInputControl().SelectSetIntputSubset(0);
                    // Framework.GetClient().GetData().GetInput_Instnace().Get_Transmit_InputBuffer().GetInputControl().LoadValuesInToInputSubset(0, period);
                    //Florence.ClientAssembly.Networking.CreateAndSendNewMessage(0);//todo
                }
            }
            if (Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(1) == false)
            {
                Florence.ClientAssembly.game_Instance.Player in_Subset_praise1 = (Florence.ClientAssembly.game_Instance.Player)Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().GetPlayer(0);
                if ((in_Subset_praise1.GetMousePos().X != Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().GetPlayer(0).GetMousePos().X)
                    || (in_Subset_praise1.GetMousePos().Y != Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().GetPlayer(0).GetMousePos().Y)
                )//mouse move
                {
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().SetIsPraiseEvent(1, true);
                    // Framework.GetClient().GetData().GetInput_Instnace().Get_Transmit_InputBuffer().SetPraiseEventId(1);
                    //Framework.GetClient().GetData().GetInput_Instnace().Get_Transmit_InputBuffer().GetInputControl().SelectSetIntputSubset(1);
                    //Framework.GetClient().GetData().GetInput_Instnace().Get_Transmit_InputBuffer().GetInputControl().LoadValuesInToInputSubset(1, period);
                    //Florence.ClientAssembly.Networking.CreateAndSendNewMessage(1);//todo
                }
            }
            if (Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(2) == false)
            {
                if ((KeyboardState.IsKeyDown(Key.W))
                    || (KeyboardState.IsKeyDown(Key.S))
                    || (KeyboardState.IsKeyDown(Key.A))
                    || (KeyboardState.IsKeyDown(Key.D))
                )//player move
                {
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().SetIsPraiseEvent(2, true);
                    //Framework.GetClient().GetData().GetInput_Instnace().Get_Transmit_InputBuffer().SetPraiseEventId(2);
                    //Framework.GetClient().GetData().GetInput_Instnace().Get_Transmit_InputBuffer().GetInputControl().SelectSetIntputSubset(2);
                    //Framework.GetClient().GetData().GetInput_Instnace().Get_Transmit_InputBuffer().GetInputControl().LoadValuesInToInputSubset(2, period);
                    //Florence.ClientAssembly.Networking.CreateAndSendNewMessage(2);//todo
                }
            }
            _lastKeyboardState = KeyboardState;
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"{_title}: FPS:{1f / e.Time:0000.0}, obj:{_gameObjects.Count}, score:{_score}";
            GL.ClearColor(Color.Black);// _backColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            int lastProgram = -1;
            foreach (var obj in _gameObjects)
            {
                var program = obj.Model.Program;
                if (lastProgram != program)
                    GL.UniformMatrix4(20, false, ref _projectionMatrix);
                lastProgram = obj.Model.Program;
                obj.Render(_camera);

            }
            SwapBuffers();
        }
        
    }
}
