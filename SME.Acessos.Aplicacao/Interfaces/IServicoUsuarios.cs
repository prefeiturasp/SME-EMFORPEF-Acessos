using SME.Acesos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Infra.Dominio.CoreSSO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoUsuarios
    {
        Task<IList<DadosUsuarioDTO>> ObterTodosUsuarios();
        Task<DadosUsuarioDTO> ObterUsuarioPorId(Guid id);
        Task<DadosUsuarioDTO> ObterUsuarioPorLogin(string login);
        Task<bool> UsuarioCadastradoCoreSSO(string login);
        Task<bool> Cadastrar(UsuarioDTO usuarioDto);
        Task<DadosUsuarioDTO?> ObterMeusDados(string login);
        Task<bool> AlterarSenha(string login, AlterarSenhaUsuarioDTO alterarSenhaUsuarioDto);
        Task<bool> AlterarEmail(string login, AlterarEmailUsuarioDTO alterarEmailUsuarioDto);
        Task<string> RecuperarSenha(string login, long sistemaId);
        Task<bool> ValidarTokenRecuperacaoSenha(Guid token, long sistemaId);
        Task<RetornoAlteracaoSenhaDto> AlterarSenhaPorToken(long sistemaId, AlterarSenhaPorTokenDto alterarSenha);
    }
}
