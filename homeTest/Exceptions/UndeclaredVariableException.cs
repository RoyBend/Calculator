using System;
using System.Collections.Generic;
using System.Text;

namespace homeTest.Exceptions
{
    public class UndeclaredVariableException : Exception
    {
        public UndeclaredVariableException()
            : base("Undeclared Variable in Expression")
        {

        }
    }
}
