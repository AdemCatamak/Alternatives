using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Alternatives.Extensions
{
    public static class ReflectionExtensions
    {
        public static bool TryGetFieldValue<T>(this object obj, string fieldName, out T result) where T : class
        {
            bool isValid = true;
            result = null;
            try
            {
                PropertyInfo propertyInfo = obj.GetType().GetProperty(fieldName);
                if (propertyInfo != null)
                {
                    result = propertyInfo.GetValue(obj) as T;
                    if (result == null)
                        isValid = false;
                }
                else
                {
                    isValid = false;
                }
            }
            catch
            {
                isValid = false;
            }

            return isValid;
        }

        public static bool TryGetFieldValue<T>(this object obj, string fieldName, out T? result) where T : struct
        {
            bool isValid = true;
            result = null;

            try
            {
                PropertyInfo propertyInfo = obj.GetType().GetProperty(fieldName);
                if (propertyInfo != null)
                {
                    object value = propertyInfo.GetValue(obj);
                    if (value != null)
                    {
                        result = value as T?;
                        if (result == null)
                            isValid = false;
                    }
                }
                else
                {
                    isValid = false;
                }
            }
            catch
            {
                isValid = false;
            }

            return isValid;
        }


        public static T GetFieldValue<T>(this object obj, string fieldName)
        {
            T result;
            PropertyInfo propertyInfo = obj.GetType().GetProperty(fieldName);
            if (propertyInfo != null)
            {
                result = (T) propertyInfo.GetValue(obj);
            }
            else
            {
                throw new FieldAccessException($"{fieldName} cannot be found");
            }
            return result;
        }


        public static bool TrySetFieldValue<T>(this object obj, string fieldName, T value)
        {
            bool isValid = true;
            try

            {
                PropertyInfo propertyInfo = obj.GetType().GetProperty(fieldName);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(obj, value);
                }
                else
                {
                    isValid = false;
                }
            }
            catch
            {
                isValid = false;
            }
            return isValid;
        }


        public static IEnumerable<Type> GetInheritedTypes(Type baseType, bool writeErrorToConsole = false)
        {
            Task<IEnumerable<Type>> appDomainTask = Task.Factory.StartNew(() => GetInheritedTypesFromAppDomain(baseType, writeErrorToConsole));

            IEnumerable<Type> typesFromAppDomain = appDomainTask.Result;

            return typesFromAppDomain.Distinct().Where(t => t != baseType);
        }

        private static IEnumerable<Type> GetInheritedTypesFromAppDomain(Type baseType, bool writeErrorToConsole)
        {
            ConcurrentBag<Type> concurrentBag = new ConcurrentBag<Type>();
            AppDomain domain = AppDomain.CurrentDomain;

            Assembly[] assemblies;
            try
            {
                assemblies = domain.GetAssemblies();
            }
            catch (Exception ex)
            {
                if (writeErrorToConsole)
                {
                    Console.WriteLine($"AppDomain - GetAssemblies fail{Environment.NewLine}" +
                                      $"Exception Message : {ex.Message}{Environment.NewLine}" +
                                      $"Exception : {ex}");
                }
                assemblies = new Assembly[]
                             {
                             };
            }

            Parallel.ForEach(assemblies, assembly =>
                                         {
                                             List<Type> typeList = SearchAssembly(assembly, baseType, writeErrorToConsole).ToList();
                                             typeList.ForEach(t => concurrentBag.Add(t));
                                         }
                            );
            return concurrentBag;
        }

        private static IEnumerable<Type> SearchAssembly(Assembly assembly, Type baseType, bool writeErrorToConsole)
        {
            ConcurrentBag<Type> parentTypes = new ConcurrentBag<Type>();
            Type[] assemblyTypes;
            try
            {
                assemblyTypes = assembly.GetTypes();
            }
            catch (Exception ex)
            {
                if (writeErrorToConsole)
                {
                    Console.WriteLine($"Assembly's types cannot be read : {assembly} {Environment.NewLine}" +
                                      $"Exception Message : {ex.Message} {Environment.NewLine}" +
                                      $"Exception : {ex}");
                }
                assemblyTypes = new Type[] { };
            }

            Parallel.ForEach(assemblyTypes, type =>
                                            {
                                                try
                                                {
                                                    if (baseType.IsClass)
                                                    {
                                                        if (DoesMatchBaseClass(type, baseType))
                                                        {
                                                            parentTypes.Add(type);
                                                        }
                                                    }
                                                    else if (baseType.IsInterface)
                                                    {
                                                        bool isGeneric = baseType.IsGenericTypeDefinition;
                                                        foreach (Type i in type.GetInterfaces())
                                                        {
                                                            if (isGeneric && i.GetGenericTypeDefinition() == baseType)
                                                            {
                                                                parentTypes.Add(type);
                                                            }
                                                            else if (i == baseType)
                                                            {
                                                                parentTypes.Add(type);
                                                            }
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    if (writeErrorToConsole)
                                                    {
                                                        Console.WriteLine($"Assembly : {assembly} {Environment.NewLine}" +
                                                                          $"Type : {type} {Environment.NewLine}" +
                                                                          $"Exception Message : {ex.Message} {Environment.NewLine}" +
                                                                          $"Exception : {ex}");
                                                    }
                                                }
                                            });
            return parentTypes;
        }

        private static bool DoesMatchBaseClass(Type t, Type baseType)
        {
            while (true)
            {
                if (t == baseType)
                {
                    return true;
                }

                if (t.BaseType != null)
                {
                    t = t.BaseType;
                    continue;
                }

                return false;
            }
        }


        public static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static object CreateInstance(string strFullyQualifiedName)
        {
            object result = null;
            Type type = Type.GetType(strFullyQualifiedName);
            if (type != null)
            {
                result = Activator.CreateInstance(type);
            }
            if (result == null)
            {
                foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    type = asm.GetType(strFullyQualifiedName);
                    if (type != null)
                    {
                        result = Activator.CreateInstance(type);
                    }
                }
            }
            return result;
        }
    }
}