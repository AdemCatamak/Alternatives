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
                                                                Address = new Address()
                                                                          {
                                                                              City = "Istanbul",
                                                                              Country = "Turkey"
                                                                          }
                                                            },
                                                            new UserModel()
                                                            {
                                                                Id = 2,
                                                                Email = "2@a.com",
                                                                SomeCount = 44,
                                                                Address = new Address()
                                                                          {
                                                                              City = "Berlin",
                                                                              Country = "Germany"
                                                                          }
                                                            },
                                                            new UserModel()
                                                            {
                                                                Id = 3,
                                                                Email = "3@a.com",
                                                                SomeCount = 34,
                                                                Address = new Address()
                                                                          {
                                                                              City = "Ankara",
                                                                              Country = "Turkey"
                                                                          }
                                                            }
                                                        }.AsQueryable();
    };
}