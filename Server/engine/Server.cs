namespace Florence.ServerAssembly
{
    public class Server
    {
        static private Florence.ServerAssembly.Algorithms algorithms;
        static private Florence.ServerAssembly.Data data;
        static private Florence.ServerAssembly.Execute execute;
        static private Florence.ServerAssembly.Global global;

        public Server()
        {
            global = new Florence.ServerAssembly.Global();
            while (global == null) { /* Wait while is created */ }

            algorithms = new Florence.ServerAssembly.Algorithms(global.Get_NumCores());
            while (algorithms == null) { /* Wait while is created */ }

            data = new Florence.ServerAssembly.Data();
            while (data == null) { /* Wait while is created */ }
            data.InitialiseControl();

            execute = new Florence.ServerAssembly.Execute();
            while (execute == null) { /* Wait while is created */ }
            execute.Initialise_Control(global.Get_NumCores(), global);

            System.Console.WriteLine("Florence.ServerAssembly: Server");
        }

        public Florence.ServerAssembly.Algorithms GetAlgorithms()
        {
            return algorithms;
        }
        public Florence.ServerAssembly.Data GetData()
        {
            return data;
        }

        public Florence.ServerAssembly.Global GetGlobal()
        {
            return global;
        }

        public Florence.ServerAssembly.Execute GetExecute()
        {
            return execute;
        }
    }
}