using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florence.ServerAssembly.UserIn
{
    public class Praise1_Input
    {
        static private Int16 position_X;
        static private Int16 position_Y;

        public Praise1_Input()
        {
            position_X = 0;
            position_Y = 0;
        }

        public Int16 Get_Position_X() 
        {   
            return position_X; 
        }

        public Int16 Get_Position_Y()
        {
            return position_Y;
        }

        public void Set_Position_X(Int16 value) 
        {
            position_X = value;
        }
        
        public void Set_Position_Y(Int16 value)
        {
            position_Y = value;
        }
    }
}
