using System.Data.SqlClient;
using Dapper;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioPessoa : RepositorioBaseCoreSSO<Pessoa>, IRepositorioPessoa
    {
        public RepositorioPessoa(IConexaoCoreSSO conexao) : base(conexao)
        {
        }

        public async Task<Guid?> InserirPessoaCustomizado(string nome)
        {
            var insertPessoa = $@"insert into [PES_Pessoa] ([pes_nome]) values ('{nome}'); 
                                  select pes_id  from [PES_Pessoa] where [pes_nome] = '{nome}'";

            var retorno = await conexao.Obter().ExecuteScalarAsync(insertPessoa);
            return (Guid)retorno;
        }
    }
}
