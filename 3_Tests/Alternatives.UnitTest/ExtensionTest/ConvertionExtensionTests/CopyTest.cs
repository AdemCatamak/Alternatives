using Alternatives.Extensions;
using Alternatives.UnitTest.TestModel.ExtensionsTestClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest.ConvertionExtensionTests
{
    [TestClass]
    public class CopyTest
    {
        //NOTE: Parametresiz constructor sahibi olmayan sınıflar için kullanılamaz

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__Copy_Null()
        {
            object actual = ((object) null).Copy();


            Assert.IsNull(actual, "Expected value is null");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__Copy_ClassItemCopy()
        {
            IsValidTestClass expected = new IsValidTestClass()
                                        {
                                            Username = "ademcatamak",
                                            Email = "ademcatamak@gmail.com",
                                            Id = 5
                                        };


            IsValidTestClass actual = expected.Copy();


            Assert.AreEqual(expected.Serialize(), actual.Serialize(), $"{actual} is not expected");
            Assert.AreNotSame(expected, actual, $"{actual} is the same expected");
        }
    }
}