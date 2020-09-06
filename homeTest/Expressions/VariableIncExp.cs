using homeTest.Common;
using System;
using System.Collections.Generic;
using System.Text;
using homeTest.Exceptions;

namespace homeTest.Expressions
{
    public class VariableIncExp : IEvaluableExp 
    {
        private readonly char c_VarName;
        private readonly bool c_OpBefore;
        private Dictionary<char, int> c_EnvVars;
        private readonly OpEnum c_Op;
        public VariableIncExp(char varName, OpEnum op, Dictionary<char, int> envVars, bool opBefore = false)
        {
            c_VarName = varName;
            c_OpBefore = opBefore;
            c_EnvVars = envVars;
            c_Op = op;
        }
        public int GetEvaluateExpValue()
        {
            // cathing Undeclared Variable 
            if (!c_EnvVars.ContainsKey(c_VarName))
                throw new UndeclaredVariableException();

            var preOutPut = c_EnvVars[c_VarName];
            switch (c_Op)
            {
                case OpEnum.Add:
                    c_EnvVars[c_VarName]++;
                    break;
                case OpEnum.Sub:
                    c_EnvVars[c_VarName]--;
                    break;
            }
            
            // 2 cases: 'j++' or ++j'
            if (c_OpBefore)
            {
                return c_EnvVars[c_VarName];
            }
            return preOutPut;
        }
    }
}
