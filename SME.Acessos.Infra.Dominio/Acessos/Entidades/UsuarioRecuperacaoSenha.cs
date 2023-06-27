namespace SME.Acessos.Infra.Dominio.Acessos.Entidades
{
    public class UsuarioRecuperacaoSenha : EntidadeBaseAcessos
    {
        public string Login { get; set; }
        public DateTime? Expiracao { get; set; }
        public Guid? Token { get; set; }
        public long CodigoSistema { get; set; }
    }
}
