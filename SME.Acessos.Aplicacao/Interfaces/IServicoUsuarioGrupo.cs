using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Infra.Dominio.CoreSSO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoUsuarioGrupo
    {
        Task<bool> VincularPerfil(string login, Guid perfilId);
    }
}
