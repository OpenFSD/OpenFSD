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

namespace Florence.ServerAssembly.Graphics
{
    public sealed class MainWindow : GameWindow
    {
        private bool done_once;
        private readonly string _title;
        private double _time;
        private readonly Color4 _backColor = new Color4(0.1f, 0.1f, 0.3f, 1.0f);
        private Matrix4 _projectionMatrix;
        private float _fov = 45f;

        private KeyboardState _lastKeyboardState;
        private MouseState mouseState;
        
        private bool _useFirstPerson = true;


        public MainWindow()
            : base(1920, // initial width
                1080, // initial height
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

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Load_Sphere_Textures();

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Load_Models();

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Create_gameObjectFactory();

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Create_gameObjects();

            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Create_Player();

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
                _fov*((float) Math.PI/180f), // field of view angle, in radians
                aspectRatio,                // current window aspect ratio
                0.1f,                       // near plane
                4000f);                     // far plane
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            _time += e.Time;
            HandleMouse();
            HandleKeyboard(e.Time);
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
                    Florence.ClientAssembly.game_Instance.Player player = Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player();
                    if (Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_IsFirstMove() == true)
                    {
                        player.Set_last_Position(player.Get_position());
                        Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Set_IsFirstMove(false);
                    }
                    else
                    {
                        switch (Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_cameraSelector())
                        {
                            case true://First Person
                                Matrix4 modelMatrix = Matrix4.CreateRotationX(player.Get_rotation_In_World().X) * Matrix4.CreateRotationY(player.Get_rotation_In_World().Y) * Matrix4.CreateRotationZ(player.Get_rotation_In_World().Z) * Matrix4.CreateTranslation(player.Get_fowards().X, player.Get_fowards().Y, player.Get_fowards().Z);
                                Quaternion q = modelMatrix.ExtractRotation();
                                Vector3 fowards = new Vector3(q.Xyz);
                                fowards.Normalize();
                                Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Set_fowards(fowards);

                                player.Set_up(player.Get_position() + player.Get_position().Normalized());

                                player.Set_right(Vector3.Cross(player.Get_fowards(), player.Get_up()));
                                player.Set_right(player.Get_right().Normalized());

                                player.Set_Direction(player.Get_position() + player.Get_fowards());

                                player.Set_Position(player.Get_position() + (player.Get_direction() * player.Get_velocity() * (float)dt));

                                player.Align_PlayerGyro();
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
                                player.Set_last_Position(player.Get_position());
                                break;

                            case false://Third Person

                                break;
                        }
                    }
                }
                
            }
            if (Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(3) == false)
            {
                if (KeyboardState.IsKeyDown(Key.S))
                {
                    Florence.ClientAssembly.game_Instance.Player player = Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player();
                    if (Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_IsFirstMove() == true)
                    {
                        player.Set_last_Position(player.Get_position());
                        Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Set_IsFirstMove(false);
                    }
                    else
                    {
                        switch (Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_cameraSelector())
                        {
                            case true://First Person
                                Matrix4 modelMatrix = Matrix4.CreateRotationX(player.Get_rotation_In_World().X) * Matrix4.CreateRotationY(player.Get_rotation_In_World().Y) * Matrix4.CreateRotationZ(player.Get_rotation_In_World().Z) * Matrix4.CreateTranslation(player.Get_fowards().X, player.Get_fowards().Y, player.Get_fowards().Z);
                                Quaternion q = modelMatrix.ExtractRotation();
                                Vector3 fowards = new Vector3(q.Xyz);
                                fowards.Normalize();
                                Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Set_fowards(fowards);

                                player.Set_up(player.Get_position() + player.Get_position().Normalized());

                                player.Set_right(Vector3.Cross(player.Get_fowards(), player.Get_up()));
                                player.Set_right(player.Get_right().Normalized());

                                player.Set_Direction(player.Get_position() - player.Get_fowards());

                                player.Set_Position(player.Get_position() + (player.Get_direction() * player.Get_velocity() * (float)dt));

                                player.Align_PlayerGyro();
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
                                player.Set_last_Position(player.Get_position());
                                break;

                            case false://Third Person

                                break;
                        }
                    }
                }

            }
            if (Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(4) == false)
            {
                if (KeyboardState.IsKeyDown(Key.A))
                {
                    Florence.ClientAssembly.game_Instance.Player player = Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player();
                    if (Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_IsFirstMove() == true)
                    {
                        player.Set_last_Position(player.Get_position());
                        Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Set_IsFirstMove(false);
                    }
                    else
                    {
                        switch (Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_cameraSelector())
                        {
                            case true://First Person
                                Matrix4 modelMatrix = Matrix4.CreateRotationX(player.Get_rotation_In_World().X) * Matrix4.CreateRotationY(player.Get_rotation_In_World().Y) * Matrix4.CreateRotationZ(player.Get_rotation_In_World().Z) * Matrix4.CreateTranslation(player.Get_fowards().X, player.Get_fowards().Y, player.Get_fowards().Z);
                                Quaternion q = modelMatrix.ExtractRotation();
                                Vector3 fowards = new Vector3(q.Xyz);
                                fowards.Normalize();
                                Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Set_fowards(fowards);

                                player.Set_up(player.Get_position() + player.Get_position().Normalized());

                                player.Set_right(Vector3.Cross(player.Get_fowards(), player.Get_up()));
                                player.Set_right(player.Get_right().Normalized());

                                player.Set_Direction(player.Get_position() - player.Get_right());

                                player.Set_Position(player.Get_position() + (player.Get_direction() * player.Get_velocity() * (float)dt));

                                player.Align_PlayerGyro();
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
                                player.Set_last_Position(player.Get_position());
                                break;

                            case false://Third Person

                                break;
                        }
                    }
                }
            }
            if (Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(5) == false)
            {
                if (KeyboardState.IsKeyDown(Key.D))
                {
                    Florence.ClientAssembly.game_Instance.Player player = Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player();
                    if (Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_IsFirstMove() == true)
                    {
                        player.Set_last_Position(player.Get_position());
                        Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Set_IsFirstMove(false);
                    }
                    else
                    {
                        switch (Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_cameraSelector())
                        {
                        case true://First Person
                            Matrix4 modelMatrix = Matrix4.CreateRotationX(player.Get_rotation_In_World().X) * Matrix4.CreateRotationY(player.Get_rotation_In_World().Y) * Matrix4.CreateRotationZ(player.Get_rotation_In_World().Z) * Matrix4.CreateTranslation(player.Get_fowards().X, player.Get_fowards().Y, player.Get_fowards().Z);
                            Quaternion q = modelMatrix.ExtractRotation();
                            Vector3 fowards = new Vector3(q.Xyz);
                            fowards.Normalize();
                            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Set_fowards(fowards);

                            player.Set_up(player.Get_position() + player.Get_position().Normalized());

                            player.Set_right(Vector3.Cross(player.Get_fowards(), player.Get_up()));
                            player.Set_right(player.Get_right().Normalized());

                            player.Set_Direction(player.Get_position() + player.Get_right());

                            player.Set_Position(player.Get_position() + (player.Get_direction() * player.Get_velocity() * (float)dt));

                            player.Align_PlayerGyro();
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
                            player.Set_last_Position(player.Get_position());
                            break;

                        case false://Third Person

                            break;
                        }
                    }
                }
            }
            _lastKeyboardState = KeyboardState;
        }

        private void HandleMouse()
        {
            System.Console.WriteLine("TESTBENCH => HandleMouse");
            MouseState new_mouseState = Mouse.GetCursorState();
            
            if (Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(1) == false)
            {
                switch (Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_cameraSelector())
                {
                case true://First Person
                    if (Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_IsFirstMouseMove()) // This bool variable is initially set to true.
                    {
                        Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Set_MousePos(new Vector2(new_mouseState.X, new_mouseState.Y));
                        Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Set_IsFirstMouseMove(false);
                    }
                    else
                    {
                        Florence.ClientAssembly.game_Instance.Player player = Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player();
                        float sensitivity = Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_Camera_FP().Get_sensitivity();
                        float deltaX = 0;
                        float deltaY = 0;

                        // Calculate the offset of the mouse position
                        if (new_mouseState.X == (1920 / 2)) deltaX = 0;
                        else deltaX = new_mouseState.X - (1920 / 2);
                        deltaX = deltaX * sensitivity;
                        player.Add_deltaX((short)deltaX);

                        if (new_mouseState.Y == (1080 / 2)) deltaY = 0;
                        else deltaY = new_mouseState.Y - (1080 / 2);
                        deltaY = deltaY * sensitivity;
                        player.Add_deltaY((short)deltaY);

                        if (deltaX != 0 || deltaY != 0)
                        {
                            System.Console.WriteLine("TESTBENCH => RAW_X = " + deltaX + "  RAW_Y = " + deltaY);
                                
                            // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                            Vector3 gyroPosition;
                            gyroPosition.X = (float)(Math.Sin(player.Get_deltaX()) * Math.Cos(player.Get_deltaY()));
                            gyroPosition.Y = (float)Math.Sin(player.Get_deltaY());
                            gyroPosition.Z = (float)(Math.Cos(player.Get_deltaX()) * Math.Cos(player.Get_deltaY()));

                            Vector3 axis_X_wolrd = player.Fowards;
                            Vector3 axis_Y_world = player.Right;
                            Vector3 axis_Z_world = player.Up;

                            Vector3 cross = Vector3.Cross(axis_X_wolrd, gyroPosition);
                            float theta_X = (float)System.Math.Asin(cross.Length / (axis_X_wolrd.Length * gyroPosition.Length));

                            cross = Vector3.Cross(axis_Y_world, gyroPosition);
                            float theta_Y = (float)System.Math.Asin(cross.Length / (axis_Y_world.Length * gyroPosition.Length));
                                
                            cross = Vector3.Cross(axis_Z_world, gyroPosition);
                            float theta_Z = (float)System.Math.Asin(cross.Length / (axis_Z_world.Length * gyroPosition.Length));

                            player.Set_Rotation(new Vector3(
                                player.Get_rotation_In_World().X + theta_X,
                                player.Get_rotation_In_World().Y + theta_Y,
                                player.Get_rotation_In_World().Z + theta_Z
                            ));
                            player.Set_Rotation(player.Get_rotation_In_World().Normalized());

                            if (player.Get_rotation_In_World().X < System.Math.PI)
                            {
                                player.Set_world_pitch((float)((2 * System.Math.PI) + player.Get_rotation_In_World().X));
                            }
                            else if (player.Get_rotation_In_World().X > System.Math.PI)
                            {
                                player.Set_world_pitch((float)((2 * System.Math.PI) + player.Get_rotation_In_World().X));
                            }

                            if (player.Get_rotation_In_World().Y < System.Math.PI)
                            {
                                player.Set_world_yaw((float)((2 * System.Math.PI) + player.Get_rotation_In_World().Y));
                            }
                            else if (player.Get_rotation_In_World().Y > System.Math.PI)
                            {
                                player.Set_world_yaw((float)((2 * System.Math.PI) + player.Get_rotation_In_World().Y));
                            }

                            if (player.Get_rotation_In_World().Z < System.Math.PI)
                            {
                                player.Set_world_roll((float)((2 * System.Math.PI) + player.Get_rotation_In_World().Z));
                            }
                            else if (player.Get_rotation_In_World().Z > System.Math.PI)
                            {
                                player.Set_world_roll((float)((2 * System.Math.PI) + player.Get_rotation_In_World().Z));
                            }

                            Matrix4 modelMatrix = Matrix4.CreateRotationX(player.Get_rotation_In_World().X) * Matrix4.CreateRotationY(player.Get_rotation_In_World().Y) * Matrix4.CreateRotationZ(player.Get_rotation_In_World().Z) * Matrix4.CreateTranslation(player.Fowards.X, player.Fowards.Y, player.Fowards.Z);
                            Quaternion q = modelMatrix.ExtractRotation();

                            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Set_fowards(q.Xyz);
                            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Set_right(Vector3.Cross(q.Xyz, player.Get_up()));

                            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Set_MousePos(new Vector2(new_mouseState.X, new_mouseState.Y));
                        }
                    }
                    break;

                case false://Third Person
                 
                    break;
                }
                /*
                Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().SetIsPraiseEvent(1, true);
                Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Back_InputDouble().GetInputControl().SelectSetIntputSubset(1);
                Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Front_InputDouble().SetPraiseEventId(1);
                Florence.ClientAssembly.Praise_Files.Praise1_Input input_subset_Praise1 = (Florence.ClientAssembly.Praise_Files.Praise1_Input)Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Front_InputDouble().Get_InputBufferSubset();
                input_subset_Praise1.Set_Mouse_X(new_mouseState.X);
                input_subset_Praise1.Set_Mouse_Y(new_mouseState.Y);
                Florence.ClientAssembly.Framework.GetClient().GetData().Flip_InBufferToWrite();
                //Florence.ClientAssembly.Networking.CreateAndSendNewMessage(1);//todo
                */
                mouseState = new_mouseState;
                System.Console.WriteLine("TESTBENCH => HandleMouse .. Done");
            }
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
