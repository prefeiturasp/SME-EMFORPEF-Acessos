using Dapper;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioPessoaDocumento(IConexaoCoreSSO conexao) : RepositorioBaseCoreSSO<PessoaDocumento>(conexao), IRepositorioPessoaDocumento
    {
        public async Task InserirPessoaDocumentoCustomizado(string numero, Guid pessoaId, Guid tipoDocumentoId)
        {
            var insert = $@"insert into [PES_PessoaDocumento] ([pes_id],[tdo_id],[psd_numero]) values ('{pessoaId}','{tipoDocumentoId}','{numero}');";

            await conexao.Obter().ExecuteScalarAsync(insert);
        }
    }
}
