namespace SME.Acessos.Infra.Dominio.Acessos.Entidades
{
    public class UsuarioRecuperacaoToken : EntidadeBaseAcessos
    {
        public string Login { get; set; }
        public DateTime? ExpiracaoRecuperacaoSenha { get; set; }
        public Guid? TokenRecuperacaoSenha { get; set; }
    }
}
