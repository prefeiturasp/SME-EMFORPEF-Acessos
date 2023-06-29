using System.Text;
using Dapper;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.Enumeradores;
using Modulo = SME.Acessos.Infra.Dominio.Acessos.Entidades.Modulo;

namespace SME.Acessos.Infra.Dados.Acessos;

public class RepositorioSistemaRecuperacaoSenha : RepositorioBaseAcessos<SistemaRecuperacaoSenha>, IRepositorioSistemaRecuperacaoSenha
{
    public RepositorioSistemaRecuperacaoSenha(IConexaoAcessos conexao) : base(conexao)
    {
    }

    public async Task<SistemaRecuperacaoSenha> ObterSistema(long sistemaId)
    {
        var query = @"select id, 
                        codigo_sistema CodigoSistema, 
                        nome_sistema NomeSistema, 
                        pagina_recuperacao_senha PaginaRecuperacaoSenha
                     from sistema_recuperacao_senha 
                     where codigo_sistema = @sistemaId";
        
        return (await conexao.Obter()
                .QueryAsync<SistemaRecuperacaoSenha>(query, new { sistemaId }))
                .ToList()
                .FirstOrDefault();
    }
}