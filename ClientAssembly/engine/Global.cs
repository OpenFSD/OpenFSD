
namespace Florence.ClientAssembly
{
    public class Global
    {
        static private int numberOfCores;

        public Global() 
        {
            numberOfCores = 2;
        }

        public int Get_NumCores()
        {
            return numberOfCores;
        }
    }
}
