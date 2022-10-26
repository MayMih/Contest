using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;


namespace Task_8
{
    /// <summary>
    /// Задача 9. "Площадь и масштаб" - понял не верно (тут оказывается прямоугольник мог быть повёрнут - нужна тригонометрия!).
    /// </summary>
    internal static class Program_8
    {
        const int REQUIRED_PRECISION = 4;

        static void Main(string[] args)
        {
            float X, Y;
            IEnumerable<float> edgeCoords;

            if (args.Length != 1)
            {
                throw new ArgumentException("Единственным параметром программы должен быть путь к файлу с данными!");
            }
#pragma warning disable CRRSP13 // A misspelled word has been found
            var flines = File.ReadLines(args[0].Trim());
#pragma warning restore CRRSP13 // A misspelled word has been found
            using (var iterator = flines.GetEnumerator())
            {
                iterator.MoveNext();
                var xyPair = iterator.Current.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (xyPair.Length != 2)
                {
                    throw new ArgumentException("В первой строке файла должно быть два вещественных числа -  координаты ﻿﻿X и Y!");
                }
                X = float.Parse(xyPair[0], CultureInfo.InvariantCulture);
                Y = float.Parse(xyPair[1], CultureInfo.InvariantCulture);
                iterator.MoveNext();
                edgeCoords = iterator.Current.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => 
                    float.Parse(x, CultureInfo.InvariantCulture)
                );
            }
            var res = GetFixedPoint(X, Y, edgeCoords);
            Console.WriteLine($"{{0:f{REQUIRED_PRECISION}}} {{1:f{REQUIRED_PRECISION}}}", Math.Round(res.Key, REQUIRED_PRECISION), 
                Math.Round(res.Value, REQUIRED_PRECISION));
            Console.ReadKey();
        }

        private static KeyValuePair<float, float> GetFixedPoint(float x, float y, IEnumerable<float> edgeCoords)
        {
            const int EDGE_COUNT = 4;

            if (x < 1 || x > 1000 || y < 1 || y > 1000)
            {
                throw new ArgumentOutOfRangeException("Координаты ﻿﻿X и ﻿Y переговорки должны удовлетворять условиям: (1 ≤ X ≤ 1000, 1 ≤ Y ≤ 1000)﻿﻿!");
            }
            if (edgeCoords.Count() != EDGE_COUNT * 2)
            {
                throw new ArgumentOutOfRangeException($"Количество углов должно быть строго равно {EDGE_COUNT} (пары к-т)!");
            }
            var roomPoints = new List<KeyValuePair<float, float>>(EDGE_COUNT);
            for (int i = 0; i < EDGE_COUNT * 2;)
            {
                roomPoints.Add(new KeyValuePair<float, float>(edgeCoords.ElementAt(i++), edgeCoords.ElementAt(i++)));
            }            
            var zoom = x / (roomPoints[0].Key - roomPoints[1].Key);

            if (zoom != y / (roomPoints[1].Value - roomPoints[2].Value))
            {
                throw new ArgumentException("Масштабы по осям X и Y для комнаты и её плана должны совпадать!");
            }

            var planPoints = new KeyValuePair<float, float>[EDGE_COUNT];
            for (int i = 0; i < EDGE_COUNT; i++)
            {
                planPoints[i] = new KeyValuePair<float, float>(roomPoints[i].Key / zoom, roomPoints[i].Value / zoom);
            }
            var randomPlanEdge = planPoints[new Random().Next(EDGE_COUNT)];
            return new KeyValuePair<float, float>(randomPlanEdge.Key * zoom, randomPlanEdge.Value * zoom);
        }
    }
}
