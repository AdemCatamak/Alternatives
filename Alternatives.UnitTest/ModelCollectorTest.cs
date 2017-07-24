using System;
using System.Collections.Generic;
using System.Linq;
using Alternatives.UnitTest.TestModel;
using Alternatives.UnitTest.TestModel.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Alternatives.UnitTest
{
    [TestClass]
    public class ModelCollectorTest
    {

        [TestMethod]
        public void AdemCatamak_DAL_UnitTest__ModelCollectorTest__GetCollection_BaseInterface()
        {
            // Act
            DateTime startTime = DateTime.Now;
            List<Type> typeList = ModelCollector.GetInheritedTypes(typeof(IInterface)).ToList();
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.AreEqual(2, typeList.Count, $"{string.Join(" - ", typeList.Select(t => t.Name))}");
            Assert.IsTrue(typeList.Contains(typeof(TestInterface)));
            Assert.IsTrue(typeList.Contains(typeof(AnotherTestInterface)));
        }
    }
}