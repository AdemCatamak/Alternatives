using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdemCatamak.Utilities.UnitTest.ExtensionTest
{
    [TestClass]
    public class TryToDoubleTest
    {
        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__TryToDouble_Null()
        {
            const double expected = default(double);


            double actual = ((object) null).TryToDouble();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__TryToDouble_NullWithDefault()
        {
            const double expected = 5.5;


            double actual = ((object) null).TryToDouble(expected);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__TryToDouble_WithComma()
        {
            const double expected = 12.3;
            object data = "12,3";


            double actual = data.TryToDouble();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__TryToDouble_WithDotAndComma()
        {
            const double expected = 15412.3;
            object data = "15.412,3";


            double actual = data.TryToDouble();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__TryToDouble_WithCommaAndDefault()
        {
            const double expected = 12.3,
                         defaultValue = 8;
            object data = "12,3";


            double actual = data.TryToDouble(defaultValue);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__TryToDouble()
        {
            const double expected = 1237;
            object data = "123.7";


            double actual = data.TryToDouble();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__TryToDouble_WithDefault()
        {
            const double expected = 1118,
                         defaultValue = 15;
            object data = "111.8";


            double actual = data.TryToDouble(defaultValue);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }
    }
}