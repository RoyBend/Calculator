using System;
using System.Collections.Generic;
using System.Text;
using homeTest.Exceptions;

namespace homeTest.Expressions
{
    public class VariableExp: IEvaluableExp 
    {
        private readonly char c_VarName;
        private readonly Dictionary<char, int> c_EnvVars;
        public VariableExp(char varName, Dictionary<char, int> envVars)
        {
            c_VarName = varName;
            c_EnvVars = envVars;
        }
        public int GetEvaluateExpValue()
        {
            // cathing Undeclared Variable 
            if (!c_EnvVars.ContainsKey(c_VarName))
                throw new UndeclaredVariableException();

            return c_EnvVars[c_VarName];
        }

    }
}
