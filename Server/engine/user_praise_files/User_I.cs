using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florence.ServerAssembly.Praise_Files
{
    public class User_I
    {
        static private Florence.ServerAssembly.UserIn.Praise0_Input praise0_Input;
        static private Florence.ServerAssembly.UserIn.Praise1_Input praise1_Input;

        public User_I() 
        {
            praise0_Input = new Florence.ServerAssembly.UserIn.Praise0_Input();
            praise1_Input = new Florence.ServerAssembly.UserIn.Praise1_Input();
        }

        public Florence.ServerAssembly.UserIn.Praise0_Input GetPraise0_Input()
        {
            return praise0_Input;
        }

        public Florence.ServerAssembly.UserIn.Praise1_Input GetPraise1_Input()
        {
            return praise1_Input;
        }
    }
}
