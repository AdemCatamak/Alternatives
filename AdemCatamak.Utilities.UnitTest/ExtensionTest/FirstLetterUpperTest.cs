using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdemCatamak.Utilities.UnitTest.ExtensionTest
{
    [TestClass]
    public class FirstLetterUpperTest
    {
        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__FirstLetterUpper_Null()
        {
            string expected = string.Empty;


            string actual = ((string) null).FirstLetterToUpper();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__FirstLetterUpper_Empty()
        {
            string expected = string.Empty;
            string data = string.Empty;

            string actual = data.FirstLetterToUpper();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__FirstLetterUpper_OneCharacter()
        {
            const string expected = "A",
                         data = "a";

            string actual = data.FirstLetterToUpper();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__FirstLetterUpper_Word()
        {
            const string expected = "Adem",
                         data = "adem";

            string actual = data.FirstLetterToUpper();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__FirstLetterUpper_WordWithSpace()
        {
            const string expected = "Adem",
                         data = "adem ";

            string actual = data.FirstLetterToUpper();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__FirstLetterUpper_Words()
        {
            const string expected = "Adem catamak",
                         data = "adem catamak";

            string actual = data.FirstLetterToUpper();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }
    }
}