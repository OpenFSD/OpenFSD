using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florence.ClientAssembly
{
    public class Algorithms
    {
        static private Florence.ClientAssembly.Concurrent[] concurrent;
        static private Florence.ClientAssembly.IO_Listen_Respond io_ListenRespond;
        static private Florence.ClientAssembly.Praise_Files.User_Alg user_Alg;

        public Algorithms(int numberOfCores) 
        {

            System.Console.WriteLine("Florence.ClientAssembly: Algorithms");//TEST
        }

        public void Initialise(int numberOfCores)
        {
            io_ListenRespond = new Florence.ClientAssembly.IO_Listen_Respond();
            while (io_ListenRespond == null) { /* wait untill class constructed */ }
            io_ListenRespond.InitialiseControl();

            concurrent = new Florence.ClientAssembly.Concurrent[2];//Number Of Cores - 2
            for (byte i = 0; i < (Florence.ClientAssembly.Framework.GetClient().GetGlobal().Get_NumCores() - 2); i++)
            {
                concurrent[i] = new Florence.ClientAssembly.Concurrent();
                while (concurrent[i] == null) { /* wait untill class constructed */ }
                concurrent[i].InitialiseControl();
            }
        }

        public Florence.ClientAssembly.Concurrent GetConcurrent(byte index)
        {
            return concurrent[index];
        }

        public Florence.ClientAssembly.IO_Listen_Respond GetIO_ListenRespond()
        {
            return io_ListenRespond;
        }
    }
}
