using Alternatives.Extensions;
using Alternatives.UnitTest.TestModel.ExtensionsTestClass;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.ReflectionExtensionTests
{
    public class TrySetFieldNameTest
    {
        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__TrySetFieldName_Null()
        {
            bool actualIsValid = ((string) null).TrySetFieldValue("Value", "");

            Assert.IsFalse(actualIsValid);
        }


        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__TrySetFieldName_NotExistFieldName()
        {
            DummyClass dummyClass = new DummyClass();
            bool actualIsValid = dummyClass.TrySetFieldValue("NotExistColumn", 5);


            Assert.IsFalse(actualIsValid);
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__TrySetFieldName_NotConvertableTypeAndFieldMatch()
        {
            const int expectedValue = 5;

            DummyClass dummyClass = new DummyClass
                                    {
                                        IntField = expectedValue,
                                        StringField = "asd"
                                    };
            bool actualIsValid = dummyClass.TrySetFieldValue(nameof(DummyClass.IntField), "asd");


            Assert.IsFalse(actualIsValid);
            Assert.AreEqual(expectedValue, dummyClass.IntField);
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__TrySetFieldName_SuccessForStructField()
        {
            const int expectedResult = 1;

            DummyClass dummyClass = new DummyClass
                                    {
                                        IntField = 5,
                                        StringField = "asd"
                                    };
            bool actualIsValid = dummyClass.TrySetFieldValue(nameof(DummyClass.IntField), expectedResult);


            Assert.IsTrue(actualIsValid);
            Assert.AreEqual(expectedResult, dummyClass.IntField);
        }


        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__TrySetFieldName_SuccessForClassField()
        {
            InnerDummyClass expectedResult = new InnerDummyClass()
                                             {
                                                 InnerDummyStringField = "TestInner"
                                             };

            DummyClass dummyClass = new DummyClass()
                                    {
                                        IntField = 5,
                                        StringField = "asd",
                                        InnerClassField = new InnerDummyClass()
                                                          {
                                                              InnerDummyStringField = "asd"
                                                          }
                                    };
            bool actualIsValid = dummyClass.TrySetFieldValue(nameof(DummyClass.InnerClassField), expectedResult);


            Assert.IsTrue(actualIsValid);
            Assert.AreEqual(expectedResult.InnerDummyStringField, dummyClass.InnerClassField.InnerDummyStringField);
        }
    }
}