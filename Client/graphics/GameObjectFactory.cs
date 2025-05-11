using System;
using System.Collections.Generic;
using OpenTK;
using Florence.ClientAssembly.Graphics.GameObjects;
using Florence.ClientAssembly.Graphics.Renderables;
using ClientAssembly.graphics.GameObjects;
using System.Reflection;

namespace Florence.ClientAssembly.Graphics
{
    public class GameObjectFactory : IDisposable
    {
        static private List<Florence.ClientAssembly.Graphics.GameObjects.AGameObject> _floorTiles;
        private readonly Dictionary<string, ARenderable> _models;
        static private Florence.ClientAssembly.game_Instance.Player player;

        public GameObjectFactory(Dictionary<string, ARenderable> models)
        {
            _models = models;
            _floorTiles = null;
        }
        public Asteroid CreateEarth(string model, Vector3 position)
        {
            var obj = new Asteroid(_models[model], position, Vector3.Zero, Vector3.Zero, 0f);
            obj.Set_Scale(new Vector3(10f));
            return obj;
        }
        public void Create_FloorForMap(string model, Vector3 position)
        {
            _floorTiles.Add(new Cube(_models[model], position, Vector3.Zero, Vector3.Zero, 0f));
        }
        public void Create_Player()
        {
            player = new Florence.ClientAssembly.game_Instance.Player(
                _models["Player"],
                new Vector3(0f, 0f, 11f),
                Vector3.Zero,
                new Vector3(0f, 0f, 0f), 
                0
            );
            while (player == null) { /* Wait while is created */ }
            player.Set_Scale(new Vector3(1f));
        }

        public Asteroid CreateAsteroid(string model, Vector3 position)
        {
            var obj = new Asteroid(_models[model], position, Vector3.Zero, Vector3.Zero, 0f);
            obj.Set_Scale(new Vector3(10f));
            return obj;
        }

        public void Dispose()
        {
            foreach (var obj in _models)
                obj.Value.Dispose();
        }
//GET
        public Florence.ClientAssembly.Graphics.GameObjects.AGameObject Get_FloorForMap()
        {
            return _floorTiles.ElementAt(0);
        }
        public ARenderable Get_Model(string model)
        {
            return _models[model];
        }
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