using System;
using Alternatives.ConversionExtensions;
using Xunit;

namespace Alternatives.ConversionExtensionsTests
{
    public class ToIntTest
    {
        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__ToInt_Null()
        {
            Assert.Throws<NullReferenceException>(() => ((object) null).ToInt());
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__ToInt_Alphabet()
        {
            const string data = "123a123";


            Assert.Throws<FormatException>(() => data.ToInt());
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__ToInt_WithComma()
        {
            const string data = "12,3";

            Assert.Throws<FormatException>(() => data.ToInt());
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__ToInt()
        {
            const int expected = 123;
            const string data = "123";


            int actual = data.ToInt();


            Assert.Equal(expected, actual);
        }
    }
}