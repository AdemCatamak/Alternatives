using System.Collections.Generic;
using System.Linq;
using Alternatives.UnitTest.ExtensionsTestClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest
{
    [TestClass]
    public class EnumToDictionaryTest
    {
        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__EnumToDictionary_WithNegativeValue()
        {
            Dictionary<int, string> expected = new Dictionary<int, string>()
                                               {
                                                   {0, "TestValue0"},
                                                   {1, "TestValue1"}
                                               };


            Dictionary<int, string> actual = Extensions.EnumToDictionary(typeof(TestEnumWithNegative));

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i), $"Actual[{i}] value is not expected");
            }
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__EnumToDictionary()
        {
            Dictionary<int, string> expected = new Dictionary<int, string>()
                                               {
                                                   {1, "TestValue1"},
                                                   {2, "TestValue2"}
                                               };


            Dictionary<int, string> actual = Extensions.EnumToDictionary(typeof(TestEnum));

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i), $"Actual[{i}] value is not expected");
            }
        }
    }
}