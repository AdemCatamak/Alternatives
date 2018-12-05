using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace Alternatives.ConversionExtensions
{
    public static class ToExtensions
    {
        #region Short

        public static short ToShort(this object value, CultureInfo culture = null)
        {
            if (value == null) throw new NullReferenceException($"{nameof(value)} Is Null");

            CultureInfo currentCulture = culture ?? CultureInfo.CurrentCulture;
            return Convert.ToInt16(value, currentCulture);
        }


        public static short TryToShort(this object value, short defaultValue = default(short))
        {
            return TryToShort(value, CultureInfo.CurrentCulture, defaultValue);
        }

        public static short TryToShort(this object value, CultureInfo culture, short defaultValue = default(short))
        {
            return TryToShort(value, out bool _, culture, defaultValue);
        }

        public static short TryToShort(this object value, out bool success, short defaultValue = default(short))
        {
            return TryToShort(value, out success, CultureInfo.CurrentCulture, defaultValue);
        }

        public static short TryToShort(this object value, out bool success, CultureInfo cultureInfo, short defaultValue = default(short))
        {
            short result;
            success = true;

            try
            {
                result = value == null
                             ? defaultValue
                             : Convert.ToInt16(value, cultureInfo);
            }
            catch
            {
                success = false;
                result = defaultValue;
            }

            return result;
        }

        #endregion

        #region Int

        public static int ToInt(this object value, CultureInfo culture = null)
        {
            if (value == null) throw new NullReferenceException($"{nameof(value)} Is Null");

            CultureInfo currentCulture = culture ?? CultureInfo.CurrentCulture;
            return Convert.ToInt32(value, currentCulture);
        }


        public static int TryToInt(this object value, int defaultValue = default(int))
        {
            return TryToInt(value, CultureInfo.CurrentCulture, defaultValue);
        }

        public static int TryToInt(this object value, CultureInfo culture, int defaultValue = default(int))
        {
            return TryToInt(value, out bool _, culture, defaultValue);
        }

        public static int TryToInt(this object value, out bool success, int defaultValue = default(int))
        {
            return TryToInt(value, out success, CultureInfo.CurrentCulture, defaultValue);
        }

        public static int TryToInt(this object value, out bool success, CultureInfo cultureInfo, int defaultValue = default(int))
        {
            int result;
            success = true;

            try
            {
                result = value == null
                             ? defaultValue
                             : Convert.ToInt32(value, cultureInfo);
            }
            catch
            {
                success = false;
                result = defaultValue;
            }

            return result;
        }

        #endregion

        #region Double

        public static double ToDouble(this object value, CultureInfo culture = null)
        {
            if (value == null) throw new NullReferenceException($"{nameof(value)} Is Null");

            CultureInfo cultureInfo = culture ?? CultureInfo.CurrentCulture;
            return Convert.ToDouble(value, cultureInfo);
        }


        public static double TryToDouble(this object value, double defaultValue = default(double))
        {
            return TryToDouble(value, CultureInfo.CurrentCulture, defaultValue);
        }

        public static double TryToDouble(this object value, CultureInfo culture, double defaultValue = default(double))
        {
            return TryToDouble(value, out bool _, culture, defaultValue);
        }

        public static double TryToDouble(this object value, out bool success, double defaultValue = default(double))
        {
            return TryToDouble(value, out success, CultureInfo.CurrentCulture, defaultValue);
        }

        public static double TryToDouble(this object value, out bool success, CultureInfo cultureInfo, double defaultValue = default(double))
        {
            double result;
            success = true;

            try
            {
                result = value == null
                             ? defaultValue
                             : Convert.ToDouble(value, cultureInfo);
            }
            catch
            {
                success = false;
                result = defaultValue;
            }

            return result;
        }

        #endregion

        #region Long

        public static long ToLong(this object value, CultureInfo culture = null)
        {
            if (value == null) throw new NullReferenceException($"{nameof(value)} Is Null");

            CultureInfo cultureInfo = culture ?? CultureInfo.CurrentCulture;
            return Convert.ToInt64(value, cultureInfo);
        }


        public static long TryToLong(this object value, long defaultValue = default(long))
        {
            return TryToLong(value, CultureInfo.CurrentCulture, defaultValue);
        }

        public static long TryToLong(this object value, CultureInfo culture, long defaultValue = default(long))
        {
            return TryToLong(value, out bool _, culture, defaultValue);
        }

        public static long TryToLong(this object value, out bool success, long defaultValue = default(long))
        {
            return TryToLong(value, out success, CultureInfo.CurrentCulture, defaultValue);
        }

        public static long TryToLong(this object value, out bool success, CultureInfo cultureInfo, long defaultValue = default(long))
        {
            long result;
            success = true;

            try
            {
                result = value == null
                             ? defaultValue
                             : Convert.ToInt64(value, cultureInfo);
            }
            catch
            {
                result = defaultValue;
                success = false;
            }

            return result;
        }

        #endregion

        #region Float

        public static float ToFloat(this object value, CultureInfo culture = null)
        {
            if (value == null) throw new NullReferenceException($"{nameof(value)} Is Null");

            CultureInfo currentCulture = culture ?? CultureInfo.CurrentCulture;
            return float.Parse(value.ToString(), currentCulture);
        }


        public static float TryToFloat(this object value, float defaultValue = default(float))
        {
            return TryToFloat(value, CultureInfo.CurrentCulture, defaultValue);
        }

        public static float TryToFloat(this object value, CultureInfo culture, float defaultValue = default(float))
        {
            return TryToFloat(value, out bool _, culture, defaultValue);
        }

        public static float TryToFloat(this object value, out bool success, float defaultValue = default(float))
        {
            return TryToFloat(value, out success, CultureInfo.CurrentCulture, defaultValue);
        }

        public static float TryToFloat(this object value, out bool success, CultureInfo cultureInfo, float defaultValue = default(float))
        {
            float result;
            success = true;

            try
            {
                result = value == null
                             ? defaultValue
                             : float.Parse(value.ToString(), cultureInfo);
            }
            catch
            {
                success = false;
                result = defaultValue;
            }

            return result;
        }

        #endregion

        #region Decimal

        public static decimal ToDecimal(this object value, CultureInfo culture = null)
        {
            if (value == null) throw new NullReferenceException($"{nameof(value)} Is Null");

            CultureInfo currentCulture = culture ?? CultureInfo.CurrentCulture;
            return Convert.ToDecimal(value, currentCulture);
        }


        public static decimal TryToDecimal(this object value, decimal defaultValue = default(decimal))
        {
            return TryToDecimal(value, CultureInfo.CurrentCulture, defaultValue);
        }

        public static decimal TryToDecimal(this object value, CultureInfo culture, decimal defaultValue = default(decimal))
        {
            return TryToDecimal(value, out bool _, culture, defaultValue);
        }

        public static decimal TryToDecimal(this object value, out bool success, decimal defaultValue = default(decimal))
        {
            return TryToDecimal(value, out success, CultureInfo.CurrentCulture, defaultValue);
        }

        public static decimal TryToDecimal(this object value, out bool success, CultureInfo cultureInfo, decimal defaultValue = default(decimal))
        {
            decimal result;
            success = true;

            try
            {
                result = value == null
                             ? defaultValue
                             : Convert.ToDecimal(value, cultureInfo);
            }
            catch
            {
                success = false;
                result = defaultValue;
            }

            return result;
        }

        #endregion

        public static Dictionary<int, string> EnumToDictionary(Type type, ResourceManager resourceManager = null)
        {
            var dictionary = new Dictionary<int, string>();

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
    }
}