using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florence.ClientAssembly
{
    public class Data
    {
        static private Florence.ClientAssembly.Data_Control data_Control;
        static private Florence.ClientAssembly.Game_Instance gameInstance;
        static private Florence.ClientAssembly.game_Instance.Settings settings;
        //byffers
        static private Florence.ClientAssembly.Inputs.Input_Instance input_Instnace;
        static private Florence.ClientAssembly.Outputs.Output_Instance output_Instnace;
        //stack        
        static private List<Florence.ClientAssembly.Outputs.Output> stack_Client_OutputRecieves;
        //praises
        static private Florence.ClientAssembly.Praise_Files.User_I user_IO;

        static private bool state_Buffer_Input_ToWrite;
        static private bool state_Buffer_Output_ToWrite;

        public Data()
        {
            data_Control = null;
            
            gameInstance = new Florence.ClientAssembly.Game_Instance();
            while (gameInstance == null) { /* Wait while is created */ }

            settings = new Florence.ClientAssembly.game_Instance.Settings();
            while (settings == null) { /* Wait while is created */ }

            input_Instnace = new Florence.ClientAssembly.Inputs.Input_Instance();
            while (input_Instnace == null) { /* Wait while is created */ }

            output_Instnace = new Florence.ClientAssembly.Outputs.Output_Instance();
            while (output_Instnace == null) { /* Wait while is created */ }

            stack_Client_OutputRecieves = new List<Florence.ClientAssembly.Outputs.Output>();
            while (stack_Client_OutputRecieves == null) { /* Wait while is created */ }

            user_IO = new Florence.ClientAssembly.Praise_Files.User_I();
            while (user_IO == null) { /* Wait while is created */ }

            state_Buffer_Input_ToWrite = true;
            state_Buffer_Output_ToWrite = false;

            System.Console.WriteLine("Florence.ClientAssembly: Data");
        }

        public void InitialiseControl()
        {
            data_Control = new Florence.ClientAssembly.Data_Control();
            while (data_Control == null) { /* Wait while is created */ }
        }

        public void Flip_InBufferToWrite()
        {
            state_Buffer_Input_ToWrite = !state_Buffer_Input_ToWrite;
        }
        public void Flip_OutBufferToWrite()
        {
            state_Buffer_Output_ToWrite = !state_Buffer_Output_ToWrite;
        }

        public Florence.ClientAssembly.Data_Control GetData_Control()
        {
            return data_Control;
        }

        public Florence.ClientAssembly.Game_Instance GetGame_Instance()
        {
            return gameInstance;
        }

        public Florence.ClientAssembly.Inputs.Input_Instance GetInput_Instnace()
        {
            return input_Instnace;
        }
        public Florence.ClientAssembly.Outputs.Output_Instance GetOutput_Instnace()
        {
            return output_Instnace;
        }

        public bool GetState_Buffer_InputPraise_SideToWrite()
        {
            return state_Buffer_Input_ToWrite;
        }
        public bool GetState_Buffer_OutputPraise_SideToWrite()
        {
            return state_Buffer_Output_ToWrite;
        }

        public Florence.ClientAssembly.game_Instance.Settings GetSettings()
        {
            return settings;
        }

        public List<Florence.ClientAssembly.Outputs.Output> GetStack_OutputsRecieved()
        {
            return stack_Client_OutputRecieves;
        }

        public Florence.ClientAssembly.Praise_Files.User_I GetUserIO()
        {
            return user_IO;
        }
    }
}
