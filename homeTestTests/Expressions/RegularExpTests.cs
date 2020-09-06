using Microsoft.VisualStudio.TestTools.UnitTesting;
using homeTest.Expressions;
using System;
using System.Collections.Generic;
using System.Text;
using homeTest.Common;

namespace homeTest.Expressions.Tests
{
    [TestClass()]
    public class RegularExpTests
    {
        [TestMethod()]
        public void GetEvaluateExpValueTest()
        {
            // case +:
            CasePlus();

            // case *:
            CaseMul();
        }

        private static void CasePlus()
        {
            var left = new SimpleExp(5);
            var right = new SimpleExp(6);
            var exp = new RegularExp(left, OpEnum.Add, right);
            Assert.AreEqual(11, exp.GetEvaluateExpValue());
        }

        private static void CaseMul()
        {
            var left = new SimpleExp(5);
            var right = new SimpleExp(2);
            var exp = new RegularExp(left, OpEnum.Mul, right);
            Assert.AreEqual(10, exp.GetEvaluateExpValue());
        }

    }
}