using System;
using System.Collections.Generic;

using NUnit.Framework;


namespace Task_10.Tests
{
    [TestFixture(TestName = "Площадь фигур (произвольных многоугольников)")]
    [Order(1)]
    public class CalculatePolygonSquare_Tests
    {
        [TestCaseSource(typeof(Program_TestData), nameof(Program_TestData.GetFigurePoints), new object[] { false })]
        [Test(Description = "Нормальные данные")]
        public double CalculatePolygonSquare_NormalTest(IEnumerable<(int X, int Y)> polygonEdges)
        {
            return Program.CalculatePolygonSquare(polygonEdges.ToKeyValueDoublesList());
        }

        [TestCaseSource(typeof(Program_TestData), nameof(Program_TestData.BadFigurePoints))]
        [Test(Description = "Ошибочные данные")]
        public double CalculatePolygonSquare_BadDataTest(IEnumerable<(int X, int Y)> polygonEdges)
        {
            return Program.CalculatePolygonSquare(polygonEdges.ToKeyValueDoublesList());
        }
    }

    [TestFixture(TestName = "Средняя точка фигур (выпуклых многоугольников)")]
    public class GetXCoordOfHalfLine_Tests
    {
        [TestCaseSource(typeof(Program_TestData), nameof(Program_TestData.GetFigurePoints), new object[] { true })]
        [Test(Description = "Нормальные данные")]
        public double GetXCoordOfHalfLine_NormalDataTest(IEnumerable<(int X, int Y)> polygonEdges)
        {
            var square = Program.CalculatePolygonSquare(polygonEdges.ToKeyValueDoublesList());
            return Program.GetXCoordOfHalfLine(polygonEdges.ToKeyValueIntsList(), square / 2);
        }

        [TestCaseSource(typeof(Program_TestData), nameof(Program_TestData.BadFigurePoints))]
        [Test(Description = "Ошибочные данные")]
        public double GetXCoordOfHalfLine_BadDataTest(IEnumerable<(int X, int Y)> polygonEdges)
        {
            var square = Program.CalculatePolygonSquare(polygonEdges.ToKeyValueDoublesList());
            double res = 0;
            Assert.Throws<ArgumentException>(() => res = Program.GetXCoordOfHalfLine(polygonEdges.ToKeyValueIntsList(), square / 2));
            return res;
        }
    }
}