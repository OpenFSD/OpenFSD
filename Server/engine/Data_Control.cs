namespace Florence.ServerAssembly
{
    public class Data_Control
    {
        static private bool[] isPraiseActive;

        public Data_Control() 
        {
            isPraiseActive = new bool[6];//Number Of Praises
            for (int index = 0; index < 6; index++)//Number Of Praises
            {
                isPraiseActive[index] = false;
            }
        }
        public bool GetFlag_IsPraiseEvent(int praiseEventId)
        {
            return isPraiseActive[praiseEventId];
        }
        public void SetIsPraiseEvent(int praiseEventId, bool value)
        {
            isPraiseActive[praiseEventId] = value;
        }
    }
}
