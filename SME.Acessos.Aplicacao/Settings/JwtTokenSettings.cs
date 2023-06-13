namespace SME.Acessos.Aplicacao.Settings
{
    public class JwtTokenSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string IssuerSigningKey { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
}
