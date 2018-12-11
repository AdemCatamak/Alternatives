using System.Collections.Generic;
using System.Linq;
using Alternatives.DynamicFilterTests.Models;
using Newtonsoft.Json.Linq;

namespace Alternatives.DynamicFilterTests.Storage
{
    public class UserRepository
    {
        public IQueryable<UserModel> GetUsers()
        {
            return _users;
        }

        private readonly IQueryable<UserModel> _users = new List<UserModel>()
                                                        {
                                                            new UserModel()
                                                            {
                                                                Id = 1,
                                                                Email = "1@a.com",
                                                                SomeCount = 4,
                                                                Attributes = JObject.Parse("{\"age\":27, \"numberOfCar\":0, \"city\" : \"istanbul\"}")
                                                            },
                                                            new UserModel()
                                                            {
                                                                Id = 2,
                                                                Email = "2@a.com",
                                                                SomeCount = 44,
                                                                Attributes = JObject.Parse("{\"age\":45, \"numberOfCar\":2, \"city\" : \"istanbul\"}"),
                                                            },
                                                            new UserModel()
                                                            {
                                                                Id = 3,
                                                                Email = "3@a.com",
                                                                SomeCount = 34,
                                                                Attributes = JObject.Parse("{\"age\":70, \"numberOfCar\":1, \"city\" : \"ankara\"}")
                                                            }
                                                        }.AsQueryable();
    };
}