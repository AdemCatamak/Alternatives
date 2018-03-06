using System.Globalization;
using Alternatives.Extensions;
using Xunit;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    public class TryToIntTest
    {
        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToInt_Null()
        {
            const int expected = default(int);


            int actual = ((object)null).TryToInt();


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToInt_NullWithDefault()
        {
            const int expected = 5;


            int actual = ((object)null).TryToInt(expected);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToInt_WithComma()
        {
            const int expected = default(int);
            object data = "12,3";


            int actual = data.TryToInt(CultureInfo.GetCultureInfo("tr-TR"));


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToInt_WithCommaAndDefault()
        {
            const int expected = 5;
            object data = "12,3";


            int actual = data.TryToInt(CultureInfo.GetCultureInfo("tr-TR"), expected);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToInt()
        {
            const int expected = 123;
            object data = "123";


            int actual = data.TryToInt();


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToInt_WithDefault()
        {
            const int expected = 111,
                      defaultValue = 15;
            object data = "111";


            int actual = data.TryToInt(defaultValue);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToInt_With_SuccessInfo()
        {
            // Arrage
            const string data = "123";
            const int expected = 123;


            // Act
            int actual = data.TryToInt(out bool expectedSuccess);


            // Assert
            Assert.True(expectedSuccess);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryToInt_With_SuccessInfo_DeaultValue()
        {
            // Arrage
            const string data = "123ad123";
            const int expected = 92;


            // Act
            int actual = data.TryToInt(out bool expectedSuccess, expected);


            // Assert
            Assert.False(expectedSuccess);
            Assert.Equal(expected, actual);
        }
    }
}
