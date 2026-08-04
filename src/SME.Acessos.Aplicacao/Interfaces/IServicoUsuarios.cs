using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoUsuarios
    {
        Task<IList<DadosUsuarioDto>> ObterTodosUsuarios();
        Task<DadosUsuarioDto> ObterUsuarioPorId(Guid id);
        Task<DadosUsuarioDto> ObterUsuarioPorLogin(string login);
        Task<bool> ExisteUsuarioCadastradoCoreSSO(string login);
        Task<bool> Cadastrar(UsuarioDTO usuarioDto);
        Task<DadosUsuarioDto?> ObterMeusDados(string login);
        Task<bool> AlterarSenha(string login, AlterarSenhaUsuarioDTO alterarSenhaUsuarioDto);
        Task<bool> AlterarEmail(string login, AlterarEmailUsuarioDTO alterarEmailUsuarioDto);
        Task<string> SolicitarRecuperacaoSenha(string login, long sistemaId);
        Task<bool> ValidarTokenSenha(Guid token, long sistemaId, TipoAcao tipoAcao = TipoAcao.RecuperacaoSenha);
        Task<string> ValidarTokenEmail(Guid token, long sistemaId, TipoAcao tipoAcao = TipoAcao.RecuperacaoSenha);
        Task<RetornoAlteracaoSenhaDto> AlterarSenhaPorToken(long sistemaId, AlterarSenhaPorTokenDto alterarSenha);
        Task<IEnumerable<ResponsavelDTO>> ObterUsuariosComPerfisResponsavel(Guid[] perfis, long sistemaId);
        Task<bool> EnviarEmailValidacaoCadastro(string login, long sistemaId);
        Task<bool> AlterarNome(string login, string nome);
        Task<bool> AlterarNomeSocial(string login, string? nomeSocial);
        Task<bool> Alterar(string login, UsuarioDTO usuarioDTO);
        Task<bool> Inativar(string login);
    }
}
