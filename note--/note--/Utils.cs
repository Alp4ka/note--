using System;
using System.Collections.Generic;
using System.Text;

namespace note__
{
    public static class Utils
    {
        public static int ReturnEdged(int l, int r, int value)
        {
            if(value < l)
            {
                return l;
            }
            if (value > r) 
            {
                return r;
            }
            return value;
 
        }
    }
}
