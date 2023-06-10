using SME.Acessos.Infra.Dominio.Acessos;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dominio.CoreSSO
{
    public interface IRepositorioModuloGrupoPermissao
    {
        Task<IList<ModuloGrupoPermissao>> ObterModulosPorPerfilSistema(Guid perfilId, int sistemaId);
    }
}
