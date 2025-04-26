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
        

        private KeyboardState _lastKeyboardState;
        private MouseState mouseState;
        
        private bool _useFirstPerson = true;


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
                        player.Set_Direction(player.Get_position() + (player.Fowards * (float)(player.Get_speed() * dt)));
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
                        player.Set_Direction(player.Get_position() - (player.Fowards * (float)(player.Get_speed() * dt)));
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
                        player.Set_Direction(player.Get_position() - (player.Right * (float)(player.Get_speed() * dt)));
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
                        player.Set_Direction(player.Get_position() + (player.Right * (float)(player.Get_speed() * dt)));
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
        }

        private void HandleMouse()
        {
            System.Console.WriteLine("TESTBENCH => HandleMouse");
            MouseState new_mouseState = Mouse.GetCursorState();
            
            if (Florence.ClientAssembly.Framework.GetClient().GetData().GetData_Control().GetFlag_IsPraiseEvent(1) == false)
            {
                Matrix4 modelMatrix;
                Quaternion rot;
                var player = Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player();
                switch (Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().Get_Player().Get_cameraSelector())
                {
                case true://First Person
                    if ((new_mouseState.X != (char)(Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_X() / 2)) || (new_mouseState.Y != (char)(Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_Y() / 2)))
                    {
                        System.Console.WriteLine("TESTBENCH => rot_X = " + player.Get_rotation_In_World().X + "  rot_Y = " + player.Get_rotation_In_World().Y + "  rot_Z = " + player.Get_rotation_In_World().Z);
                        float anglePerPixle = Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_fov() / Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_Y();
                        float deltaX = anglePerPixle * (new_mouseState.X - (char)(Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_X() / 2));
                        float deltaY = anglePerPixle * (new_mouseState.Y - (char)(Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_Y() / 2));
                        System.Console.WriteLine("TESTBENCH => RAW_X = " + deltaX + "  RAW_Y = " + deltaY);
                        
                        player.Add_deltaX_To_player_Yaw_radians(deltaX);
                        player.Add_deltaY_To_player_Pitch_radians(deltaY);
                    
                        // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                        Vector3 gyroPosition;
                        gyroPosition.X = (float)(Math.Sin(player.Get_player_Yaw_radians()) * Math.Cos(player.Get_player_Pitch_radians()));
                        gyroPosition.Y = (float)Math.Sin(player.Get_player_Pitch_radians());
                        gyroPosition.Z = (float)(Math.Cos(player.Get_player_Yaw_radians()) * Math.Cos(player.Get_player_Pitch_radians()));
                        System.Console.WriteLine("TESTBENCH => gyroPosition_X = " + gyroPosition.X + "  gyroPosition_Y = " + gyroPosition.Y + "  gyroPosition_Z = " + gyroPosition.Z);

                        player.Set_Rotation_In_World(player.Calculate_Rotations(gyroPosition));

                        System.Console.WriteLine("TESTBENCH => rot_X = " + player.Get_rotation_In_World().X + "  rot_Y = " + player.Get_rotation_In_World().Y + "  rot_Z = " + player.Get_rotation_In_World().Z);

                        modelMatrix = Matrix4.CreateRotationX(player.Get_rotation_In_World().X) * Matrix4.CreateRotationY(player.Get_rotation_In_World().Y) * Matrix4.CreateRotationZ(player.Get_rotation_In_World().Z) * Matrix4.CreateTranslation(player.Get_player_axis_X().X, player.Get_player_axis_X().Y, player.Get_player_axis_X().Z);
                        rot = modelMatrix.ExtractRotation();
                        player.Set_player_axis_X(Vector3.Transform(player.Get_player_axis_X(), rot));

                        modelMatrix = Matrix4.CreateRotationX(player.Get_rotation_In_World().X) * Matrix4.CreateRotationY(player.Get_rotation_In_World().Y) * Matrix4.CreateRotationZ(player.Get_rotation_In_World().Z) * Matrix4.CreateTranslation(player.Get_player_axis_Y().X, player.Get_player_axis_Y().Y, player.Get_player_axis_Y().Z);
                        rot = modelMatrix.ExtractRotation();
                        player.Set_player_axis_X(Vector3.Transform(player.Get_player_axis_Y(), rot));

                        modelMatrix = Matrix4.CreateRotationX(player.Get_rotation_In_World().X) * Matrix4.CreateRotationY(player.Get_rotation_In_World().Y) * Matrix4.CreateRotationZ(player.Get_rotation_In_World().Z) * Matrix4.CreateTranslation(player.Get_player_axis_Z().X, player.Get_player_axis_Z().Y, player.Get_player_axis_Z().Z);
                        rot = modelMatrix.ExtractRotation();
                        player.Set_player_axis_X(Vector3.Transform(player.Get_player_axis_Z(), rot));

                        modelMatrix = Matrix4.CreateRotationX(player.Get_rotation_In_World().X) * Matrix4.CreateRotationY(player.Get_rotation_In_World().Y) * Matrix4.CreateRotationZ(player.Get_rotation_In_World().Z) * Matrix4.CreateTranslation(player.Fowards.X, player.Fowards.Y, player.Fowards.Z);
                        rot = modelMatrix.ExtractRotation();
                        player.Set_fowards(Vector3.Transform(player.Get_fowards(), rot));

                        OpenTK.Input.Mouse.SetPosition((char)(Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_X() / 2), (char)(Florence.ClientAssembly.Framework.GetClient().GetData().GetSettings().Get_ScreenSize_Y() / 2));
                            
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
                    }
                    break;

                case false://Third Person
                    player.Set_Position(player.Get_position().Normalized() * 103f);
                    player.Set_up(player.Get_position() + player.Get_position().Normalized());

                    modelMatrix = Matrix4.CreateRotationX(player.Get_rotation_In_World().X) * Matrix4.CreateRotationY(player.Get_rotation_In_World().Y) * Matrix4.CreateRotationZ(player.Get_rotation_In_World().Z) * Matrix4.CreateTranslation(player.Get_player_axis_X().X, player.Get_player_axis_X().Y, player.Get_player_axis_X().Z);
                    rot = modelMatrix.ExtractRotation();
                    player.Set_player_axis_X(Vector3.Transform(player.Get_player_axis_X(), rot));

                    modelMatrix = Matrix4.CreateRotationX(player.Get_rotation_In_World().X) * Matrix4.CreateRotationY(player.Get_rotation_In_World().Y) * Matrix4.CreateRotationZ(player.Get_rotation_In_World().Z) * Matrix4.CreateTranslation(player.Get_player_axis_Y().X, player.Get_player_axis_Y().Y, player.Get_player_axis_Y().Z);
                    rot = modelMatrix.ExtractRotation();
                    player.Set_player_axis_X(Vector3.Transform(player.Get_player_axis_Y(), rot));

                    modelMatrix = Matrix4.CreateRotationX(player.Get_rotation_In_World().X) * Matrix4.CreateRotationY(player.Get_rotation_In_World().Y) * Matrix4.CreateRotationZ(player.Get_rotation_In_World().Z) * Matrix4.CreateTranslation(player.Get_player_axis_Z().X, player.Get_player_axis_Z().Y, player.Get_player_axis_Z().Z);
                    rot = modelMatrix.ExtractRotation();
                    player.Set_player_axis_X(Vector3.Transform(player.Get_player_axis_Z(), rot));


                    break;
                }
            }
            System.Console.WriteLine("TESTBENCH => HandleMouse .. Done");
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
