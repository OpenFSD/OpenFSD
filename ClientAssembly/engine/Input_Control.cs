using OpenTK;

namespace Florence.ClientAssembly.Inputs
{
    public class Input_Control
    {
        public Input_Control()
        {

        }
/*
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
*/
        public void SelectSetIntputSubset(
            int praiseEventId
        )
        {
            switch (praiseEventId)
            {
                case 0:
                    Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Front_InputDouble().Set_InputBuffer_SubSet(Florence.ClientAssembly.Framework.GetClient().GetData().GetUserI().GetPraise0_Input());
                    break;

                case 1:
                    Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Front_InputDouble().Set_InputBuffer_SubSet(Florence.ClientAssembly.Framework.GetClient().GetData().GetUserI().GetPraise1_Input());
                    break;

		        case 2:
                    Framework.GetClient().GetData().GetInput_Instnace().GetBuffer_Front_InputDouble().Set_InputBuffer_SubSet(Florence.ClientAssembly.Framework.GetClient().GetData().GetUserI().GetPraise2_Input());
                    break;
            }
		}
    }
}
