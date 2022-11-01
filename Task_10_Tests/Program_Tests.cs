using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace Task_10.Tests
{
    [TestFixture(TestName = "Площадь фигур (произвольных многоугольников)")]
    public class Program_Tests
    {
        private static IEnumerable BadFigurePoints
        {
            get
            {
                yield return new TestCaseData(new ValueTuple<int, int>[] { (0, 2), (2, 2) }).Returns(0).SetName("Line");
                yield return new TestCaseData(new ValueTuple<int, int>[] { (0, 2) }).Returns(0).SetName("Single Point");
            }
        }

        /// <summary>
        /// Метод генерации тестовых данных
        /// </summary>
        /// <remarks>
        /// <see cref="https://www.geogebra.org/">
        /// Классный инструмент построения фигур и их расчёта (в версии для Windows есть русский язык)
        /// </see>
        /// </remarks>
        private static IEnumerable GetFigurePoints()
        {
            yield return new TestCaseData(new ValueTuple<int, int>[] { (0, 0), (0, 2), (2, 2), (2, 0) }).Returns(4).
                SetName("Квадрат с началом в нуле координат");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (-1, -1), (-1, 1), (1, 1), (1, -1) }).Returns(4).
                SetName("Квадрат с началом в центре координат");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (0, 0), (0, 6), (8, 0) }).Returns(24).
                SetName("Треугольник прямоугольный с началом в нуле координат");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (-2, 5), (6, -1), (-2, -1) }).Returns(24).
                SetName("Треугольник прямоугольный c координатами обоих знаков");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (4, 2), (5, 5), (7, 2) }).Returns(4.5).
                SetName("Треугольник со стороной параллельной оси Х");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (0, 2), (2, 2), (0, -1), (-2, -1) }).Returns(6).
                SetName("Параллелограмм содержащий центр координат");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (2, 5), (1, 6), (1, 7), (2, 8), (3, 7), (3, 6) }).Returns(4).
                SetName("Шестиугольник стоящий на вершине");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (-5, 5), (-6, 5), (-7, 6), (-6, 7), (-5, 7), (-4, 6) }).Returns(4).
                SetName("Шестиугольник стоящий на ребре");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (7, -2), (9, -3), (6, -6), (3, -4), (4, -3) }).Returns(12.5).
                SetName("Сложный выпуклый пятиугольник");
            yield return new TestCaseData(new ValueTuple<int, int>[] { (-4, -2), (-2, -4), (-4, -6), (-5, -4), (-7, -5) }).Returns(7.5).
                SetName("Невыпуклый пятиугольник");
        }

        [TestCaseSource(nameof(GetFigurePoints))]
        public double CalculatePolygonSquare_NormalTest(IEnumerable<(int X, int Y)> polygonEdges)
        {
            return Program.CalculatePolygonSquare(polygonEdges.Select(p => new KeyValuePair<double, double>(p.X, p.Y)).ToList());
        }

        [TestCaseSource(nameof(BadFigurePoints))]
        public double CalculatePolygonSquare_BadDataTest(IEnumerable<(int X, int Y)> polygonEdges)
        {
            return Program.CalculatePolygonSquare(polygonEdges.Select(p => new KeyValuePair<double, double>(p.X, p.Y)).ToList());
        }
    }
}