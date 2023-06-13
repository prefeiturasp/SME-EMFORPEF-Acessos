using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dominio.CoreSSO.Repositorios
{
    public interface IRepositorioUsuario : IRepositorioBaseCoreSSO<Usuario>
    {
        Task<Usuario> ObterPorLogin(string login);
    }
}
