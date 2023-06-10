using SME.Acessos.Infra.Dominio.Acessos;

namespace SME.Acessos.Infra.Dominio;

public interface IRepositorioPermissao
{
    Task<IEnumerable<int>> ObterPermissoesPorModulos(IList<ModuloGrupoPermissao> modulosGrupoPermissao);
}