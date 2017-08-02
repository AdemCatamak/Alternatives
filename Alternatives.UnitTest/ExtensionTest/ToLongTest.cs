using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest
{
    [TestClass]
    public class ToLongTest
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Argument not null")]
        public void Alternatives_UnitTest_ExtensionsTest__ToLong_Null()
        {
            ((object) null).ToLong();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Value cannot be converted")]
        public void Alternatives_UnitTest_ExtensionsTest__ToLong_Alphabet()
        {
            const string data = "123a123.24";


            data.ToLong();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Value cannot be converted")]
        public void Alternatives_UnitTest_ExtensionsTest__ToLong_DotSeperator()
        {
            const string data = "12.15";


            data.ToLong();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Value cannot be converted")]
        public void Alternatives_UnitTest_ExtensionsTest__ToLong_CommaSeperator()
        {
            const string data = "12,15";


            data.ToLong();
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__ToLong()
        {
            const long expected = 123123123;
            const string data = "123123123";


            long actual = data.ToLong();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }
    }
}