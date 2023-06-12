using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dominio.CoreSSO.Repositorios
{
    public interface IRepositorioPerfilUsuario
    {
        Task<IList<Grupo>> ObterPerfisUsuario(string login, int sistemaId);
    }
}
