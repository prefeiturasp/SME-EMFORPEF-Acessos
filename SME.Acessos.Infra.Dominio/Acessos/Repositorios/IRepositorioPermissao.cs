
namespace SME.Acessos.Infra.Dominio.Acessos.Repositorios;

public interface IRepositorioPermissao
{
    Task<IList<Acessos.Entidades.Modulo>> ObterPermissoesPorModulos(IList<Dominio.CoreSSO.Entidades.GrupoPermissao> modulosGrupoPermissao);
}