using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;
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
                result = DetectValidator(obj, out message);

                ICollection<ValidationResult> validateMessages = new List<ValidationResult>();
                ValidationContext validationContext = new ValidationContext(obj);

                bool dataAnnotationResult = Validator.TryValidateObject(obj,
                                                                        validationContext,
                                                                        validateMessages,
                                                                        true);
                result = result && dataAnnotationResult;

                if (!dataAnnotationResult)
                {
                    message += Environment.NewLine + string.Join(Environment.NewLine, validateMessages.Select(v => v.ErrorMessage));
                }
            }
            return result;
        }

        private static bool DetectValidator<T>(T obj, out string message)
        {
            message = string.Empty;

            if (!(typeof(T).GetCustomAttributes(typeof(ValidatorAttribute), true).FirstOrDefault() is ValidatorAttribute validatorAttribute))
            {
                return true;
            }

            AbstractValidator<T> validator = Activator.CreateInstance(validatorAttribute.ValidatorType) as AbstractValidator<T>;

            if (validator == null)
            {
                return true;
            }

            FluentValidation.Results.ValidationResult validationResult = validator.Validate(obj);
            message = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));
            return validationResult.IsValid;
        }


        public static string FirstLetterToUpperAll(this string text, params char[] markers)
        {
            if (markers.IsNullOrEmpty())
            {
                markers = new[] { ' ' };
            }

            if (text == null)
            {
                text = string.Empty;
            }

            foreach (char marker in markers)
            {
                string[] split = text.Split(new[] { marker }, StringSplitOptions.RemoveEmptyEntries);
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


        public static string SolutionName
        {
            get
            {
                AssemblyProductAttribute productAttribute = (AssemblyProductAttribute) Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(),
                                                                                                                    typeof(AssemblyProductAttribute));

                return productAttribute.Product;
            }
        }


        public static T ReadAppSettingsRealTime<T>(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "cannot be null");
            }

            const string sectionName = "appSettings";
            ConfigurationManager.RefreshSection(sectionName);

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                throw new KeyNotFoundException($"{key} is not exist in appSettings");
            }

            string value = ConfigurationManager.AppSettings[key];

            return (T) Convert.ChangeType(value, typeof(T));
        }
    }
}