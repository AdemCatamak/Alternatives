using Alternatives.Extensions;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.ReflectionExtensionTests
{
    public class CreateInstanceTest
    {
        //NOTE : Parametresiz constructor sahibi olmayan sınıflarda kullanılamaz

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
        public void CreateInstance_WhenTypeGiven()
        {
            object actual = ReflectionExtensions.CreateInstance(typeof(DummyClass));


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as DummyClass,
                             "Cast operation show that IsValidClass instance cannot be created");
        }

        [Test]
        public void CreateInstance_WhenFullNameGiven()
        {
            string fullName = typeof(DummyClass).AssemblyQualifiedName;


            object actual = ReflectionExtensions.CreateInstance(fullName);


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as DummyClass,
                             "Cast operation show that IsValidClass instance cannot be created");
        }
    }
}