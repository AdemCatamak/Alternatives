using Newtonsoft.Json.Linq;

namespace Alternatives.DynamicFilterTests.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public long SomeCount { get; set; }
        public JObject Attributes { get; set; }
    }
}