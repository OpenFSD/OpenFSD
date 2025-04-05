using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florence.ServerAssembly
{
    public class Global
    {
        static private byte numberOfCores;

        public Global()
        {
            numberOfCores = 2;
        }

        public byte Get_NumCores()
        {
            return numberOfCores;
        }
    }
}