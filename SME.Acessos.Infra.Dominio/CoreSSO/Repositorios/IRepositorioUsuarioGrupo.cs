
namespace SME.Acessos.Infra.Dominio.CoreSSO.Repositorios
{
    public interface IRepositorioUsuarioGrupo
    {
        Task<bool> InserirUsuarioGrupoCustomizado(Guid usuarioId, Guid grupoId);
    }
}
