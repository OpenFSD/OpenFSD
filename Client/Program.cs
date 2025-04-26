// Client Assembly
namespace Florence.ClientAssembly
{
    class Program
    {
        private static Florence.ClientAssembly.Framework framework = null;

        static void Main(string[] args)
        {
            framework = new Florence.ClientAssembly.Framework();
            while (framework == null) { /* wait untill is created */ }

            System.Console.WriteLine("Florence.ClientAssembly START");//TEST
            while (true)
            {

            }
        }
    }
}
