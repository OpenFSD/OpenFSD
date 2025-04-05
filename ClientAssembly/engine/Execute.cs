using System.Threading;

namespace Florence.ClientAssembly
{
    public class Execute
    {
        static private Florence.ClientAssembly.Execute_Control execute_Control;
        static private Thread[] concurrent = null;
        static private Thread liaten_OutputRecieve = null;
        static private Thread send_InputAction = null;
        static private Thread[] threads = null;
        
        public Execute(int numberOfCores) 
        {
            execute_Control = null;
            threads = new Thread[4];//NUMBER OF CORES
            concurrent = new Thread[2];//NUMBER OF CORES
        }

        public void Initialise_Control(
            int numberOfCores,
            Global global
        )
        {
            execute_Control = new Florence.ClientAssembly.Execute_Control(numberOfCores);
            while (execute_Control == null) { /* Wait while is created */ }
        }

        public void Initialise()
        {
            Framework.GetClient().GetAlgorithms().Initialise(Framework.GetClient().GetGlobal().Get_NumCores());

        }

        public void Initialise_Threads(
            int numberOfCores
        )
        {
            threads[0] = Thread.CurrentThread;

            threads[1] = new Thread(Framework.GetClient().GetAlgorithms().GetIO_ListenRespond().Thread_Output_Respond);
            threads[1].Start();

            for (byte i = 0; i < (Florence.ClientAssembly.Framework.GetClient().GetGlobal().Get_NumCores() - 2); i++)
            {
                Framework.GetClient().GetAlgorithms().GetConcurrent(i).Set_ConcurrentCoreId(i);
                threads[i] = new Thread(Framework.GetClient().GetAlgorithms().GetConcurrent(i).Thread_Concurrent);
                threads[i].Start();
            }
        }

        public void Create_And_Run_Graphics()
        {
            //new Florence.ServerAssembly.Graphics.MainWindow().Run(60);//ToDo
        }

        public Florence.ClientAssembly.Execute_Control GetExecute_Control()
        {
            return execute_Control;
        }

        public Thread GetThread(int index)
        {
            return threads[index];
        }
    }   
}
