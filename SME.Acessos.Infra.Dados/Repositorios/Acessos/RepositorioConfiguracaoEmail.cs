using System.Text;
using Dapper;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.Enumeradores;
using Modulo = SME.Acessos.Infra.Dominio.Acessos.Entidades.Modulo;

namespace SME.Acessos.Infra.Dados.Acessos;

public class RepositorioConfiguracaoEmail : RepositorioBaseAcessos<ConfiguracaoEmail>, IRepositorioConfiguracaoEmail
{
    public RepositorioConfiguracaoEmail(IConexaoAcessos conexao) : base(conexao)
    {
    }

    public async Task<ConfiguracaoEmail> ObterConfiguracaoEmail(int sistemaId)
    {
        return (await conexao.Obter()
                .QueryAsync<ConfiguracaoEmail>(
              "select id, codigo_sistema, email, nome, smtp, usuario,senha, porta,tls " +
              "   from configuracao_email " +
              "   where codigo_sistema = @sistemaId", new { sistemaId }))
                 .ToList()
                 .FirstOrDefault();
    }
}