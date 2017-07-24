using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdemCatamak.Utilities.UnitTest.ExtensionTest
{
    [TestClass]
    public class ToDoubleTest
    {

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Argument not null")]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__ToDouble_Null()
        {
            ((object)null).ToDouble();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Value cannot be converted")]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__ToDouble_Alphabet()
        {
            const string data = "123a123.24";


            data.ToDouble(CultureInfo.GetCultureInfo("tr-TR"));
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__ToDouble_WithComma()
        {
            const double expected = 12.5;
            const string data = "12,5";


            double actual = data.ToDouble(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__ToDouble_WithDot()
        {
            const double expected = 1215;
            const string data = "12.15";


            double actual = data.ToDouble(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }
    }
}
