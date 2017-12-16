using System;
using System.Globalization;
using Alternatives.Extensions;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    public class ToDoubleTest
    {
        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__ToDouble_Null()
        {
            Assert.Throws<NullReferenceException>(() =>
                                                           {
                                                               ((object) null).ToDouble();
                                                           });
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__ToDouble_Alphabet()
        {
            const string data = "123a123.24";

            Assert.Throws<FormatException>(() =>
                                                    {
                                                        data.ToDouble(CultureInfo.GetCultureInfo("tr-TR"));
                                                    });
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__ToDouble_WithComma()
        {
            const double expected = 12.5;
            const string data = "12,5";


            double actual = data.ToDouble(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__ToDouble_WithDot()
        {
            const double expected = 1215;
            const string data = "12.15";


            double actual = data.ToDouble(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }
    }
}