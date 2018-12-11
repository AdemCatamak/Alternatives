using Alternatives;
using Xunit;

namespace AlternativesTests.GeneralExtensionTests
{
    public class FirstLetterUpperTest
    {
        [Fact]
        public void FirstLetterUpper_WhenNullAsObject_ResponseMustBeEmpty()
        {
            string expected = string.Empty;

            
            string actual = ((string) null).FirstLetterToUpper();


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FirstLetterUpper_WhenEmptyAsObject_ResponseMustBeEmpty()
        {
            string expected = string.Empty;
            string data = string.Empty;

            string actual = data.FirstLetterToUpper();


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FirstLetterUpper_WhenOneLetterAsObject_ResponseMustBeCapitalized()
        {
            const string expected = "A",
                         data = "a";

            string actual = data.FirstLetterToUpper();


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FirstLetterUpper_WhenWordHasMultipleLetter_OnlyFirstLetterCapitalized()
        {
            const string expected = "Adem",
                         data = "adem";

            string actual = data.FirstLetterToUpper();


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FirstLetterUpper_WhenWordHasMultipleLetterAndWhiteSpace_OnlyFirstLetterCapitalized()
        {
            const string expected = "Adem ",
                         data = "adem ";

            string actual = data.FirstLetterToUpper();


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FirstLetterUpper_WhenWordsAsObject_OnlyFirstLetterCapitalized()
        {
            const string expected = "Adem catamak",
                         data = "adem catamak";

            string actual = data.FirstLetterToUpper();


            Assert.Equal(expected, actual);
        }
    }
}