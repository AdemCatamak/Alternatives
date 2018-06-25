using System;
using Alternatives.Extensions;
using Xunit;

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
        }

        private class InnerDummyClass
        {
            public string InnerDummyStringField { get; set; }
        }

        #endregion

        [Fact]
        public void Deserialize_WhenDeserializeNullAsObject_ResultMustBeNull()
        {
            DummyClass actual = @"null".Deserialize<DummyClass>();


            Assert.Null(actual);
        }

        [Fact]
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

            Assert.NotNull(actual);
            Assert.Null(actual.StringField);
            Assert.Equal(0, actual.IntField);
            Assert.Null(actual.InnerClassField);
        }

        [Fact]
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


            Assert.Equal(expected.IntField, actual.IntField);
            Assert.Equal(expected.StringField, actual.StringField);
            Assert.Equal(expected.InnerClassField.InnerDummyStringField, actual.InnerClassField.InnerDummyStringField);
        }
    }
}