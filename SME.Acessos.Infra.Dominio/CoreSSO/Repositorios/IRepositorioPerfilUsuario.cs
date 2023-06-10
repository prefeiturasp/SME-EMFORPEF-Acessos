using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dominio.CoreSSO
{
    public interface IRepositorioPerfilUsuario
    {
        Task<IList<PerfilUsuario>> ObterPerfisUsuario(string login, int sistemaId);
    }
}
