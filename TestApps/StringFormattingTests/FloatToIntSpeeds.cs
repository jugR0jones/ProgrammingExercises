using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StringFormattingTests
{
    class FloatToIntSpeeds
    {

        public void Run()
        {
            int numberOfTests = 11;
            int numberOfIterationsPerTest = 10_000_000;

            Stopwatch stopwatch = new Stopwatch();
            Random randon = new Random();

            float valueToConvert = (float)randon.NextDouble() * 1000.0f;
            float average = 0;

            Console.WriteLine($"Converting {valueToConvert} to an integer.");

            int convertedValue = int.MaxValue;
            for (int j = 0; j < numberOfTests; j++)
            {
                stopwatch.Reset();
                stopwatch.Start();
                for (int i = 0; i < numberOfIterationsPerTest; i++)
                {
                    convertedValue = (int)valueToConvert;
                }
                stopwatch.Stop();
                Console.WriteLine("(int)valueToConvert: " + stopwatch.ElapsedMilliseconds);

                average += stopwatch.ElapsedMilliseconds;
            }
            Console.WriteLine("Average for (int)valueToConvert: " + (average / numberOfTests));
            Console.WriteLine("Value: " + convertedValue);
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
                    convertedValue = (int)Math.Round(valueToConvert);
                }
                stopwatch.Stop();
                Console.WriteLine("(int)Math.Round(): " + stopwatch.ElapsedMilliseconds);

                average += stopwatch.ElapsedMilliseconds;
            }
            Console.WriteLine("Average for (int)Math.Round(): " + (average / numberOfTests));
            Console.WriteLine("Value: " + convertedValue);
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
                    convertedValue = (int)Math.Round(valueToConvert, 0);
                }
                stopwatch.Stop();
                Console.WriteLine("(int)Math.Round(, 0): " + stopwatch.ElapsedMilliseconds);

                average += stopwatch.ElapsedMilliseconds;
            }
            Console.WriteLine("Average for (int)Math.Round(, 0): " + (average / numberOfTests));
            Console.WriteLine("Value: " + convertedValue);
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
                    convertedValue = Convert.ToInt32(valueToConvert);
                }
                stopwatch.Stop();
                Console.WriteLine("Convert.ToInt32(): " + stopwatch.ElapsedMilliseconds);

                average += stopwatch.ElapsedMilliseconds;
            }
            Console.WriteLine("Average for Convert.ToInt32(): " + (average / numberOfTests));
            Console.WriteLine("Value: " + convertedValue);
            Console.WriteLine();
            Console.WriteLine();

            // -----------------------------------

            //  average = 0;

            //string output = "";
            //for (int j = 0; j < numberOfTests; j++)
            //{
            //    stopwatch.Reset();
            //    stopwatch.Start();
            //    for (int i = 0; i < numberOfIterationsPerTest; i++)
            //    {
            //        output = valueToConvert.ToString("f0");
            //    }
            //    stopwatch.Stop();
            //    Console.WriteLine("speedInKPH.ToString(f0): " + stopwatch.ElapsedMilliseconds);

            //    average += stopwatch.ElapsedMilliseconds;
            //}
            //Console.WriteLine("Average for speedInKPH.ToString(f0): " + (average / numberOfTests));
            //Console.WriteLine("Value: " + output);
            //Console.WriteLine();
            //Console.WriteLine();

            // -----------------------------------

            //average = 0;

            string output = "";
            //for (int j = 0; j < numberOfTests; j++)
            //{
            //    stopwatch.Reset();
            //    stopwatch.Start();
            //    for (int i = 0; i < numberOfIterationsPerTest; i++)
            //    {
            //        convertedValue = (int)Math.Round(valueToConvert);
            //        output = convertedValue.ToString();
            //    }
            //    stopwatch.Stop();
            //    Console.WriteLine("convertedValue.ToString(): " + stopwatch.ElapsedMilliseconds);

            //    average += stopwatch.ElapsedMilliseconds;
            //}
            //Console.WriteLine("Average for convertedValue.ToString(): " + (average / numberOfTests));
            //Console.WriteLine("Value: " + output);
            //Console.WriteLine();
            //Console.WriteLine();

            // -----------------------------------

            //average = 0;

            //output = "";
            //for (int j = 0; j < numberOfTests; j++)
            //{
            //    stopwatch.Reset();
            //    stopwatch.Start();
            //    for (int i = 0; i < numberOfIterationsPerTest; i++)
            //    {
            //        convertedValue = (int)Math.Round(Math.Abs(valueToConvert));
            //        output = "" + convertedValue;
            //    }
            //    stopwatch.Stop();
            //    Console.WriteLine("'' + convertedValue: " + stopwatch.ElapsedMilliseconds);

            //    average += stopwatch.ElapsedMilliseconds;
            //}
            //Console.WriteLine("Average for '' + convertedValue: " + (average / numberOfTests));
            //Console.WriteLine("Value: " + output);
            //Console.WriteLine();
            //Console.WriteLine();

            // -----------------------------------

            average = 0;

            output = "";
            for (int j = 0; j < numberOfTests; j++)
            {
                stopwatch.Reset();
                stopwatch.Start();
                for (int i = 0; i < 100_000; i++)
                {
                    output = "World position X: " + 10 + " Y: " + 20 + " Z: " + 30;

                }
                stopwatch.Stop();
                Console.WriteLine("+ + +: " + stopwatch.ElapsedMilliseconds);

                average += stopwatch.ElapsedMilliseconds;
            }
            Console.WriteLine("Average for + + +: " + (average / numberOfTests));
            Console.WriteLine("Value: " + output);
            Console.WriteLine();
            Console.WriteLine();

            // -----------------------------------

            average = 0;

            output = "";
            for (int j = 0; j < numberOfTests; j++)
            {
                stopwatch.Reset();
                stopwatch.Start();
                for (int i = 0; i < 100_000; i++)
                {
                    output = String.Concat("World position X: ", 10, " Y: ", 20, " Z: ", 30);

                }
                stopwatch.Stop();
                Console.WriteLine("String.Concat: " + stopwatch.ElapsedMilliseconds);

                average += stopwatch.ElapsedMilliseconds;
            }
            Console.WriteLine("Average for String.Concat: " + (average / numberOfTests));
            Console.WriteLine("Value: " + output);
            Console.WriteLine();
            Console.WriteLine();

            // -----------------------------------

            average = 0;

            output = "";
            for (int j = 0; j < numberOfTests; j++)
            {
                stopwatch.Reset();
                stopwatch.Start();
                for (int i = 0; i < 100_000; i++)
                {
                    output = String.Join("", "World position X:", 10, "Y:", 20, "Z:", 30);

                }
                stopwatch.Stop();
                Console.WriteLine("String.Join: " + stopwatch.ElapsedMilliseconds);

                average += stopwatch.ElapsedMilliseconds;
            }
            Console.WriteLine("Average for String.Join: " + (average / numberOfTests));
            Console.WriteLine("Value: " + output);
            Console.WriteLine();
            Console.WriteLine();

            // -----------------------------------

            average = 0;

            StringBuilder stringBuilder = new StringBuilder(60);
            output = "";
            for (int j = 0; j < numberOfTests; j++)
            {
                stopwatch.Reset();
                stopwatch.Start();
                for (int i = 0; i < 100_000; i++)
                {
                      stringBuilder.Clear();
                    stringBuilder.Append("World position X: ");
                    stringBuilder.Append(10);
                    stringBuilder.Append(" Y: ");
                    stringBuilder.Append(20);
                    stringBuilder.Append(" Z: ");
                    stringBuilder.Append(.0);

                    //stringBuilder.AppendJoin(" ", "World position X:", 10, "Y:", 20, "Z:", 30);

                    output = stringBuilder.ToString();
                }
                stopwatch.Stop();
                Console.WriteLine("StringBuilder: " + stopwatch.ElapsedMilliseconds);

                average += stopwatch.ElapsedMilliseconds;
            }
            Console.WriteLine("Average for StringBuilder: " + (average / numberOfTests));
            Console.WriteLine("Value: " + output);
            Console.WriteLine();
            Console.WriteLine();

            // -----------------------------------
        }


    }
}
