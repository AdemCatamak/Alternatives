using Alternatives.Extensions;
using Xunit;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    public class CopyTest
    {
        //NOTE: Parametresiz constructor sahibi olmayan sınıflar için kullanılamaz

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

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__Copy_Null()
        {
            object actual = ((object) null).Copy();


            Assert.Null(actual);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__Copy_ClassItemCopy()
        {
            DummyClass expected = new DummyClass
                                  {
                                      InnerClassField = new InnerDummyClass { InnerDummyStringField = "ademcatamak" },
                                      StringField = "ademcatamak@gmail.com",
                                      IntField = 5
                                  };


            DummyClass actual = expected.Copy();


            Assert.Equal(expected.Serialize(), actual.Serialize());
            Assert.NotSame(expected, actual);
        }
    }
}