using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace Task_10.Tests
{
    internal static class Program_TestData
    {

        internal static IEnumerable BadFigurePoints 
        {
            get
            {
                yield return new TestCaseData(new ValueTuple<int, int>[] { (0, 2), (2, 2) }).Returns(0).SetName("Horizontal Line");
                yield return new TestCaseData(new ValueTuple<int, int>[] { (2, 0), (2, 2) }).Returns(0).SetName("Vertical Line");
                yield return new TestCaseData(new ValueTuple<int, int>[] { (0, 2) }).Returns(0).SetName("Single Point");
            }
        }

        /// <summary>
        /// Метод генерации тестовых данных (набор координат полигона с обходом по час. стрелке и координата Х середины)
        /// </summary>
        /// <param name="isSquareRes">False - ожидаемый результат - средняя точка, True - площадь фигуры</param>
        /// <remarks>
        /// <see cref="https://www.geogebra.org/">
        /// Классный инструмент построения фигур и их расчёта (в версии для Windows есть русский язык)
        /// </see>
        /// </remarks>
        /// <returns>
        /// Набор тестовых данных и результатов к ним (последнее только при <paramref name="isSquareRes"/> == true)
        /// </returns>
        internal static IEnumerable<TestCaseData> GetFigurePoints(bool isSquareRes)
        {
            var res = new TestCaseData(new ValueTuple<int, int>[] { (0, 0), (0, 3), (3, 3), (3, 0) }, 1.5f).
                SetName("Квадрат с началом в нуле координат");
            if (isSquareRes)
            {
                res.Returns(9f);
            }
            yield return res;
            res = new TestCaseData(new ValueTuple<int, int>[] { (-1, -1), (-1, 1), (1, 1), (1, -1) }, 0f).
                SetName("Квадрат с началом в центре координат");
            if (isSquareRes)
            {
                res.Returns(4f);
            }
            yield return res;
            res = new TestCaseData(new ValueTuple<int, int>[] { (-8, 0), (-5, 3), (-3, 1) }, -5.261388f).
                SetName("Треугольник разносторонний со всеми отрицательными координатами");
            if (isSquareRes)
            {
                res.Returns(6f);
            }
            yield return res;
            // значение проверено сверкой с результатаим онлайн утилит расчёта площади многоугольников
            res = new TestCaseData(new ValueTuple<int, int>[] { (-2, 5), (6, -1), (-2, -1) }, 0.343146f).
                SetName("Треугольник прямоугольный c координатами обоих знаков");
            if (isSquareRes)
            {
                res.Returns(24f);
            }
            yield return res;
            res = new TestCaseData(new ValueTuple<int, int>[] { (4, 2), (5, 5), (7, 2) }, 5.267949f).
                SetName("Треугольник со стороной параллельной оси Х");
            if (isSquareRes)
            {
                res.Returns(4.5f);
            }
            yield return res;
            res = new TestCaseData(new ValueTuple<int, int>[] { (0, 2), (2, 2), (0, -1), (-2, -1) }, 0f).
                SetName("Параллелограмм содержащий центр координат");
            if (isSquareRes)
            {
                res.Returns(6f);
            }
            yield return res;
            res = new TestCaseData(new ValueTuple<int, int>[] { (2, 5), (1, 6), (1, 7), (2, 8), (3, 7), (3, 6) }, 2f).
                SetName("Шестиугольник стоящий на вершине");
            if (isSquareRes)
            {
                res.Returns(4f);
            }
            yield return res;
            res = new TestCaseData(new ValueTuple<int, int>[] { (-5, 5), (-6, 5), (-7, 6), (-6, 7), (-5, 7), (-4, 6) }, -5.5f).
                SetName("Шестиугольник стоящий на ребре");
            if (isSquareRes)
            {
                res.Returns(4f);
            }
            yield return res;
            // значение проверено сверкой с результатаим онлайн утилит расчёта площади многоугольников
            res = new TestCaseData(new ValueTuple<int, int>[] { (7, -2), (9, -3), (6, -6), (3, -4), (4, -3) }, 6.022774f).
                SetName("Сложный выпуклый пятиугольник");
            if (isSquareRes)
            {
                res.Returns(12.5f);
            }
            yield return res;
            // значение проверено сверкой с результатаим онлайн утилит расчёта площади многоугольников
            res = new TestCaseData(new ValueTuple<int, int>[] { (-4, -2), (-2, -4), (-4, -6), (-5, -4), (-7, -5) }, -3.936492f).
                SetName("Невыпуклый пятиугольник");
            if (isSquareRes)
            {
                res.Returns(7.5f);
            }
            yield return res;
        }

        internal static IList<KeyValuePair<double, double>> ToKeyValueDoublesList(this IEnumerable<(int X, int Y)> points) 
        {
            return points.Select(p => new KeyValuePair<double, double>(p.X, p.Y)).ToList();
        }
        
        internal static IList<KeyValuePair<int, int>> ToKeyValueIntsList(this IEnumerable<(int X, int Y)> points) 
        {
            return points.Select(p => new KeyValuePair<int, int>(p.X, p.Y)).ToList();
        }
    }
}