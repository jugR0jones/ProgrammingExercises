using System;
using System.Collections.Generic;

namespace SpellingNumbers
{
    class Program
    {
        static void Main()
        {
            int[] testData =
            {
                1, 5, 10, 16, 25, 40, 73, 112, 123, 179, 234, 519, 863, 999, 1000
            };
            
            foreach(int number in testData)
            {
                //string output = FirstDraft.Convert(number);
                //string output = SecondDraft.Convert(number);
                string output = ThirdDraft.Convert(number);
                
                Console.WriteLine(number + ": " + output);
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}