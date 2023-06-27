
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dominio.Acessos.Repositorios;

public interface IRepositorioConfiguracaoEmail
{
    Task<ConfiguracaoEmail> ObterConfiguracaoEmail(int sistemaId);
}