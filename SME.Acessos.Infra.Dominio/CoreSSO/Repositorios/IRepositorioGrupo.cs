using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dominio.CoreSSO.Repositorios
{
    public interface IRepositorioGrupo
    {
        Task<IEnumerable<Grupo>> ObterPorSistemaId(long sistemaId);
    }
}
