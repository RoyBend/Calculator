using Microsoft.VisualStudio.TestTools.UnitTesting;
using homeTest;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace homeTest.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void EvaluateTest()
        {
            Case1();
            CasePlusPlus();
            CaseMul();
            
        }

        private void CaseMul()
        {
            // mul need to have priority in calculation
            Calculator calculator = new Calculator();
            calculator.Evaluate("i=5+10*4");
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                calculator.PrintVars();
                var eq = "(i=45)\r\n" == sw.ToString();
                Assert.IsTrue(eq);
            }
        }

        private void CasePlusPlus()
        {
            // evalution of '++" syntax
            Calculator calculator = new Calculator();
            calculator.Evaluate("i=5");
            calculator.Evaluate("j=i++");
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                calculator.PrintVars();
                var eq = "(i=6,j=5)\r\n" == sw.ToString();
                Assert.IsTrue(eq);
            }
        }

        private static void Case1()
        {
            // override same var
            Calculator calculator = new Calculator();
            calculator.Evaluate("i=5");
            calculator.Evaluate("i=6");
            calculator.Evaluate("i+=1");
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                calculator.PrintVars();
                var eq = "(i=7)\r\n" == sw.ToString();
                Assert.IsTrue(eq);
            }
        }
    }
}

namespace homeTestTests
{
    class CalculatorTests
    {
    }
}
