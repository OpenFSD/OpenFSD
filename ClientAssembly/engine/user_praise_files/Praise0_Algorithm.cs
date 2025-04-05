using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florence.ClientAssembly.Praise_Files
{
    public class Praise0_Algorithm
    {
        public Praise0_Algorithm() 
        { 
        
        }
        public void Do_Praise(
            Florence.ClientAssembly.Praise_Files.Praise0_Input in_SubSet,
            Florence.ClientAssembly.Praise_Files.Praise0_Output out_SubSet
        )
        {
            out_SubSet.SetFlag_IsPingActive(in_SubSet.GetFlag_IsPingActive());
        }
    }
}
