using System;
using System.Diagnostics;

namespace Conditionals
{
    class Program
    {
        static void Main()
        {
            TestTernaryOperator();
            
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void TestTernaryOperator()
        {
            int numberOfTests = 11;
            int numberOfIterationsPerTest = 10_000_000;

            Stopwatch stopwatch = new Stopwatch();
            Random randon = new Random();

            float valueToConvert = (float)randon.NextDouble() * 1000.0f;
            float average = 0;

            Console.WriteLine($"Converting {valueToConvert} to hundreds, tens and units.");

            int result = 0;
            int a = 10;
            int b = 15;
            //for (int j = 0; j < numberOfTests; j++)
            //{
            //    stopwatch.Reset();
            //    stopwatch.Start();
            //    for (int i = 0; i < numberOfIterationsPerTest; i++)
            //    {
            //        if((float)randon.NextDouble() * 1000.0f > 500.0f)
            //        {
            //            result = a;
            //        }
            //        else
            //        {
            //            result = b;
            //        }
            //    }
            //    stopwatch.Stop();
            //    Console.WriteLine("If statement: " + stopwatch.ElapsedMilliseconds);

            //    average += stopwatch.ElapsedMilliseconds;
            //}
            //Console.WriteLine("Average for If statement: " + (average / numberOfTests));
            //Console.WriteLine();
            //Console.WriteLine();

            // -----------------------------------

            //average = 0;

            //for (int j = 0; j < numberOfTests; j++)
            //{
            //    stopwatch.Reset();
            //    stopwatch.Start();
            //    for (int i = 0; i < numberOfIterationsPerTest; i++)
            //    {
            //        result = ((float)randon.NextDouble() * 1000.0f > 500.0f) ? a : b;
            //    }
            //    stopwatch.Stop();
            //    Console.WriteLine("Ternary operator: " + stopwatch.ElapsedMilliseconds);

            //    average += stopwatch.ElapsedMilliseconds;
            //}
            //Console.WriteLine("Average for Ternary operator: " + (average / numberOfTests));
            //Console.WriteLine();
            //Console.WriteLine();

            // -----------------------------------

            //average = 0;

            //for (int j = 0; j < numberOfTests; j++)
            //{
            //    stopwatch.Reset();
            //    stopwatch.Start();
            //    for (int i = 0; i < numberOfIterationsPerTest; i++)
            //    {
            //        result = (randon.NextDouble() * 1000.0 > 500.0) ? a : b;
            //    }
            //    stopwatch.Stop();
            //    Console.WriteLine("Ternary operator: " + stopwatch.ElapsedMilliseconds);

            //    average += stopwatch.ElapsedMilliseconds;
            //}
            //Console.WriteLine("Average for Ternary operator: " + (average / numberOfTests));
            //Console.WriteLine();
            //Console.WriteLine();

            // -----------------------------------

            //average = 0;

            //for (int j = 0; j < numberOfTests; j++)
            //{
            //    stopwatch.Reset();
            //    stopwatch.Start();
            //    for (int i = 0; i < numberOfIterationsPerTest; i++)
            //    {
            //        _ = (randon.NextDouble() * 1000.0 > 500.0) ? a : b;
            //    }
            //    stopwatch.Stop();
            //    Console.WriteLine("Ternary operator: " + stopwatch.ElapsedMilliseconds);

            //    average += stopwatch.ElapsedMilliseconds;
            //}
            //Console.WriteLine("Average for Ternary operator: " + (average / numberOfTests));
            //Console.WriteLine();
            //Console.WriteLine();

            // -----------------------------------

            average = 0;
            bool c = true;
            for (int j = 0; j < numberOfTests; j++)
            {
                stopwatch.Reset();
                stopwatch.Start();
                for (int i = 0; i < numberOfIterationsPerTest; i++)
                {
                    result = c == true ? a : b;

                    c = !c;
                }
                stopwatch.Stop();
                Console.WriteLine("Ternary operator: " + stopwatch.ElapsedMilliseconds);

                average += stopwatch.ElapsedMilliseconds;
            }
            Console.WriteLine("Average for Ternary operator: " + (average / numberOfTests));
            Console.WriteLine();
            Console.WriteLine();

            // -----------------------------------

            average = 0;
            c = true;
            for (int j = 0; j < numberOfTests; j++)
            {
                stopwatch.Reset();
                stopwatch.Start();
                for (int i = 0; i < numberOfIterationsPerTest; i++)
                {
                    if(c == true)
                    {
                        result = a;
                    }
                    else
                    {
                        result = b;
                    }

                    c = !c;
                }
                stopwatch.Stop();
                Console.WriteLine("Ternary operator: " + stopwatch.ElapsedMilliseconds);

                average += stopwatch.ElapsedMilliseconds;
            }
            Console.WriteLine("Average for Ternary operator: " + (average / numberOfTests));
            Console.WriteLine();
            Console.WriteLine();

            // -----------------------------------
        }
    }
}
