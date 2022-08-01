using System;
using System.Diagnostics;

namespace UnitConversions
{
    class Program
    {
        static void Main()
        {
            TestPressure();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void TestPressure()
        {
            const float pressure = 1234.5678f;

            Console.WriteLine(" Original Pressure: " + pressure);
            Console.WriteLine("==============================");
            Console.WriteLine("Default output: " + Format.Pressure(pressure));
            Console.WriteLine("With format string: " + Format.Pressure(pressure, "N2"));
            Console.WriteLine("As Bar: " + Format.Pressure(pressure, PressureConfiguration.Units.Bar));
        }
    }
}
