using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florence.ClientAssembly.Outputs
{
    public class Output_Control
    {
        public Output_Control() 
        {

        }

        void SelectSetOutputSubset(
            int praiseEventId
        )
        {
            switch (praiseEventId)
            {
                case 0:
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetOutput_Instnace().GetBuffer_BackOutputDouble().SetInputBufferSubSet(Framework.GetClient().GetData().GetUserI().GetPraise0_Input());
                    break;

                case 1:
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetOutput_Instnace().GetBuffer_BackOutputDouble().SetInputBufferSubSet(Framework.GetClient().GetData().GetUserI().GetPraise1_Input());
                    break;

                case 2:
                    Florence.ClientAssembly.Framework.GetClient().GetData().GetOutput_Instnace().GetBuffer_BackOutputDouble().SetInputBufferSubSet(Framework.GetClient().GetData().GetUserI().GetPraise2_Input());
                    break;

            }
        }
    }
}
