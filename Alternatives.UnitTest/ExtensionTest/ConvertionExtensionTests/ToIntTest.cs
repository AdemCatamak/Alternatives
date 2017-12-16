using System;
using Alternatives.Extensions;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    
    public class ToIntTest
    {
        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__ToInt_Null()
        {
            Assert.Throws<NullReferenceException>(() =>
                                                           {
                                                               ((object) null).ToInt();
                                                           });
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__ToInt_Alphabet()
        {
            const string data = "123a123";


            Assert.Throws<FormatException>(() =>
                                                    {
                                                        data.ToInt();
                                                    });
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__ToInt_WithComma()
        {
            const string data = "12,3";

            Assert.Throws<FormatException>(() =>
                                                    {
                                                        data.ToInt();
                                                    });
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__ToInt()
        {
            const int expected = 123;
            const string data = "123";


            int actual = data.ToInt();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }
    }
}