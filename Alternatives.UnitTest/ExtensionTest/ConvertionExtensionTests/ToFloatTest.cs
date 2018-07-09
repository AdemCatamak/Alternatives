using System;
using System.Globalization;
using Alternatives.Extensions;
using Xunit;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    public class ToFloatTest
    {
        [Fact]
        public void ToFloat_Null()
        {
            Assert.Throws<NullReferenceException>(() => ((object) null).ToFloat());
        }

        [Fact]
        public void ToFloat_Alphabet()
        {
            const string data = "123a123.24";

            Assert.Throws<FormatException>(() => data.ToFloat(CultureInfo.GetCultureInfo("tr-TR")));
        }

        [Fact]
        public void ToFloat_WithComma()
        {
            const float expected = (float)12.5;
            const string data = "12,5";


           float actual = data.ToFloat(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToFloat_WithDot()
        {
            const float expected = (float)1215;
            const string data = "12.15";


            double actual = data.ToFloat(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }
    }
}