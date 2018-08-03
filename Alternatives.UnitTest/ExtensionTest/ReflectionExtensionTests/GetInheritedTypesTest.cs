using System;
using System.Collections.Generic;
using System.Linq;
using Alternatives.Extensions;
using Xunit;

namespace Alternatives.UnitTest.ExtensionTest.ReflectionExtensionTests
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
            List<Type> typeList = ReflectionExtensions.GetInheritedTypes(typeof(IInterface)).ToList();
#pragma warning restore 618
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Equal(2, typeList.Count);
            Assert.True(typeList.Contains(typeof(TestInterface)));
            Assert.True(typeList.Contains(typeof(AnotherTestInterface)));
        }

        [Fact]
        public void Obsolete_GetInheritedTypes_BaseGenericInterface()
        {
            // Act
            DateTime startTime = DateTime.Now;
#pragma warning disable 618
            List<Type> typeList = ReflectionExtensions.GetInheritedTypes(typeof(IGenericInterface<>)).ToList();
#pragma warning restore 618
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Equal(2, typeList.Count);
            Assert.True(typeList.Contains(typeof(TestGenericInterface)));
            Assert.True(typeList.Contains(typeof(AnotherTestGenericInterface)));
        }


        [Fact]
        public void Obsolete_GetInheritedTypes_BaseGenericInterfaceSpesific()
        {
            // Act
            DateTime startTime = DateTime.Now;
#pragma warning disable 618
            List<Type> typeList = ReflectionExtensions.GetInheritedTypes(typeof(IGenericInterface<int>)).ToList();
#pragma warning restore 618
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Equal(1, typeList.Count);
            Assert.True(typeList.Contains(typeof(TestGenericInterface)));
        }


        [Fact]
        public void Obsolete_GetInheritedTypes_BaseAbstractClassImplemetor_Parent()
        {
            // Act
            DateTime startTime = DateTime.Now;
#pragma warning disable 618
            List<Type> typeList = ReflectionExtensions.GetInheritedTypes(typeof(SecondLevelAbstractClass)).ToList();
#pragma warning restore 618
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Equal(1, typeList.Count);
            Assert.True(typeList.Contains(typeof(TestAbstract)));
        }

        [Fact]
        public void Obsolete_GetInheritedTypes_BaseAbstractClassImplemetor_GrandParent()
        {
            // Act
            DateTime startTime = DateTime.Now;
#pragma warning disable 618
            List<Type> typeList = ReflectionExtensions.GetInheritedTypes(typeof(FirstLevelAbstract)).ToList();
#pragma warning restore 618
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Equal(2, typeList.Count);
            Assert.True(typeList.Contains(typeof(TestAbstract)));
        }

        [Fact]
        public void Obsolete_GetInheritedTypes_BaseAbstractClassImplemetor_GenericAbstractClass()
        {
            // Act
            DateTime startTime = DateTime.Now;
#pragma warning disable 618
            List<Type> typeList = ReflectionExtensions.GetInheritedTypes(typeof(GenericAbstract<>)).ToList();
#pragma warning restore 618
            DateTime endTime = DateTime.Now;


            Console.WriteLine($"Runtime : {endTime.Ticks - startTime.Ticks:#,0}");

            // Assert
            Assert.Equal(1, typeList.Count);
            Assert.True(typeList.Contains(typeof(GenericAbstractClassImplementation)));
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
            Assert.True(typeList.Contains(typeof(TestInterface)));
            Assert.True(typeList.Contains(typeof(AnotherTestInterface)));
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
            Assert.True(typeList.Contains(typeof(TestGenericInterface)));
            Assert.True(typeList.Contains(typeof(AnotherTestGenericInterface)));
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
            Assert.Equal(1, typeList.Count);
            Assert.True(typeList.Contains(typeof(TestGenericInterface)));
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
            Assert.Equal(1, typeList.Count);
            Assert.True(typeList.Contains(typeof(TestAbstract)));
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
            Assert.True(typeList.Contains(typeof(TestAbstract)));
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
            Assert.Equal(1, typeList.Count);
            Assert.True(typeList.Contains(typeof(GenericAbstractClassImplementation)));
        }
    }
}