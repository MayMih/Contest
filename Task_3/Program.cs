using System;
using System.Linq;


namespace Task_3
{
    /// <summary>
    /// Задача 3. "Разнос договоров (Катя в лифте)" - задача комивояжёра - поиск оптимального пути.
    /// </summary>
    internal static class Program
    {
        static void Main()
        {
            // ввод данных 
            Console.WriteLine("В первой строке вводятся целые положительные числа n и t (количество сотрудников и " +
                "время в минутах до ухода одного из сотрудников):");
            var arrNT = Console.ReadLine().Trim().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            if (arrNT.Length != 2)
            {
                throw new ArgumentException("В первой строке вводятся целые положительные числа ﻿﻿n﻿﻿ и ﻿t﻿");
            }
            uint n = uint.Parse(arrNT[0].Trim());
            uint t = uint.Parse(arrNT[1].Trim());
            if (n < 2 || n > 100 || t < 2 || t > 100)
            {
                throw new ArgumentException("Числа ﻿n﻿﻿ и ﻿t﻿﻿  ﻿(2 ≤ n,t ≤ 100) — количество сотрудников и время (в минутах)");
            }
            Console.WriteLine("Во 2-й строке вводится n чисел через пробел — номера этажей, на которых находятся сотрудники:");
            var arrFloors = Console.ReadLine().Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (arrFloors.Length != n)
            {
                throw new ArgumentOutOfRangeException("Во 2-й строке должно быть n чисел — номера этажей, на которых находятся сотрудники!");
            }
            // то, что по уловию все числа различны и расположены по возрастанию, проверять не будем.
            var floorNumbers = arrFloors.Select(x => { 
                var fn = uint.Parse(x.Trim());
                if (fn <= 0 || fn > 100)
                {
                    throw new ArgumentOutOfRangeException("Все числа по абсолютной величине не должны превосходить 100!");
                }
                return fn;
            }).ToArray();

            Console.WriteLine("В 3-й строке вводится номер сотрудника уходящего первым:");
            uint empIndex = uint.Parse(Console.ReadLine().Trim());
            if (empIndex <= 0 || empIndex > n)
            {
                throw new ArgumentException("Номер сотрудника уходящего первым должен быть в пределах от 1 до n включительно!");
            }
            // конец ввода данных.

            // рассчёты

            uint minFloorsCount = 0;
            // сначала проверяем, можем ли мы подниматься по порядку, т.е. хватит ли в этом случае времени до ухода сотрудника.
            uint empFloor = floorNumbers[empIndex - 1];
            // если подняться до мин. сотрудника успеваем, то кол-во пролётов - это макс. этаж минус мин. этаж.
            if (empFloor - floorNumbers[0] < t)
            {
                minFloorsCount = floorNumbers[n - 1] - floorNumbers[0];
            }
            // если по порядку обойти не сможем, то сначала поднимаемся к мин. сотруднику, а затем обходим остальных.
            else
            {
                // макс. номер этажа над мин. сотрудником
                uint maxFloorNumAboveEmp = empIndex == n ? 0 : floorNumbers[n - 1];
                uint tAbove = maxFloorNumAboveEmp > 0 ? (maxFloorNumAboveEmp - empFloor) * 2 : 0;
                uint tUnder = (empFloor - floorNumbers[0]) * 2;
                minFloorsCount = tAbove < tUnder ? tAbove + (empFloor - floorNumbers[0]) : tUnder + tAbove / 2;
            }
            Console.WriteLine(minFloorsCount);
            Console.ReadKey();
        }
    }
}
