using System;
using Alternatives.Extensions;
using Xunit;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    public class ToShortTest
    {
        [Fact]
        public void ToShort_Null()
        {
            Assert.Throws<NullReferenceException>(() => ((object) null).ToShort());
        }

        [Fact]
        public void ToShort_Alphabet()
        {
            const string data = "123a123";


            Assert.Throws<FormatException>(() => data.ToShort());
        }

        [Fact]
        public void ToShort_WithComma()
        {
            const string data = "12,3";

            Assert.Throws<FormatException>(() => data.ToShort());
        }

        [Fact]
        public void ToShort()
        {
            const short expected = 123;
            const string data = "123";


            short actual = data.ToShort();


            Assert.Equal(expected, actual);
        }
    }
}