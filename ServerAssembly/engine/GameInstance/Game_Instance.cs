namespace Florence.ServerAssembly
{
    public class Game_Instance
    {
        static private Florence.ServerAssembly.game_Instance.Arena arena;
        //static private Florence.ServerAssembly.game_Instance.Map_Default mapDefault;
        //static private Florence.ServerAssembly.game_Instance.Player[] player;

        public Game_Instance()
        {
            arena = new Florence.ServerAssembly.game_Instance.Arena();
            while (arena == null) { /* Wait while is created */ }

            //mapDefault = new Florence.ServerAssembly.game_Instance.Map_Default();
            //while (mapDefault == null) { /* Wait while is created */ }

           // player = new Florence.ServerAssembly.game_Instance.Player[2];
           // for (ushort numberOfPlayers = 0; numberOfPlayers < 2; numberOfPlayers++)
           // {
           //     player[numberOfPlayers] = new Florence.ServerAssembly.game_Instance.Player();
           ///     while (player[numberOfPlayers] == null) { /* Wait while is created */ }
           // }
        }
        public void Initalise_Graphics()
        {
            /*
            using (var graphics = new Florence.ServerAssembly.game_Instance.gFX.Graphics(
                Framework.GetGameServer().GetData().GetGame_Instance().GetSettings().GetGameWindowSettings(),
                Framework.GetGameServer().GetData().GetGame_Instance().GetSettings().GetNativeWindowSettings()
            ))
            {
                graphics.Run();
            }
            *///ToDo
        }

        public Florence.ServerAssembly.game_Instance.Arena GetArena()
        {
            return arena;
        }
        /*
        public Florence.ServerAssembly.game_Instance.Map_Default GetMapDefault()
        {
            return mapDefault;
        }
        */

       // public Florence.ServerAssembly.game_Instance.Player GetPlayer(ushort index_playerID)
       // {
      //      return player[index_playerID];
       // }

        //public void SetAdd_NewPlayer(Florence.ServerAssembly.game_Instance.Player value)
        //{
        //    settings.Add_Player();
       //    player = new Florence.ServerAssembly.Player[settings.GetNumberOfPlayers()];

        //    player[settings.GetNumberOfPlayers()-1] = value;
        //}
    }
}
