using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Alternatives.Extensions
{
    public static class GeneralExtensions
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
    }
}