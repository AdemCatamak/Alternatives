﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Resources;
using Newtonsoft.Json;

namespace Alternatives.Extensions
{
    public static class ConverterExtensions
    {
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
                int enumValue = (int)Enum.Parse(type, enumName);
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
    }
}