using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Infra.Dominio.CoreSSO.Repositorios
{
    public interface IRepositorioUsuario : IRepositorioBaseCoreSSO<Usuario>
    {
        Task<Usuario> ObterPorLogin(string login, bool somenteAtivos = false);
        Task<bool> UsuarioCadastradoCoreSSO(string login);
        Task InserirUsuarioCustomizado(string login, string email, string senha, Guid pessoa, Guid entidade);
        Task<bool> ValidarSenhaAtual(Guid usuarioId, string senhaAtual);
        Task AlterarSenha(Guid usuarioId, string senhaNova, TipoCriptografia criptografia);
        Task InserirHistoricoSenha(Guid usuarioId, string senha, TipoCriptografia criptografia);
        Task AlterarEmail(Guid usuarioId, string email);
        Task<IEnumerable<string>> ObterDresPorLoginEPerfil(string login, Guid? perfil);
        Task<IEnumerable<DadosUsuario>> ObterUsuariosComPerfisResponsavel(Guid[] perfis, long sistemaId);
        Task<string> ObterLoginUsuarioPorCpfCadastradoCoreSSO(string login);
        Task AlterarNome(Guid usuarioId, string nome);
        Task AlterarUsuario(Guid usuarioId, string senhaNova, TipoCriptografia criptografia, string email);
        Task Inativar(Guid id);
        Task<IEnumerable<string>> ObterLoginsExistentesAsync(IEnumerable<string> logins);
        Task InserirUsuariosEmMassaAsync(IEnumerable<UsuarioBulkInsertDto> usuarios, Guid perfilId);
        Task ExcluirUsuariosEmMassaAsync(IEnumerable<string> logins);
    }
}
