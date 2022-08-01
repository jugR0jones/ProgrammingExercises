using System;

namespace LogicExampes
{
    class OrderInConditionalStatement
    {
        public void Run()
        {
            FirstConditionIsFalse();
            FirstConditionIsTrue();
        }

        private void FirstConditionIsFalse()
        {
            Console.WriteLine();
            Console.WriteLine("FirstConditionIsFalse:");

            bool condition1 = false;

            if(condition1 && Condition2())
            {
                Console.WriteLine("Both conditions are true.");
            }
            else
            {
                Console.WriteLine("One condition is false.");
            }
        }

        private void FirstConditionIsTrue()
        {
            Console.WriteLine();
            Console.WriteLine("FirstConditionIsTrue:");

            bool condition1 = true;

            if (condition1 && Condition2())
            {
                Console.WriteLine("Both conditions are true.");
            }
            else
            {
                Console.WriteLine("One condition is false.");
            }
        }

        private bool Condition2()
        {
            Console.WriteLine("Condition2");
            return false;
        }
    }
}
