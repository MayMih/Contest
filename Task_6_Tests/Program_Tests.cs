using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;


namespace Task_6.Tests
{
    [TestFixture(TestName = "Главный метод - поиск пары учеников для замены"), TestOf(typeof(Program))]
    public class GetSwapPupilsNumbers_Tests
    {
        private static readonly KeyValuePair<int, int> EMPTY_RESULT = new KeyValuePair<int, int>(-1, -1);

        private static IEnumerable PredefinedTestCases
        {
            get
            {
                yield return new TestCaseData(new uint[] { 2u, 1u, 4u, 6u }).Returns(EMPTY_RESULT);
                yield return new TestCaseData(new uint[] { 2u, 1u, 3u, 6u }).Returns(new KeyValuePair<int, int>(1, 2));
                yield return new TestCaseData(new uint[] { 1u, 1u, 3u, 6u, 5u }).Returns(EMPTY_RESULT);
                yield return new TestCaseData(new uint[] { 2u, 2u, 3u, 7u, 5u }).Returns(new KeyValuePair<int, int>(1, 4));
                yield return new TestCaseData(new uint[] { 2u, 1u, 4u, 3u }).Returns(EMPTY_RESULT);
                yield return new TestCaseData(new uint[] { 1u, 2u }).Returns(EMPTY_RESULT);
                yield return new TestCaseData(new uint[] { 2u, 1u }).Returns(new KeyValuePair<int, int>(1, 2));
                yield return new TestCaseData(new uint[] { 2u, 2u }).Returns(EMPTY_RESULT);
                yield return new TestCaseData(new uint[] { 1u, 1u }).Returns(EMPTY_RESULT);
                yield return new TestCaseData(new uint[] { 129u, 88u, 82u, 172u, 101u, 157u, 215u }).Returns(
                    new KeyValuePair<int, int>(3, 6));
            }
        }

        [TestOf(nameof(Program.GetSwapPupilsNumbers))]
        [TestCaseSource(nameof(PredefinedTestCases))]
        public KeyValuePair<int, int> GetSwapPupilsNumbers_PredefinedNormalTest(uint[] source)
        {
            return Program.GetSwapPupilsNumbers(source);
        }        
        
        [Test]
        [Repeat(10)]
        public void GetSwapPupilsNumbers_RandomTest()
        {
            var sourceNumbers = new uint[TestContext.CurrentContext.Random.NextByte(2, 10)];
            for (int i = 0; i < sourceNumbers.Length; i++)
            {
                sourceNumbers[i] = (uint)TestContext.CurrentContext.Random.NextByte() + 1;
            }
            TestContext.WriteLine("Источник: {0}", string.Join(" ", sourceNumbers));
            var res = Program.GetSwapPupilsNumbers(sourceNumbers);
            TestContext.WriteLine("Результат: {0} {1}", res.Key, res.Value);
        }        
    }

    [TestFixture(TestName = "Вспомогательный метод - проверка \"двойной\" чётности массива")]
    public class CheckHeightsArray_Tests
    {
        [TestCase(2u, 1u, 4u, 6u, ExpectedResult = false)]
        [TestCase(1u, 2u, 3u, 4u, ExpectedResult = true)]
        [TestCase(1u, 2u, 1u, 2u, ExpectedResult = true)]
        public bool CheckHeightsArray_4_ElementTest(uint a, uint b, uint c, uint d)
        {
            return Program.CheckHeightsArray(new uint[] { a, b, c, d});
        }

        [TestCase(2u, 1u, ExpectedResult = false)]
        [TestCase(2u, 2u, ExpectedResult = false)]
        [TestCase(1u, 1u, ExpectedResult = false)]
        public bool CheckHeightsArray_2_ElementTest(uint a, uint b)
        {
            return Program.CheckHeightsArray(new uint[] { a, b });
        }        
    }
}