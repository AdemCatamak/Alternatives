using System;
using System.Linq;
using System.Net;
using System.Text;

namespace Alternatives
{
    public static class GeneralExtensions
    {
        public static string FirstLetterToUpperAll(this string text, params char[] markers)
        {
            if (markers == null || !markers.Any())
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
                for (int i = 0; i < split.Length; i++)
                {
                    split[i] = split[i].FirstLetterToUpper();
                }

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

            int firstLetterIndex = text.IndexOf(text.ToCharArray()
                                                    .FirstOrDefault(char.IsLetter));
            if (firstLetterIndex >= 0)
            {
                var sb = new StringBuilder(text);
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

        public static string AppendWithSeparator(this string source, string text)
        {
            return source.AppendWithSeparator(text, Environment.NewLine);
        }

        public static string AppendWithSeparator(this string source, string text, string separator)
        {
            source = string.IsNullOrEmpty(source)
                         ? text
                         : $"{source}{separator}{text}";
            return source;
        }
    }
}