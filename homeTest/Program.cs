using System;
using System.ComponentModel.DataAnnotations;
using homeTest.Exceptions;

namespace homeTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();

            Console.WriteLine("Taboola home-test based calculator\n" +
                    "The following operations are supported: addition, subtraction, multiplication, pre-inc/dec, and post-inc/dec.\n" +
                    "declartion of char variables only\n");
     
            while (true)
            {
                Console.WriteLine("Enter exp or 'e' to exit: [example: j=5+10]");
                var curExp = Console.ReadLine();
                
                // exit point
                if (curExp == "e"){
                    break;
                }

                try
                {
                    calculator.Evaluate(curExp);
                }

                catch (Exception ex)
                {   
                    //cathing known issues(syntax or undecler var)
                    if (ex is InvalidExpressionException || ex is InvalidOpException || ex is UndeclaredVariableException)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }

            Console.WriteLine("printing saved vars:\n");
            calculator.PrintVars();
        }
    }
}
