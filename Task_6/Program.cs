using System;
using System.Collections.Generic;
using System.Linq;


namespace Task_6
{
    /// <summary>
    /// Задача 6. "Рост учеников" (сортировка)
    /// </summary>
    public static class Program
    {
        const uint MAX_HEIGHT = 1_000_000_000;

        static void Main()
        {
            Console.WriteLine("Введите кол-во учеников в шеренге:");
            uint pupilCount = uint.Parse(Console.ReadLine().Trim());
            var heights = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => {
                var ai = uint.Parse(x.Trim());
                if (ai < 1 || ai > MAX_HEIGHT)
                {
                    throw new ArgumentOutOfRangeException("Рост ученика должен удовлетворять условию: 1 ≤ ai ≤ 10^9 !");
                }
                return ai;
                }
            ).ToArray();
            if (pupilCount < 2 || pupilCount > 1000)
            {
                throw new ArgumentOutOfRangeException("В первой строке должно находится число ﻿﻿(2 ≤ n ≤ 1000)﻿!﻿"); 
            }
            if (heights.Length != pupilCount)
            {
                throw new ArgumentException($"Длина введённого массива ({heights.Length}) не соответствует указанному кол-ву учеников ({pupilCount})!");
            }
            var res = GetSwapPupilsNumbers(heights);
            Console.WriteLine(res.Key + ' ' + res.Value);
            Console.ReadKey();
        }

        /// <summary>
        /// Метод получения пары номеров учеников, которых нужно поменять местами
        /// </summary>
        /// <param name="heights"></param>
        /// <returns>
        /// Пара номеров учеников, которых нужно поменять местами, чтобы все ученики во входном массиве <paramref name="heights"/>
        ///     стояли на местах соот-щих чётности их роста.
        ///     <para>(номера отсчитываются от 1)</para>
        ///     <para>Если менять некого или одной замены недостаточно, то возвращает (-1, -1)</para>
        /// </returns>
        public static KeyValuePair<int, int> GetSwapPupilsNumbers(uint[] pupilHeights)
        {
            uint[] heights = new uint[pupilHeights.Length];
            Array.Copy(pupilHeights, heights, pupilHeights.Length);
            var res = new KeyValuePair<int, int>(-1, -1);
            // список элементов чётность которых не соот-т чётности номера их позиции в исходном массиве
            var irregularIndexElements = new List<int>();
            for (int i = 0; i < heights.Length; i++)
            {
                // если чётность элемента не соот-т чётности его номера, то запоминаем номер этого эл-та
                if ((heights[i] % 2 == 0) ^ ((i + 1) % 2 == 0))
                {
                    irregularIndexElements.Add(i);
                }                
            }
            if (irregularIndexElements.Count % 2 != 0)
            {
                // Пробуем по очереди поменять каждый неправильный элемент с каждым другим неправильным, пока не найдём
                // пару, которая приводит последовательность в нужный вид (или пока не кончатся элементы).
                for (int i = 0; i < irregularIndexElements.Count; i++)
                {
                    var el_A = heights[irregularIndexElements[i]];
                    for (int j = 0; j < irregularIndexElements.Count; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }
                        var el_B = heights[irregularIndexElements[j]];
                        if (el_A == el_B)
                        {
                            continue;
                        }
                        heights[irregularIndexElements[i]] = el_B;
                        heights[irregularIndexElements[j]] = el_A;
                        if (CheckHeightsArray(heights))
                        {
                            return new KeyValuePair<int, int>(irregularIndexElements[i] + 1, irregularIndexElements[j] + 1);
                        }
                        else
                        {
                            // если пара не найдена, то откатываем замену
                            heights[irregularIndexElements[i]] = el_A;
                            heights[irregularIndexElements[j]] = el_B;
                        }
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Проверяет массив на соот-ии чётности элементов и их позиций
        /// </summary>
        /// <param name="heights"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static bool CheckHeightsArray(uint[] heights)
        {
            for (int i = 0; i < heights.Length; i++)
            {
                if ((heights[i] % 2 == 0) ^ ((i + 1) % 2 == 0))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
