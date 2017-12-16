using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Alternatives.CustomAnnotations
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

            bool isValid = strValue == string.Empty
                           || (strValue.All(char.IsDigit) &&
                               strValue.Length == 10);

            return isValid;
        }
    }
}