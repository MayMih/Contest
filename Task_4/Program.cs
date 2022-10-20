using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Task_4
{
    /// <summary>
    /// Задача 4. "Максимальная сумма чисел"
    /// </summary>
    internal static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("Единственным параметром программы должен быть путь к файлу с исходными данными!");
            }
            long[] numbers;
            string[] arrNK;
            long sum1 = 0, sum2 = 0;
            long maxDiff = 0;
            int maxDiffSourceIndex = -1;
            uint n, k;

            using (var enumerator = File.ReadLines(args[0].Trim()).GetEnumerator())
            {
                enumerator.MoveNext();
                arrNK = enumerator.Current.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                n = uint.Parse(arrNK[0]);
                k = uint.Parse(arrNK[1]);
                if (n < 1 || n > 1000)
                {
                    throw new ArgumentOutOfRangeException("В первой строке входного файла должны быть два целых числа ﻿n и k," +
                        " где 1 ≤ n ≤ 1000");
                }
                if (k < 1 || k > 10_000)
                {
                    throw new ArgumentOutOfRangeException("В первой строке входного файла должны быть два целых числа ﻿n и k," +
                        " где 1 ≤ k ≤ 10 000");
                }
                enumerator.MoveNext();
                var arrNumbers = enumerator.Current.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (arrNumbers.Length != n)
                {
                    throw new ArgumentException("Во второй строке файла должно быть записано n чисел!");
                }
                int i = 0;
                numbers = arrNumbers.Select(x => {
                    var num = long.Parse(x);
                    if (num > 10_000_000_000)
                    {
                        throw new ArgumentOutOfRangeException("ai﻿﻿ — чисел на бумажке должно выполняться (1 ≤ ai ≤ 10 000 000 000)");
                    }
                    sum1 += num;
                    long localMaxDiff = getDiffWithMaxNumberInRange(num);
                    if (localMaxDiff > maxDiff)
                    {
                        maxDiffSourceIndex = i;
                        maxDiff = localMaxDiff;
                    }
                    i++;
                    return num;
                }).ToArray();
            }

            if (maxDiffSourceIndex < 0)
            {
#if DEBUG
                Console.WriteLine(0);
                Console.ReadKey();
#endif
                File.WriteAllText("out.txt", "0"); ;
                return;
            }

            // используем все попытки изменения цифры числа (одну попытку мы уже задействовали при разборе входных данных)
            for (int i = 0; i < k; i++)
            {
                numbers[maxDiffSourceIndex] = modifyDigitInNumber(numbers[maxDiffSourceIndex]);
                maxDiff = 0;
                maxDiffSourceIndex = -1;
                for (int j = 0; j < n; j++)
                {
                    if (i == k - 1)
                    {
                        sum2 += numbers[j];
                    }
                    else
                    {
                        long localMaxDiff = getDiffWithMaxNumberInRange(numbers[j]);
                        if (localMaxDiff > maxDiff)
                        {
                            maxDiffSourceIndex = j;
                            maxDiff = localMaxDiff;
                        }
                    }
                }
            }
#if DEBUG
            Console.WriteLine(sum2 - sum1);
            Console.ReadKey();
#endif
            File.WriteAllText("out.txt", (sum2 - sum1).ToString());
        }

        /// <summary>
        /// Метод замены цифры в числе
        /// </summary>
        /// <returns>
        /// Возвращает новое число на основе исходного, в котором заменена цифра старшего разряда отличная от 9
        /// </returns>
        private static long modifyDigitInNumber(long number)
        {
            long result = 0;
            bool exchacnged = false;
            List<int> digits = new List<int>();
            do
            {
                int digit = (int)(number % 10);
                number /= 10;
                digits.Add(digit);
            }            
            while (number > 0);

            for (int i = digits.Count; i > 0; i--)
            {
                int digit = digits[i - 1];
                if (!exchacnged && digit < 9)
                {
                    digit = 9;
                    exchacnged = true;
                }
                for (int j = 0; j < i - 1; j++)
                {
                    digit *= 10;
                }
                result += digit;
            }
            return result;
        }

        /// <summary>
        /// Возвращает разность указанного числа с макс. числом имеющим столько же разрядов
        /// </summary>
        /// <example>
        /// <paramref name="number"/> = 100
        /// => 899
        /// </example>
        /// <param name="number"></param>
        private static long getDiffWithMaxNumberInRange(long number)
        {
            long diff = 0;
            int count = 0;
            do
            {
                long digit = number % 10;
                long localDiff = 9 - digit;
                for (int i = 0; i < count; i++)
                {
                    localDiff *= 10;
                }
                diff += localDiff;
                number /= 10;
                count++;
            }
            while (number > 0);
            return diff;
        }


    }
}
