using Alternatives.Extensions;
using Xunit;

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

        [Fact]
        public void CreateInstance_WhenTypeGiven()
        {
            object actual = typeof(DummyClass).CreateInstance();


            Assert.NotNull(actual);
            Assert.NotNull(actual as DummyClass);
        }

        [Fact]
        public void CreateInstance_WhenFullNameGiven()
        {
            string fullName = typeof(DummyClass).AssemblyQualifiedName;


            object actual = ReflectionExtensions.CreateInstance(fullName);


            Assert.NotNull(actual);
            Assert.NotNull(actual as DummyClass);
        }
    }
}