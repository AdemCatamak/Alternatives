using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Alternatives.CustomDataAnnotations
{
    public class TurkeyPhone : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            value = value ?? string.Empty;

            string strValue = value.ToString()
                                   .Replace("-", "")
                                   .Replace(" ", "");

            strValue = strValue.TrimStart('+')
                               .TrimStart('9')
                               .TrimStart('0');

            bool isValid = strValue.All(char.IsDigit) &&
                           strValue.Length == 10;

            return isValid;
        }
    }
}