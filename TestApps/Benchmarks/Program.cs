using System;
using System.Diagnostics;

namespace Benchmarks
{
    class Program
    {
        class Benchmark
        {

            public static long Run(long iterations, Action code)
            {
                Stopwatch stopWatch = new Stopwatch();

                stopWatch.Start();
                for(int i=0; i <= iterations;i++)
                {
                    code();
                }
                stopWatch.Stop();

                return stopWatch.ElapsedTicks;
            }

        }

        public struct Vector3
        {
            public float x, y, z;
            private int v1;
            private int v2;
            private int v3;

            public Vector3(float v1, float v2, float v3) : this()
            {
                this.x = v1;
                this.y = v2;
                this.z = v3;
}
}

        public static float Distance(Vector3 a, Vector3 b)
        {
            float num = a.x - b.x;
            float num2 = a.y - b.y;
            float num3 = a.z - b.z;
            return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
        }
        public static float Distance1(Vector3 a, Vector3 b)
        {
            return (float)Math.Sqrt((a.x - b.x)*(a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z));
        }

        public static float SquaredDistance(Vector3 a, Vector3 b)
        {
            float num = a.x - b.x;
            float num2 = a.y - b.y;
            float num3 = a.z - b.z;
            return (num * num) + (num2 * num2) + (num3 * num3);
        }
        static void Main(string[] args)
        {
            const long iterations = 10000000;

            Vector3 vector1 = new Vector3(23, 43, 2);
            Vector3 vector2 = new Vector3(-3, 4, -56);

            long time = Benchmark.Run(iterations, () => {
                float distance = Distance(vector1, vector2);
            });
            Console.WriteLine("Distance: " + time);

            time = Benchmark.Run(iterations, () => {
                float distance = Distance1(vector1, vector2);
            });
            Console.WriteLine("Distance1: " + time);

            time = Benchmark.Run(iterations, () => {
                float distance = SquaredDistance(vector1, vector2);
            });
            Console.WriteLine("Distance Squared: " + time);
        }
    }
}
