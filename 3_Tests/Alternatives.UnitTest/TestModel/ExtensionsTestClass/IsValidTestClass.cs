using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alternatives.CustomDataAnnotations;

namespace Alternatives.UnitTest.TestModel.ExtensionsTestClass
{
    [Table("asd")]
    internal class IsValidTestClass : IsValidTestClassPartial
    {
        [TurkeyPhone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Phone, Required]
        public string RequiredPhone { get; set; }
    }
}