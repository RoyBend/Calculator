using System;
using System.Collections.Generic;
using System.Text;

namespace homeTest.Exceptions
{
    public class InvalidExpressionException : Exception
    {
        public InvalidExpressionException()
            : base("Invalid Syntax Expression")
        {

        }
    }
    
}
