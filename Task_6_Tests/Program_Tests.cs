using NUnit.Framework;
using Task_6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_6.Tests
{
    [TestFixture(TestName = "Главный метод - пара учеников для замены")]
    public class GetSwapPupilsNumbers_Tests
    {
        [Test()]
        public void GetSwapPupilsNumbers_Test()
        {
            Assert.Fail();
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
        
        [TestCase(new uint[] { 2u, 1u }, ExpectedResult = false)]
        [TestCase(new uint[] { 1u, 2u, 3u, 4u }, ExpectedResult = true)]
        [TestCase(new uint[] { 1u, 1u }, ExpectedResult = false)]
        public bool CheckHeightsArray_Arr_ElementTest(uint[] ab)
        {
            return Program.CheckHeightsArray(ab);
        }
    }
}