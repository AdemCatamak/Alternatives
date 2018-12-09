using System;
using System.Globalization;
using Alternatives.ConversionExtensions;
using Xunit;

namespace Alternatives.ConversionExtensionsTests
{
    public class ToDecimalTest
    {
        [Fact]
        public void ToDecimal_Null()
        {
            Assert.Throws<NullReferenceException>(() => ((object)null).ToDecimal());
        }

        [Fact]
        public void ToDecimal_Alphabet()
        {
            const string data = "123a123.24";

            Assert.Throws<FormatException>(() => data.ToDecimal(CultureInfo.GetCultureInfo("tr-TR")));
        }

        [Fact]
        public void ToDecimal_WithComma()
        {
            const decimal expected = (decimal)12.5;
            const string data = "12,5";


            decimal actual = data.ToDecimal(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToDecimal_WithDot()
        {
            const decimal expected = (decimal)1215;
            const string data = "12.15";


            decimal actual = data.ToDecimal(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }
    }
}