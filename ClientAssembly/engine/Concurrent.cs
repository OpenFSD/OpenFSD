
namespace Florence.ClientAssembly
{
    public class Concurrent
    {
        static private byte concurrentCoreId;
        static private Florence.ClientAssembly.Concurrent_Control concurrent_Control;

        public Concurrent() 
        {
            concurrentCoreId = 255;
            concurrent_Control = null;
        } 

        public void InitialiseControl()
        {
            concurrent_Control = new Florence.ClientAssembly.Concurrent_Control();
            while (concurrent_Control == null) { /* Wait while is created */ }
        }

        public void Thread_Concurrent()
        {
            bool done_once = true;
            while (Florence.ClientAssembly.Framework.GetClient().GetExecute().GetExecute_Control().GetFlag_ThreadInitialised(Get_CoreId()) == true)
            {
                if (done_once == true)
                {
                    Florence.ClientAssembly.Framework.GetClient().GetExecute().GetExecute_Control().SetFlag_ThreadInitialised(Get_CoreId(), false);
                    done_once = false;
                }
            }
            System.Console.WriteLine("Thread Initalised => Thread_Concurrent()");//TestBench
            while (Florence.ClientAssembly.Framework.GetClient().GetExecute().GetExecute_Control().GetFlag_Client_App_Initialised() == true)
            {

            }
            System.Console.WriteLine("Thread Starting => Thread_Concurrent()");//TestBench
            while (Florence.ClientAssembly.Framework.GetClient().GetExecute().GetExecute_Control().GetFlag_Client_App_Initialised() == false)
            {
                //todo
            }
        }

        private byte Get_CoreId()
        {
            return (byte)(2 + concurrentCoreId);
        }
        private byte Get_ConcurrentCoreId()
        {
            return concurrentCoreId;
        }
        public void Set_ConcurrentCoreId(byte value)
        { 
            concurrentCoreId = value; 
        }
    }
}
