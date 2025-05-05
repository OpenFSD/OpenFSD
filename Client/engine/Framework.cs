//using Florence.NetworkingClient;
using System;
using System.Text;

namespace Florence.ClientAssembly
{
    public class Framework
    {
        static private Florence.ClientAssembly.Client client = null;
        //static private Networking networkingClient;

        static private Int16 threadId = 0;

        public Framework() 
        {
            client = new Florence.ClientAssembly.Client();
            while (client == null){ /* Wait whileis created */ }
            client.GetExecute().Initialise();
            client.GetExecute().Initialise_Threads(Framework.GetClient().GetGlobal().Get_NumCores());

            //Florence.Concurrency.ConcurrentQue_Client.Create_ConcurrentQue();
            
           // Florence.WriteEnable.Stack_Client_OutputRecieve.Create_WriteEnable();
/*
            Valve.Sockets.Library.Initialize();
            Valve.Sockets.Library.Initialize(new StringBuilder(1024));
            Valve.Sockets.NetworkingIdentity networkingIdentity = new Valve.Sockets.NetworkingIdentity();
            Valve.Sockets.Library.Initialize(ref networkingIdentity, new StringBuilder(1024));
*/
            Framework.GetClient().GetExecute().Create_And_Run_Graphics();//ToDo re enable

            System.Console.WriteLine("Florence.ClientAssembly: Framework");//TEST
        }

        static public Florence.ClientAssembly.Client GetClient()
        {
            return client;
        }

        //static public Networking GetNetworking()
        //{
        //    return networkingClient;
        //}
    }
}
