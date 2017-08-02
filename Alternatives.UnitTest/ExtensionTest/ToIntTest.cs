using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest
{
    [TestClass]
   public  class ToIntTest
    {

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Argument not null")]
        public void Alternatives_UnitTest_ExtensionsTest__ToInt_Null()
        {
            ((object)null).ToInt();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Value cannot be converted")]
        public void Alternatives_UnitTest_ExtensionsTest__ToInt_Alphabet()
        {
            const string data = "123a123";


            data.ToInt();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Value cannot be converted")]
        public void Alternatives_UnitTest_ExtensionsTest__ToInt_WithComma()
        {
            const string data = "12,3";


            data.ToInt();
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
