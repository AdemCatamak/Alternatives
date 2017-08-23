﻿using System.Collections.Generic;
using Alternatives.Extensions;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.GeneralExtensionTests
{
    public class IsNullOrEmptyTest
    {
        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_Null()
        {
            // Act
            bool isNullOrEmpty = ((List<string>) null).IsNullOrEmpty();


            // Assert
            Assert.IsTrue(isNullOrEmpty);
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_Empty_NotFiltered()
        {
            // Act
            bool isNullOrEmpty = new List<string>().IsNullOrEmpty();


            // Assert
            Assert.IsTrue(isNullOrEmpty);
        }


        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_Empty_Filtered()
        {
            // Act
            bool isNullOrEmpty = new List<string>().IsNullOrEmpty(x=>x == "test");


            // Assert
            Assert.IsTrue(isNullOrEmpty);
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_NotEmpty_Filtered_NotMatch()
        {
            // Arrange
            List<string> list = new List<string>{"test"};


            // Act
            bool isNullOrEmpty = list.IsNullOrEmpty(x => x == "notmatch");


            // Assert
            Assert.IsTrue(isNullOrEmpty);
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__IsNullOrEmpty_NotEmpty_Filtered_Match()
        {
            // Arrange
            List<string> list = new List<string> { "test" };


            // Act
            bool isNullOrEmpty = list.IsNullOrEmpty(x => x == "test");


            // Assert
            Assert.IsFalse(isNullOrEmpty);
        }

        [Test]
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