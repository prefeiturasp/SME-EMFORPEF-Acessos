
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dominio.Acessos.Repositorios;

public interface IRepositorioConfiguracaoEmail
{
    Task<ConfiguracaoEmail> ObterConfiguracaoEmailPorSistema(long sistemaId);
    Task<IList<ConfiguracaoEmail>> ObterTodos();
}