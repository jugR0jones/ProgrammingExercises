using System;
using System.Diagnostics;

namespace Rounding
{
    class Program
    {
        static void Main(string[] args)
        {
           const int numberOfIterations = 100000;
           const float input = 10.7654543f; 
           
            Stopwatch stopwatch = new Stopwatch();

            ////////////////////////////////////////////
  
            stopwatch.Restart();

            float a = 0.0f;
            for (int i = 0; i < numberOfIterations; i++)
            {
                a = (float)Math.Round(input, 2);
            }
            
            stopwatch.Stop();
            
            Console.WriteLine(a);
            Console.WriteLine("Elapsed Ticks: " + stopwatch.ElapsedTicks);
            
            ////////////////////////////////////////////
            
            stopwatch.Restart();

            double b = 0.0f;
            for (int i = 0; i < numberOfIterations; i++)
            {
                b = Math.Round(input, 2);
            }
            
            stopwatch.Stop();

            Console.WriteLine(b);
            Console.WriteLine("Elapsed Ticks: " + stopwatch.ElapsedTicks);
            
            ////////////////////////////////////////////
        }
    }
}