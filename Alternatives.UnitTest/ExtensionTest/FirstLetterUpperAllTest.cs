using Alternatives.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest
{
    [TestClass]
    public class FirstLetterUpperAllTest
    {
        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__FirstLetterUpperAll_Null()
        {
            string expected = string.Empty;


            string actual = ((string) null).FirstLetterToUpperAll();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__FirstLetterUpperAll_Empty()
        {
            string expected = string.Empty;


            string actual = string.Empty.FirstLetterToUpperAll();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__FirstLetterUpperAll_Word()
        {
            const string expected = "Adem",
                         data = "adem";

            string actual = data.FirstLetterToUpperAll();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__FirstLetterUpperAll_WordWithSpace()
        {
            const string expected = "Adem",
                         data = "adem ";

            string actual = data.FirstLetterToUpperAll();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__FirstLetterUpperAll_Words()
        {
            const string expected = "Adem Catamak",
                         data = "adem catamak ";

            string actual = data.FirstLetterToUpperAll();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }
    }
}