using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
                message = Environment.NewLine + string.Join(Environment.NewLine, validateMessages.Select(v => v.ErrorMessage));
            }
            return result;
        }

        public static string FirstLetterToUpperAll(this string text, params char[] markers)
        {
            if (markers.IsNullOrEmpty())
            {
                markers = new[] {' '};
            }

            if (text == null)
            {
                text = string.Empty;
            }

            foreach (char marker in markers)
            {
                string[] split = text.Split(new[] {marker}, StringSplitOptions.RemoveEmptyEntries);
                Parallel.For(0, split.Length, i => { split[i] = split[i].FirstLetterToUpper(); }
                            );


                text = string.Join(marker.ToString(), split);
            }

            return text;
        }

        public static string FirstLetterToUpper(this string text)
        {
            if (text == null)
            {
                text = string.Empty;
            }

            int firstLetterIndex = text.IndexOf(text.FirstOrDefault(char.IsLetter));
            if (firstLetterIndex >= 0)
            {
                StringBuilder sb = new StringBuilder(text);
                sb[firstLetterIndex] = char.ToUpper(text[firstLetterIndex]);
                text = sb.ToString();
            }

            return text;
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