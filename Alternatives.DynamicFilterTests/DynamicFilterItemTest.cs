using System;
using System.Collections.Generic;
using System.Linq;
using Alternatives.DynamicFilter;
using Alternatives.DynamicFilter.Operator;
using Alternatives.DynamicFilterTests.Models;
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
            Assert.Throws<ArgumentNullException>(() =>
                                                 {
                                                      new DynamicFilterItem(propertyName, op, value);
                                                 });
        }

        [Fact]
        public void WhenPropertyDoesNotFound_ArgumentExceptionShouldBeThrown()
        {
            Assert.Throws<ArgumentException>(() =>
                                             {
                                                 var dynamicFilterItem = new DynamicFilterItem("NotExistProperty", Operators.LessThan, 5);
                                                 dynamicFilterItem.Apply(new List<UserModel>().AsQueryable());
                                             });
        }
    }
}