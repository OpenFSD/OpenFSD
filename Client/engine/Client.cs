
namespace Florence.ClientAssembly
{
    public class Client
    {
        static private Florence.ClientAssembly.Algorithms algorithms;
        static private Florence.ClientAssembly.Data data;
        static private Florence.ClientAssembly.Execute execute;
        static private Florence.ClientAssembly.Global global;

        public Client() 
        {
            global = new Florence.ClientAssembly.Global();
            while (global == null) { /* Wait while is created */ }

            algorithms = new Florence.ClientAssembly.Algorithms(global.Get_NumCores());
            while (algorithms == null) { /* Wait while is created */ }

            data = new Florence.ClientAssembly.Data();
            while (data == null) { /* Wait while is created */ }
            data.InitialiseControl();

            execute = new Florence.ClientAssembly.Execute(global.Get_NumCores());
            while (execute == null) { /* Wait while is created */ }
            execute.Initialise_Control(global.Get_NumCores(), global);

            System.Console.WriteLine("Florence.ClientAssembly: Client");
        }

        public Florence.ClientAssembly.Algorithms GetAlgorithms()
        {
            return algorithms;
        }
        public Florence.ClientAssembly.Data GetData()
        {
            return data;
        }
        public Florence.ClientAssembly.Execute GetExecute()
        {
            return execute;
        }

        public Florence.ClientAssembly.Global GetGlobal()
        {
            return global;
        }
    }
}