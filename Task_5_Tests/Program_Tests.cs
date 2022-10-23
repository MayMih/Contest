using System;

using NUnit.Framework;


namespace Task_5.Tests
{
    [TestFixture(Description = "Проверка метода подсчёта кол-ва чисел из одной цифры в указанных пределах")]
    public class GetOneDigitNumbersCount_Tests
    {
        [Test(Description = "Нормальная ситуация")]
        [TestCase(4ul, 7ul, ExpectedResult = 4ul)]
        [TestCase(7ul, 7ul, ExpectedResult = 1ul)]
        [TestCase(8ul, 4ul, ExpectedResult = 0ul)]
        [TestCase(10ul, 100ul, ExpectedResult = 9ul)]
        [TestCase(10ul, 1000ul, ExpectedResult = 18ul)]
        [TestCase(22ul, 78ul, ExpectedResult = 6ul)]
        [TestCase(10ul, 25ul, ExpectedResult = 2ul)]
        [TestCase(11ul, 22ul, ExpectedResult = 2ul)]
        [TestCase(13ul, 22ul, ExpectedResult = 1ul)]
        [TestCase(13ul, 21ul, ExpectedResult = 0ul)]
        [TestCase(7ul, 68ul, ExpectedResult = 9ul)]
        [TestCase(9ul, 10ul, ExpectedResult = 1ul)]
        [TestCase(102ul, 255ul, ExpectedResult = 2ul)]
        [TestCase(132ul, 212ul, ExpectedResult = 0ul)]
        [TestCase(103ul, 111ul, ExpectedResult = 1ul)]
        [TestCase(12ul, 255ul, ExpectedResult = 10ul)]
        [TestCase(1ul, 255ul, ExpectedResult = 20ul)]
        [TestCase(25ul, 34_147ul, ExpectedResult = 7+18+3ul)]   //26
        public ulong GetOneDigitNumbersCount_NormalTest(ulong leftMargin, ulong rightMargin)
        {            
            return Program.GetOneDigitNumbersCount(leftMargin, rightMargin);            
        }

        [Test(Description = "Некорректные параметры")]
        [TestCase(0UL, 10UL)]
        [TestCase(Program.MAX_NUMBER + 1, 2UL)]     
        [TestCase(1ul, 0UL)]
        [TestCase(100UL, Program.MAX_NUMBER + 1)]
        public void GetOneDigitNumbersCount_IllegalArgumentsTest(ulong leftMargin, ulong rightMargin)
        {            
             Assert.Throws(typeof(ArgumentOutOfRangeException), () => Program.GetOneDigitNumbersCount(leftMargin, rightMargin));            
        }

        
    }

    [Order(1)]
    [TestFixture(Description = "Проверка метода генерации числа из одной и той же цифры")]
    public class GenerateNumberByDigit_Tests
    {
        [Test(Description = "Нормальная ситуация")]        
        [TestCase(1U, 2U, ExpectedResult = 11UL)]
        [TestCase(2U, 3U, ExpectedResult = 222UL)]
        [TestCase(9U, 4u, ExpectedResult = 9999UL)]
        [TestCase(5u, 1u, ExpectedResult = 5ul)]
        public ulong GenerateNumberByDigit_Test(uint digit, uint length)
        {
            return Program.GenerateNumberByDigit(digit, length);
        }

        [Test(Description = "Некорректные параметры")]
        [TestCase(0U, 5u)]
        [TestCase(6U, 0u)]
        public void GenerateNumberByDigit_IllegalArgumentsTest(uint digit, uint length)
        {
            Assert.Throws(typeof(ArgumentException), () => Program.GenerateNumberByDigit(digit, length));
        }
    }
}