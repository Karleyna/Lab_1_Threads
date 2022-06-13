using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_Threads
{
    class ParallelStream
    {
        private static int TrueCount = 0; //счетчик наличия простых чисел
        public static void Execute(List<int> arr)
        {
            ThreadsSolution(arr); //вызов основной работы

            if (TrueCount == 0) //проверка наличия простых чисел
                Console.WriteLine("FALSE: В массиве НЕТ простых чисел чисел");
            else
                Console.WriteLine("TRUE: В массиве ЕСТЬ хотя бы одно простое число");
        }

        private static void ThreadsSolution(List<int> array) //основная работа с классом Parallel
        {
            //List<int> arr = new List<int>() { };
            int trueCount = 0;

            Parallel.ForEach(array, (int elem) => {
                int def = (int)Math.Sqrt(elem);
                int del = 0;
                for (int j = 1; j <= def; j++)
                {
                    if (elem % j == 0)
                        del++;
                }
                if (del == 0 || elem == 1 || del == 1)
                    trueCount++;
            });

            TrueCount = trueCount;
        }
    }
}
