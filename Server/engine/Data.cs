
using System;
using System.Collections.Generic;

namespace Florence.ServerAssembly
{
    public class Data
    {
        static private Florence.ServerAssembly.Data_Control data_Control;
        static private Florence.ServerAssembly.Game_Instance gameInstance;
        //static private Florence.ServerAssembly.game_Instance.Settings settings;
        //byffers
        static private Florence.ServerAssembly.Inputs.Input_Instance input_Instnace;
        static private Florence.ServerAssembly.Outputs.Output_Instance output_Instnace;
        //stacks        
        static private List<Florence.ServerAssembly.Inputs.Input> stack_InputPraise;
        static private List<Florence.ServerAssembly.Outputs.Output> stack_OutputPraise;
        //praises
        static private Florence.ServerAssembly.Praise_Files.User_I user_I;
        static private Florence.ServerAssembly.Praise_Files.User_O user_O;

        static private bool state_Buffer_InputPraise_SideToWrite;
        static private bool state_Buffer_OutputPraise_SideToWrite;

        public Data()
        {
            data_Control = null;
            gameInstance = new Florence.ServerAssembly.Game_Instance();
            while (gameInstance == null) { /* Wait while is created */ }

            //settings = new Florence.ServerAssembly.game_Instance.Settings();
            //while (settings == null) { /* Wait while is created */ }

            input_Instnace = new Florence.ServerAssembly.Inputs.Input_Instance();
            output_Instnace = new Florence.ServerAssembly.Outputs.Output_Instance();

            user_I = new Florence.ServerAssembly.Praise_Files.User_I();
            while (user_I == null) { /* Wait while is created */ }

            user_O = new Florence.ServerAssembly.Praise_Files.User_O();
            while (user_O == null) { /* Wait while is created */ }

            state_Buffer_InputPraise_SideToWrite = true;
            state_Buffer_OutputPraise_SideToWrite = false;
         
            System.Console.WriteLine("Florence.ServerAssembly: Data");
        }

        public Int16 BoolToInt16(bool value)
        {
            Int16 temp = new Int16();
            if (value)
            {
                temp = (Int16)1;
            }
            else if (!value)
            {
                temp = (Int16)0;
            }
            return temp;
        }
        
        public void InitialiseControl()
        {
            data_Control = new Florence.ServerAssembly.Data_Control();
            while (data_Control == null) { /* Wait while is created */ }
        }

        public void Flip_InBufferToWrite()
        {
            state_Buffer_InputPraise_SideToWrite = !state_Buffer_InputPraise_SideToWrite;
        }
        public void Flip_OutBufferToWrite()
        {
            state_Buffer_OutputPraise_SideToWrite = !state_Buffer_OutputPraise_SideToWrite;
        }

        public Florence.ServerAssembly.Data_Control GetData_Control()
        {
            return data_Control;
        }

        public Florence.ServerAssembly.Game_Instance GetGame_Instance()
        {
            return gameInstance;
        }
        public Florence.ServerAssembly.Inputs.Input_Instance GetInput_Instnace()
        {
            return input_Instnace;
        }
        public Florence.ServerAssembly.Outputs.Output_Instance GetOutput_Instnace()
        {
            return output_Instnace;
        }
        public bool GetState_Buffer_InputPraise_SideToWrite()
        {
            return state_Buffer_InputPraise_SideToWrite;
        }
        public bool GetState_Buffer_OutputPraise_SideToWrite()
        {
            return state_Buffer_OutputPraise_SideToWrite;
        }

        //public Florence.ServerAssembly.game_Instance.Settings GetSettings()
        //{
        //    return settings;
        //}
        public List<Florence.ServerAssembly.Inputs.Input> GetStack_InputPraise()
        {
            return stack_InputPraise;
        }
        public List<Florence.ServerAssembly.Outputs.Output> GetStack_OutputsPraise()
        {
            return stack_OutputPraise;
        }

        public Florence.ServerAssembly.Praise_Files.User_I GetUserI()
        {
            return user_I;
        }
    }
}
