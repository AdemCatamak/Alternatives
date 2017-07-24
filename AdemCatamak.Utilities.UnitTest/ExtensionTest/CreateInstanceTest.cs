using AdemCatamak.Utilities.UnitTest.ExtensionsTestClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdemCatamak.Utilities.UnitTest.ExtensionTest
{
    [TestClass]
    public class CreateInstanceTest
    {
        //NOTE : Parametresiz constructor sahibi olmayan sınıflarda kullanılamaz

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__CreateInstance_InSameAssembly_Type()
        {
            object actual = Extensions.CreateInstance(typeof(IsValidTestClass));


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as IsValidTestClass,
                             "Cast operation show that IsValidClass instance cannot be created");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__CreateInstance_InDifferentAssembly_Type()
        {
            object actual = Extensions.CreateInstance(typeof(CryptographyEngine));


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as CryptographyEngine,
                             "Cast operation show that IsValidClass instance cannot be created");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__CreateInstance_InSameAssembly()
        {
            string fullName = typeof(IsValidTestClass).AssemblyQualifiedName;


            object actual = Extensions.CreateInstance(fullName);


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as IsValidTestClass,
                             "Cast operation show that IsValidClass instance cannot be created");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest_ExtensionsTest__CreateInstance_InDifferentAssembly()
        {
            string fullName = typeof(CryptographyEngine).FullName;


            object actual = Extensions.CreateInstance(fullName);


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as CryptographyEngine,
                             "Cast operation show that IsValidClass instance cannot be created");
        }
    }
}