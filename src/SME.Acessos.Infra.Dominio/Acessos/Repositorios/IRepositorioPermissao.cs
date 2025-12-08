
namespace SME.Acessos.Infra.Dominio.Acessos.Repositorios;

public interface IRepositorioPermissao
{
    Task<IEnumerable<Acessos.Entidades.Modulo>> ObterPermissoesPorModulos(IEnumerable<Dominio.CoreSSO.Entidades.GrupoPermissao> modulosGrupoPermissao);
}