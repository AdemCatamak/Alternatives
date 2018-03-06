using Alternatives.Extensions;
using Xunit;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    public class MapTest
    {
        //NOTE : Parametresiz constructor sahibi olmayan sınıflar için kullanılamaz

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

        private class AnotherDummyClass
        {
            public string StrField { get; set; }
            public double DoubleField { get; set; }
        }

        #endregion

        [Fact]
        public void Map_WhenMapNullAsObject_ResponseMustBeNull()
        {
            object actual = ((object) null).Map<object, object>();


            Assert.Null(actual);
        }

        [Fact]
        public void Map_WhenMapSameKindOfClassObject_ItemsFieldMustBeEqual()
        {
            DummyClass expected = new DummyClass
                                  {
                                      IntField = 5,
                                      StringField = "ademcatamak",
                                      InnerClassField = new InnerDummyClass { InnerDummyStringField = "inner" }
                                  };
            DummyClass data = new DummyClass
                              {
                                  IntField = 5,
                                  StringField = "ademcatamak",
                                  InnerClassField = new InnerDummyClass { InnerDummyStringField = "inner" }
                              };


            DummyClass actual = data.Map<DummyClass, DummyClass>();


            Assert.Equal(expected.IntField, actual.IntField);
            Assert.Equal(expected.StringField, actual.StringField);
            Assert.Equal(expected.InnerClassField.InnerDummyStringField, actual.InnerClassField.InnerDummyStringField);
        }

        [Fact]
        public void Map_WhenMapDifferentKindOfClassObject_DestinationObjectFieldMustBeDefault()
        {
            DummyClass data = new DummyClass
                              {
                                  IntField = 5,
                                  StringField = "ademcatamak"
                              };

            AnotherDummyClass actual = new AnotherDummyClass
                                       {
                                           DoubleField = 11,
                                           StrField = "another"
                                       };

            actual = data.Map<DummyClass, AnotherDummyClass>();

            Assert.NotNull(actual);
            Assert.Equal(0, actual.DoubleField);
            Assert.Null(actual.StrField);
        }
    }
}