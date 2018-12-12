using System;
using System.Collections.Generic;
using System.Linq;
using Alternatives.DynamicFilter;
using Alternatives.DynamicFilter.Operator;
using Alternatives.DynamicFilterTests.Models;
using Alternatives.DynamicFilterTests.Storage;
using Xunit;

namespace Alternatives.DynamicFilterTests
{
    public class DynamicFilterItemTest
    {
        [Theory]
        [InlineData(null, Operators.LessThan, 5)]
        [InlineData("Id", (Operators) (-1), 5)]
        [InlineData("Id", Operators.LessThan, null)]
        public void WhenArgumentsIsNull_ArgumentNullExceptionShouldBeThrown(string propertyName, Operators op, object value)
        {
            Assert.Throws<ArgumentNullException>(() => { new DynamicFilterItem(propertyName, op, value); });
        }

        [Fact]
        public void WhenPropertyDoesNotFound_ArgumentExceptionShouldBeThrown()
        {
            Assert.Throws<ArgumentException>(() =>
                                             {
                                                 var dynamicFilterItem = new DynamicFilterItem("Not" + nameof(UserModel.Id), Operators.LessThan, 5);
                                                 dynamicFilterItem.Apply(new List<UserModel>().AsQueryable());
                                             });
        }

        [Fact]
        public void WhenPropertyDoesNotMatchSuppliedProperty_ArgumentExceptionShouldBeThrown()
        {
            Assert.Throws<ArgumentException>(() =>
                                             {
                                                 var dynamicFilterItem = new DynamicFilterItem(nameof(UserModel.Email), Operators.LessThan, 5);
                                                 dynamicFilterItem.Apply(new List<UserModel>().AsQueryable());
                                             });
        }

        [Fact]
        public void WhenOperationCannotAppliedProperty_InvalidOperationExceptionShouldBeThrown()
        {
            Assert.Throws<InvalidOperationException>(() =>
                                                     {
                                                         var dynamicFilterItem = new DynamicFilterItem(nameof(UserModel.Address), Operators.GreaterThan, new Address() {City = "Istanbul", Country = "Turkey"});
                                                         dynamicFilterItem.Apply(new List<UserModel>().AsQueryable());
                                                     });
        }

        [Fact]
        public void WhenPropertyMatchSuppliedPropertyAndTypeCanBeApplied_ResponseShouldBeFiltered()
        {
            var dynamicFilterItem = new DynamicFilterItem(nameof(UserModel.Id), Operators.Equal, 1);

            IQueryable<UserModel> users = new UserRepository().GetUsers();

            users = dynamicFilterItem.Apply(users);

            Assert.Single(users);
            Assert.Equal(1, users.First().Id);
        }
    }
}