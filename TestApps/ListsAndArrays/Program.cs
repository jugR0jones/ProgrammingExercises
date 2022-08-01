using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ListsAndArrays
{
    class Program
    {
        static void Main()
        {
            List<int> list = new List<int>
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };
            int[] sorted = SortListToArray(list);

            PrintList(list);
            PrintArray(sorted);
        }

        private static int[] SortListToArray(List<int> list)
        {
            int[] sorted = new int[list.Count];

            for(int i=0; i < list.Count; i++)
            {
                sorted[i] = list[i];
            }

            return sorted;
        }

        private static void PrintList(List<int> list)
        {
            for(int i=0; i < list.Count - 1; i++)
            {
                Console.Write(list[i] + ", ");
            }
            Console.WriteLine(list[list.Count-1]);
        }

        private static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                Console.Write(array[i] + ", ");
            }
            Console.WriteLine(array[array.Length - 1]);

        }
    }
}
