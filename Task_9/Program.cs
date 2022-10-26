using System;
using System.Collections.Generic;


namespace Task_9
{
    /// <summary>
    /// Задача 9. "Обеды в кафе" (минимакс)
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Граничная стоимость одного обеда, за превышение которой дают купон на бесплатный обед.
        /// </summary>
        public const uint FREE_DINNER_BOUNDARY = 100;

        static void Main()
        {
            Console.WriteLine("Введите кол-во дней, на которые запланированы обеды:");
            uint daysCount = uint.Parse(Console.ReadLine().Trim());
            if (daysCount > 100)
            {
                throw new ArgumentOutOfRangeException("В первой строке должно быть натуральное число ﻿﻿(0 ≤ n ≤ 100)");
            }
            Console.WriteLine($"Введите {daysCount} строк со стоимостью обеда в этот день:");
            var dinnerCosts = new List<uint>();
            while (dinnerCosts.Count < daysCount)
            {
                var cost = uint.Parse(Console.ReadLine().Trim());
                if (cost > 300)
                {
                    throw new ArgumentOutOfRangeException("Стоимость обеда должна быть неотрицательным целым числом ≤ 300!");
                }
                dinnerCosts.Add(cost);
            }
            var minSumCost = GetMinSumCost(dinnerCosts.AsReadOnly(), FREE_DINNER_BOUNDARY);
            Console.WriteLine(minSumCost);
            Console.ReadKey();
        }

        /// <summary>
        /// Метод получения минимально возможной суммарной стоимости обедов (с учётом купонов) за указанное кол-во дней.
        /// </summary>
        public static uint GetMinSumCost(IReadOnlyList<uint> dinnerCosts, uint freeDinnerBoundary)
        {
            if (dinnerCosts.Count  == 0)
            {
                return 0;
            }
            uint minSumCost = 0;
            var ignoredElements = new List<uint>();
            for (int i = 0; i < dinnerCosts.Count; i++)
            {
                if (ignoredElements.Contains((uint)i))
                {
                    continue;
                }
                minSumCost += dinnerCosts[i];                
                if (dinnerCosts[i] > freeDinnerBoundary)
                {
                    uint localMaxIndex = GetMaxElementIndex(dinnerCosts, i + 1);
                    ignoredElements.Add(localMaxIndex);
                }
            }
            return minSumCost;
        }

        /// <summary>
        /// Метод поиска максимального элемента в коллекции начиная с указанной позиции
        /// </summary>
        /// <param name="dinnerCosts"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static uint GetMaxElementIndex(IReadOnlyList<uint> dinnerCosts, int startIndex)
        {
            if ((dinnerCosts?.Count ?? 0) == 0)
            {
                throw new ArgumentException("Список не может быть пустым!");
            }
            if (startIndex < 0 || startIndex >= dinnerCosts.Count)
            {
                throw new ArgumentOutOfRangeException($"Параметр {nameof(startIndex)} должен быть меньше {dinnerCosts.Count}!");
            }
            int maxIndex = startIndex;
            var max = dinnerCosts[maxIndex];
            for (int i = startIndex; i < dinnerCosts.Count; i++)
            {
                var elem = dinnerCosts[i];
                if (elem > max)
                {
                    max = elem;
                    maxIndex = i;
                }
            }
            return (uint)maxIndex;
        }
    }
}
