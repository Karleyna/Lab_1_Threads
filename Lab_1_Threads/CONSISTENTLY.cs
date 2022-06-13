using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_Threads
{
    class Consistantly
    {
        public static void Execute(List<int> array) //красивый вызов основной работы
        {
            Solution(array);
            
            if (TrueCount == 0)
                Console.WriteLine("FALSE: В массиве НЕТ простых чисел чисел");
            else
                Console.WriteLine("TRUE: В массиве ЕСТЬ хотя бы одно простое число");
        }

        //последовательное решение поиска простых  чисел или Решето Эратосфена
        private static int TrueCount = 0;
        private static void Solution(List<int> a)
        {
            int trueCount = 0;
            foreach (int i in a)
            {
                int def = (int)Math.Sqrt(i); // в отличии от обычного поиска простых чисел, здесь мы делим
                int del = 0;                // каждое число на диапазон от одного до корня самого числа
                for (int j = 1; j <= def; j++)                    //что достаточно для того чтобы определить, простое число или нет
                {
                    if (i % j == 0)
                        del++; //счетчик делителей
                }
                if (del == 0 || i == 1 || del == 1)
                    trueCount++; //счетчик наличия простых чисел
            }
            TrueCount += trueCount; 
        }
    }
}
