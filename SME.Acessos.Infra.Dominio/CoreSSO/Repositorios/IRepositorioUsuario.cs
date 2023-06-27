using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dominio.CoreSSO.Repositorios
{
    public interface IRepositorioUsuario : IRepositorioBaseCoreSSO<Usuario>
    {
        Task<Usuario> ObterPorLogin(string login);
        Task<bool> UsuarioCadastradoCoreSSO(string login);
        Task InserirUsuarioCustomizado(string login, string email, string senha, Guid pessoa, Guid entidade);
        Task<bool> ValidarTokenRecuperacaoSenha(Guid token, int sistema);
    }
}
