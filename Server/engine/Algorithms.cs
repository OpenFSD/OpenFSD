namespace Florence.ServerAssembly
{
    public class Algorithms
    {
        static private Florence.ServerAssembly.IO_ListenRespond io_ListenRespond;

        public Algorithms(byte numberOfCores)
        {
            System.Console.WriteLine("Florence.ServerAssembly: Algorithms");//TEST
        }

        public void Initialise(byte numberOfCores)
        {
            io_ListenRespond = new Florence.ServerAssembly.IO_ListenRespond();
            while (io_ListenRespond == null) { /* wait untill class constructed */ }
            io_ListenRespond.InitialiseControl();
        }

        public Florence.ServerAssembly.IO_ListenRespond GetIO_ListenRespond()
        {
            return io_ListenRespond;
        }
    }
}