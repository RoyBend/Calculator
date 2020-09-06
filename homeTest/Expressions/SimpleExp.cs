using System;
using System.Collections.Generic;
using System.Text;

namespace homeTest.Expressions
{
    public class SimpleExp: IEvaluableExp 
    {
        private readonly int c_Value;
        public SimpleExp(int value)
        {
            c_Value = value;
        }
        public int GetEvaluateExpValue()
        {
            return c_Value;
        }

    }
}
