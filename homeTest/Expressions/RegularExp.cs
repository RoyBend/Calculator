using System;
using System.Collections.Generic;
using System.Text;
using homeTest.Common;
using homeTest.Exceptions;

namespace homeTest.Expressions
{
    public class RegularExp : IEvaluableExp
    {
        private IEvaluableExp m_LeftExp;
        private readonly OpEnum c_Op;
        private IEvaluableExp m_RightExp;

        public RegularExp(IEvaluableExp leftExp, OpEnum op, IEvaluableExp rightExp)
        {
            m_LeftExp = leftExp;
            c_Op = op;
            m_RightExp = rightExp;
        }
        public int GetEvaluateExpValue()
        {
            var leftValue = m_LeftExp.GetEvaluateExpValue();
            var rightValue = m_RightExp.GetEvaluateExpValue();

            switch (c_Op)
            {
                case OpEnum.Add:
                    return leftValue + rightValue;
                case OpEnum.Sub:
                    return leftValue - rightValue;
                case OpEnum.Mul:
                    return leftValue * rightValue;
                default:
                    throw new InvalidExpressionException();
            }
        }

    }
}
