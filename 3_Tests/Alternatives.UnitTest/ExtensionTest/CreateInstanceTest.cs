using Alternatives.Extensions;
using Alternatives.UnitTest.ExtensionsTestClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest
{
    [TestClass]
    public class CreateInstanceTest
    {
        //NOTE : Parametresiz constructor sahibi olmayan sınıflarda kullanılamaz

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__CreateInstance_InSameAssembly_Type()
        {
            object actual = ReflectionExtensions.CreateInstance(typeof(IsValidTestClass));


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as IsValidTestClass,
                             "Cast operation show that IsValidClass instance cannot be created");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__CreateInstance_InDifferentAssembly_Type()
        {
            object actual = ReflectionExtensions.CreateInstance(typeof(CryptographyEngine));


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as CryptographyEngine,
                             "Cast operation show that IsValidClass instance cannot be created");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__CreateInstance_InSameAssembly()
        {
            string fullName = typeof(IsValidTestClass).AssemblyQualifiedName;


            object actual = ReflectionExtensions.CreateInstance(fullName);


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as IsValidTestClass,
                             "Cast operation show that IsValidClass instance cannot be created");
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__CreateInstance_InDifferentAssembly()
        {
            string fullName = typeof(CryptographyEngine).FullName;


            object actual = ReflectionExtensions.CreateInstance(fullName);


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as CryptographyEngine,
                             "Cast operation show that IsValidClass instance cannot be created");
        }
    }
}