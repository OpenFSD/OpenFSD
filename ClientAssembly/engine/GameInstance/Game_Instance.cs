﻿using Florence.ServerAssembly.Graphics.Renderables;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Florence.ClientAssembly
{
    public class Game_Instance
    {
        static private Florence.ClientAssembly.game_Instance.Arena arena;
        static private Florence.ClientAssembly.game_Instance.Map_Default mapDefault;

        static private Florence.ServerAssembly.Graphics.GameObjectFactory _gameObjectFactory;
        static private List<Florence.ServerAssembly.Graphics.GameObjects.AGameObject> _gameObjects;
        static private Florence.ClientAssembly.game_Instance.Player player;
        static private Florence.ServerAssembly.Graphics.GameObjects.Spacecraft spaceCraft;
        static private Dictionary<string, ARenderable> models;
        static private Florence.ServerAssembly.Graphics.ShaderProgram _solidProgram;
        static private Florence.ServerAssembly.Graphics.ShaderProgram _texturedProgram;

        public Game_Instance()
        {
            arena = new Florence.ClientAssembly.game_Instance.Arena();
            while (arena == null) { /* Wait while is created */ }

            mapDefault = new Florence.ClientAssembly.game_Instance.Map_Default();
            while (mapDefault == null) { /* Wait while is created */ }

            _gameObjects = new List<Florence.ServerAssembly.Graphics.GameObjects.AGameObject>(7);
            while (_gameObjects == null) { /* Wait while is created */ }

        }
        public void Initalise_Graphics()
        {
            Florence.ClientAssembly.Framework.GetClient().GetExecute().Create_And_Run_Graphics();
        }

        public void Create_gameObjectFactory()
        {
            _gameObjectFactory = new Florence.ServerAssembly.Graphics.GameObjectFactory(models);
            while (_gameObjectFactory == null) { /* Wait while is created */ }
        }

        public void Create_gameObjects()
        {
            Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().CreateSpacecraft();
            _gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_SpaceCraft());
            _gameObjects.RemoveAt(0);
            _gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().CreateAsteroid("Asteroid", new Vector4(-1f, 0, 0, 0)));
            _gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().CreateAsteroid("Asteroid", new Vector4(1f, 0, 0, 0)));
            _gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().CreateAsteroid("Golden", new Vector4(0, -1f, 0, 0)));
            _gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().CreateAsteroid("Golden", new Vector4(0, 1f, 0, 0)));
            _gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().CreateAsteroid("Wooden", new Vector4(0, 0, -1f, 0)));
            _gameObjects.Add(Florence.ClientAssembly.Framework.GetClient().GetData().GetGame_Instance().Get_gameObjectFactory().CreateAsteroid("Wooden", new Vector4(0, 0, 1f, 0)));
        }

        public void Create_PlayerOnClient()
        {
            player = new Florence.ClientAssembly.game_Instance.Player(models["Spacecraft"], new Vector4(0, 0, 0, 0), Vector4.Zero, Vector4.Zero, 0);
            while (player == null) { /* Wait while is created */ }
        }

        public void Load_Sphere_Solid()
        {
            _solidProgram = new Florence.ServerAssembly.Graphics.ShaderProgram();
            _solidProgram.AddShader(ShaderType.VertexShader, "..\\..\\graphics\\Shaders\\1Vert\\simplePipeVert.c");
            _solidProgram.AddShader(ShaderType.FragmentShader, "..\\..\\graphics\\Shaders\\5Frag\\simplePipeFrag.c");
            _solidProgram.Link();
        }

        public void Load_Sphere_Textures()
        {
            _texturedProgram = new Florence.ServerAssembly.Graphics.ShaderProgram();
            _texturedProgram.AddShader(ShaderType.VertexShader, "..\\..\\graphics\\Shaders\\1Vert\\simplePipeTexVert.c");
            _texturedProgram.AddShader(ShaderType.FragmentShader, "..\\..\\graphics\\Shaders\\5Frag\\simplePipeTexFrag.c");
            _texturedProgram.Link();
        }
        public void Load_Models()
        {
            models = new Dictionary<string, ARenderable>();
            while (models == null) { /* Wait while is created */ }
            models.Add("Wooden", new MipMapGeneratedRenderObject(new Florence.ServerAssembly.Graphics.IcoSphereFactory().Create(3), _texturedProgram.Id, "..\\..\\graphics\\Textures\\wooden.png", 8));
            models.Add("Golden", new MipMapGeneratedRenderObject(new Florence.ServerAssembly.Graphics.IcoSphereFactory().Create(3), _texturedProgram.Id, "..\\..\\graphics\\Textures\\golden.bmp", 8));
            models.Add("Asteroid", new MipMapGeneratedRenderObject(new Florence.ServerAssembly.Graphics.IcoSphereFactory().Create(3), _texturedProgram.Id, "..\\..\\graphics\\Textures\\moonmap1k.jpg", 8));
            models.Add("Spacecraft", new MipMapGeneratedRenderObject(Florence.ServerAssembly.Graphics.RenderObjectFactory.CreateTexturedCube6(1, 1, 1), _texturedProgram.Id, "..\\..\\graphics\\Textures\\spacecraft.png", 8));
            models.Add("Gameover", new MipMapGeneratedRenderObject(Florence.ServerAssembly.Graphics.RenderObjectFactory.CreateTexturedCube6(1, 1, 1), _texturedProgram.Id, "..\\..\\graphics\\Textures\\gameover.png", 8));
            models.Add("Bullet", new MipMapGeneratedRenderObject(new Florence.ServerAssembly.Graphics.IcoSphereFactory().Create(3), _texturedProgram.Id, "..\\..\\graphics\\Textures\\dotted.png", 8));
        }

        public Florence.ClientAssembly.game_Instance.Arena Get_Arena()
        {
            return arena;
        }

        public List<Florence.ServerAssembly.Graphics.GameObjects.AGameObject> Get_GameObjects()
        {
            return _gameObjects;
        }
        public Florence.ServerAssembly.Graphics.GameObjectFactory Get_gameObjectFactory()
        {
            return _gameObjectFactory;
        }

        public Florence.ClientAssembly.game_Instance.Map_Default Get_MapDefault()
        {
            return mapDefault;
        }

        public Dictionary<string, ARenderable> Get_models()
        {
            return models;
        }

        public Florence.ClientAssembly.game_Instance.Player Get_Player()
        {
            return player;
        }
        public Florence.ServerAssembly.Graphics.GameObjects.Spacecraft Get_SpaceCraft()
        {
            return spaceCraft;
        }

        public Florence.ServerAssembly.Graphics.ShaderProgram Get_texturedProgram()
        {
            return _texturedProgram;
        }

        public Florence.ServerAssembly.Graphics.ShaderProgram Get_solidProgram()
        {
            return _solidProgram;
        }

        public void SetAdd_NewPlayer(Florence.ClientAssembly.game_Instance.Player value)
        {
            player = value;
        }
    }
}
