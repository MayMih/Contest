using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_10
{
    internal static class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите кол-во вершин выпуклого многоугольника:");
            uint edgeCount = uint.Parse(Console.ReadLine().Trim());
            if (edgeCount < 1 || edgeCount > 1000)
            {
                throw new ArgumentOutOfRangeException("Количество вершин многоугольника должно быть (1 ≤ n ≤ 1000)!");
            }            
            Console.WriteLine("Введите координаты вершин многоугольника (целые числа) - каждую пару с новой строки:");
            var points = new List<KeyValuePair<int, int>>((int)edgeCount);
            int i = 0;
            do
            {
                var arrXY = Console.ReadLine().Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                points.Add(new KeyValuePair<int, int>(ParseCoord(arrXY[0]), ParseCoord(arrXY[1])));
                i++;
            }
            while (i < edgeCount);
            if (edgeCount <= 2)
            {
                Console.WriteLine(0);
            }
            else
            {
                double res = GetXCoordOfHalfLine(points);
                Console.WriteLine("{0:f9}", Math.Round(res, 9));
            }
            Console.ReadKey();
        }

        private static double GetXCoordOfHalfLine(IList<KeyValuePair<int, int>> points, double desiredAccuracy = 0.000_001)
        {
            // ищем половину площади многоугольника по формуле Гаусса
            double halfSquare = 0;
            for (int i = 0; i < points.Count; i++)
            {
                int y1_Index = i + 1 < points.Count ? i + 1 : 0;
                int y2_Index = i - 1 > 0 ? i - 1 : points.Count - 1;
                halfSquare += points[i].Key * (points[y1_Index].Value - points[y2_Index].Value);
            }            
            halfSquare = Math.Abs(halfSquare) / 4;
            // пробуем найти простое решение - ищем координату половины ширины фигуры по оси Х
            int max_X = points[0].Key;
            int min_X = points[0].Key;
            foreach (var p in points)
            {
                if (p.Key > max_X)
                {
                    max_X = p.Key;
                }
                else if (p.Key < min_X)
                {
                    min_X = p.Key;
                }
            }
            double middleX = (max_X - min_X) / 2;
            // найти две ближайшие вершины между которыми попала точка middleX (снизу и сверху)
            KeyValuePair<int, int> leftPoint_1, leftPoint_2;
            KeyValuePair<int, int> rightPoint_1, rightPoint_2;

            double middleY1, middleY2;
            var linePoint_1 = new KeyValuePair<double, double>(middleX, middleY1);
            var linePoint_2 = new KeyValuePair<double, double>(middleX, middleY2);            
            double calculatedHalfSquare = 0;

            if (calculatedHalfSquare - halfSquare <= desiredAccuracy)
            {
                return calculatedHalfSquare;
            }
        }

        private static int ParseCoord(string value)
        {
            int res = int.Parse(value.Trim());
            if (Math.Abs(res) > 1000)
            {
                throw new ArgumentOutOfRangeException("Координаты должны быть целыми числами, не превосходящие по модулю ﻿﻿10^3 !");
            }
            return res;
        }
    }
}
