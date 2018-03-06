using Alternatives.Extensions;
using Xunit;

namespace Alternatives.UnitTest.ExtensionTest.ReflectionExtensionTests
{
    
    public class TryGetFieldNameTest
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
        public void Alternatives_UnitTest_ExtensionsTest__TryGetFieldName_NullForClass()
        {
            bool actualIsValid = ((string) null).TryGetFieldValue("Value", out string actualResult);


            Assert.Null(actualResult);
            Assert.False(actualIsValid);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryGetFieldName_NullForStruct()
        {
            bool actualIsValid = ((string)null).TryGetFieldValue("Value", out int? actualResult);


            Assert.Null(actualResult);
            Assert.False(actualIsValid);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryGetFieldName_NotExistFieldNameForStruct()
        {
            const bool expectedIsValid = false;

            DummyClass dummyClass = new DummyClass();
            bool actualIsValid = dummyClass.TryGetFieldValue("NotExistColumn", out int? actualResult);


            Assert.Null(actualResult);
            Assert.Equal(expectedIsValid, actualIsValid);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryGetFieldName_NotExistFieldNameForClass()
        {
            const InnerDummyClass expectedResult = null;
            const bool expectedIsValid = false;

            DummyClass dummyClass = new DummyClass();
            bool actualIsValid = dummyClass.TryGetFieldValue("NotExistColumn", out InnerDummyClass actualResult);


            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedIsValid, actualIsValid);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryGetFieldName_NotConvertableTypeAndFieldMatch()
        {
            const bool expectedIsValid = false;

            DummyClass dummyClass = new DummyClass
                                    {
                                        IntField = 5,
                                        StringField = "asd"
                                    };
            bool actualIsValid = dummyClass.TryGetFieldValue(nameof(DummyClass.StringField), out int? actualResult);


            Assert.Null(actualResult);
            Assert.Equal(expectedIsValid, actualIsValid);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryGetFieldName_SuccessForStructField()
        {
            const int expectedResult = 5;
            const bool expectedIsValid = true;

            DummyClass dummyClass = new DummyClass
                                    {
                                        IntField = 5,
                                        StringField = "asd"
                                    };
            bool actualIsValid = dummyClass.TryGetFieldValue(nameof(DummyClass.IntField), out int? actualResult);


            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedIsValid, actualIsValid);
        }


        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__TryGetFieldName_SuccessForClassField()
        {
            InnerDummyClass expectedResult = new InnerDummyClass
                                             {
                                                 InnerDummyStringField = "TestInner"
                                             };
            const bool expectedIsValid = true;

            DummyClass dummyClass = new DummyClass
                                    {
                                        IntField = 5,
                                        StringField = "asd",
                                        InnerClassField = new InnerDummyClass
                                                          {
                                                              InnerDummyStringField = "TestInner"
                                                          }
                                    };
            bool actualIsValid = dummyClass.TryGetFieldValue(nameof(DummyClass.InnerClassField), out InnerDummyClass actualResult);


            Assert.Equal(expectedResult.InnerDummyStringField, actualResult.InnerDummyStringField);
            Assert.Equal(expectedIsValid, actualIsValid);
        }
    }
}