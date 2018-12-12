using Newtonsoft.Json.Linq;

namespace Alternatives.DynamicFilterTests.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int SomeCount { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
}