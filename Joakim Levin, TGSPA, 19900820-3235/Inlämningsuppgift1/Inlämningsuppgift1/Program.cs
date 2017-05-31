using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//Joakim Levin, TGSPA, 19900820
namespace Inlämningsuppgift1
{
    class Program
    {
        //Tests the stack functions.
        static void UppgiftA(Stack stack)
        {
            //Adds three elements to the stack.
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Console.WriteLine("The stack count is: " + stack.Count());
            Console.WriteLine("Two pops results in: " + stack.Pop() + ", " + stack.Pop());
            Console.WriteLine("Let's peek at the stack: " + stack.Peek());
        }

        //Parses a text and makes sure it contains the right brackets in the right order.
        static void UppgiftB(Stack stack)
        {
            Console.WriteLine("\nEnter a text to be parsed.\n");

            string correctText = "The text input is correct, you may proceed.";
            string incorrectText = "The text contains an error. Please check your text.";
            string textInput = Console.ReadLine();

            int error = 0;

            foreach (char c in textInput)
            {
                if (c == '{' || c == '(' || c == '[')
                {
                    stack.Push(c);
                }

                if (c == '}' || c == ')' || c == ']')
                {
                    char data = Convert.ToChar(stack.Pop()); //Converts the poped data to a char.

                    if (c == '}' && data != '{')
                    {
                        error++;
                    }

                    if (c == ')' && data != '(')
                    {
                        error++;
                    }

                    if (c == ']' && data != '[')
                    {
                        error++;
                    }
                }
            }

            if (error != 0 || error == 0 && stack.Count() > 0)
            {
                Console.WriteLine(incorrectText);
            }
            else
            {
                Console.WriteLine(correctText);
            }
        }

        static void Main(string[] args)
        {
            Stack stackA = new Stack();
            Stack stackB = new Stack();

            UppgiftA(stackA);
            UppgiftB(stackB);

            Console.ReadLine();

        }

        
    }
}
