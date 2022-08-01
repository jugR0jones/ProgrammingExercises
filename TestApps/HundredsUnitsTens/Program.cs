using System;
using System.Diagnostics;

namespace HundredsUnitsTens
{
    class Program
    {
        static void Main()
        {
            TestConversionToUnits();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void TestConversionToUnits()
        {
            int numberOfTests = 11;
            int numberOfIterationsPerTest = 10_000_000;

            Stopwatch stopwatch = new Stopwatch();
            Random randon = new Random();

            float valueToConvert = (float)randon.NextDouble() * 1000.0f;
            float average = 0;

            Console.WriteLine($"Converting {valueToConvert} to hundreds, tens and units.");

            for (int j = 0; j < numberOfTests; j++)
            {
                stopwatch.Reset();
                stopwatch.Start();
                for (int i = 0; i < numberOfIterationsPerTest; i++)
                {
                    OldConversionRoutineWithExtensionMethod(valueToConvert);
                }
                stopwatch.Stop();
                Console.WriteLine("Old Routine with extension method: " + stopwatch.ElapsedMilliseconds);

                average += stopwatch.ElapsedMilliseconds;
            }
            Console.WriteLine("Average for old routine with extension method: " + (average / numberOfTests));
            PrintResultForOldConversionRoutineWithExtensionMethod(valueToConvert);
            Console.WriteLine();
            Console.WriteLine();

            // -----------------------------------

            average = 0;

            for (int j = 0; j < numberOfTests; j++)
            {
                stopwatch.Reset();
                stopwatch.Start();
                for (int i = 0; i < numberOfIterationsPerTest; i++)
                {
                    OldConversionRoutineWithoutExtensionMethod(valueToConvert);
                }
                stopwatch.Stop();
                Console.WriteLine("Old Routine without extension method: " + stopwatch.ElapsedMilliseconds);

                average += stopwatch.ElapsedMilliseconds;
            }
            Console.WriteLine("Average for old routine without extension method: " + (average / numberOfTests));
            PrintResultForOldConversionRoutineWithoutExtensionMethod(valueToConvert);
            Console.WriteLine();
            Console.WriteLine();

            // -----------------------------------

            average = 0;

            for (int j = 0; j < numberOfTests; j++)
            {
                stopwatch.Reset();
                stopwatch.Start();
                for (int i = 0; i < numberOfIterationsPerTest; i++)
                {
                    NewConversionRoutine(valueToConvert);
                }
                stopwatch.Stop();
                Console.WriteLine("New Routine: " + stopwatch.ElapsedMilliseconds);

                average += stopwatch.ElapsedMilliseconds;
            }
            Console.WriteLine("Average for new routine: " + (average / numberOfTests));
            PrintResultForNewConversionRoutine(valueToConvert);

            // -----------------------------------
        }

        private static void OldConversionRoutineWithExtensionMethod(float value)
        {
            int intValue, hundreds, tens, units;

            intValue = value.ToInt();
            units = intValue % 10;
            tens = (intValue / 10) % 10;
            hundreds = (intValue / 100) % 10;
        }

        private static void PrintResultForOldConversionRoutineWithExtensionMethod(float value)
        {
            int intValue, hundreds, tens, units;

            intValue = value.ToInt();
            units = intValue % 10;
            tens = (intValue / 10) % 10;
            hundreds = (intValue / 100) % 10;

            Console.WriteLine($"{hundreds}.{tens}.{units}");
        }

        private static void OldConversionRoutineWithoutExtensionMethod(float value)
        {
            int intValue, hundreds, tens, units;

            intValue = (int)value;

            units = intValue % 10;
            tens = (intValue / 10) % 10;
            hundreds = (intValue / 100) % 10;
        }

        private static void PrintResultForOldConversionRoutineWithoutExtensionMethod(float value)
        {

            int intValue = value.ToInt();
            int units = intValue % 10;
            int tens = (intValue / 10) % 10;
            int hundreds = (intValue / 100) % 10;

            Console.WriteLine($"{hundreds}.{tens}.{units}");
        }

        private static void NewConversionRoutine(float value)
        {
            int intValue = (int)value;
            int hundreds, tens, units;

            hundreds = (intValue / 100);

            intValue = intValue - (hundreds * 100);

            tens = intValue / 10;

            intValue = intValue - tens * 10;

            units = intValue;
        }

        private static void PrintResultForNewConversionRoutine(float value)
        {
            int intValue, hundreds, tens, units;

            intValue = value.ToInt();
            units = intValue % 10;
            tens = (intValue / 10) % 10;
            hundreds = (intValue / 100) % 10;

            Console.WriteLine($"{hundreds}.{tens}.{units}");
        }
    }
}
