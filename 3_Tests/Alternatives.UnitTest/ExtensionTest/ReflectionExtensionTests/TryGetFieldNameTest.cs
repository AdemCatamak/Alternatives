using Alternatives.Extensions;
using Alternatives.UnitTest.TestModel.ExtensionsTestClass;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.ReflectionExtensionTests
{
    
    public class TryGetFieldNameTest
    {
        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__TryGetFieldName_NullForClass()
        {
            bool actualIsValid = ((string) null).TryGetFieldValue("Value", out string actualResult);


            Assert.IsNull(actualResult, $"{actualResult} is not expected");
            Assert.IsFalse(actualIsValid, $"{actualIsValid} is not expected");
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__TryGetFieldName_NullForStruct()
        {
            bool actualIsValid = ((string)null).TryGetFieldValue("Value", out int? actualResult);


            Assert.IsNull(actualResult, $"{actualResult} is not expected");
            Assert.IsFalse(actualIsValid, $"{actualIsValid} is not expected");
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__TryGetFieldName_NotExistFieldNameForStruct()
        {
            const bool expectedIsValid = false;

            DummyClass dummyClass = new DummyClass();
            bool actualIsValid = dummyClass.TryGetFieldValue("NotExistColumn", out int? actualResult);


            Assert.IsNull(actualResult, $"{actualResult} is not expected");
            Assert.AreEqual(expectedIsValid, actualIsValid, $"{actualIsValid} is not expected");
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__TryGetFieldName_NotExistFieldNameForClass()
        {
            const InnerDummyClass expectedResult = null;
            const bool expectedIsValid = false;

            DummyClass dummyClass = new DummyClass();
            bool actualIsValid = dummyClass.TryGetFieldValue("NotExistColumn", out InnerDummyClass actualResult);


            Assert.AreEqual(expectedResult, actualResult, $"{actualResult} is not expected");
            Assert.AreEqual(expectedIsValid, actualIsValid, $"{actualIsValid} is not expected");
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__TryGetFieldName_NotConvertableTypeAndFieldMatch()
        {
            const string expectedResult = null;
            const bool expectedIsValid = false;

            DummyClass dummyClass = new DummyClass
                                    {
                                        IntField = 5,
                                        StringField = "asd"
                                    };
            bool actualIsValid = dummyClass.TryGetFieldValue(nameof(DummyClass.StringField), out int? actualResult);


            Assert.AreEqual(expectedResult, actualResult, $"{actualResult} is not expected");
            Assert.AreEqual(expectedIsValid, actualIsValid, $"{actualIsValid} is not expected");
        }

        [Test]
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


            Assert.AreEqual(expectedResult, actualResult, $"{actualResult} is not expected");
            Assert.AreEqual(expectedIsValid, actualIsValid, $"{actualIsValid} is not expected");
        }


        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__TryGetFieldName_SuccessForClassField()
        {
            InnerDummyClass expectedResult = new InnerDummyClass()
                                             {
                                                 InnerDummyStringField = "TestInner"
                                             };
            const bool expectedIsValid = true;

            DummyClass dummyClass = new DummyClass()
                                    {
                                        IntField = 5,
                                        StringField = "asd",
                                        InnerClassField = new InnerDummyClass()
                                                          {
                                                              InnerDummyStringField = "TestInner"
                                                          }
                                    };
            bool actualIsValid = dummyClass.TryGetFieldValue(nameof(DummyClass.InnerClassField), out InnerDummyClass actualResult);


            Assert.AreEqual(expectedResult.InnerDummyStringField, actualResult.InnerDummyStringField, $"{actualResult.InnerDummyStringField} is not expected");
            Assert.AreEqual(expectedIsValid, actualIsValid, $"{actualIsValid} is not expected");
        }
    }
}