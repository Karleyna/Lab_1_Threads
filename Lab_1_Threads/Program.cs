using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Lab_1_Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> array = FileReadArray(); //считывание массива с файла
            Console.Read();

            if (array != null)
            {
                Stopwatch time = new Stopwatch();

                Console.Write("Последовательное решение решетом Эратосфена - ");
                time.Start();
                Consistantly.Execute(array);
                time.Stop();
                TimeCount(time);

                Console.Write("Через класс Parallel - ");
                time.Start();
                ParallelStream.Execute(array);
                time.Stop();
                TimeCount(time);

                for (int i = 1; i <= 10; i++)
                {
                    Console.Write($"С {i} потоками - ");
                    time.Start();
                    Threads.Execute(array, i);
                    time.Stop();
                    TimeCount(time);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Массив пустой, выход из программы...");
            }
        }
        private static void TimeCount(Stopwatch st)
        {
            TimeSpan ts = st.Elapsed;
            string elapsedTime = String.Format("{1:00}.{2:00} \n",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Время выполнения программы - " + elapsedTime);
            st.Reset();
        }
        private static List<int> FileReadArray()
        {
            List<int> array = new List<int>();
            try
            {
                string path = "array.txt";
                using (StreamReader reader = new StreamReader(path))
                {
                    string text = reader.ReadToEnd();
                    //Console.WriteLine(text);
                    array = text.Split(" ").Select(Int32.Parse).ToList();
                }
                Console.WriteLine("Массив СЧИТАЛСЯ с файла");
                return array;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Массив НЕ считался с файла");
                return array = null;
            }
        }
    }
}
