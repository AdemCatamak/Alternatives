using System;
using System.Collections.Generic;
using System.Linq;
using Alternatives.Extensions;
using Alternatives.UnitTest.TestModel;
using Alternatives.UnitTest.TestModel.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Alternatives.UnitTest.ExtensionTest
{
    [TestClass]
    public class ModelCollectorTest
    {

        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__GetCollection_BaseInterface()
        {
            // Act
            DateTime startTime = DateTime.Now;
            List<Type> typeList = ReflectionExtensions.GetInheritedTypes(typeof(IInterface)).ToList();
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.AreEqual(2, typeList.Count, $"{string.Join(" - ", typeList.Select(t => t.Name))}");
            Assert.IsTrue(typeList.Contains(typeof(TestInterface)));
            Assert.IsTrue(typeList.Contains(typeof(AnotherTestInterface)));
        }
    }
}