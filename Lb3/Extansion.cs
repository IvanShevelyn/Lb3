using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lb3
{
    public static class ExtensionMethods
    {
        public static double Do_Investment(this Factory Factory, double value)
        {
            Random rnd = new Random();
            int num = rnd.Next(2, 5);
            double result = value * num;
            return result;
        }
    }
}
