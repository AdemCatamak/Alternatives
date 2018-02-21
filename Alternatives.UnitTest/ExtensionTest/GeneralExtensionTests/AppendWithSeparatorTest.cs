using System;
using System.Net;
using Alternatives.Extensions;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.GeneralExtensionTests
{
    public class AppendWithSeparatorTest
    {
        [Test]
        public void AppendWithSeparatorTest_IfSourceIsNull_ResponseEqualsToText()
        {
            const string text = "new text";
            const string source = null;

            string actual = source.AppendWithSeparator(text);

            Assert.AreEqual(text, actual);
        }

        [Test]
        public void AppendWithSeparatorTest_IfSourceIsNull_EvenCustomSeparatorGiven_ResponseEqualsToText()
        {
            const string text = "new text";
            const string source = null;

            string actual = source.AppendWithSeparator(text, "custom separator");

            Assert.AreEqual(text, actual);
        }

        [Test]
        public void AppendWithSeparatorTest_IfSourceIsEmpty_ResponseEqualsToText()
        {
            const string text = "new text";
            const string source = "";

            string actual = source.AppendWithSeparator(text);

            Assert.AreEqual(text, actual);
        }

        [Test]
        public void AppendWithSeparatorTest_IfSourceIsEmpty_EvenCustomSeparatorGiven_ResponseEqualsToText()
        {
            const string text = "new text";
            const string source = "";

            string actual = source.AppendWithSeparator(text, " | ");

            Assert.AreEqual(text, actual);
        }

        [Test]
        public void AppendWithSeparatorTest_IfSourceHasMeaning_ResponseEqualsToCombineoOfSourceAndText()
        {
            const string text = "new text";
            const string source = "source text";

            string actual = source.AppendWithSeparator(text);

            Assert.AreEqual($"{source}{Environment.NewLine}{text}", actual);
        }

        [Test]
        public void AppendWithSeparatorTest_IfSourceHasMeaning_CustomSeparatorMustBeBetweenSourceAndText()
        {
            const string text = "new text";
            const string source = "source text";

            string actual = source.AppendWithSeparator(text, " - ");

            Assert.AreEqual($"{source} - {text}", actual);
        }
    }
}