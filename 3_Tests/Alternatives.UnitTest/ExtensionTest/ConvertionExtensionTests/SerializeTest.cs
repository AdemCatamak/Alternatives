using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alternatives.CustomDataAnnotations;
using Alternatives.Extensions;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    public class SerializeTest
    {
        #region TestModel

        private class DummyClassPartial
        {
            [Key]
            public int Id { get; set; }

            public int? ExtraData { get; set; }
        }

        [Table("asd")]
        private class DummyClass : DummyClassPartial
        {
            [TurkeyPhone]
            public string Phone { get; set; }

            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Username { get; set; }

            [Phone, Required]
            public string RequiredPhone { get; set; }
        }

        #endregion

        [Test]
        public void Serialize_WhenSerializeNullAsObject_ResponseMustBeStringThatNULL()
        {
            const string expected = @"null";


            string actual = ((DummyClass) null).Serialize();


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
        }

        [Test]
        public void Serialize_WhenSerializeJson_IfFieldMathc_ResponseContainsValue()
        {
            string expected = @"{""Phone"":null,""Email"":""ademcatamak@gmail.com"",""Username"":""ademcatamak"",""RequiredPhone"":null,""Id"":3,""ExtraData"":null}"
                .Replace(" ", string.Empty)
                .Replace(Environment.NewLine, string.Empty);

            DummyClass item = new DummyClass
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