using System;
using System.Collections.Generic;
using System.Text;

namespace homeTest.Exceptions
{
    public class InvalidOpException : Exception
    {
        public InvalidOpException()
            : base("Invalid Op in Expression")
        {

        }
    }
    
}
