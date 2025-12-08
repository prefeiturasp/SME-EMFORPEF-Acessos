namespace SME.Acessos.Infra.Dominio.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime HorarioBrasilia()
            => DateTime.UtcNow.AddHours(-3);
    }
}
