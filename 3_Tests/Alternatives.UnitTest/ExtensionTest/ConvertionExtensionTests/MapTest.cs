using Alternatives.Extensions;
using Alternatives.UnitTest.TestModel.ExtensionsTestClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    [TestClass]
    public class MapTest
    {
        //NOTE : Parametresiz constructor sahibi olmayan sınıflar için kullanılamaz

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__Map_Null()
        {
            object actual = ((object) null).Map<object, object>();


            Assert.IsNull(actual, "Expected value is null");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__Map_ClassItemMap()
        {
            IsValidTestClassPartial expected = new IsValidTestClassPartial
                                               {
                                                   Id = 5
                                               };
            IsValidTestClass data = new IsValidTestClass
                                    {
                                        Id = 5,
                                        Username = "ademcatamak"
                                    };


            IsValidTestClassPartial actual = data.Map<IsValidTestClass, IsValidTestClassPartial>();


            Assert.AreEqual(expected.Id, actual.Id, $"{actual} is not expected");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__Map_DifferentClassItemMap()
        {
            IsValidTestClass data = new IsValidTestClass
                                    {
                                        Id = 5,
                                        Username = "ademcatamak"
                                    };


            DummyClass actual = data.Map<IsValidTestClass, DummyClass>();

            Assert.IsNotNull(actual);
            Assert.IsNull(actual.InnerClassField);
            Assert.AreEqual(actual.IntField, default(int));
        }
    }
}