using System;


namespace Task_5
{
    /// <summary>
    /// Задача 5. "Натуральные числа" (комбинаторика)
    /// </summary>
    internal static class Program
    {
        // 10^18
        const ulong MAX_NUMBER = 1_000_000_000_000_000_000;

        static void Main()
        {
            Console.WriteLine("Введите ограничения - натуральные числа l и r (через пробел):");
            var arrLR = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            ulong l = ulong.Parse(arrLR[0].Trim());
            ulong r = ulong.Parse(arrLR[1].Trim());
            if (l < 1 || l > MAX_NUMBER || r < 1 || r > MAX_NUMBER)
            {
                throw new ArgumentOutOfRangeException("Должны быть введены два натуральных числа ﻿﻿l,r (1 ≤ l,r ≤ 10^18)!");
            }
            if (l > r)
            {
                Console.WriteLine(0);
            }
            else if (r <= 9)
            {
                Console.WriteLine(r - l + 1);
            }
            else
            {
                ulong res = 0;
                if (l <= 9)
                {
                    res = 9 - l + 1;
                    l = 10;
                }                
                ulong l_nearestNumber, r_nearestNumber;
                int l_digitsCount = GetDigitsCountAndNearestNumber(l, out l_nearestNumber, true);
                int r_digitsCount = GetDigitsCountAndNearestNumber(r, out r_nearestNumber, false);
                if ((l_nearestNumber != 0 && l_nearestNumber > r) || (r_nearestNumber != 0 && r_nearestNumber < l))
                {
                    Console.WriteLine(res);
                }
                else if ((l_digitsCount == r_digitsCount) || (l_digitsCount == r_digitsCount - 1 && (r % 10 == 0)))
                {
                    res += GetSameDigitsCountRes(Math.Max(l_nearestNumber, l), (r % 10 == 0) ? r - 1 : Math.Min(
                        r_nearestNumber, r), l_digitsCount);
                    if (l_nearestNumber >= l)
                    {
                        res++;
                    }
                    Console.WriteLine(res);
                }
                else
                {
                    // получаем ближайшее число следующего разряда (вроде 1000)                    
                    ulong local_r = (ulong)Math.Pow(10, l_digitsCount);
                    res += GetSameDigitsCountRes(l, local_r - 1, l_digitsCount);
                    if (l_nearestNumber >= l)
                    {
                        res++;
                    }
                    res += (ulong)(9 * (r_digitsCount - l_digitsCount - 1));
                    if (r % 10 != 0 && r_nearestNumber < r)
                    {
                        res += GetSameDigitsCountRes((ulong)Math.Pow(10, r_digitsCount - 1), r_nearestNumber, r_digitsCount);
                        res++;
                    }
                    Console.WriteLine(res);
                }
            }
            Console.ReadKey();
        }

        private static ulong GetSameDigitsCountRes(ulong l, ulong r, int l_digitsCount)
        {
            return (r - l) / (ulong)Math.Pow(10, l_digitsCount - 1);
        }

        /// <summary>
        /// Метод получения числа вида 111, 3333, 44444... 
        /// </summary>
        /// <param name="digit">Цифра, на основе которой будет создано число</param>
        /// <param name="length">Кол-во цифр в желаемом числе</param>
        /// <returns></returns>
        private static ulong GetNumberByDigit(int digit, int length)
        {
            ulong num = 0;
            for (int i = 0; i < length; i++)
            {
                num *= 10;
                num += (uint)digit;                
            }
            return num;
        }

        /// <summary>
        /// Метод получения кол-ва цифр в числе и ближайшего к нему целого числа вида 11 или 222, 5555 и т.п.
        /// </summary>
        /// <param name="sourceNumber"></param>
        /// <param name="nearestNumber"></param>
        /// <param name="isLeftBorder">True - возвращается ближайшее число справа</param>
        /// <returns></returns>
        private static int GetDigitsCountAndNearestNumber(ulong sourceNumber, out ulong nearestNumber, bool isLeftBorder)
        {
            int count = 0;
            int lastDigit;
            ulong supposedNearestNumber;
            ulong number = sourceNumber;
            do
            {
                lastDigit = (int)number % 10;
                number /= 10;
                count++;
            }
            while (number > 0);

            supposedNearestNumber = GetNumberByDigit(lastDigit, count);

            nearestNumber = (isLeftBorder && (supposedNearestNumber >= number)) || (!isLeftBorder &&
                supposedNearestNumber <= sourceNumber) ? supposedNearestNumber : GetNumberByDigit(lastDigit + (isLeftBorder ? 
                +1 : -1), count);
            return count;
        }
    }
}
