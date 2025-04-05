using Florence.ServerAssembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florence.ServerAssembly.Outputs
{
    public class Output_Instance
    {
        static private Florence.ServerAssembly.Outputs.Output empty_OutputBuffer;
        static private Florence.ServerAssembly.Outputs.Output[] outputDoubleBuffer;
        static private Florence.ServerAssembly.Outputs.Output transmitOutputBuffer;

        public Output_Instance()
        {
            empty_OutputBuffer = new Florence.ServerAssembly.Outputs.Output();
            while (empty_OutputBuffer == null) { /* Wait while is created */ }
            empty_OutputBuffer.InitialiseControl();

            outputDoubleBuffer = new Florence.ServerAssembly.Outputs.Output[2];
            for (byte index = 0; index < 2; index++)
            {
                outputDoubleBuffer[index] = empty_OutputBuffer;
                while (outputDoubleBuffer == null) { /* Wait while is created */ }
            }

            transmitOutputBuffer = new Florence.ServerAssembly.Outputs.Output();
            while (transmitOutputBuffer == null) { /* Wait while is created */ }

        }

        private ushort BoolToInt16(bool value)
        {
            ushort temp = new ushort();
            if (value)
            {
                temp = 1;
            }
            else if (!value)
            {
                temp = 0;
            }
            return temp;
        }

        public Florence.ServerAssembly.Outputs.Output GetEmptyOutput()
        {
            return empty_OutputBuffer;
        }
        public Florence.ServerAssembly.Outputs.Output GetBuffer_FrontOutputDouble()
        {
            return outputDoubleBuffer[BoolToInt16(Florence.ServerAssembly.Framework.GetGameServer().GetData().GetState_Buffer_OutputPraise_SideToWrite())];
        }
        public Florence.ServerAssembly.Outputs.Output GetBuffer_BackOutputDouble()
        {
            return outputDoubleBuffer[BoolToInt16(!Florence.ServerAssembly.Framework.GetGameServer().GetData().GetState_Buffer_OutputPraise_SideToWrite())];
        }

        public Florence.ServerAssembly.Outputs.Output GetTransmitOutputBuffer()
        {
            return transmitOutputBuffer;
        }


        public void SetBuffer_Output(Florence.ServerAssembly.Outputs.Output value)
        {
            outputDoubleBuffer[BoolToInt16(!Framework.GetGameServer().GetData().GetState_Buffer_InputPraise_SideToWrite())] = value;
        }
    }
}
