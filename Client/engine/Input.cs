using System;

namespace Florence.ClientAssembly.Inputs
{
    public class Input
    {
        static private Florence.ClientAssembly.Inputs.Input_Control input_Control;
        static private Object praiseInputBuffer_Subset;
        static private UInt16 praiseEventId;

        public Input()
        {
            input_Control = null;
            praiseInputBuffer_Subset = null;
            praiseEventId = new int();
            praiseEventId = 0;
            System.Console.WriteLine("Florence.ClientAssembly: Input");
        }

        public void InitialiseControl() 
        {
            input_Control = new Florence.ClientAssembly.Inputs.Input_Control();
            while (input_Control == null) { /* Wait while is created */ }
        }

        public Object Get_InputBufferSubset()
        {
            return praiseInputBuffer_Subset;
        }

        public Florence.ClientAssembly.Inputs.Input_Control GetInputControl()
        {
            return input_Control;
        }
        public int GetPraiseEventId()
        {
            return praiseEventId;
        }

        public void Set_InputBuffer_SubSet(Object value)
        {
            praiseInputBuffer_Subset = value;
        }
        
        public void SetPraiseEventId(UInt16 value)
        {
            praiseEventId = value;
        }
    }
}