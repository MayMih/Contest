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
        /// Метод генерации тестовых данных
        /// </summary>
        /// <param name="isMiddlePointRes">True - ожидаемый результат - средняя точка, False - площадь фигуры</param>
        /// <remarks>
        /// <see cref="https://www.geogebra.org/">
        /// Классный инструмент построения фигур и их расчёта (в версии для Windows есть русский язык)
        /// </see>
        /// </remarks>
        internal static IEnumerable GetFigurePoints(bool isMiddlePointRes)
        {
            yield return new TestCaseData(new ValueTuple<int, int>[] { (0, 0), (0, 3), (3, 3), (3, 0) }).
                Returns(isMiddlePointRes ? 1.5f : 9f).
                SetName("Квадрат с началом в нуле координат");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (-1, -1), (-1, 1), (1, 1), (1, -1) }).
                Returns(isMiddlePointRes ? 0f : 4f).
                SetName("Квадрат с началом в центре координат");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (-8, 0), (-5, 3), (-3, 1) }).
                Returns(isMiddlePointRes ? -5.3f : 6f).
                SetName("Треугольник разносторонний со всеми отр. координатами");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (-2, 5), (6, -1), (-2, -1) }).
                Returns(isMiddlePointRes ? 0.7f : 24f).
                SetName("Треугольник прямоугольный c координатами обоих знаков");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (4, 2), (5, 5), (7, 2) }).
                Returns(isMiddlePointRes ? 5.3f : 4.5f).
                SetName("Треугольник со стороной параллельной оси Х");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (0, 2), (2, 2), (0, -1), (-2, -1) }).
                Returns(isMiddlePointRes ? 0f : 6f).
                SetName("Параллелограмм содержащий центр координат");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (2, 5), (1, 6), (1, 7), (2, 8), (3, 7), (3, 6) }).
                Returns(isMiddlePointRes ? 2f : 4f).
                SetName("Шестиугольник стоящий на вершине");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (-5, 5), (-6, 5), (-7, 6), (-6, 7), (-5, 7), (-4, 6) }).
                Returns(isMiddlePointRes ? -5.5f : 4f).
                SetName("Шестиугольник стоящий на ребре");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (7, -2), (9, -3), (6, -6), (3, -4), (4, -3) }).
                Returns(isMiddlePointRes ? 6f : 12.5f).
                SetName("Сложный выпуклый пятиугольник");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (-4, -2), (-2, -4), (-4, -6), (-5, -4), (-7, -5) }).
                Returns(isMiddlePointRes ? -4f : 7.5f).
                SetName("Невыпуклый пятиугольник");
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