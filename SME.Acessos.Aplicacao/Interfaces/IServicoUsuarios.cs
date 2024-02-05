using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoUsuarios
    {
        Task<IList<DadosUsuarioDTO>> ObterTodosUsuarios();
        Task<DadosUsuarioDTO> ObterUsuarioPorId(Guid id);
        Task<DadosUsuarioDTO> ObterUsuarioPorLogin(string login);
        Task<bool> ExisteUsuarioCadastradoCoreSSO(string login);
        Task<bool> Cadastrar(UsuarioDTO usuarioDto);
        Task<DadosUsuarioDTO?> ObterMeusDados(string login);
        Task<bool> AlterarSenha(string login, AlterarSenhaUsuarioDTO alterarSenhaUsuarioDto);
        Task<bool> AlterarEmail(string login, AlterarEmailUsuarioDTO alterarEmailUsuarioDto);
        Task<string> SolicitarRecuperacaoSenha(string login, long sistemaId);
        Task<bool> ValidarTokenSenha(Guid token, long sistemaId, TipoAcao tipoAcao = TipoAcao.RecuperacaoSenha);
        Task<string> ValidarTokenEmail(Guid token, long sistemaId, TipoAcao tipoAcao = TipoAcao.RecuperacaoSenha);
        Task<RetornoAlteracaoSenhaDto> AlterarSenhaPorToken(long sistemaId, AlterarSenhaPorTokenDto alterarSenha);
        Task<IEnumerable<ResponsavelDTO>> ObterUsuariosComPerfisResponsavel(Guid[] perfis, long sistemaId);
        Task<bool> EnviarEmailValidacaoCadastro(string login, long sistemaId);
    }
}
