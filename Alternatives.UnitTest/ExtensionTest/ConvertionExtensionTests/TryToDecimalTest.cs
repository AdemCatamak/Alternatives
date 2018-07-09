using System.Globalization;
using Alternatives.Extensions;
using Xunit;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{

    public class TryToDecimalTest
    {
        [Fact]
        public void TryToDecimal_Null()
        {
            const decimal expected = default(decimal);


            decimal actual = ((object)null).TryToDecimal();


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToDecimal_NullWithDefault()
        {
            const decimal expected = (decimal)5.5;


            decimal actual = ((object)null).TryToDecimal(expected);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToDecimal_WithComma()
        {
            const decimal expected = (decimal)12.3;
            object data = "12,3";


            decimal actual = data.TryToDecimal(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToDecimal_WithDotAndComma()
        {
            const decimal expected = (decimal)15412.3;
            object data = "15.412,3";


            decimal actual = data.TryToDecimal(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToDecimal_WithCommaAndDefault()
        {
            const decimal expected = (decimal)12.3,
                         defaultValue = 8;
            object data = "12,3";


            decimal actual = data.TryToDecimal(CultureInfo.GetCultureInfo("tr-TR"), defaultValue);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToDecimal()
        {
            const decimal expected = (decimal)1237;
            object data = "123.7";


            decimal actual = data.TryToDecimal(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToDecimal_WithDefault()
        {
            const decimal expected = 1118,
                         defaultValue = 15;
            object data = "111.8";


            decimal actual = data.TryToDecimal(CultureInfo.GetCultureInfo("tr-TR"), defaultValue);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToDecimal_With_SuccessInfo()
        {
            // Arrage
            const string data = "123";
            const decimal expected = (decimal)123;


            // Act
            decimal actual = data.TryToDecimal(out bool expectedSuccess);


            // Assert
            Assert.True(expectedSuccess);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToDecimal_With_SuccessInfo_DeaultValue()
        {
            // Arrage
            const string data = "123ad123";
            const decimal expected = 92;


            // Act
            decimal actual = data.TryToDecimal(out bool expectedSuccess, expected);


            // Assert
            Assert.False(expectedSuccess);
            Assert.Equal(expected, actual);
        }
    }
}