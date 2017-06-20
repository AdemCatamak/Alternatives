using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.ComponentModel;
using System.Resources;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Newtonsoft.Json;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace AdemCatamak.Utilities
{
    public static class Extensions
    {
        public static bool IsValid<T>(this T obj)
        {
            return IsValid(obj, out string _);
        }

        public static bool IsValid<T>(this T obj, out string message)
        {
            bool result;

            if (obj == null)
            {
                result = false;
                message = "Object is null";
            }
            else
            {
                ICollection<ValidationResult> validateMessages = new List<ValidationResult>();
                ValidationContext validationContext = new ValidationContext(obj);

                result = Validator.TryValidateObject(obj,
                                                     validationContext,
                                                     validateMessages,
                                                     true);

                message = string.Join(Environment.NewLine, validateMessages.Select(v => v.ErrorMessage));
            }
            return result;
        }


        public static int ToInt(this object value)
        {
            return int.Parse(value.ToString());
        }

        public static int TryToInt(this object value, int defaultValue = default(int))
        {
            if (value == null || !int.TryParse(value.ToString(), out int result))
                result = defaultValue;

            return result;
        }


        public static double ToDouble(this object value)
        {
            return double.Parse(value.ToString());
        }

        public static double TryToDouble(this object value, double defaultValue = default(double))
        {
            if (value == null || !double.TryParse(value.ToString(), out double result))
                result = defaultValue;

            return result;
        }


        public static long ToLong(this object value)
        {
            return long.Parse(value.ToString());
        }

        public static long TryToLong(this object value, long defaultValue = default(long))
        {
            if (value == null || !long.TryParse(value.ToString(), out long result))
                result = defaultValue;

            return result;
        }


        public static string FirstLetterToUpperAll(this string text)
        {
            if (text == null)
                text = string.Empty;

            string[] split = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length; i++)
                split[i] = split[i].FirstLetterToUpper();

            return string.Join(" ", split);
        }

        public static string FirstLetterToUpper(this string text)
        {
            if (text == null)
                text = string.Empty;

            text = text.Trim();

            return text.Length == 0
                       ? text
                       : text.First().ToString().ToUpper() +
                         text.Substring(1);
        }


        public static TDestination Deserialize<TDestination>(this string source)
        {
            TDestination result = JsonConvert.DeserializeObject<TDestination>(source,
                                                                              new JsonSerializerSettings()
                                                                              {
                                                                                  ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                                                                              });
            return result;
        }

        public static string Serialize<TSource>(this TSource source)
        {
            string result = JsonConvert.SerializeObject(source,
                                                        new JsonSerializerSettings()
                                                        {
                                                            ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                                                        });

            return result;
        }


        public static T Copy<T>(this T source)
        {
            return Map<T, T>(source);
        }

        public static TDestination Map<TSource, TDestination>(this TSource source)
        {
            AutoMapper.MapperConfiguration config =
                new AutoMapper.MapperConfiguration(cfg => { cfg.CreateMap<TSource, TDestination>(); });

            AutoMapper.IMapper mapper = config.CreateMapper();

            return mapper.Map<TSource, TDestination>(source);
        }


        public static Dictionary<int, string> EnumToDictionary(Type type, ResourceManager resourceManager = null)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            string[] enumNameArray = Enum.GetNames(type);

            foreach (string enumName in enumNameArray)
            {
                int enumValue = (int) Enum.Parse(type, enumName);
                if (enumValue < 0)
                    continue;

                string name;
                try
                {
                    name = resourceManager != null ? resourceManager.GetString(enumName) : enumName;
                }
                catch
                {
                    name = enumName;
                }

                dictionary.Add(enumValue, name);
            }

            return dictionary;
        }


        public static object GetDbValue<T>(this T parameter)
        {
            if (parameter == null)
                return DBNull.Value;
            return parameter;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> dataList)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType &&
                    prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                }
                else
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
            }

            object[] values = new object[props.Count];
            foreach (T item in dataList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }

            return table;
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
        
        public static bool CheckInRange(IPAddress minIpAddress, IPAddress maxIpAddress, IPAddress ipAddress)
        {
            bool result = true;
            byte[] minAddressBytes = minIpAddress.GetAddressBytes();
            byte[] maxAddressBytes = maxIpAddress.GetAddressBytes();

            if (ipAddress.AddressFamily == minIpAddress.AddressFamily)
            {
                byte[] addressBytes = ipAddress.GetAddressBytes();

                bool lowerBoundary = true, upperBoundary = true;

                for (int i = 0; i < minAddressBytes.Length && (lowerBoundary || upperBoundary); i++)
                {
                    if ((lowerBoundary && addressBytes[i] < minAddressBytes[i]) ||
                        (upperBoundary && addressBytes[i] > maxAddressBytes[i]))
                    {
                        result = false;
                        break;
                    }

                    lowerBoundary &= (addressBytes[i] == minAddressBytes[i]);
                    upperBoundary &= (addressBytes[i] == maxAddressBytes[i]);
                }
            }

            return result;
        }
    }
}