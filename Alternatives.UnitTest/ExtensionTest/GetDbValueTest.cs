using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest
{
    [TestClass]
    public class GetDbValueTest
    {
        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__GetDbValue_Null()
        {
            DBNull expected = DBNull.Value;


            object actual = ((object) null).GetDbValue();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__GetDbValue_NullInt()
        {
            DBNull expected = DBNull.Value;
            int? data = null;


            // ReSharper disable once ExpressionIsAlwaysNull
            object actual = data.GetDbValue();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__GetDbValue_EmptyString()
        {
            string expected = string.Empty;
            string data = string.Empty;


            object actual = data.GetDbValue();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__GetDbValue_Int()
        {
            const int expected = 5;
            const int data = 5;


            object actual = data.GetDbValue();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }
    }
}