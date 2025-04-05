using OpenTK;

namespace Florence.ClientAssembly.Inputs
{
    public class Input_Control
    {
        public Input_Control()
        {

        }
        public void LoadValuesInToInputSubset(
            ushort praiseEventId,
            float period
        )
        {
            Florence.ClientAssembly.Inputs.Input newSLot_Stack_InputAction = Florence.ClientAssembly.Framework.GetClient().GetData().GetInput_Instnace().GetEmptyInput();
            newSLot_Stack_InputAction.SetPraiseEventId(praiseEventId);
            switch (praiseEventId)
            {
                case 0:

                    break;

                case 1:
                    Florence.ClientAssembly.Praise_Files.Praise1_Input desternation_Subset = (Florence.ClientAssembly.Praise_Files.Praise1_Input)Framework.GetClient().GetData().GetInput_Instnace().Get_Transmit_InputBuffer().Get_InputBufferSubset();
                    Vector2 mouse = Framework.GetClient().GetData().GetGame_Instance().GetPlayer(0).GetMousePos();
                    desternation_Subset.Set_Mouse_X(mouse.X);
                    desternation_Subset.Set_Mouse_Y(mouse.Y);
                    break;
            }
        }

        public void SelectSetIntputSubset(
            int praiseEventId
        )
        {
            switch (praiseEventId)
            {
                case 0:
                    break;

                case 1:
                    Florence.ClientAssembly.Praise_Files.Praise1_Input obj_praise1 = (Florence.ClientAssembly.Praise_Files.Praise1_Input)Framework.GetClient().GetData().GetUserIO().GetPraise0_Input();
                    Framework.GetClient().GetData().GetInput_Instnace().Get_Transmit_InputBuffer().Set_InputBuffer_SubSet(obj_praise1);
                    break;

		        case 2:
                    Florence.ClientAssembly.Praise_Files.Praise2_Input obj_praise2 = (Florence.ClientAssembly.Praise_Files.Praise2_Input)Framework.GetClient().GetData().GetUserIO().GetPraise1_Input();
                    Framework.GetClient().GetData().GetInput_Instnace().Get_Transmit_InputBuffer().Set_InputBuffer_SubSet(obj_praise2);
                    break;
            }
		}
    }
}
