using System.Collections.Generic;
using System.Linq;
using Alternatives.CollectionExtensions;
using Xunit;

namespace Alternatives.CollectionExtensionsTests
{
    public class IsNullOrEmptyTest
    {
        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_Null__IEnumarable()
        {
            // Act
            bool isNullOrEmpty = ((List<string>) null).IsNullOrEmpty();


            // Assert
            Assert.True(isNullOrEmpty);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_Null__IQuaryable()
        {
            // Act
            bool isNullOrEmpty = ((IQueryable<int>)null).IsNullOrEmpty();


            // Assert
            Assert.True(isNullOrEmpty);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_Empty_NotFiltered()
        {
            // Act
            bool isNullOrEmpty = new List<string>().IsNullOrEmpty();


            // Assert
            Assert.True(isNullOrEmpty);
        }


        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_Empty_Filtered()
        {
            // Act
            bool isNullOrEmpty = new List<string>().IsNullOrEmpty(x=>x == "test");


            // Assert
            Assert.True(isNullOrEmpty);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_NotEmpty_Filtered_NotMatch()
        {
            // Arrange
            List<string> list = new List<string>{"test"};


            // Act
            bool isNullOrEmpty = list.IsNullOrEmpty(x => x == "notmatch");


            // Assert
            Assert.True(isNullOrEmpty);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_NotEmpty_Filtered_Match()
        {
            // Arrange
            List<string> list = new List<string> { "test" };


            // Act
            bool isNullOrEmpty = list.IsNullOrEmpty(x => x == "test");


            // Assert
            Assert.False(isNullOrEmpty);
        }

        [Fact]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_NotEmpty_NotFiltered()
        {
            // Arrange
            List<string> list = new List<string> { "test" };


            // Act
            bool isNullOrEmpty = list.IsNullOrEmpty();


            // Assert
            Assert.False(isNullOrEmpty);
        }
    }
}