using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayBasedStackImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            LHDFrameworkStack<int> stack = new LHDFrameworkStack<int>();

            stack.Push(1);
            PrintStack(stack);

            stack.Push(2);
            PrintStack(stack);

            stack.Push(3);
            PrintStack(stack);

            int a = stack.Peek();
            Console.WriteLine("\nTop element: " + a);

            int b = stack.Pop();
            Console.WriteLine("\nPopped element: " + b);

            PrintStack(stack);

            stack.Push(4);
            PrintStack(stack);

            stack.Push(5);
            PrintStack(stack);

            stack.Push(6);
            PrintStack(stack);

            stack.Push(7);
            PrintStack(stack);

            stack.Push(8);
            PrintStack(stack);

            stack.MoveItemToTopOfStack(1);
            PrintStack(stack);

            int c = stack.Peek(3);
            Console.WriteLine("\nPeek(3) element: " + c);

            Console.ReadKey();
            Console.WriteLine("Press any key to exit...");
        }

        private static void PrintStack(LHDFrameworkStack<int> stack)
        {
            int[] destination = new int[stack.Count];
            stack.CopyTo(destination, 0);

            Console.WriteLine("\n----------------------------");

            foreach(int i in destination)
            {
                Console.WriteLine(i);
            }
        }
    }
}
