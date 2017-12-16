using System.Collections.Generic;
using System.Linq;
using Alternatives.Extensions;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    public class EnumToDictionaryTest
    {
        #region TestModel

        private enum TestEnum
        {
            TestValue1 = 1,
            TestValue2 = 2
        }

        private enum TestEnumWithNegative
        {
            TestValueNegative1 = -1,
            TestValue0 = 0,
            TestValue1 = 1
        }

        #endregion

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__EnumToDictionary_WithNegativeValue()
        {
            Dictionary<int, string> expected = new Dictionary<int, string>
                                               {
                                                   { 0, "TestValue0" },
                                                   { 1, "TestValue1" }
                                               };


            Dictionary<int, string> actual = ConvertionExtensions.EnumToDictionary(typeof(TestEnumWithNegative));


            Assert.AreEqual(2, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i), $"Actual[{i}] value is not expected");
            }
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__EnumToDictionary()
        {
            Dictionary<int, string> expected = new Dictionary<int, string>
                                               {
                                                   { 1, "TestValue1" },
                                                   { 2, "TestValue2" }
                                               };


            Dictionary<int, string> actual = ConvertionExtensions.EnumToDictionary(typeof(TestEnum));

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i), $"Actual[{i}] value is not expected");
            }
        }
    }
}