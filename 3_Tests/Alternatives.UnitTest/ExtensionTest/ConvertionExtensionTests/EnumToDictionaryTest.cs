using System.Collections.Generic;
using System.Linq;
using Alternatives.Extensions;
using Alternatives.UnitTest.TestModel.ExtensionsTestClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    [TestClass]
    public class EnumToDictionaryTest
    {
        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__EnumToDictionary_WithNegativeValue()
        {
            Dictionary<int, string> expected = new Dictionary<int, string>()
                                               {
                                                   {0, "TestValue0"},
                                                   {1, "TestValue1"}
                                               };


            Dictionary<int, string> actual = ConvertionExtensions.EnumToDictionary(typeof(TestEnumWithNegative));


            Assert.AreEqual(2, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i), $"Actual[{i}] value is not expected");
            }
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__EnumToDictionary()
        {
            Dictionary<int, string> expected = new Dictionary<int, string>()
                                               {
                                                   {1, "TestValue1"},
                                                   {2, "TestValue2"}
                                               };


            Dictionary<int, string> actual = ConvertionExtensions.EnumToDictionary(typeof(TestEnum));

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i), $"Actual[{i}] value is not expected");
            }
        }
    }
}