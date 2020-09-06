using System;
using System.Collections.Generic;
using System.Text;
using homeTest.Expressions;
using homeTest.Common;

namespace homeTest
{
    public class Calculator
    {
        private Dictionary<char, int> m_EnvironmentVars =  new Dictionary<char, int>();
        public void Evaluate(string exp)
        {
            ExpressionBuilder expBuilder = new ExpressionBuilder();
            
            // build the exp form the input string
            IEvaluableExp evaluableExp = expBuilder.BuildExp(exp, m_EnvironmentVars);

            // update new var
            m_EnvironmentVars[expBuilder.Variable] = evaluableExp.GetEvaluateExpValue();

        }
        public void PrintVars()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            int loops = 0;

            // itarate over all saved local vars
            foreach(var varExp in m_EnvironmentVars.Keys)
            {
                loops++;
                // for not  printing en extra ','
                var ending = loops < m_EnvironmentVars.Keys.Count ? ",": string.Empty;
                sb.Append($"{varExp}={m_EnvironmentVars[varExp]}{ending}"); 
            }
            sb.Append(")");
            Console.WriteLine(sb.ToString());
        }
    }
}
