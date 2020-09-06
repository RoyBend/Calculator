using Microsoft.VisualStudio.TestTools.UnitTesting;
using homeTest.Expressions;
using System;
using System.Collections.Generic;
using System.Text;
using homeTest.Exceptions;

namespace homeTest.Expressions.Tests
{
    [TestClass()]
    public class VariableExpTests
    {
        [TestMethod()]
        public void GetEvaluateExpValueTest()
        {
            var dic = new Dictionary<char, int>();
            var varName = 'j';
            dic[varName] = 0;
            var exp = new VariableExp(varName, dic);

            // op act before evalution
            Assert.AreEqual(0, exp.GetEvaluateExpValue());

            // undeclared var exception pop out
            try
            {
                exp = new VariableExp('x', dic);
                var r = exp.GetEvaluateExpValue();
            }
            catch(Exception ex)
            {
                bool exType = ex is UndeclaredVariableException;
                Assert.IsTrue(exType);
            }
        }
    }
}