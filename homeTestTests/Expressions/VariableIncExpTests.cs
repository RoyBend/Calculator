using Microsoft.VisualStudio.TestTools.UnitTesting;
using homeTest.Expressions;
using System;
using System.Collections.Generic;
using System.Text;
using homeTest.Common;

namespace homeTest.Expressions.Tests
{
    [TestClass()]
    public class VariableIncExpTests
    {
        [TestMethod()]
        public void GetEvaluateExpValueTest()
        {
            CasePrePlus();
            CasePostSub();
        }
        private static void CasePrePlus()
        {
            var dic = new Dictionary<char, int>();
            var varName = 'j';
            dic[varName] = 0;
            var exp = new VariableIncExp(varName, OpEnum.Add, dic, true);
           
            // op act before evalution
            Assert.AreEqual(1, exp.GetEvaluateExpValue());
        }
        private static void CasePostSub()
        {
            var dic = new Dictionary<char, int>();
            var varName = 'j';
            dic[varName] = 0;
            var exp = new VariableIncExp(varName, OpEnum.Sub, dic, false);
            
            // op act after evalution
            Assert.AreEqual(0, exp.GetEvaluateExpValue());
            Assert.AreEqual(-1, dic[varName]);
        }
    }
};