
namespace SME.Acessos.Infra.Dominio.CoreSSO.Repositorios
{
    public interface IRepositorioUsuarioGrupo
    {
        Task<bool> AtivarVinculo(Guid id, Guid perfilId);
        Task<bool> DeletarUsuarioGrupoCustomizado(Guid usuarioId, Guid grupoId);
        Task<bool> InserirUsuarioGrupoCustomizado(Guid usuarioId, Guid grupoId);
        Task<bool> PerfilJaVinculado(Guid id, Guid perfilId);
    }
}
