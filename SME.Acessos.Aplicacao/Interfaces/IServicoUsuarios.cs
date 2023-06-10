using SME.Acessos.Infra.Dominio.CoreSSO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoUsuarios
    {
        Task<IList<DadosUsuarioDto>> ObterTodosUsuarios();
        Task<DadosUsuarioDto> ObterUsuarioPorId(Guid id);
        Task<DadosUsuarioDto> ObterUsuarioPorLogin(string login);
    }
}
