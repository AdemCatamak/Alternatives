using System;
using Alternatives.Extensions;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.ReflectionExtensionTests
{
    public class GetFieldNameTest
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
        public void Alternatives_UnitTest_ExtensionsTest__GetFieldNameTest_Null()
        {
            Assert.Throws<NullReferenceException>(() =>
                                                           {
                                                               ((string) null).GetFieldValue<bool>("Value");
                                                           });
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__GetFieldNameTest_NotExistFieldNameForStruct()
        {
            DummyClass dummyClass = new DummyClass();
            Assert.Throws<FieldAccessException>(() =>
                                                         {
                                                             dummyClass.GetFieldValue<string>("NotExistColumn");
                                                         });
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__GetFieldNameTest_NotExistFieldNameForClass()
        {
            DummyClass dummyClass = new DummyClass();
            Assert.Throws<FieldAccessException>(() =>
                                                         {
                                                             dummyClass.GetFieldValue<InnerDummyClass>("NotExistColumn");
                                                         });
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__GetFieldNameTest_NotConvertableTypeAndFieldMatch()
        {
            DummyClass dummyClass = new DummyClass
                                    {
                                        IntField = 5,
                                        StringField = "asd"
                                    };

            Assert.Throws<InvalidCastException>(() =>
                                                         {
                                                             dummyClass.GetFieldValue<int>(nameof(DummyClass.StringField));
                                                         });
        }

        [Test]
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

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__GetFieldNameTest_PrivateField()
        {
            DummyClass dummyClass = new DummyClass
                                    {
                                        IntField = 5,
                                        StringField = "asd"
                                    };

            Assert.Throws<FieldAccessException>(() =>
                                                         {
                                                             dummyClass.GetFieldValue<InnerDummyClass>("InnerPrivateFild");
                                                         });
        }


        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__GetFieldNameTest_SuccessForClassField()
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
                                                              InnerDummyStringField = "TestInner"
                                                          }
                                    };
            InnerDummyClass actualResult = dummyClass.GetFieldValue<InnerDummyClass>(nameof(DummyClass.InnerClassField));


            Assert.AreEqual(expectedResult.InnerDummyStringField, actualResult.InnerDummyStringField, $"{actualResult.InnerDummyStringField} is not expected");
        }
    }
}