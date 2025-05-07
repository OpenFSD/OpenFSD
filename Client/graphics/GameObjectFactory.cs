using System;
using System.Collections.Generic;
using OpenTK;
using Florence.ClientAssembly.Graphics.GameObjects;
using Florence.ClientAssembly.Graphics.Renderables;

namespace Florence.ClientAssembly.Graphics
{
    public class GameObjectFactory : IDisposable
    {
        private static Florence.ClientAssembly.Graphics.GameObjects.MapFloor[][] floorTile;
        private readonly Dictionary<string, ARenderable> _models;
        static private Florence.ClientAssembly.game_Instance.Player player;

        public GameObjectFactory(Dictionary<string, ARenderable> models)
        {
            _models = models;
            MapFloor aFloorObject = new MapFloor(_models["Floor"], new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0);
            floorTile = new MapFloor[10][];
            for (int i = 0; i < 10; i++)
            {
                floorTile[i] = [aFloorObject, aFloorObject, aFloorObject, aFloorObject, aFloorObject, aFloorObject, aFloorObject, aFloorObject, aFloorObject, aFloorObject];
            }
        }
        public Asteroid CreateEarth(string model, Vector3 position)
        {
            var obj = new Asteroid(_models[model], position, Vector3.Zero, Vector3.Zero, 0f);
            obj.Set_Scale(new Vector3(10f));
            return obj;
        }
        public void Create_MapFloor()
        {
            for (int index_X = 0; index_X < 10; index_X++)
            {
                for (int index_Y = 0; index_Y < 10; index_Y++)
                {
                    floorTile[index_X][index_Y] = new Florence.ClientAssembly.Graphics.GameObjects.MapFloor(
                        _models["Floor"], 
                        new Vector3((float)(index_X + 0.5f), (float)(index_Y + 0.5f), 0), 
                        new Vector3(0, 0, 0), 
                        new Vector3(0, 0, 0), 
                        0
                    );
                }
            }
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
        public Florence.ClientAssembly.Graphics.GameObjects.MapFloor[][] Get_Floor()
        {
            return floorTile;
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