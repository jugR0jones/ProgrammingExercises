using System;
using System.Diagnostics;

namespace DateToStringTests
{
    class Program
    {
        static void Main(string[] args)
        {
            const int numberOfIterations = 10000000;
            
            Stopwatch stopwatch = new Stopwatch();

            ////////////////////////////////////////////////////////////////////////////

            stopwatch.Restart();

            for (int i = 0; i < numberOfIterations; i++)
            {
                DateTime now = DateTime.Now;

                string time = DateTime.Now.ToString("hh:mm:ss");
            }
            

            stopwatch.Stop();
            Console.WriteLine("Elapsed ticks: " + stopwatch.ElapsedTicks);

            ////////////////////////////////////////////////////////////////////////////

            stopwatch.Restart();
 
            char[] characters = new char[]
            {
                '0', '0', ':', '0', '0', ':', '0', '0'
            };
            
            for (int i = 0; i < numberOfIterations; i++)
            {  
                DateTime now = DateTime.Now;
                
                if (now.Hour < 10)
                {
                    characters[1] = (char)(now.Hour + 48);
                }

                if (now.Minute < 10)
                {
                    
                }
                
                string time = new string(characters);
            }
            

            stopwatch.Stop();
            Console.WriteLine("Elapsed ticks: " + stopwatch.ElapsedTicks);
            
            ////////////////////////////////////////////////////////////////////////////
        }
    }
}