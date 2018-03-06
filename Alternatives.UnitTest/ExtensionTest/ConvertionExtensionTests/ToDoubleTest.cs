using System;
using System.Globalization;
using Alternatives.Extensions;
using Xunit;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    public class ToDoubleTest
    {
        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__ToDouble_Null()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                ((object)null).ToDouble();
            });
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__ToDouble_Alphabet()
        {
            const string data = "123a123.24";

            Assert.Throws<FormatException>(() =>
                                                    {
                                                        data.ToDouble(CultureInfo.GetCultureInfo("tr-TR"));
                                                    });
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__ToDouble_WithComma()
        {
            const double expected = 12.5;
            const string data = "12,5";


            double actual = data.ToDouble(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__ToDouble_WithDot()
        {
            const double expected = 1215;
            const string data = "12.15";


            double actual = data.ToDouble(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }
    }
}