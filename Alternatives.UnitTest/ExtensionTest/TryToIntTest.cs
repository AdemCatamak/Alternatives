using Alternatives.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest
{
    [TestClass]
    public class TryToIntTest
    {
        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__TryToInt_Null()
        {
            const int expected = default(int);


            int actual = ((object)null).TryToInt();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__TryToInt_NullWithDefault()
        {
            const int expected = 5;


            int actual = ((object)null).TryToInt(expected);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__TryToInt_WithComma()
        {
            const int expected = default(int);
            object data = "12,3";


            int actual = data.TryToInt();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__TryToInt_WithCommaAndDefault()
        {
            const int expected = 5;
            object data = "12,3";


            int actual = data.TryToInt(expected);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__TryToInt()
        {
            const int expected = 123;
            object data = "123";


            int actual = data.TryToInt();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__TryToInt_WithDefault()
        {
            const int expected = 111,
                      defaultValue = 15;
            object data = "111";


            int actual = data.TryToInt(defaultValue);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }
    }
}
