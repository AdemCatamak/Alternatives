using System.ComponentModel.DataAnnotations;
using Alternatives.CustomDataAnnotations;

namespace Alternatives.UnitTest.TestModel.ExtensionsTestClass
{
    public class DataTableTestClass
    {
        [Key]
        public int Id { get; set; }

        [TurkeyPhone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        public int? ExtraData { get; set; }
    }
}