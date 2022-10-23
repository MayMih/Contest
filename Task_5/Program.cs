using System;


namespace Task_5
{
    /// <summary>
    /// Задача 5. "Натуральные числа" (комбинаторика)
    /// </summary>
    public static class Program
    {
        // 10^18
        public const ulong MAX_NUMBER = 1_000_000_000_000_000_000;

        static void Main()
        {
            Console.WriteLine("Введите ограничения - натуральные числа l и r (через пробел):");
            var arrLR = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            ulong l = ulong.Parse(arrLR[0].Trim());
            ulong r = ulong.Parse(arrLR[1].Trim());
            ulong result = GetOneDigitNumbersCount(l, r);
            Console.WriteLine(result);
            Console.ReadKey();
        }

        /// <summary>
        /// Метод решение всей задачи
        /// </summary>
        /// <param name="l">Нижняя (левая) включённая граница</param>
        /// <param name="r">Верхняя (правая) включённая граница</param>
        /// <returns>Суммарное кол-во чисел только из одной цифры (как единственной, так и повторяющейся, вроде 555)</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Когда параметры не попадают в ограничения (1 ≤ l,r ≤ 10^18)
        /// </exception>
        public static ulong GetOneDigitNumbersCount(ulong l, ulong r)
        {
            if (l < 1 || l > MAX_NUMBER || r < 1 || r > MAX_NUMBER)
            {
                throw new ArgumentOutOfRangeException("Должны быть введены два натуральных числа ﻿﻿l,r (1 ≤ l,r ≤ 10^18)!");
            }
            if (l > r)
            {
                return 0;
            }
            else if (r <= 9)
            {
                return r - l + 1;
            }
            else
            {
                ulong res = 0;
                if (l <= 9)
                {
                    res = 9 - l + 1;
                    l = 10;
                }
                else if (l % 10 == 0 && r % 10 == 0)
                {
                    return (ulong)Math.Log10(r / l) * 9;
                }
                ulong l_nearestNumber, r_nearestNumber;
                uint l_digitsCount = GetDigitsCountAndNearestNumber(l, out l_nearestNumber, true);
                uint r_digitsCount = GetDigitsCountAndNearestNumber(r, out r_nearestNumber, false);
                if ((l_nearestNumber != 0 && l_nearestNumber > r) || (r_nearestNumber != 0 && r_nearestNumber < l))
                {
                    return res;
                }                
                else if ((l_digitsCount == r_digitsCount) || (l_digitsCount == r_digitsCount - 1 && (r % 10 == 0)))
                {
                    res += GetSameDigitsCount(Math.Max(l_nearestNumber, l), (r % 10 == 0) ? r - 1 : Math.Min(
                        r_nearestNumber, r), l_digitsCount);
                    if (l_nearestNumber >= l || r_nearestNumber <= r)       
                    {
                        res++;
                    }
                    return res;
                }
                else
                {
                    // получаем ближайшее число следующего разряда (вроде 1000)                    
                    ulong local_r = (ulong)Math.Pow(10, l_digitsCount);
                    res += GetSameDigitsCount(l, local_r - 1, l_digitsCount);
                    if (l_nearestNumber >= l)
                    {
                        res++;
                    }
                    res += 9 * (r_digitsCount - l_digitsCount - 1);
                    if (r % 10 != 0 && r_nearestNumber <= r)
                    {
                        res += GetSameDigitsCount((ulong)Math.Pow(10, r_digitsCount - 1), r_nearestNumber, r_digitsCount);
                        res++;  // учитываем вхождение нижней границы (r_nearestNumber)
                    }
                    return res;
                }
            }
        }

        private static ulong GetSameDigitsCount(ulong l, ulong r, uint l_digitsCount)
        {
            return (r - l) / (ulong)Math.Pow(10, l_digitsCount - 1);
        }

        /// <summary>
        /// Метод получения числа вида 111, 3333, 44444... 
        /// </summary>
        /// <param name="digit">Цифра, на основе которой будет создано число</param>
        /// <param name="length">Кол-во цифр в желаемом числе</param>
        /// <returns></returns>
        public static ulong GenerateNumberByDigit(uint digit, uint length)
        {
            if (length == 0)
            {
                throw new ArgumentException($@"Параметр ""{nameof(length)}"" должен быть > 0");
            }  
            if (digit == 0)
            {
                throw new ArgumentException($@"Параметр ""{nameof(digit)}"" должен быть > 0");
            }
            ulong num = 0;
            for (int i = 0; i < length; i++)
            {
                num *= 10;
                num += digit;                
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
        private static uint GetDigitsCountAndNearestNumber(ulong sourceNumber, out ulong nearestNumber, bool isLeftBorder)
        {
            uint count = 0;
            uint lastDigit;
            ulong supposedNearestNumber;
            ulong number = sourceNumber;
            do
            {
                lastDigit = (uint)number % 10;
                number /= 10;
                count++;
            }
            while (number > 0);

            supposedNearestNumber = GenerateNumberByDigit(lastDigit, count);

            nearestNumber = (isLeftBorder && (supposedNearestNumber >= number)) || (!isLeftBorder &&
                supposedNearestNumber <= sourceNumber) ? supposedNearestNumber : (lastDigit == 1 ? sourceNumber - 1 : 
                GenerateNumberByDigit((uint)(lastDigit + (isLeftBorder ? +1 : -1)), count)
            );
            return count;
        }
    }
}
