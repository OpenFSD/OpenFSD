using System;
using System.Collections.Generic;
using OpenTK;
using Florence.ServerAssembly.Graphics.GameObjects;
using Florence.ServerAssembly.Graphics.Renderables;
using ServerAssembly.GameInstance;

namespace Florence.ServerAssembly.Graphics
{
    public class GameObjectFactory : IDisposable
    {
        private const float Z = -2.7f;
        private readonly Random _random = new Random();
        private readonly Dictionary<string, ARenderable> _models;
        private Player _player;
        public GameObjectFactory(Dictionary<string, ARenderable> models)
        {
            _models = models;
        }
        public void Create_PlayerOnClient()
        {
            _player = new Florence.ServerAssembly.game_Instance.Player(
                _models["Player"],
                new OpenTK.Vector3(0, 0, 1),
                Vector3.Zero,
                new OpenTK.Vector3((float)-Math.PI/2, 0, 0),
                0
            );
            while (_player == null) { /* Wait while is created */ }
        }
        public Asteroid CreateSphericalAsteroid(string model, Vector3 position, Vector3 sclae)
        {
            var obj = new Asteroid(_models[model], position, Vector3.Zero, Vector3.Zero, 0.0f);
            obj.SetScale(new Vector3(sclae));
            return obj;
        }
        public AGameObject CreateCube(string model, Vector3 position, Vector3 rotation, Vector3 sclae)
        {
            var obj = new GameOverCube(_models[model], position, rotation, Vector3.Zero, 0.0f);
            obj.SetScale(new Vector3(sclae));
            return obj;
        }
        public void Dispose()
        {
            foreach (var obj in _models)
                obj.Value.Dispose();
        }

        public Dictionary<string, ARenderable> Get_models()
        {
            return _models;
        }
        public Player Get_player()
        {
            return _player;
        }
    }
}