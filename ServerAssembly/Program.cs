using System;
using System.Diagnostics;
using Florence.ServerAssembly.Graphics;

namespace Florence.ServerAssembly
{
    static class Program
    {
        private static Florence.ServerAssembly.Framework framework_ServerAssembly = null;

        static void Main()
        {
            framework_ServerAssembly = new Florence.ServerAssembly.Framework();
            while (framework_ServerAssembly == null) { /* wait until class created */ }

            while (true)
            {

            }
        }

    }
}