using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Alternatives.ReflectionExtensions
{
    public static class ScanExtensions
    {
        [Obsolete("Assemblies should be supplied from user")]
        public static IEnumerable<Type> GetInheritedTypes(Type baseType, bool writeErrorToConsole = false)
        {
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

                assemblies = new Assembly[] { };
            }

            return GetInheritedTypes(baseType, writeErrorToConsole, assemblies);
        }

        public static IEnumerable<Type> GetInheritedTypes(this Type baseType, params Assembly[] assemblies)
        {
            return GetInheritedTypes(baseType, false, assemblies);
        }

        public static IEnumerable<Type> GetInheritedTypes(this Type baseType, bool writeErrorToConsole, params Assembly[] assemblies)
        {
            ConcurrentBag<Type> parentTypes = new ConcurrentBag<Type>();

            Parallel.ForEach(assemblies, assembly =>
                                         {
                                             IEnumerable<Type> types = SearchAssembly(assembly, baseType, writeErrorToConsole);
                                             foreach (Type type in types)
                                             {
                                                 parentTypes.Add(type);
                                             }
                                         });

            return parentTypes.Distinct().Where(t => t != baseType);
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
                                                    if (baseType.IsGenericTypeDefinition)
                                                    {
                                                        baseType = baseType.GetGenericTypeDefinition();
                                                    }

                                                    if (baseType.IsClass)
                                                    {
                                                        if (DoesMatchBaseType(type, baseType, t => new[] {t.BaseType}))
                                                        {
                                                            parentTypes.Add(type);
                                                        }
                                                    }
                                                    else if (baseType.IsInterface)
                                                    {
                                                        foreach (Type i in type.GetInterfaces())
                                                        {
                                                            if (DoesMatchBaseType(i, baseType, t =>
                                                                                               {
                                                                                                   List<Type> subTypes = t.GetInterfaces()
                                                                                                                          .ToList();
                                                                                                   subTypes.Add(t.BaseType);
                                                                                                   return subTypes;
                                                                                               }))
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

        private static bool DoesMatchBaseType(Type t, Type baseType, Func<Type, IEnumerable<Type>> getChildren)
        {
            if (t == null) return false;

            if (t == baseType)
            {
                return true;
            }

            if (t.IsGenericType && t.GetGenericTypeDefinition() == baseType)
            {
                return true;
            }

            IEnumerable<Type> subs = getChildren(t);

            bool result = subs.Any(x => DoesMatchBaseType(x, baseType, getChildren));
            return result;
        }
    }
}