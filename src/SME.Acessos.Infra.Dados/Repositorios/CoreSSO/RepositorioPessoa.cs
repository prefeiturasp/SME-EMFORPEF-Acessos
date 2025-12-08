using Dapper;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioPessoa(IConexaoCoreSSO conexao) : RepositorioBaseCoreSSO<Pessoa>(conexao), IRepositorioPessoa
    {
        public async Task<Guid> InserirPessoaCustomizado(string nome)
        {
            var insertPessoa = @" insert into [PES_Pessoa] ([pes_nome]) 
                                       OUTPUT INSERTED.[pes_id]
                                       values (@nome) ";
            return  await conexao.Obter().QuerySingleOrDefaultAsync<Guid>(insertPessoa, new{nome});
        }
    }
}
