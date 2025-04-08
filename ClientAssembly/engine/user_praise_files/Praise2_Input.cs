using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florence.ClientAssembly.Praise_Files
{
    public class Praise2_Input
    {
        static private float position_X;
        static private float position_Y;
        static private float position_Z;
        static private bool _fowards;
        static private bool _backwards;
        static private bool _left;
        static private bool _right;
        static private float period;

        public Praise2_Input()
        {
            position_X = 0;
            position_Y = 0;
            position_Z = 0;
            period = 0;
        }

        public bool Get_Propergation()
        {
            return _fowards;
        }
        public bool Get_Sidewayes()
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
        public float Get_Position_X() 
        {   
            return position_X; 
        }

        public float Get_Position_Y()
        {
            return position_Y;
        }

        public float Get_Position_Z()
        {
            return position_Z;
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

        public void Set_Position_X(float value) 
        {
            position_X = value;
        }
        
        public void Set_Position_Y(float value)
        {
            position_Y = value;
        }

        public void Set_Position_Z(float value)
        {
            position_Z = value;
        }

        public void Set_Period(float value)
        {
            period = value;
        }
    }
}
