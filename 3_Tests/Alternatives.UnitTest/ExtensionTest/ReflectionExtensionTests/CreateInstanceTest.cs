using Alternatives.Extensions;
using Alternatives.UnitTest.TestModel.ExtensionsTestClass;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.ReflectionExtensionTests
{
    public class CreateInstanceTest
    {
        //NOTE : Parametresiz constructor sahibi olmayan sınıflarda kullanılamaz

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__CreateInstance_InSameAssembly_Type()
        {
            object actual = ReflectionExtensions.CreateInstance(typeof(IsValidTestClass));


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as IsValidTestClass,
                             "Cast operation show that IsValidClass instance cannot be created");
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__CreateInstance_InDifferentAssembly_Type()
        {
            object actual = ReflectionExtensions.CreateInstance(typeof(DummyClass));


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as DummyClass,
                             "Cast operation show that IsValidClass instance cannot be created");
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__CreateInstance_InSameAssembly()
        {
            string fullName = typeof(IsValidTestClass).AssemblyQualifiedName;


            object actual = ReflectionExtensions.CreateInstance(fullName);


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as IsValidTestClass,
                             "Cast operation show that IsValidClass instance cannot be created");
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__CreateInstance_InDifferentAssembly()
        {
            string fullName = typeof(DummyClass).FullName;


            object actual = ReflectionExtensions.CreateInstance(fullName);


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as DummyClass,
                             "Cast operation show that IsValidClass instance cannot be created");
        }
    }
}