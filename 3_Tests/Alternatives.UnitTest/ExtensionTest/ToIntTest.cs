using System;
using Alternatives.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest
{
    [TestClass]
    public class ToIntTest
    {
        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__ToInt_Null()
        {
            Assert.ThrowsException<NullReferenceException>(() =>
                                                           {
                                                               ((object) null).ToInt();
                                                           });
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__ToInt_Alphabet()
        {
            const string data = "123a123";


            Assert.ThrowsException<FormatException>(() =>
                                                    {
                                                        data.ToInt();
                                                    });
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__ToInt_WithComma()
        {
            const string data = "12,3";

            Assert.ThrowsException<FormatException>(() =>
                                                    {
                                                        data.ToInt();
                                                    });
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__ToInt()
        {
            const int expected = 123;
            const string data = "123";


            int actual = data.ToInt();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }
    }
}