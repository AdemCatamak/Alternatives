using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Resources;
using Newtonsoft.Json;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Alternatives
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


        public static int ToInt(this object value, CultureInfo culture = null)
        {
            return culture == null
                       ? int.Parse(value.ToString())
                       : int.Parse(value.ToString(), culture);
        }

        public static int TryToInt(this object value, int defaultValue = default(int))
        {
            if (value == null || !int.TryParse(value.ToString(), out int result))
                result = defaultValue;

            return result;
        }

        public static int TryToInt(this object value, CultureInfo culture, int defaultValue = default(int))
        {
            culture = culture ?? CultureInfo.CurrentCulture;
            if (value == null || !int.TryParse(value.ToString(), NumberStyles.Any, culture, out int result))
                result = defaultValue;

            return result;
        }


        public static double ToDouble(this object value, CultureInfo culture = null)
        {
            return culture == null
                       ? double.Parse(value.ToString())
                       : double.Parse(value.ToString(), culture);
        }

        public static double TryToDouble(this object value, double defaultValue = default(double))
        {
            if (value == null || !double.TryParse(value.ToString(), out double result))
                result = defaultValue;

            return result;
        }

        public static double TryToDouble(this object value, CultureInfo culture, double defaultValue = default(double))
        {
            culture = culture ?? CultureInfo.CurrentCulture;
            if (value == null || !double.TryParse(value.ToString(), NumberStyles.Any, culture, out double result))
                result = defaultValue;

            return result;
        }

        public static long ToLong(this object value, CultureInfo culture = null)
        {
            return culture == null
                       ? long.Parse(value.ToString())
                       : long.Parse(value.ToString(), culture);
        }

        public static long TryToLong(this object value, long defaultValue = default(long))
        {
            if (value == null || !long.TryParse(value.ToString(), out long result))
                result = defaultValue;

            return result;
        }

        public static long TryToLong(this object value, CultureInfo culture, long defaultValue = default(long))
        {
            culture = culture ?? CultureInfo.CurrentCulture;
            if (value == null || !long.TryParse(value.ToString(), NumberStyles.Any, culture, out long result))
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
                    name = resourceManager != null
                               ? resourceManager.GetString(enumName)
                               : enumName;
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

        public static bool CheckIpInRange(this IPAddress ipAddress, IPAddress minIpAddress, IPAddress maxIpAddress)
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

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> iEnumerable, Expression<Func<T, bool>> predicate = null)
        {
            bool result = iEnumerable == null ||
                          IsNullOrEmpty(iEnumerable.AsQueryable(), predicate);
            return result;
        }

        public static bool IsNullOrEmpty<T>(this IQueryable<T> iQueryable, Expression<Func<T, bool>> predicate = null)
        {
            bool result;

            if (iQueryable == null)
            {
                result = true;
            }
            else
            {
                result = predicate == null
                             ? !iQueryable.Any()
                             : !iQueryable.Any(predicate);
            }

            return result;
        }

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
    }
}