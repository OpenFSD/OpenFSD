﻿using System;
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
        //private GameObjectFactory _gameObjectFactory;
        //private readonly List<AGameObject> _gameObjects = new List<AGameObject>();
        private double _time;
        private readonly Color4 _backColor = new Color4(0.1f, 0.1f, 0.3f, 1.0f);
        private Matrix4 _projectionMatrix;
        private float _fov = 45f;
        //private ShaderProgram _texturedProgram;
        //private ShaderProgram _solidProgram;
        private KeyboardState _lastKeyboardState;
        private MouseState mouseState;
        //private Spacecraft _player;
        private int _score;
        private bool _gameOver;
        private Bullet.BulletType _bulletType;
        private Bullet _lastBullet;
        private bool _useFirstPerson = true;
        //private ICamera _camera;

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

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Load_Sphere_Solid();
            /*
            _solidProgram = new ShaderProgram();
            _solidProgram.AddShader(ShaderType.VertexShader, "..\\..\\graphics\\Shaders\\1Vert\\simplePipeVert.c");
            _solidProgram.AddShader(ShaderType.FragmentShader, "..\\..\\graphics\\Shaders\\5Frag\\simplePipeFrag.c");
            _solidProgram.Link();
            */

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Load_Sphere_Textures();
            /*
            _texturedProgram = new ShaderProgram();
            _texturedProgram.AddShader(ShaderType.VertexShader, "..\\..\\graphics\\Shaders\\1Vert\\simplePipeTexVert.c");
            _texturedProgram.AddShader(ShaderType.FragmentShader, "..\\..\\graphics\\Shaders\\5Frag\\simplePipeTexFrag.c");
            _texturedProgram.Link();
            */

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Load_Models();
            /*
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
            */

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Create_gameObjectFactory();
            //_gameObjectFactory = new GameObjectFactory(models);

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Create_gameObjects();

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Create_PlayerOnClient();
            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_Player().Set_Camera();

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
            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Dispose();
            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_solidProgram().Dispose();
            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_texturedProgram().Dispose();
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
            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_Player().Get_Camera().Update(_time, e.Time);
            HandleKeyboard(e.Time);
        }

        private void HandleKeyboard(double dt)
        {
            var KeyboardState = Keyboard.GetState();
            mouseState = new MouseState();
            if (done_once == true)
            {
                Florence.ClientAssembly.Framework.GetClient().GetExecute().GetExecute_Control().SetFlag_ThreadInitialised(0, false);
                System.Console.WriteLine("Thread Initalised => Thread_OnUpdateFrame()");//TestBench
                System.Console.WriteLine("Thread Starting => Thread_OnUpdateFrame()");//TestBench
                done_once = false;
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
                    /*
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().SetIsPraiseEvent(0, true);
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Back_InputDouble().GetInputControl().SelectSetIntputSubset(0);
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Front_InputDouble().SetPraiseEventId(0);
                    Florence.ClientAssembly.Praise_Files.Praise0_Input input_subset_Praise0 = (Florence.ClientAssembly.Praise_Files.Praise0_Input)Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Front_InputDouble().Get_InputBufferSubset();
                    input_subset_Praise0.SetFlag_IsPingActive(true);
                    Florence.ClientAssembly.Framework.GetClient().GetData().Flip_InBufferToWrite();
                    //Florence.ClientAssembly.Networking.CreateAndSendNewMessage(0);//todo
                    */
                }
            }
            if (Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(1) == false)
            {
                if ((mouseState.X != 0) || (mouseState.Y != 0))//mouse move
                {
                    /*
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().SetIsPraiseEvent(1, true);
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Back_InputDouble().GetInputControl().SelectSetIntputSubset(1);
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Front_InputDouble().SetPraiseEventId(1);
                    Florence.ClientAssembly.Praise_Files.Praise1_Input input_subset_Praise1 = (Florence.ClientAssembly.Praise_Files.Praise1_Input)Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Front_InputDouble().Get_InputBufferSubset();
                    input_subset_Praise1.Set_Mouse_X(mouseState.X);
                    input_subset_Praise1.Set_Mouse_Y(mouseState.Y);
                    Florence.ClientAssembly.Framework.GetClient().GetData().Flip_InBufferToWrite();
                    //Florence.ClientAssembly.Networking.CreateAndSendNewMessage(1);//todo
                    */
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
                    Florence.ClientAssembly.game_Instance.Player player = Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_Player();
                    Vector4 position = player.Position;
                    Vector4 foward = player.Get_foward();
                    Vector4 right = player.Get_right();
                    Vector4 up = player.Get_up();
                    if (KeyboardState.IsKeyDown(Key.W)) player.SetPosition(position += position * foward * player.Get_cameraSpeed() * (float)period);
                    if (KeyboardState.IsKeyDown(Key.S)) player.SetPosition(position -= position * foward * player.Get_cameraSpeed() * (float)period);
                    if (KeyboardState.IsKeyDown(Key.A)) player.SetPosition(position -= position * right * player.Get_cameraSpeed() * (float)period);
                    if (KeyboardState.IsKeyDown(Key.D)) player.SetPosition(position += position * right * player.Get_cameraSpeed() * (float)period);
                    /*
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().SetIsPraiseEvent(2, true);
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Front_InputDouble().GetInputControl().SelectSetIntputSubset(2);
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Front_InputDouble().SetPraiseEventId(2);
                    Florence.ClientAssembly.Praise_Files.Praise2_Input input_subset_Praise2 = (Florence.ClientAssembly.Praise_Files.Praise2_Input)Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Front_InputDouble().Get_InputBufferSubset();
                    if (KeyboardState.IsKeyDown(Key.W)) input_subset_Praise2.Set_Fowards(true);
                    if (KeyboardState.IsKeyDown(Key.S)) input_subset_Praise2.Set_Backwards(true);
                    if (KeyboardState.IsKeyDown(Key.A)) input_subset_Praise2.Set_Left(true);
                    if (KeyboardState.IsKeyDown(Key.D)) input_subset_Praise2.Set_Right(true);
                    input_subset_Praise2.Set_Period(period);
                    Florence.ClientAssembly.Framework.GetClient().GetData().Flip_InBufferToWrite();
                    //Florence.ClientAssembly.Networking.CreateAndSendNewMessage(2);//todo
                    */
                }
            }
            _lastKeyboardState = KeyboardState;
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"{_title}: FPS:{1f / e.Time:0000.0}, obj:{Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_GameObjects().Count}, score:{_score}";
            GL.ClearColor(Color.Black);// _backColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            int lastProgram = -1;
            for (byte index = 0; index < Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_GameObjects().Count(); index++)
            {
                AGameObject obj = Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_GameObjects().ElementAt(index);
                int program = obj.Model.Program;
                if (lastProgram != program)
                    GL.UniformMatrix4(20, false, ref _projectionMatrix);
                lastProgram = obj.Model.Program;
                obj.Render(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_Player().Get_Camera());

            }
            SwapBuffers();
        }
        
    }
}
