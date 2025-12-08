using Dapper;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;

namespace SME.Acessos.Infra.Dados.Repositorios.Acessos;

public class RepositorioConfiguracaoEmail(IConexaoAcessos conexao) : RepositorioBaseAcessos<ConfiguracaoEmail>(conexao), IRepositorioConfiguracaoEmail
{
    public async Task<ConfiguracaoEmail> ObterConfiguracaoEmailPorSistema(long sistemaId)
    {
        var query = @"select id, 
                           codigo_sistema, 
                           email, 
                           nome, 
                           smtp, 
                           usuario,
                           senha, 
                           porta,
                           tls 
                   from configuracao_email 
                   where codigo_sistema = @sistemaId";

        return await conexao.Obter().QueryFirstOrDefaultAsync<ConfiguracaoEmail>(query, new { sistemaId });
    }
}