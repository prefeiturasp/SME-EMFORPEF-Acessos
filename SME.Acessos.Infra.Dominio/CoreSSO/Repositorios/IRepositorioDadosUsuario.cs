using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dominio.CoreSSO.Repositorios
{
    public interface IRepositorioDadosUsuario : IRepositorioBaseCoreSSO<DadosUsuario>
    {
        Task<DadosUsuario> ObterMeusDados(string login, int sistemaId);
    }
}
