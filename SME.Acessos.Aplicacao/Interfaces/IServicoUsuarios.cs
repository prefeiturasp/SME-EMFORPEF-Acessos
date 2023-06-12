using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Infra.Dominio.CoreSSO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoUsuarios
    {
        Task<IList<DadosUsuarioDTO>> ObterTodosUsuarios();
        Task<DadosUsuarioDTO> ObterUsuarioPorId(Guid id);
        Task<DadosUsuarioDTO> ObterUsuarioPorLogin(string login);
    }
}
