using System.Globalization;
using Alternatives.Extensions;
using Xunit;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    
    public class TryToFloatTest
    {
        [Fact]
        public void TryToFloat_Null()
        {
            const float expected = default(float);


            float actual = ((object) null).TryToFloat();


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToFloat_NullWithDefault()
        {
            const float expected = (float)5.5;


            float actual = ((object) null).TryToFloat(expected);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToFloat_WithComma()
        {
            const float expected = (float)12.3;
            object data = "12,3";


            float actual = data.TryToFloat(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToFloat_WithDotAndComma()
        {
            const float expected = (float)15412.3;
            object data = "15.412,3";


            float actual = data.TryToFloat(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToFloat_WithCommaAndDefault()
        {
            const float expected = (float)12.3,
                         defaultValue = 8;
            object data = "12,3";


            float actual = data.TryToFloat(CultureInfo.GetCultureInfo("tr-TR"), defaultValue);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToFloat()
        {
            const float expected = 1237;
            object data = "123.7";


            float actual = data.TryToFloat(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToFloat_WithDefault()
        {
            const float expected = 1118,
                         defaultValue = 15;
            object data = "111.8";


            float actual = data.TryToFloat(CultureInfo.GetCultureInfo("tr-TR"), defaultValue);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToFloat_With_SuccessInfo()
        {
            // Arrage
            const string data = "123";
            const float expected = 123;


            // Act
            float actual = data.TryToFloat(out bool expectedSuccess);


            // Assert
            Assert.True(expectedSuccess);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToFloat_With_SuccessInfo_DeaultValue()
        {
            // Arrage
            const string data = "123ad123";
            const float expected = 92;


            // Act
            float actual = data.TryToFloat(out bool expectedSuccess, expected);


            // Assert
            Assert.False(expectedSuccess);
            Assert.Equal(expected, actual);
        }
    }
}