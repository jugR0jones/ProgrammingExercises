using System;
using System.Diagnostics;

namespace OptimisationExercises
{
    class Program
    {
        static void Main()
        {
            //SumOddExercise exercise = new SumOddExercise();

            //exercise.Run();
            //  var summary = BenchmarkRunner.Run<SumOddExercise>();

            int numberOfTests = 11;
            int numberOfIterationsPerTest = 1_000_000;

            Stopwatch stopwatch = new Stopwatch();
            Random randon = new Random();

            float valueToConvert = (float)randon.NextDouble() * 1000.0f;
            float average = 0;

            //Console.WriteLine($"Converting {valueToConvert} to an integer.");

            SumOddExercise exercise = new SumOddExercise();

            int convertedValue = int.MaxValue;
            for (int j = 0; j < numberOfTests; j++)
            {
                stopwatch.Reset();
                stopwatch.Start();
                for (int i = 0; i < numberOfIterationsPerTest; i++)
                {
                    exercise.Run();
                }
                stopwatch.Stop();
                Console.WriteLine("Time " + stopwatch.ElapsedMilliseconds);

                average += stopwatch.ElapsedMilliseconds;
            }
            Console.WriteLine("Average for (int)valueToConvert: " + (average / numberOfTests));
            Console.WriteLine("Value: " + convertedValue);
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
