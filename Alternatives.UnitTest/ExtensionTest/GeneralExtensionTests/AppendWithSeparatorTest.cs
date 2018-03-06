using System;
using System.Net;
using Alternatives.Extensions;
using Xunit;

namespace Alternatives.UnitTest.ExtensionTest.GeneralExtensionTests
{
    public class AppendWithSeparatorTest
    {
        [Fact]
        public void AppendWithSeparatorTest_IfSourceIsNull_ResponseEqualsToText()
        {
            const string text = "new text";
            const string source = null;

            string actual = source.AppendWithSeparator(text);

            Assert.Equal(text, actual);
        }

        [Fact]
        public void AppendWithSeparatorTest_IfSourceIsNull_EvenCustomSeparatorGiven_ResponseEqualsToText()
        {
            const string text = "new text";
            const string source = null;

            string actual = source.AppendWithSeparator(text, "custom separator");

            Assert.Equal(text, actual);
        }

        [Fact]
        public void AppendWithSeparatorTest_IfSourceIsEmpty_ResponseEqualsToText()
        {
            const string text = "new text";
            const string source = "";

            string actual = source.AppendWithSeparator(text);

            Assert.Equal(text, actual);
        }

        [Fact]
        public void AppendWithSeparatorTest_IfSourceIsEmpty_EvenCustomSeparatorGiven_ResponseEqualsToText()
        {
            const string text = "new text";
            const string source = "";

            string actual = source.AppendWithSeparator(text, " | ");

            Assert.Equal(text, actual);
        }

        [Fact]
        public void AppendWithSeparatorTest_IfSourceHasMeaning_ResponseEqualsToCombineoOfSourceAndText()
        {
            const string text = "new text";
            const string source = "source text";

            string actual = source.AppendWithSeparator(text);

            Assert.Equal($"{source}{Environment.NewLine}{text}", actual);
        }

        [Fact]
        public void AppendWithSeparatorTest_IfSourceHasMeaning_CustomSeparatorMustBeBetweenSourceAndText()
        {
            const string text = "new text";
            const string source = "source text";

            string actual = source.AppendWithSeparator(text, " - ");

            Assert.Equal($"{source} - {text}", actual);
        }
    }
}