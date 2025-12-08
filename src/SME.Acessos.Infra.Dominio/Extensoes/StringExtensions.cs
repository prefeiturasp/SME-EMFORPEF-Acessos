
using System.Text.RegularExpressions;

namespace SME.Acessos.Infra.Dominio.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotNull(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNull(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        
        public static bool EmailEhValido(this string email)
        {
            var regex = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
            return Regex.IsMatch(email, regex);
        }

        public static bool IsEqualsToInvariantCulture(this string source, string target)
        {
            return string.Equals(source,
                                target,
                                StringComparison.InvariantCultureIgnoreCase
                                );
        }

        public static bool ContainsInvariantCulture(this string source, string target)
        {
            return source.Contains(target,
                                StringComparison.InvariantCultureIgnoreCase
                                );
        }
    }
}
