using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Infra.Dominio.CoreSSO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoUsuarios
    {
        Task<IList<LoginEmailDTO>> ObterTodosUsuarios();
        Task<LoginEmailDTO> ObterUsuarioPorId(Guid id);
        Task<LoginEmailDTO> ObterUsuarioPorLogin(string login);
        Task<bool> UsuarioCadastradoCoreSSO(string login);
        Task<bool> Cadastrar(UsuarioDTO usuarioDto);
        Task<DadosUsuarioDTO?> ObterMeusDados(string login);
        Task<bool> AlterarSenha(string login, string senhaAtual, string senhaNova, int sistemaId);
    }
}
