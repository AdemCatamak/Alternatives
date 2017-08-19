using System;
using System.Globalization;
using Alternatives.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest
{
    [TestClass]
    public class ToDoubleTest
    {
        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__ToDouble_Null()
        {
            Assert.ThrowsException<NullReferenceException>(() =>
                                                           {
                                                               ((object) null).ToDouble();
                                                           });
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__ToDouble_Alphabet()
        {
            const string data = "123a123.24";

            Assert.ThrowsException<FormatException>(() =>
                                                    {
                                                        data.ToDouble(CultureInfo.GetCultureInfo("tr-TR"));
                                                    });
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__ToDouble_WithComma()
        {
            const double expected = 12.5;
            const string data = "12,5";


            double actual = data.ToDouble(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__ToDouble_WithDot()
        {
            const double expected = 1215;
            const string data = "12.15";


            double actual = data.ToDouble(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }
    }
}