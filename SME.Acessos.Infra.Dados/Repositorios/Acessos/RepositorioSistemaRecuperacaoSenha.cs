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

    public async Task<SistemaRecuperacaoSenha> ObterSistema(int sistemaId)
    {
        return (await conexao.Obter()
                .QueryAsync<SistemaRecuperacaoSenha>(
                    "select id, codigo_sistema, nome_sistema, pagina_recuperacao_senha " +
                    "   from sistema_recuperacao_senha " +
                    "   where codigo_sistema = @sistemaId", new { sistemaId }))
            .ToList()
            .FirstOrDefault();
    }
}