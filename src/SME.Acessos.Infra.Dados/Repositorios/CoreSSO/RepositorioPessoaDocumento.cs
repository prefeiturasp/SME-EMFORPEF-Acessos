using System.Data.SqlClient;
using Dapper;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioPessoaDocumento : RepositorioBaseCoreSSO<PessoaDocumento>, IRepositorioPessoaDocumento
    {
        public RepositorioPessoaDocumento(IConexaoCoreSSO conexao) : base(conexao)
        {
        }

        public async Task InserirPessoaDocumentoCustomizado(string numero, Guid pessoaId, Guid tipoDocumentoId)
        {
            var insert = $@"insert into [PES_PessoaDocumento] ([pes_id],[tdo_id],[psd_numero]) values ('{pessoaId}','{tipoDocumentoId}','{numero}');";

            await conexao.Obter().ExecuteScalarAsync(insert);
        }
    }
}
