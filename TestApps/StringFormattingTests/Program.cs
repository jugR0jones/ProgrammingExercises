using System;

namespace StringFormattingTests
{
    class Program
    {
        static void Main()
        {
            FloatToIntSpeeds floatToIntSpeeds = new FloatToIntSpeeds();

            floatToIntSpeeds.Run();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
