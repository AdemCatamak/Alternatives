using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest
{
    [TestClass]
    public class TryToLongTest
    {
        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__TryToLong_Null()
        {
            const long expected = default(long);


            long actual = ((object) null).TryToLong();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__TryToLong_NullWithDefault()
        {
            const long expected = 5;


            long actual = ((object) null).TryToLong(expected);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__TryToLong_WithComma()
        {
            const long expected = default(long);
            object data = "12,3";


            long actual = data.TryToLong(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__TryToLong_WithDotAndComma()
        {
            const long expected = default(long);
            object data = "15.412,3";


            long actual = data.TryToLong(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__TryToLong_WithCommaAndDefault()
        {
            const long expected = 8,
                       defaultValue = expected;
            object data = "12,3";


            long actual = data.TryToLong(CultureInfo.GetCultureInfo("tr-TR"), defaultValue);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__TryToLong()
        {
            const long expected = 1237;
            object data = "123.7";


            long actual = data.TryToLong(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__TryToLong_WithDefault()
        {
            const double expected = 1118,
                         defaultValue = 15;
            object data = "111.8";


            double actual = data.TryToDouble(CultureInfo.GetCultureInfo("tr-TR"), defaultValue);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }
    }
}