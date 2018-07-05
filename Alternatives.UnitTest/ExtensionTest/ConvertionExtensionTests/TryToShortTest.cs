using System.Globalization;
using Alternatives.Extensions;
using Xunit;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    public class TryToShortTest
    {
        [Fact]
        public void TryToShort_Null()
        {
            const short expected = default(short);


            short actual = ((object)null).TryToShort();


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToShort_NullWithDefault()
        {
            const short expected = 5;


            short actual = ((object)null).TryToShort(expected);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToShort_WithComma()
        {
            const short expected = default(short);
            object data = "12,3";


            short actual = data.TryToShort(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToShort_WithCommaAndDefault()
        {
            const short expected = 5;
            object data = "12,3";


            short actual = data.TryToShort(CultureInfo.GetCultureInfo("tr-TR"), expected);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToShort()
        {
            const short expected = 123;
            object data = "123";


            short actual = data.TryToShort();


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToShort_WithDefault()
        {
            const short expected = 111,
                      defaultValue = 15;
            object data = "111";


            short actual = data.TryToShort(defaultValue);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToShort_With_SuccessInfo()
        {
            // Arrage
            const string data = "123";
            const short expected = 123;


            // Act
            short actual = data.TryToShort(out bool expectedSuccess);


            // Assert
            Assert.True(expectedSuccess);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryToShort_With_SuccessInfo_DeaultValue()
        {
            // Arrage
            const string data = "123ad123";
            const short expected = 92;


            // Act
            short actual = data.TryToShort(out bool expectedSuccess, expected);


            // Assert
            Assert.False(expectedSuccess);
            Assert.Equal(expected, actual);
        }
    }
}
