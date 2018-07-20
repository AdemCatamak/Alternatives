using System;
using System.Collections.Generic;
using System.Linq;
using Alternatives.Extensions;
using Xunit;

namespace Alternatives.UnitTest.ExtensionTest.GeneralExtensionTests
{
    public class GetAllSubsetTest
    {
        [Fact]
        public void WhenCollectionIsNull_ResponseShouldBeEmpty()
        {
            var result = ((int[])null).GetAllSubSet();

            Assert.Empty(result);
        }

        [Fact]
        public void WhenCollectionIsEmpty_ResponseShouldContainsOnlyOneItem()
        {
            var source = new List<int>();
            var result = source.GetAllSubSet();

            Assert.NotEmpty(result);
            Assert.Equal(1, result.Count());
            Assert.Contains(new List<int>(), result);
        }

        [Fact]
        public void WhenCollectionContainsOneItem_ResponseShouldContainsTwoItems()
        {
            var source = new List<int> { 3 };
            var result = source.GetAllSubSet();

            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(new List<int>(), result);
        }

        [Fact]
        public void WhenCollectionContainsTwoItem_ResponseShouldContains4Items()
        {

            var source = new[] { 4, 7 };
            var result = source.GetAllSubSet();

            Assert.NotEmpty(result);
            Assert.Equal(4, result.Count());
            Assert.Contains(new List<int>(), result);
            Assert.Contains(new List<int> { 4 }, result);
            Assert.Contains(new List<int> { 7 }, result);
            Assert.Contains(new List<int> { 4, 7 }, result);
        }

        [Theory]
        [MemberData(nameof(SourceContainsMultipleItem))]
        public void WhenCollectionContainsManyItems_ResponseShouldContainsItems_PowerOfTwo(string source)
        {
            int expectedCount = Math.Pow(2, source.Length).ToInt();
            var result = source.ToList().GetAllSubSet();

            Assert.NotEmpty(result);
            Assert.Equal(expectedCount, result.Count());
        }

        public static IEnumerable<object[]> SourceContainsMultipleItem()
        {
            return new List<object[]>
            {
                new object[] { "123" },
                new object[] { "1234" },
                new object[] { "12345" },
            };
        }
    }
}
