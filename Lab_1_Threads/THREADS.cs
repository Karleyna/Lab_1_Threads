using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_1_Threads
{
    class Threads
    {
        public static List<int> Array = null; //основной массив с которым идет работа
        static int TrueCount = 0; //счетчик простых чисел

        public static void Execute(List<int> arr, int count) //красивое выполнение функции
        {
            ThreadsWork(arr, count); //вызов основной работы

            if (TrueCount == 0) //проверка наличия простых чисел через счетчик
                Console.WriteLine("FALSE: В массиве НЕТ простых чисел чисел");
            else
                Console.WriteLine("TRUE: В массиве ЕСТЬ хотя бы одно простое число");
        }

        private static void ThreadsWork(List<int> array, int threadCount) //основная работа
        {
            Array = new List<int>(array);

            //for(int i = 0; i<array.Count;i++)
            //{
            //    Array[i] = array[i];
            //}

            List<Thread> threads = new List<Thread>();

            int elemsArray = Array.Count / threadCount;
            int elemsOut = Array.Count % threadCount;

            for (int i = 0; i < threadCount; i++) //основное распределение элементов массива в потоки, их выполнение
            {
                List<int> arr = Array.GetRange(0, elemsArray);
                Array.RemoveRange(0, elemsArray);

                Thread thread = new Thread(solution);
                threads.Add(thread);
                threads[i].Name = "Поток номер " + (i + 1);
                threads[i].Start(arr);
                threads[i].Join();
            }

            while (elemsOut != 0) //проверка остатка в массиве чисел при распределении по потокам
            {
                for (int i = 0; i < threadCount; i++)
                {
                    if (!threads[i].IsAlive)
                    {
                        elemsArray = Array.Count / threadCount;
                        elemsOut -= elemsArray;

                        List<int> arr = Array.GetRange(0, elemsOut);
                        Array.RemoveRange(0, elemsOut);
                        threads.RemoveAt(i);

                        Thread thread = new Thread(solution);
                        threads.Insert(i, thread);
                        threads[i].Name = "Поток номер " + (i + 1);
                        threads[i].Start(arr);
                        //threads[i].Join();

                        elemsOut -= elemsOut;
                        if (elemsOut <= 0)
                            break;
                    }
                }
            }
        }

        private static void solution(object obj) //функция передающаяся потокам при создании
        {
            if (obj is List<int> b)
            {
                int trueCount = 0;
                foreach (int i in b)
                {
                    int def = (int)Math.Sqrt(i);
                    int del = 0;
                    for (int j = 1; j <= def; j++)
                    {
                        if (i % j == 0)
                            del++;
                    }
                    if (del == 0 || i == 1 || del == 1)
                        trueCount++;
                }
                TrueCount += trueCount; 
            }
        }
    }
}
