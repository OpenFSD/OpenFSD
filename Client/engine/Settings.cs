using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli.Dat
{
    public class Settings
    {
        private static int refreshRate = 60;
        private static bool systemInitialised = false;

        public Settings()
        {

        }

        public int Get_refreshRate()
        {
            return refreshRate;
        }

        public static bool Get_systemInitialised()
        {
            return systemInitialised;

        }
        public void Set_refreshRate(int value)
        {
            refreshRate = value;
        }

        public static void Set_systemInitialised(bool value)
        {
            systemInitialised = value;
        }
    }
}
