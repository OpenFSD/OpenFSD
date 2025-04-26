using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florence.ClientAssembly.Praise_Files
{
    public class Praise2_Input
    {
        static private bool _fowards;
        static private bool _backwards;
        static private bool _left;
        static private bool _right;
        static private float period;

        public Praise2_Input()
        {
            _fowards = false;
            _backwards = false;
            _left = false;
            _right = false;
            period = 0;
        }

        public bool Get_Fowards()
        {
            return _fowards;
        }
        public bool Get_Backwards()
        {
            return _backwards;
        }
        public bool Get_Left()
        {
            return _left;
        }
        public bool Get_Right()
        {
            return _right;
        }

        public float GetPeriod()
        {
            return period;
        }

        public void Set_Fowards(bool value)
        {
            _fowards = value;
        }
        public void Set_Backwards(bool value)
        {
            _backwards = value;
        }
        public void Set_Left(bool value)
        {
            _left = value;
        }
        public void Set_Right(bool value)
        {
            _right = value;
        }

        public void Set_Period(float value)
        {
            period = value;
        }
    }
}
