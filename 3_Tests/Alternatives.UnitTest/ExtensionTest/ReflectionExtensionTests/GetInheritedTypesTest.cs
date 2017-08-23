using System;
using System.Collections.Generic;
using System.Linq;
using Alternatives.Extensions;
using Alternatives.UnitTest.TestModel;
using Alternatives.UnitTest.TestModel.Implementations;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.ReflectionExtensionTests
{
    public class GetInheritedTypesTest
    {
        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__GetInheritedTypes_BaseInterface()
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

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__GetInheritedTypes_BaseGenericInterface()
        {
            // Act
            DateTime startTime = DateTime.Now;
            List<Type> typeList = ReflectionExtensions.GetInheritedTypes(typeof(IGenericInterface<>)).ToList();
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.AreEqual(2, typeList.Count, $"{string.Join(" - ", typeList.Select(t => t.Name))}");
            Assert.IsTrue(typeList.Contains(typeof(TestGenericInterface)));
            Assert.IsTrue(typeList.Contains(typeof(AnotherTestGenericInterface)));
        }


        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__GetInheritedTypes_BaseGenericInterfaceSpesific()
        {
            // Act
            DateTime startTime = DateTime.Now;
            List<Type> typeList = ReflectionExtensions.GetInheritedTypes(typeof(IGenericInterface<int>)).ToList();
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.AreEqual(1, typeList.Count, $"{string.Join(" - ", typeList.Select(t => t.Name))}");
            Assert.IsTrue(typeList.Contains(typeof(TestGenericInterface)));
        }



        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__GetInheritedTypes_BaseAbstractClassImplemetor_Parent()
        {
            // Act
            DateTime startTime = DateTime.Now;
            List<Type> typeList = ReflectionExtensions.GetInheritedTypes(typeof(SecondLevelAbstractClass)).ToList();
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.AreEqual(1, typeList.Count, $"{string.Join(" - ", typeList.Select(t => t.Name))}");
            Assert.IsTrue(typeList.Contains(typeof(TestAbstract)));
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__GetInheritedTypes_BaseAbstractClassImplemetor_GrandParent()
        {
            // Act
            DateTime startTime = DateTime.Now;
            List<Type> typeList = ReflectionExtensions.GetInheritedTypes(typeof(FirstLevelAbstract)).ToList();
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.AreEqual(2, typeList.Count, $"{string.Join(" - ", typeList.Select(t => t.Name))}");
            Assert.IsTrue(typeList.Contains(typeof(TestAbstract)));
        }
    }
}