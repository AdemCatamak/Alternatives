namespace Alternatives.UnitTest.TestModel.ExtensionsTestClass
{
    public class DummyClass
    {
        public string StringField { get; set; }
        public int IntField { get; set; }

        public InnerDummyClass InnerClassField { get; set; }
    }

    public class InnerDummyClass
    {
        public string InnerDummyStringField { get; set; }
    }
}