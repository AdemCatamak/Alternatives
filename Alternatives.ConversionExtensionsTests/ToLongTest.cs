using System;
using Alternatives.ConversionExtensions;
using Xunit;

namespace Alternatives.ConversionExtensionsTests
{
    public class ToLongTest
    {
        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__ToLong_Null()
        {
            Assert.Throws<NullReferenceException>(() => ((object) null).ToLong());
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__ToLong_Alphabet()
        {
            const string data = "123a123.24";

            Assert.Throws<FormatException>(() => data.ToLong());
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__ToLong_DotSeperator()
        {
            const string data = "12.15";


            Assert.Throws<FormatException>(() => data.ToLong());
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__ToLong_CommaSeperator()
        {
            const string data = "12,15";

            Assert.Throws<FormatException>(() => data.ToLong());
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__ToLong()
        {
            const long expected = 123123123;
            const string data = "123123123";


            long actual = data.ToLong();


            Assert.Equal(expected, actual);
        }
    }
}