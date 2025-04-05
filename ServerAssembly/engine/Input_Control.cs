using Florence.ServerAssembly.UserIn;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florence.ServerAssembly.Inputs
{
    public class Input_Control
    {
        static private bool[] isSelected_PraiseEventId = new bool[0];
        static int numberOfPraises;

        public Input_Control()
        {
            numberOfPraises = 2;//move to global
            isSelected_PraiseEventId = new bool[numberOfPraises];
        }
    }
}
