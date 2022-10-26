using System;
using System.Collections.Generic;
using System.Linq;


namespace Task_7
{
    /// <summary>
    /// Задача 7. "Тайный Санта" (псевдосортировка)
    /// </summary>
    public static class Program
    {
        const int MAX_COUNT = 100_000;

        static void Main()
        {
            Console.WriteLine("Введите число учеников:");
            uint studentsCount = uint.Parse(Console.ReadLine());
            if (studentsCount < 2 || studentsCount > MAX_COUNT)
            {
                throw new ArgumentOutOfRangeException($"Должно выполняться (2 ≤ n ≤ 10^5)﻿, однако было введено {studentsCount}");
            }
            Console.WriteLine("Введите через пробел номера одариваемых учеников (дарящими считаются номера их позиций):");
            var accepters = Console.ReadLine().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries).Select(x => {
                var ai = uint.Parse(x);
                if (ai < 1 || ai > studentsCount)
                {
                    throw new ArgumentOutOfRangeException($"Должно выполняться (1 ≤ ai ≤ {studentsCount}), однако введено {ai}");
                }
                return ai;
            }).ToArray();
            if (accepters.Length != studentsCount)
            {
                throw new ArgumentException($"Введено неверное число студентов ({accepters.Length}), ожидалось ({studentsCount})");
            }
            var res = GetIndexAndNewValue(accepters);
            Console.WriteLine("{0} {1}", res.Key, res.Value);
            Console.ReadKey();
        }

        /// <summary>
        /// Метод получает номер ученика во входном массиве, для которого нужно задать новое значение одариваемого ученика.
        /// </summary>
        /// <param name="accepters"></param>
        /// <returns>
        /// Пара чисел: номер ученика и новый номер ученика, который нужно поставить на его место
        /// </returns>
        public static KeyValuePair<int, int> GetIndexAndNewValue(uint[] accepters)
        {
            var res = new KeyValuePair<int, int>(-1, -1);
            int incorrectIndex = -1;
            int absentStudent = -1;
            // частоты элементов (большие 2-х не считаем, т.к. в этом случае уже нет решения)
            var elementCount = new uint[accepters.Length];
            for (int i = 0; i < accepters.Length; i++)
            {
                if (accepters[i] > accepters.Length)
                {
                    return res;
                }
                // если три ученика повторяются, то сделать ничего нельзя, т.к. замена у нас только одна!
                if (elementCount[i] == 2)
                {
                    return res;
                }
                elementCount[accepters[i] - 1]++;
            }
            // ищем номер студента, которому никто не дарит подарки
            for (int i = 0; i < elementCount.Length && absentStudent < 0; i++)
            {
                if (elementCount[i] == 0)
                {
                    absentStudent = i + 1;
                }
            }
            if (absentStudent <= 0)
            {
                return res;
            }
            // ищем позицию первого попавшегося студента, котрому подарки дарят несколько раз (но не когда он дарит сам себе)
            for (int i = 0; i < elementCount.Length && incorrectIndex < 0; i++)
            {
                if (elementCount[i] > 1 && (incorrectIndex < 0 || incorrectIndex == absentStudent))
                {
                    for (int j = 0; j < accepters.Length; j++)
                    {
                        if (accepters[j] == i + 1 && j + 1 != absentStudent)
                        {
                            incorrectIndex = j + 1;
                            break;
                        }
                    }
                }
            }
            return new KeyValuePair<int, int>(incorrectIndex, absentStudent);
        }
    }
}
