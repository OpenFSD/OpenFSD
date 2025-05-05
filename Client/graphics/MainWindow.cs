using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using Florence.ServerAssembly.Graphics.Cameras;
using Florence.ServerAssembly.Graphics.GameObjects;
using Florence.ServerAssembly.Graphics.Renderables;
using Florence.ClientAssembly.game_Instance;

namespace Florence.ServerAssembly.Graphics
{
    public sealed class MainWindow : GameWindow
    {
        private bool done_once;

        private readonly string _title;
        private double _time;
        private readonly Color4 _backColor = new Color4(0.1f, 0.1f, 0.3f, 1.0f);
        private Matrix4 _projectionMatrix;
        
        private KeyboardState _lastKeyboardState;
        private MouseState _lastMouseState;

        public MainWindow()
            : base(Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_X(), // initial width
                Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_Y(), // initial height
                GraphicsMode.Default,
                "",  // initial title
                GameWindowFlags.Fullscreen,
                DisplayDevice.Default,
                4, // OpenGL major version
                5, // OpenGL minor version
                GraphicsContextFlags.ForwardCompatible)
        {
            _title += "github.com/OpenFSD: OpenGL Version: " + GL.GetString(StringName.Version);
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

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Load_Sphere_Textures();

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Load_Models();

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Create_gameObjectFactory();

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Create_gameObjects();

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Create_Player();
            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Initialise_Player();

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Create_Cameras();

            CursorVisible = false;
            
            
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
                Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_fov() * ((float) Math.PI/180f), // field of view angle, in radians
                aspectRatio,                // current window aspect ratio
                0.1f,                       // near plane
                4000f);                     // far plane
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            _time += e.Time;
            HandleKeyboard(e.Time);
            HandleMouse();
            switch (Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_cameraSelector())
            {
                case true://First Person
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_Camera_FP().Update(_time, e.Time);
                    break;

                case false://Third Person
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_Camera_TP().Update(_time, e.Time);
                    break;
            }
            
        }

