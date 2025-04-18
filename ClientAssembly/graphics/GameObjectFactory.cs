using System;
using System.Collections.Generic;
using OpenTK;
using Florence.ServerAssembly.Graphics.GameObjects;
using Florence.ServerAssembly.Graphics.Renderables;

namespace Florence.ServerAssembly.Graphics
{
    public class GameObjectFactory : IDisposable
    {
        private readonly Dictionary<string, ARenderable> _models;
        static private Florence.ClientAssembly.game_Instance.Player player;

        public GameObjectFactory(Dictionary<string, ARenderable> models)
        {
            _models = models;
        }
        public Asteroid CreateEarth(string model, Vector3 position)
        {
            var obj = new Asteroid(_models[model], position, Vector3.Zero, Vector3.Zero, 0f);
            obj.Set_Scale(new Vector3(100f));
            return obj;
        }

        public void Create_Player()
        {
            Vector3 position = new Vector3(0f, 0f, 101f);
            Vector3 rotation = new Vector3(0f, 0f, 0f);
            Vector3 fowards = new Vector3(1f, 0f, 0f);
            player = new Florence.ClientAssembly.game_Instance.Player(
                _models["Player"], 
                position,
                fowards, 
                rotation, 
                0
            );
            while (player == null) { /* Wait while is created */ }
            player.Set_Scale(new Vector3(10f));
        }

        public Asteroid CreateAsteroid(string model, Vector3 position)
        {
            var obj = new Asteroid(_models[model], position, Vector3.Zero, Vector3.Zero, 0f);
            obj.Set_Scale(new Vector3(1f));
            return obj;
        }

        public void Dispose()
        {
            foreach (var obj in _models)
                obj.Value.Dispose();
        }
//GET
        public Florence.ClientAssembly.game_Instance.Player Get_Player()
        {
            return player;
        }
//SET
        public void SetAdd_NewPlayer(Florence.ClientAssembly.game_Instance.Player value)
        {
            player = value;
        }
    }
}