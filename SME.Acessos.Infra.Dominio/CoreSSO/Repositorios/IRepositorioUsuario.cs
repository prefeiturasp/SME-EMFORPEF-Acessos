namespace SME.Acessos.Infra.Dominio.CoreSSO
{
    public interface IRepositorioUsuario : IRepositorioBaseCoreSSO<Usuario>
    {
        Task<Usuario> ObterPorLogin(string login);
    }
}
