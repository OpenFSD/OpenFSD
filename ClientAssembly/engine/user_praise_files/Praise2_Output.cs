using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florence.ClientAssembly.Praise_Files
{
    public class Praise2_Output
    {
        Vector3 _position;
        Vector3 _foward;
        Vector3 _right;
        Vector3 _up;

        public Praise2_Output() 
        {
            _position = new Vector3(0,0,0);
            _foward = new Vector3(0, 0, 0);
            _right = new Vector3(0, 0, 0);
            _up = new Vector3(0, 0, 0);
        }

        public Vector3 Get_position()
        {
            return _position;
        }

        public Vector3 Get_fowards()
        {
            return _foward;
        }

        public Vector3 Get_right()
        {
            return _right;
        }

        public Vector3 Get_up()
        {
            return _up;
        }

    }
}
