using System;
using AdemCatamak.Utilities.UnitTest.ExtensionsTestClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdemCatamak.Utilities.UnitTest.ExtensionTest
{
    [TestClass]
    public class SerializeTest
    {
        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__Serialize_Null()
        {
            const string expected = @"null";


            string actual = ((IsValidTestClass) null).Serialize();


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__Serialize()
        {
            string expected = @"{""Phone"":null,""Email"":""ademcatamak@gmail.com"",""Username"":""ademcatamak"",""RequiredPhone"":null,""Id"":3,""ExtraData"":null}"
                .Replace(" ", string.Empty)
                .Replace(Environment.NewLine, string.Empty);

            IsValidTestClass item = new IsValidTestClass
                                    {
                                        Id = 3,
                                        Username = "ademcatamak",
                                        Email = "ademcatamak@gmail.com"
                                    };


            string actual = item.Serialize()
                                .Replace(" ", string.Empty)
                                .Replace(Environment.NewLine, string.Empty);


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
        }
    }
}