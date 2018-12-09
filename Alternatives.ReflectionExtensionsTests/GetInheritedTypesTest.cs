using System;
using System.Collections.Generic;
using System.Linq;
using Alternatives.ReflectionExtensions;
using Xunit;

namespace Alternatives.ReflectionExtensionsTests
{
    public class GetInheritedTypesTest
    {
        #region TestModel
        private interface IInterface
        {
        }

        private interface IGenericInterface<T>
        {
            T GenericField { get; set; }
        }

        private abstract class GenericAbstract<T>
        {
            public string GetTypeName = typeof(T).Name;
        }

        private abstract class FirstLevelAbstract
        {
        }

        private abstract class SecondLevelAbstractClass : FirstLevelAbstract
        {
        }

        private class TestAbstract : SecondLevelAbstractClass
        {
        }

        private class TestInterface : IInterface
        {
        }

        private class TestGenericInterface : IGenericInterface<int>
        {
            public int GenericField { get; set; }
        }

        private class AnotherTestInterface : IInterface
        {
        }

        private class AnotherTestGenericInterface : IGenericInterface<string>
        {
            public string GenericField { get; set; }
        }

        private class GenericAbstractClassImplementation : GenericAbstract<AnotherTestGenericInterface>
        {
        }

        #endregion

        [Fact]
        public void Obsolete_GetInheritedTypes_BaseInterface()
        {
            // Act
            DateTime startTime = DateTime.Now;
#pragma warning disable 618
            List<Type> typeList = ScanExtensions.GetInheritedTypes(typeof(IInterface))
                                                .ToList();
#pragma warning restore 618
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Equal(2, typeList.Count);
            Assert.Contains(typeof(TestInterface), typeList);
            Assert.Contains(typeof(AnotherTestInterface), typeList);
        }

        [Fact]
        public void Obsolete_GetInheritedTypes_BaseGenericInterface()
        {
            // Act
            DateTime startTime = DateTime.Now;
#pragma warning disable 618
            List<Type> typeList = ScanExtensions.GetInheritedTypes(typeof(IGenericInterface<>)).ToList();
#pragma warning restore 618
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Equal(2, typeList.Count);
            Assert.Contains(typeof(TestGenericInterface), typeList);
            Assert.Contains(typeof(AnotherTestGenericInterface), typeList);
        }


        [Fact]
        public void Obsolete_GetInheritedTypes_BaseGenericInterfaceSpesific()
        {
            // Act
            DateTime startTime = DateTime.Now;
#pragma warning disable 618
            List<Type> typeList = ScanExtensions.GetInheritedTypes(typeof(IGenericInterface<int>)).ToList();
#pragma warning restore 618
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Single(typeList);
            Assert.Contains(typeof(TestGenericInterface), typeList);
        }


        [Fact]
        public void Obsolete_GetInheritedTypes_BaseAbstractClassImplemetor_Parent()
        {
            // Act
            DateTime startTime = DateTime.Now;
#pragma warning disable 618
            List<Type> typeList = ScanExtensions.GetInheritedTypes(typeof(SecondLevelAbstractClass)).ToList();
#pragma warning restore 618
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Single(typeList);
            Assert.Contains(typeof(TestAbstract), typeList);
        }

        [Fact]
        public void Obsolete_GetInheritedTypes_BaseAbstractClassImplemetor_GrandParent()
        {
            // Act
            DateTime startTime = DateTime.Now;
#pragma warning disable 618
            List<Type> typeList = ScanExtensions.GetInheritedTypes(typeof(FirstLevelAbstract)).ToList();
#pragma warning restore 618
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Equal(2, typeList.Count);
            Assert.Contains(typeof(TestAbstract), typeList);
        }

        [Fact]
        public void Obsolete_GetInheritedTypes_BaseAbstractClassImplemetor_GenericAbstractClass()
        {
            // Act
            DateTime startTime = DateTime.Now;
#pragma warning disable 618
            List<Type> typeList = ScanExtensions.GetInheritedTypes(typeof(GenericAbstract<>)).ToList();
#pragma warning restore 618
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Single(typeList);
            Assert.Contains(typeof(GenericAbstractClassImplementation), typeList);
        }


        [Fact]
        public void GetInheritedTypes_BaseInterface()
        {
            // Act
            DateTime startTime = DateTime.Now;
            List<Type> typeList = typeof(IInterface).GetInheritedTypes(AppDomain.CurrentDomain.GetAssemblies()).ToList();
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Equal(2, typeList.Count);
            Assert.Contains(typeof(TestInterface), typeList);
            Assert.Contains(typeof(AnotherTestInterface), typeList);
        }

        [Fact]
        public void GetInheritedTypes_BaseGenericInterface()
        {
            // Act
            DateTime startTime = DateTime.Now;
            List<Type> typeList = typeof(IGenericInterface<>).GetInheritedTypes(AppDomain.CurrentDomain.GetAssemblies()).ToList();
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Equal(2, typeList.Count);
            Assert.Contains(typeof(TestGenericInterface), typeList);
            Assert.Contains(typeof(AnotherTestGenericInterface), typeList);
        }


        [Fact]
        public void GetInheritedTypes_BaseGenericInterfaceSpesific()
        {
            // Act
            DateTime startTime = DateTime.Now;
            List<Type> typeList = typeof(IGenericInterface<int>).GetInheritedTypes(AppDomain.CurrentDomain.GetAssemblies()).ToList();
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Single(typeList);
            Assert.Contains(typeof(TestGenericInterface), typeList);
        }


        [Fact]
        public void GetInheritedTypes_BaseAbstractClassImplemetor_Parent()
        {
            // Act
            DateTime startTime = DateTime.Now;
            List<Type> typeList = typeof(SecondLevelAbstractClass).GetInheritedTypes(AppDomain.CurrentDomain.GetAssemblies()).ToList();
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Single(typeList);
            Assert.Contains(typeof(TestAbstract), typeList);
        }

        [Fact]
        public void GetInheritedTypes_BaseAbstractClassImplemetor_GrandParent()
        {
            // Act
            DateTime startTime = DateTime.Now;
            List<Type> typeList = typeof(FirstLevelAbstract).GetInheritedTypes(AppDomain.CurrentDomain.GetAssemblies()).ToList();
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Equal(2, typeList.Count);
            Assert.Contains(typeof(TestAbstract), typeList);
        }

        [Fact]
        public void GetInheritedTypes_BaseAbstractClassImplemetor_GenericAbstractClass()
        {
            // Act
            DateTime startTime = DateTime.Now;
            List<Type> typeList = typeof(GenericAbstract<>).GetInheritedTypes(AppDomain.CurrentDomain.GetAssemblies()).ToList();
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Single(typeList);
            Assert.Contains(typeof(GenericAbstractClassImplementation), typeList);
        }
    }
}