namespace RollingMean
{
    internal static class Program
    {
        private static void Main()
        {
            const int sampleSize = 10000;

            RollingMeanWithFixedSize rollingMeanWithFixedSize1 = new RollingMeanWithFixedSize(15);
            RollingMeanWithFixedSize rollingMeanWithFixedSize2 = new RollingMeanWithFixedSize(16);
            RollingMeanWithFixedSize rollingMeanWithFixedSize3 = new RollingMeanWithFixedSize(30);
            RollingMean rollingMean = new RollingMean();

            double total = 0.0;
            
            Random random = new Random(10);

            for (int i = 0; i < sampleSize; i++)
            {
                double randomNumber = random.NextDouble();
                
                rollingMeanWithFixedSize1.Update(randomNumber);
                rollingMeanWithFixedSize2.Update(randomNumber);
                rollingMeanWithFixedSize3.Update(randomNumber);
                rollingMean.Update(randomNumber);

                total += randomNumber;
            }
            
            Console.WriteLine("rollingMeanWithFixedSize1: " + rollingMeanWithFixedSize1.Average);
            Console.WriteLine("rollingMeanWithFixedSize2: " + rollingMeanWithFixedSize2.Average);
            Console.WriteLine("rollingMeanWithFixedSize3: " + rollingMeanWithFixedSize3.Average);
            Console.WriteLine("rollingMean: " + rollingMean.Average);
            Console.WriteLine("Average: " + total / sampleSize);
        }
    }

    internal class RollingMeanWithFixedSize
    {
        public double Average { get; private set; }
        
        private readonly int numberOfSamples = 0;

        public RollingMeanWithFixedSize(int numberOfSamples)
        {
            this.numberOfSamples = numberOfSamples;
            Average = 0.0f;
        }

        public void Update(double number)
        {
            Average -= Average / numberOfSamples;
            Average += number / numberOfSamples;
        }
    }
    
    public class RollingMean
    {
        public double Average { get; private set; }

        private int numberOfTerms = 0;
        
        public RollingMean()
        {
            Average = 0.0f;
            numberOfTerms = 0;
        }

        public void Update(double number)
        {
            numberOfTerms++;
            Average += (number - Average) / numberOfTerms;
        }
    }
}