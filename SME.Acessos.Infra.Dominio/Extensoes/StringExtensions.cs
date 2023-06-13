
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
