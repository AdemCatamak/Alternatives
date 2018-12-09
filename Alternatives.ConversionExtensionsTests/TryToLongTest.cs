using System.Globalization;
using Alternatives.ConversionExtensions;
using Xunit;

namespace Alternatives.ConversionExtensionsTests
{
    public class TryToLongTest
    {
        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToLong_Null()
        {
            const long expected = default(long);


            long actual = ((object) null).TryToLong();


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToLong_NullWithDefault()
        {
            const long expected = 5;


            long actual = ((object) null).TryToLong(expected);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToLong_WithComma()
        {
            const long expected = default(long);
            object data = "12,3";


            long actual = data.TryToLong(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToLong_WithDotAndComma()
        {
            const long expected = default(long);
            object data = "15.412,3";


            long actual = data.TryToLong(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToLong_WithCommaAndDefault()
        {
            const long expected = 8,
                       defaultValue = expected;
            object data = "12,3";


            long actual = data.TryToLong(CultureInfo.GetCultureInfo("tr-TR"), defaultValue);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToLong()
        {
            object data = "123.7";


            long actual = data.TryToLong(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(default(long), actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToLong_WithDefault()
        {
            const long defaultValue = 15;
            object data = "111.8";


            double actual = data.TryToLong(CultureInfo.GetCultureInfo("tr-TR"), defaultValue);


            Assert.Equal(defaultValue, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToLong_With_SuccessInfo()
        {
            // Arrage
            const string data = "123";
            const long expected = 123;


            // Act
            long actual = data.TryToLong(out bool expectedSuccess);


            // Assert
            Assert.True(expectedSuccess);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToLong_With_SuccessInfo_DeaultValue()
        {
            // Arrage
            const string data = "123ad123";
            const long expected = 92;


            // Act
            long actual = data.TryToLong(out bool expectedSuccess, expected);


            // Assert
            Assert.False(expectedSuccess);
            Assert.Equal(expected, actual);
        }
    }
}