using Alternatives.ReflectionExtensions;
using Xunit;

namespace Alternatives.ReflectionExtensionsTests
{
    public class TrySetFieldNameTest
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

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TrySetFieldName_Null()
        {
            bool actualIsValid = ((string) null).TrySetFieldValue("Value", "");

            Assert.False(actualIsValid);
        }


        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TrySetFieldName_NotExistFieldName()
        {
            DummyClass dummyClass = new DummyClass();
            bool actualIsValid = dummyClass.TrySetFieldValue("NotExistColumn", 5);


            Assert.False(actualIsValid);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TrySetFieldName_NotConvertableTypeAndFieldMatch()
        {
            const int expectedValue = 5;

            DummyClass dummyClass = new DummyClass
                                    {
                                        IntField = expectedValue,
                                        StringField = "asd"
                                    };
            bool actualIsValid = dummyClass.TrySetFieldValue(nameof(DummyClass.IntField), "asd");


            Assert.False(actualIsValid);
            Assert.Equal(expectedValue, dummyClass.IntField);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TrySetFieldName_SuccessForStructField()
        {
            const int expectedResult = 1;

            DummyClass dummyClass = new DummyClass
                                    {
                                        IntField = 5,
                                        StringField = "asd"
                                    };
            bool actualIsValid = dummyClass.TrySetFieldValue(nameof(DummyClass.IntField), expectedResult);


            Assert.True(actualIsValid);
            Assert.Equal(expectedResult, dummyClass.IntField);
        }


        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TrySetFieldName_SuccessForClassField()
        {
            InnerDummyClass expectedResult = new InnerDummyClass
                                             {
                                                 InnerDummyStringField = "TestInner"
                                             };

            DummyClass dummyClass = new DummyClass
                                    {
                                        IntField = 5,
                                        StringField = "asd",
                                        InnerClassField = new InnerDummyClass
                                                          {
                                                              InnerDummyStringField = "asd"
                                                          }
                                    };
            bool actualIsValid = dummyClass.TrySetFieldValue(nameof(DummyClass.InnerClassField), expectedResult);


            Assert.True(actualIsValid);
            Assert.Equal(expectedResult.InnerDummyStringField, dummyClass.InnerClassField.InnerDummyStringField);
        }
    }
}