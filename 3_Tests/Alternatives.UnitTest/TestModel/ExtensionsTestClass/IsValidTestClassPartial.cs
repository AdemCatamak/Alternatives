using System.ComponentModel.DataAnnotations;

namespace Alternatives.UnitTest.TestModel.ExtensionsTestClass
{
    public class IsValidTestClassPartial
    {
        [Key]
        public int Id { get; set; }

        public int? ExtraData { get; set; }
    }
}