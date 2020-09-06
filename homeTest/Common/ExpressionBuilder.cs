using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using homeTest.Expressions;
using homeTest.Exceptions;
using System.Linq;

namespace homeTest.Common
{
    public class ExpressionBuilder
    {
        // the new var from the exp ( "j = ...")
        public char Variable { get; set; }
        public ExpressionBuilder() { }
        public IEvaluableExp BuildExp(string exp, Dictionary<char, int> envVars)
        {
            if (!isValid(exp))
            {
                throw new InvalidExpressionException();
            }

            bool regularAss;
            OpEnum op; 
            (Variable, regularAss, op) = ExtractVar(exp);
            var postExp = exp.Split('=')[1];
            // handle potential '+=' assignment
            if (!regularAss)
            {
                var leftexp = new VariableExp(Variable, envVars);

                // continue evalute next exp from the postfix of the string
                return new RegularExp(leftexp, op, BuildExpAfterAss(postExp, envVars));
            }

            return BuildExpAfterAss(postExp, envVars);
        }

        private IEvaluableExp BuildExpAfterAss(string exp, Dictionary<char, int> envVars)
        {
            if (string.IsNullOrEmpty(exp))
            {
                throw new InvalidExpressionException();
            }

            // special case "++/--" excluding ( '5++'+j )
            if (exp.Length > 2 && !Char.IsDigit(exp[0]) && (exp.Substring(0,3).Contains("++") || exp.Substring(0,3).Contains("--")))
            {
                IEvaluableExp leftexp = BuildExpPre(exp.Substring(0, 3), envVars);
                return exp.Length == 3 ? leftexp :
                    new RegularExp(leftexp, GetOP(exp[3]), BuildExpAfterAss(exp.Substring(4), envVars));

            }

            // +/- evalutint first order not important
            if (exp.Contains('+'))
            {
                var pivot = exp.IndexOf('+');
                (string lefExp, string rightExp) = SplitExpByPivot(exp, pivot);
                return new RegularExp(BuildExpAfterAss(lefExp, envVars), OpEnum.Add, BuildExpAfterAss(rightExp, envVars));
            }
            else if (exp.Contains('-'))
            {
                var pivot = exp.IndexOf('-');
                (string lefExp, string rightExp) = SplitExpByPivot(exp, pivot);
                return new RegularExp(BuildExpAfterAss(lefExp, envVars), OpEnum.Sub, BuildExpAfterAss(rightExp, envVars));
            }

            // mul is evaluted last because it has priorty in calculation over (+, -)
            else if (exp.Contains('*'))
            {
                var pivot = exp.IndexOf('*');
                (string lefExp, string rightExp) = SplitExpByPivot(exp, pivot);
                return new RegularExp(BuildExpAfterAss(lefExp, envVars), OpEnum.Mul, BuildExpAfterAss(rightExp, envVars));
            }

            // base case: exp is var('j') or num(79)
            if (IsSimpleExp(exp))
            {
                return new SimpleExp(int.Parse(exp));
            }
            else
            {
                if (exp.Length > 1)
                    throw new InvalidExpressionException();

                return new VariableExp(exp[0], envVars);
            }

        }

        private (string lefExp, string rightExp) SplitExpByPivot(string exp, int pivot)
        {
            // spliting the exp to 2  by pivot ["j++ + 5*8" pivot=3]
            if (string.IsNullOrEmpty(exp))
            {
                return (exp, exp);
            }
            else if(pivot == 0)
            {
                return (string.Empty, exp.Substring(1));
            }
            else if(pivot == exp.Length - 1)
            {
                return (exp.Substring(0, exp.Length - 1), string.Empty);
            }
            else
            {
                return (exp.Substring(0, pivot), exp.Substring(pivot + 1));
            }
        }

        private bool IsSimpleExp(string exp)
        {
            return int.TryParse(exp, out _);
        }

        private IEvaluableExp BuildExpPre(string exp, Dictionary<char, int> envVars)
        {
            // build exp case: "j++" or "++j" :
            OpEnum curOP = OpEnum.Add;
            bool opBefore;
            char var;
            if (exp[0] == '+' || exp[0] == '-') {
                opBefore = true;
                var = exp[2];
            }
            else
            {
                opBefore = false;
                var = exp[0];
            }

            // Extractor the right op
            curOP = exp.Contains("++") ? OpEnum.Add : OpEnum.Sub;
 
            return new VariableIncExp(var, curOP, envVars, opBefore);
        }

        private (char,bool, OpEnum) ExtractVar(string exp)
        {
            var localVar = exp.Split('=')[0];
            // can be : (var = {exp}) or (var[+/-/*] = {exp}) , var is char
            if (localVar.Length > 2)
                throw new Exception();
            else if(localVar.Length == 2)
            {
                return (localVar[0],false, GetOP(localVar[1]));
            }
            return (localVar[0],true, OpEnum.Add);
        }

        private OpEnum GetOP(char op)
        {
            if (op == '+')
            {
                return OpEnum.Add;
            }
            else if (op == '*')
            {
                return OpEnum.Mul;
            }
            else if (op == '-')
            {
                return OpEnum.Sub;
            }
            else
                throw new InvalidOpException();
        }

        private bool isValid(string exp)
        {
            if (exp.Split('=').Length != 2 || exp.Contains("***"))
                return false;
 
            return exp.All(c => Char.IsLetterOrDigit(c) || c == '=' || c == '+' || c == '-' || c == '*');
        }
    }
}
