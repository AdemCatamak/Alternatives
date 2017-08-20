using Alternatives.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest.GeneralExtensionTests
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
        public void Alternatives_UnitTest_ExtensionsTest__FirstLetterUpperAll_DigitNotEffected()
        {
            const string expected = "2131";
            const string data = "2131";

            string actual = data.FirstLetterToUpperAll();


            Assert.AreEqual(expected, actual);
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


            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__FirstLetterUpperAll_Words_GivenSplitter_NotMatch()
        {
            const string expected = "All first letter is change upper version of all sentences after FirstLetterToUpperAll function run. Try it if you do not believe",
                         data = "all first letter is change upper version of all sentences after FirstLetterToUpperAll function run. try it if you do not believe";

            string actual = data.FirstLetterToUpperAll('.');


            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__FirstLetterUpperAll_Words_GivenSplitter_OnlyLetter()
        {
            const string expected = "Adem, Catamak",
                         data = "adem, catamak";

            string actual = data.FirstLetterToUpperAll(',');


            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__FirstLetterUpperAll_Words_GivenSplitter_OneAfterAnother()
        {
            const string expected = "Adem, Catamak",
                         data = "adem, catamak";

            string actual = data.FirstLetterToUpperAll(',', ' ');


            Assert.AreEqual(expected, actual);
        }
    }
}