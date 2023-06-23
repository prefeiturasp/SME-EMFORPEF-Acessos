namespace SME.Acessos.Infra.Dominio.Acessos.Entidades
{
    public class ConfiguracaoEmail : EntidadeBaseAcessos
    {
        public long CodigoSistema { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string smtp { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public int Porta { get; set; }
        public bool TLS { get; set; }
    }
}
