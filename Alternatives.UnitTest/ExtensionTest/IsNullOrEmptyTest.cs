using System.Collections.Generic;
using Alternatives.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest
{
    [TestClass]
    public class IsNullOrEmptyTest
    {
        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_Null()
        {
            // Act
            bool isNullOrEmpty = ((List<string>) null).IsNullOrEmpty();


            // Assert
            Assert.IsTrue(isNullOrEmpty);
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_Empty_NotFiltered()
        {
            // Arrange
            List<string> list = new List<string>();


            // Act
            bool isNullOrEmpty = list.IsNullOrEmpty();


            // Assert
            Assert.IsTrue(isNullOrEmpty);
        }


        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_Empty_Filtered()
        {
            // Arrange
            List<string> list = new List<string>();


            // Act
            bool isNullOrEmpty = list.IsNullOrEmpty(x=>x == "test");


            // Assert
            Assert.IsTrue(isNullOrEmpty);
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_NotEmpty_Filtered_NotMatch()
        {
            // Arrange
            List<string> list = new List<string>{"test"};


            // Act
            bool isNullOrEmpty = list.IsNullOrEmpty(x => x == "notmatch");


            // Assert
            Assert.IsTrue(isNullOrEmpty);
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_NotEmpty_Filtered_Match()
        {
            // Arrange
            List<string> list = new List<string> { "test" };


            // Act
            bool isNullOrEmpty = list.IsNullOrEmpty(x => x == "test");


            // Assert
            Assert.IsFalse(isNullOrEmpty);
        }

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_NotEmpty_NotFiltered()
        {
            // Arrange
            List<string> list = new List<string> { "test" };


            // Act
            bool isNullOrEmpty = list.IsNullOrEmpty();


            // Assert
            Assert.IsFalse(isNullOrEmpty);
        }
    }
}