using System;
using Alternatives.Extensions;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    public class DeserializeTest
    {
        #region TestModel

        private class DummyClass
        {
            public string StringField { get; set; }
            public int IntField { get; set; }

            public InnerDummyClass InnerClassField { get; set; }

            private InnerDummyClass InnerPrivateFild { get; set; } = new InnerDummyClass { InnerDummyStringField = "private" };
        }

        private class InnerDummyClass
        {
            public string InnerDummyStringField { get; set; }
        }

        #endregion

        [Test]
        public void Deserialize_WhenDeserializeNullAsObject_ResultMustBeNull()
        {
            DummyClass actual = @"null".Deserialize<DummyClass>();


            Assert.AreEqual(null, actual, $"{actual} is not expected");
        }

        [Test]
        public void Deserialize_WhenDeserializeJson_IfClassNotMatch_ResponseMustBeDefaultObject()
        {
            string item = @"
{""Phone"":null,
""Email"":""ademcatamak@gmail.com"",
""Username"":""ademcatamak"",
""RequiredPhone"":null,
""Id"":3,""ExtraData"":null}"
                .Replace(" ", string.Empty)
                .Replace(Environment.NewLine, string.Empty);

            DummyClass actual = item.Deserialize<DummyClass>();

            Assert.IsNotNull(actual);
            Assert.IsNull(actual.StringField);
            Assert.AreEqual(0, actual.IntField);
            Assert.IsNull(actual.InnerClassField);
        }

        [Test]
        public void Deserialize_WhenDeserializeJson_IfClassPropertiesMathc_ResponseMustBeFilled()
        {
            DummyClass expected = new DummyClass
                                  {
                                      IntField = 3,
                                      StringField = "ademcatamak",
                                      InnerClassField = new InnerDummyClass { InnerDummyStringField = "ademcatamak@gmail.com" }
                                  };
            string item = @"
{""IntField"":3,
""StringField"":""ademcatamak"",
""InnerClassField"":{""InnerDummyStringField"":""ademcatamak@gmail.com""}}"
                .Replace(" ", string.Empty)
                .Replace(Environment.NewLine, string.Empty);


            DummyClass actual = item.Deserialize<DummyClass>();


            Assert.AreEqual(expected.IntField, actual.IntField);
            Assert.AreEqual(expected.StringField, actual.StringField);
            Assert.AreEqual(expected.InnerClassField.InnerDummyStringField, actual.InnerClassField.InnerDummyStringField);
        }
    }
}