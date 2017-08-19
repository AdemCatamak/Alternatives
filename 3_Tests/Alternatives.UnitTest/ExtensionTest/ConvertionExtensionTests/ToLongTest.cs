using System;
using Alternatives.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    [TestClass]
    public class ToLongTest
    {
        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__ToLong_Null()
        {
            Assert.ThrowsException<NullReferenceException>(() =>
                                                           {
                                                               ((object) null).ToLong();
                                                           });
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__ToLong_Alphabet()
        {
            const string data = "123a123.24";

            Assert.ThrowsException<FormatException>(() =>
                                                    {
                                                        data.ToLong();
                                                    });
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__ToLong_DotSeperator()
        {
            const string data = "12.15";


            Assert.ThrowsException<FormatException>(() =>
                                                    {
                                                        data.ToLong();
                                                    });
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__ToLong_CommaSeperator()
        {
            const string data = "12,15";

            Assert.ThrowsException<FormatException>(() =>
                                                    {
                                                        data.ToLong();
                                                    });
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