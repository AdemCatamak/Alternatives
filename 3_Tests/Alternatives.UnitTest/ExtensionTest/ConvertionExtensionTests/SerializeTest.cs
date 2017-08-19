﻿using System;
using Alternatives.Extensions;
using Alternatives.UnitTest.TestModel.ExtensionsTestClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    [TestClass]
    public class SerializeTest
    {
        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__Serialize_Null()
        {
            const string expected = @"null";


            string actual = ((IsValidTestClass) null).Serialize();


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__Serialize()
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