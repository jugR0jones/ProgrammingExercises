using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimisationExercises
{
    public class SumOddExercise
    {
        private int[] array;

        public SumOddExercise()
        {
            Run();
        }

        public void Run()
        {
            const int numberOfItems = 100;
            array = CreateArray(numberOfItems);

            int sum = SumOdd();

       //     Console.WriteLine("Sum: " + sum);
        }

        private int[] CreateArray(int numberOfItems)
        {
            Random random = new Random();

            int[] array = new int[numberOfItems];

            for (int i = 0; i < numberOfItems; i++)
            {
                array[i] = (int)(random.NextDouble() * 1000);
            }

            return array;
        }

        unsafe public int SumOdd()
        {
            int counter = 0;
            int counter2 = 0;


            //for (int i = 1; i < array.Length; i += 4)
            //{
            //    // int element = array[i];
            //    // if ((array[i] & 1) == 1)
            //    counter += array[i];
            //    counter2 += array[i + 2];
            //}

            //var result = Parallel.For(0, array.Length/2, (i) => {
            //     counter += (array[i] & 1) * array[i];
            //     counter2 += (array[i+1] & 1) * array[i+1];
            // });

            fixed (int* data = &array[0])
            {
                int* ptr = (int*)data;

                for (int i = 0; i < array.Length; i += 8)
                {
                    counter += ptr[0];
                    counter2 += ptr[2];
                    counter += ptr[4];
                    counter2 += ptr[6];

                    ptr += 8;
                }
            }

           
            return counter+counter2;
        }

    }
}
