using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Alternatives
{
    public static class ModelCollector
    {
        public static IEnumerable<Type> GetInheritedTypes(Type baseType)
        {
            Task<IEnumerable<Type>> dllTask = Task.Factory.StartNew(() => GetInheritedTypesFromDll(baseType));
            Task<IEnumerable<Type>> appDomainTask = Task.Factory.StartNew(() => GetInheritedTypesFromAppDomain(baseType));

            IEnumerable<Type> typesFromDll = dllTask.Result;
            IEnumerable<Type> typesFromAppDomain = appDomainTask.Result;

            List<Type> typeList = new List<Type>();
            typeList.AddRange(typesFromDll);
            typeList.AddRange(typesFromAppDomain);

            return typeList.Distinct();
        }


        private static IEnumerable<Type> GetInheritedTypesFromAppDomain(Type baseType)
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
                Console.WriteLine($"AppDomain - GetAssemblies fail{Environment.NewLine}" +
                                  $"Exception Message : {ex.Message}{Environment.NewLine}" +
                                  $"Exception : {ex}");
                assemblies = new Assembly[] { };
            }

            Parallel.ForEach(assemblies, assembly =>
                                         {
                                             List<Type> typeList = SearchAssembly(assembly, baseType).ToList();
                                             typeList.ForEach(t => concurrentBag.Add(t));
                                         }
                            );

            return concurrentBag;
        }

        private static IEnumerable<Type> GetInheritedTypesFromDll(Type baseType)
        {
            ConcurrentBag<Type> concurrentBag = new ConcurrentBag<Type>();

            string[] files;
            try
            {
                files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            }
            catch (Exception ex)
            {
                files = new string[] { };
                Console.WriteLine($"FromDll - files cannot be reached{Environment.NewLine}" +
                                  $"Exception Message : {ex.Message}{Environment.NewLine}" +
                                  $"Exception : {ex}");
            }

            Parallel.ForEach(files, file =>
                                    {
                                        Assembly assembly = null;
                                        try
                                        {
                                            assembly = Assembly.LoadFrom(file);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"FromDll - File : {file} {Environment.NewLine}" +
                                                              $"Exception Message : {ex.Message} {Environment.NewLine}" +
                                                              $"Exception : {ex}");
                                        }

                                        if (assembly != null)
                                        {
                                            List<Type> types = SearchAssembly(assembly, baseType).ToList();
                                            types.ForEach(t => concurrentBag.Add(t));
                                        }
                                    });

            return concurrentBag;
        }

        private static IEnumerable<Type> SearchAssembly(Assembly assembly, Type baseType)
        {
            List<Type> parentTypes = new List<Type>();

            Type[] assemblyTypes;
            try
            {
                assemblyTypes = assembly.GetTypes();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Assembly's types cannot be read : {assembly} {Environment.NewLine}" +
                                  $"Exception Message : {ex.Message} {Environment.NewLine}" +
                                  $"Exception : {ex}");
                assemblyTypes = new Type[] { };
            }

            foreach (Type type in assemblyTypes)
            {
                try
                {
                    if (type.IsClass)
                    {
                        if (type.BaseType != null && type.BaseType == baseType)
                        {
                            parentTypes.Add(type);
                            continue;
                        }

                        foreach (Type i in type.GetInterfaces())
                        {
                            if (i == baseType)
                            {
                                parentTypes.Add(type);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Assembly : {assembly} {Environment.NewLine}" +
                                      $"Type : {type} {Environment.NewLine}" +
                                      $"Exception Message : {ex.Message} {Environment.NewLine}" +
                                      $"Exception : {ex}");
                }
            }


            return parentTypes;
        }
    }
}