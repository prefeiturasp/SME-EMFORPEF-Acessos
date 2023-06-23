using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Infra.Dominio.CoreSSO.Repositorios
{
    public interface IRepositorioUsuario : IRepositorioBaseCoreSSO<Usuario>
    {
        Task<Usuario> ObterPorLogin(string login);
        Task<bool> UsuarioCadastradoCoreSSO(string login);
        Task InserirUsuarioCustomizado(string login, string email, string senha, Guid pessoa, Guid entidade);
        Task<bool> ValidarSenhaAtual(Guid usuarioId, string senhaAtual);
        Task AlterarSenha(Guid usuarioId, string senhaNova);
        Task InserirHistoricoSenha(Guid usuarioId, string senha, TipoCriptografia criptografia);
        Task AlterarEmail(Guid usuarioId, string email);
    }
}