        private void HandleKeyboard(double dt)
        {
            KeyboardState KeyboardState = Keyboard.GetState();
            var player = Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player();

            if (done_once == true)
            {
                Florence.ClientAssembly.Framework.GetClient().GetExecute().GetExecute_Control().SetFlag_ThreadInitialised(0, false);
                System.Console.WriteLine("Thread Initalised => Thread_OnUpdateFrame()");//TestBench
                System.Console.WriteLine("Thread Starting => Thread_OnUpdateFrame()");//TestBench
                done_once = false;
            }

            Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().SetBuffer_Input(Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetEmptyInput());

            if (KeyboardState.IsKeyDown(Key.Escape))
            {
                this.Close();
            }

            switch (Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_cameraSelector())
            {
                case true://First Person
                    Vector3 temp_W = new Vector3(0);
                    Vector3 temp_S = new Vector3(0);
                    Vector3 temp_A = new Vector3(0);
                    Vector3 temp_D = new Vector3(0);

                    if (KeyboardState.IsKeyDown(Key.Space))
                    {
                        Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Set_cameraSelector(
                            !Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_cameraSelector()
                        );
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
                    if (Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(2) == false)
                    {
                        if (KeyboardState.IsKeyDown(Key.W))
                        {
                            Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().SetIsPraiseEvent(2, true);
                            temp_W = (player.Get_position() + player.Fowards);
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
                    if (Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(3) == false)
                    {
                        if (KeyboardState.IsKeyDown(Key.S))
                        {
                            Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().SetIsPraiseEvent(3, true);
                            temp_S = (player.Get_position() - player.Fowards);
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
                    if (Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(4) == false)
                    {
                        if (KeyboardState.IsKeyDown(Key.A))
                        {
                            Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().SetIsPraiseEvent(4, true);
                            temp_A = (player.Get_position() - player.Right);
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
                    if (Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(5) == false)
                    {
                        if (KeyboardState.IsKeyDown(Key.D))
                        {
                            Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().SetIsPraiseEvent(5, true);
                            temp_D = (player.Get_position() + player.Right);
                            /*
                            
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
                    if (Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(2) == true
                        || Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(3) == true
                        || Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(4) == true
                        || Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(5) == true
                    )
                    {
                        player.Set_Position(player.Get_position() + (Vector3.Normalize(temp_W + temp_S + temp_A + temp_D) * (float)(player.Get_speed() * dt)));
                    }
                    break;

                case false://Third Person

                    if (KeyboardState.IsKeyDown(Key.Space))
                    {
                        Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Set_cameraSelector(
                            !Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_cameraSelector()
                        );
                    }
                    //ToDo
                    break;
            }
            _lastKeyboardState = KeyboardState;
            for (int praiseEventId = 0; praiseEventId < 6; praiseEventId++)//undo after networking
            {
                Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().SetIsPraiseEvent(praiseEventId, false);
            }
        }

        private void HandleMouse()
        {
            System.Console.WriteLine("TESTBENCH => HandleMouse");
            MouseState mouseState = Mouse.GetCursorState();
            
            if (Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(1) == false)
            {
                
                switch (Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_cameraSelector())
                {
                case true://First Person
                    var player = Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player();
                    if (mouseState.X != (char)(Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_X() / 2) || mouseState.Y != (char)(Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_Y() / 2)) // check to see if the window is focused  
                    {
                        System.Console.WriteLine("TESTBENCH => rot_X = " + player.Get_rotation_In_World().X + "  rot_Y = " + player.Get_rotation_In_World().Y + "  rot_Z = " + player.Get_rotation_In_World().Z);
                        float anglePerPixle = Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_fov() / Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_Y();
                        float deltaX = -(Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_X() / 2) + mouseState.X;
                        float deltaY = -(Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_Y() / 2) + mouseState.Y;
                        System.Console.WriteLine("TESTBENCH => RAW_X = " + deltaX + "  RAW_Y = " + deltaY);

                        //player.Set_Last_MousePos(new Vector2(mouseState.X, mouseState.Y));

                        player.Set_player_Yaw_radians(player.Get_player_Yaw_radians() + (float)((System.Math.PI / 180) * (deltaX * Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_Camera_FP().Get_sensitivity())));
                        if (player.Get_player_Yaw_radians() > System.Math.PI)
                        {
                            player.Set_player_Yaw_radians(player.Get_player_Yaw_radians() - (float)(2 * System.Math.PI));
                        }
                        else if (player.Get_player_Yaw_radians() <= -System.Math.PI)
                        {
                            player.Set_player_Yaw_radians(player.Get_player_Yaw_radians() + (float)(2 * System.Math.PI));
                        }

                        player.Set_player_Pitch_radians(player.Get_player_Pitch_radians() + (float)((System.Math.PI / 180) * (deltaY * Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_Camera_FP().Get_sensitivity())));
                        if (player.Get_player_Pitch_radians() >= (float)((System.Math.PI / 180) * 85f))
                        {
                            player.Set_player_Pitch_radians((float)((System.Math.PI / 180) * 85f));
                        }
                        else if (player.Get_player_Pitch_radians() <= (float)((System.Math.PI / 180) * -85f))
                        {
                            player.Set_player_Pitch_radians((float)((System.Math.PI / 180) * -85f));
                        }
                        Vector3 front;
                        front.X = (float)(Math.Cos(player.Get_player_Yaw_radians()) * Math.Cos(player.Get_player_Pitch_radians()));
                        front.Y = (float)Math.Sin(player.Get_player_Pitch_radians());
                        front.Z = (float)(Math.Sin(player.Get_player_Yaw_radians()) * Math.Cos(player.Get_player_Pitch_radians()));
                        front = Vector3.Normalize(front);
                        player.Set_fowards(front);
                        OpenTK.Input.Mouse.SetPosition((char)(Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_X() / 2), (char)(Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_Y() / 2));
                        player.Set_up(Vector3.UnitY);
                        player.Set_right(Vector3.Cross(player.Get_fowards(), player.Get_up()));
                    }
                    break;

                case false://Third Person

                    break;
                }
            }
            System.Console.WriteLine("TESTBENCH => HandleMouse .. Done");
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
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
                switch (Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_cameraSelector())
                {
                    case true://First Person
                        obj.Render(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_Camera_FP());
                        break;

                    case false://Third Person
                        obj.Render(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_Camera_TP());
                        break;
                }
            }
            SwapBuffers();
        }
    }
}
