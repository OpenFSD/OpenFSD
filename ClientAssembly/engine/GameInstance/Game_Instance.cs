using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florence.ClientAssembly
{
    public class Game_Instance
    {
        static private Florence.ClientAssembly.game_Instance.Arena arena;
        static private Florence.ClientAssembly.game_Instance.Map_Default mapDefault;
        static private Florence.ClientAssembly.game_Instance.Settings settings;
        static private Florence.ClientAssembly.game_Instance.Player[] player;

        public Game_Instance()
        {
            arena = new Florence.ClientAssembly.game_Instance.Arena();
            while (arena == null) { /* Wait while is created */ }

            settings = new Florence.ClientAssembly.game_Instance.Settings();
            while (settings == null) { /* Wait while is created */ }

            mapDefault = new Florence.ClientAssembly.game_Instance.Map_Default();
            while (mapDefault == null) { /* Wait while is created */ }

            player = new Florence.ClientAssembly.game_Instance.Player[2];
            for(ushort numberOfPlayers =0; numberOfPlayers < 2; numberOfPlayers++)
            {
                player[numberOfPlayers] = new Florence.ClientAssembly.game_Instance.Player();
                while (player[numberOfPlayers] == null) { /* Wait while is created */ }
            }
        }
        public void Initalise_Graphics()
        {
            Florence.ClientAssembly.Framework.GetClient().GetExecute().Create_And_Run_Graphics();
        }

        public Florence.ClientAssembly.game_Instance.Arena GetArena()
        {
            return arena;
        }

        public Florence.ClientAssembly.game_Instance.Map_Default GetMapDefault()
        {
            return mapDefault;
        }

        public Florence.ClientAssembly.game_Instance.Settings GetSettings()
        {
            return settings;
        }

        public Florence.ClientAssembly.game_Instance.Player GetPlayer(ushort index_playerID)
        {
            return player[index_playerID];
        }

        public void SetAdd_NewPlayer(Florence.ClientAssembly.game_Instance.Player value)
        {/*
            settings.Add_Player();
            player = new Florence.ClientAssembly.Player[settings.GetNumberOfPlayers()];

            player[settings.GetNumberOfPlayers()-1] = value;

        */}
    }
}
