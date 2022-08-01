using System;

class Program
{
    static void Main(string[] args)
    {
        float probabilityOfSpawningPerSecond = 10;
        var rand = new Random();

        var fakeDeltaTime = 0.02;   //50 FPS
        var totalTime = 0.0;
        var totalHits = 0;
        var totalIterations = 1000000;

        for (var iterations = 0; iterations < totalIterations; iterations++)
        {
            totalTime += fakeDeltaTime;

            var randomPercentage = rand.NextDouble();
            if (randomPercentage < 0.01)
            {
                totalHits++;
            }
        }

        Console.WriteLine("Total Time: " + totalTime);
        Console.WriteLine("Total Hits: " + totalHits);
        Console.WriteLine("Total Iterations: " + totalIterations);
        Console.WriteLine("Percentage: " + (float)totalHits / totalIterations);
       // Console.ReadLine();

    }
}