using Dapper;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Infra.Dados.Acessos;

public class RepositorioSistemaAcao : RepositorioBaseAcessos<SistemaAcao>, IRepositorioSistemaAcao
{
    public RepositorioSistemaAcao(IConexaoAcessos conexao) : base(conexao)
    {
    }

    public async Task<SistemaAcao> ObterSistemaAcaoPorAcaoESistema(long sistemaId, TipoAcao tipoAcao = TipoAcao.RecuperacaoSenha)
    {
        var query = @"select id, 
                        codigo_sistema CodigoSistema, 
                        nome_sistema NomeSistema, 
                        endereco,
                        tipo
                     from sistema_acao 
                     where codigo_sistema = @sistemaId
                     and tipo = @tipoAcao";
        
        return await conexao.Obter().QueryFirstOrDefaultAsync<SistemaAcao>(query, new { sistemaId, tipoAcao });
    }
}