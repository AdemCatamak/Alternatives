using System;
using System.Reflection;

namespace Alternatives.ReflectionExtensions
{
    public static class FieldExtensions
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
    }
}