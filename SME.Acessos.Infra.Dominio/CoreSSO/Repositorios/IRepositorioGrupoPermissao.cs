using SME.Acessos.Infra.Dominio.Acessos;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dominio.CoreSSO.Repositorios
{
    public interface IRepositorioGrupoPermissao
    {
        Task<IList<GrupoPermissao>> ObterModulosPorPerfilSistema(Guid perfilId, int sistemaId);
    }
}
