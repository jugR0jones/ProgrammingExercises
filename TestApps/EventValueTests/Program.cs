using System;
using System.Diagnostics;

namespace EventValueTests
{
    class Program
    {
        private const int totalIterations = 10000000;

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            EventValue<bool> boolEventValue = new EventValue<bool>(false, OnBoolHasChanged);

            stopwatch.Start();
            for(int i=0; i < totalIterations; i++)
            {
                boolEventValue.Value = false;
            }
            stopwatch.Stop();
            Console.WriteLine("Setting EventValue to false: " + stopwatch.ElapsedTicks);

            //---------------------------------------------------------------------------------------------------------

            bool instanceBool = false;
            stopwatch.Restart();
            for (int i = 0; i < totalIterations; i++)
            {
                boolEventValue.Value = instanceBool;
            }
            stopwatch.Stop();
            Console.WriteLine("EventValue toggling instanceBool values: " + stopwatch.ElapsedTicks);

            //---------------------------------------------------------------------------------------------------------

            EventValueBool eventValueBool = new EventValueBool(false, OnBoolHasChanged);
            stopwatch.Restart();
            for (int i = 0; i < totalIterations; i++)
            {
                eventValueBool.Value = false;
            }
            stopwatch.Stop();
            Console.WriteLine("Setting EventValueBool to false: " + stopwatch.ElapsedTicks);

            //---------------------------------------------------------------------------------------------------------

            instanceBool = false;
            stopwatch.Restart();
            for (int i = 0; i < totalIterations; i++)
            {
                eventValueBool.Value = instanceBool;
            }
            stopwatch.Stop();
            Console.WriteLine("EventValueBool toggling instanceBool values: " + stopwatch.ElapsedTicks);

            //---------------------------------------------------------------------------------------------------------

            instanceBool = false;
            stopwatch.Restart();
            for (int i = 0; i < totalIterations; i++)
            {
                eventValueBool.SetValue(instanceBool);
            }
            stopwatch.Stop();
            Console.WriteLine("EventValueBool toggling instanceBool values: " + stopwatch.ElapsedTicks);

            //---------------------------------------------------------------------------------------------------------

            bool previousValue = false;
            stopwatch.Restart();
            for (int i = 0; i < totalIterations; i++)
            {
                if(previousValue != false)
                {
                    previousValue = false;

                    instanceBool = !false;
                }
            }
            stopwatch.Stop();
            Console.WriteLine("In code to false: " + stopwatch.ElapsedTicks);

            //---------------------------------------------------------------------------------------------------------

            instanceBool = false;
            stopwatch.Restart();
            for (int i = 0; i < totalIterations; i++)
            {
                if (previousValue != instanceBool)
                {
                    previousValue = instanceBool;

                    instanceBool = !instanceBool;
                }
            }
            stopwatch.Stop();
            Console.WriteLine("In code toggling instanceBool values: " + stopwatch.ElapsedTicks);

            //---------------------------------------------------------------------------------------------------------

#pragma warning disable IDE0060 // Remove unused parameter
            void OnBoolHasChanged(bool value)
            {
                instanceBool = !value;
            }
#pragma warning restore IDE0060 // Remove unused parameter
        }


    }
}
