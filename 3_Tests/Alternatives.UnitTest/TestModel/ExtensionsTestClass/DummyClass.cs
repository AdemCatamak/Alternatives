namespace Alternatives.UnitTest.TestModel.ExtensionsTestClass
{
    public class DummyClass
    {
        public string StringField { get; set; }
        public int IntField { get; set; }

        public InnerDummyClass InnerClassField { get; set; }

        private InnerDummyClass InnerPrivateFild { get; set; } = new InnerDummyClass {InnerDummyStringField = "private"};
    }

    public class InnerDummyClass
    {
        public string InnerDummyStringField { get; set; }
    }
}