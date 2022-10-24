using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;


namespace Task_7.Tests
{
    [TestFixture()]
    public class Program_Tests
    {
        private static readonly KeyValuePair<int, int> EMPTY_RESULT = new KeyValuePair<int, int>(-1, -1);

        private static IEnumerable TestDataAndResults
        {
            get
            {
                yield return new TestCaseData(new uint[] { 1, 2, 3 }).Returns(EMPTY_RESULT);
                yield return new TestCaseData(new uint[] { 1, 3, 1 }).Returns(new KeyValuePair<int, int>(1, 2));
                yield return new TestCaseData(new uint[] { 4, 1, 1, 2 }).Returns(new KeyValuePair<int, int>(2, 3));
                yield return new TestCaseData(new uint[] { 4, 1, 2, 1 }).Returns(new KeyValuePair<int, int>(2, 3));
                yield return new TestCaseData(new uint[] { 4, 2, 1, 1 }).Returns(new KeyValuePair<int, int>(4, 3));
                yield return new TestCaseData(new uint[] { 1, 3 }).Returns(EMPTY_RESULT);
                yield return new TestCaseData(new uint[] { 1, 2 }).Returns(EMPTY_RESULT);
                yield return new TestCaseData(new uint[] { 2, 1 }).Returns(EMPTY_RESULT);
            }
        }

        [TestCaseSource(nameof(TestDataAndResults))]
        public KeyValuePair<int, int> GetIndexAndNewValue_Test(uint[] source)
        {
            return Program.GetIndexAndNewValue(source);
        }
    }
}