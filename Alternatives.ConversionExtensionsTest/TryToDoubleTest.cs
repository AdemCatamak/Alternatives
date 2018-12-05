using System.Globalization;
using Alternatives.ConversionExtensions;
using Xunit;

namespace Alternatives.ConversionExtensionsTests
{

    public class TryToDoubleTest
    {
        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToDouble_Null()
        {
            const double expected = default(double);


            double actual = ((object) null).TryToDouble();


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToDouble_NullWithDefault()
        {
            const double expected = 5.5;


            double actual = ((object) null).TryToDouble(expected);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToDouble_WithComma()
        {
            const double expected = 12.3;
            object data = "12,3";


            double actual = data.TryToDouble(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToDouble_WithDotAndComma()
        {
            const double expected = 15412.3;
            object data = "15.412,3";


            double actual = data.TryToDouble(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToDouble_WithCommaAndDefault()
        {
            const double expected = 12.3,
                         defaultValue = 8;
            object data = "12,3";


            double actual = data.TryToDouble(CultureInfo.GetCultureInfo("tr-TR"), defaultValue);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToDouble()
        {
            const double expected = 1237;
            object data = "123.7";


            double actual = data.TryToDouble(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToDouble_WithDefault()
        {
            const double expected = 1118,
                         defaultValue = 15;
            object data = "111.8";


            double actual = data.TryToDouble(CultureInfo.GetCultureInfo("tr-TR"), defaultValue);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToDouble_With_SuccessInfo()
        {
            // Arrage
            const string data = "123";
            const double expected = 123;


            // Act
            double actual = data.TryToDouble(out bool expectedSuccess);


            // Assert
            Assert.True(expectedSuccess);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToDouble_With_SuccessInfo_DeaultValue()
        {
            // Arrage
            const string data = "123ad123";
            const double expected = 92;


            // Act
            double actual = data.TryToDouble(out bool expectedSuccess, expected);


            // Assert
            Assert.False(expectedSuccess);
            Assert.Equal(expected, actual);
        }
    }
}