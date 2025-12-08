using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Infra.Dominio.CoreSSO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoUsuarioGrupo
    {
        Task<bool> DesvincularPerfil(string login, Guid perfilId);
        Task<bool> VincularPerfil(string login, Guid perfilId);
    }
}
