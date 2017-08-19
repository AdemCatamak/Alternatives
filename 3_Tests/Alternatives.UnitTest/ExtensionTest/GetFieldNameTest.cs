using System;
using Alternatives.Extensions;
using Alternatives.UnitTest.ExtensionsTestClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest
{
    [TestClass]
    public class GetFieldNameTest
    {
        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__GetFieldNameTest_Null()
        {
            Assert.ThrowsException<NullReferenceException>(() =>
                                                           {
                                                               ((string) null).GetFieldValue<bool>("Value");
                                                           });
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__GetFieldNameTest_NotExistFieldNameForStruct()
        {
            DummyClass dummyClass = new DummyClass();
            Assert.ThrowsException<FieldAccessException>(() =>
                                                         {
                                                             dummyClass.GetFieldValue<string>("NotExistColumn");
                                                         });
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__GetFieldNameTest_NotExistFieldNameForClass()
        {
            DummyClass dummyClass = new DummyClass();
            Assert.ThrowsException<FieldAccessException>(() =>
                                                         {
                                                             dummyClass.GetFieldValue<InnerDummyClass>("NotExistColumn");
                                                         });
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__GetFieldNameTest_NotConvertableTypeAndFieldMatch()
        {
            DummyClass dummyClass = new DummyClass
                                    {
                                        IntField = 5,
                                        StringField = "asd"
                                    };

            Assert.ThrowsException<InvalidCastException>(() =>
                                                         {
                                                             dummyClass.GetFieldValue<int>(nameof(DummyClass.StringField));
                                                         });
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__GetFieldNameTest_SuccessForStructField()
        {
            const int expectedResult = 5;

            DummyClass dummyClass = new DummyClass
                                    {
                                        IntField = 5,
                                        StringField = "asd"
                                    };
            int actualResult = dummyClass.GetFieldValue<int>(nameof(DummyClass.IntField));


            Assert.AreEqual(expectedResult, actualResult, $"{actualResult} is not expected");
        }


        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__GetFieldNameTest_SuccessForClassField()
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
                                                              InnerDummyStringField = "TestInner"
                                                          }
                                    };
            InnerDummyClass actualResult = dummyClass.GetFieldValue<InnerDummyClass>(nameof(DummyClass.InnerClassField));


            Assert.AreEqual(expectedResult.InnerDummyStringField, actualResult.InnerDummyStringField, $"{actualResult.InnerDummyStringField} is not expected");
        }
    }
}