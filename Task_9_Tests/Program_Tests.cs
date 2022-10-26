using System;

using NUnit.Framework;


namespace Task_9.Tests
{
    [TestFixture(TestName = "Основной метод - поиск минимальной суммы покупок с учётом купонов")]
    public class GetMinSumCost_Tests
    {
        [TestCase(new uint[] { 5, 35, 40, 101, 59, 63 }, ExpectedResult = 240)]
        public uint GetMinSumCost_NormalTest(uint[] numbers)
        {
            return Program.GetMinSumCost(numbers, Program.FREE_DINNER_BOUNDARY);
        }

        
    }

    [TestFixture(TestName = "Вспомогательный метод - локальный максимум")]
    public class GetMaxElementIndex_Tests
    {
        [TestCase(new uint[] { 5, 35, 40, 101, 59, 63 }, 3, ExpectedResult = 101)]
        [TestCase(new uint[] { 5, 35, 40, 101, 59, 63 }, 4, ExpectedResult = 63)]
        [TestCase(new uint[] { 5, 35, 40, 101, 59, 63 }, 2, ExpectedResult = 101)]
        [TestCase(new uint[] { 5, 101, 35, 63, 40, 59 }, 2, ExpectedResult = 63)]
        [TestCase(new uint[] { 5, 101, 35, 63, 40, 59 }, 1, ExpectedResult = 101)]
        [TestCase(new uint[] { 5, 101, 35, 63, 40, 59 }, 4, ExpectedResult = 59)]
        [TestCase(new uint[] { 5, 35 }, 1, ExpectedResult = 35)]
        [TestCase(new uint[] { 5, 35 }, 0, ExpectedResult = 35)]
        [TestCase(new uint[] { 3 }, 0, ExpectedResult = 3)]
        [Test]
        public uint GetMaxElementIndex_PredefinedNormalTest(uint[] numbers, int startIndex)
        {
            return numbers[Program.GetMaxElementIndex(numbers, startIndex)];
        }
        
        [TestCase(new uint[] { 5 }, 1)]
        [TestCase(new uint[] { 5, 6 }, -5)]
        [Test]
        public void GetMaxElementIndex_IllegalArgTest(uint[] numbers, int startIndex)
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => Program.GetMaxElementIndex(numbers, startIndex));
        }
        
        [TestCase(null, 1)]
        [TestCase(new uint[0], 5)]
        [Test]
        public void GetMaxElementIndex_EmptyListTest(uint[] numbers, int startIndex)
        {
            Assert.Throws(typeof(ArgumentException), () => Program.GetMaxElementIndex(numbers, startIndex));
        }
    }
}