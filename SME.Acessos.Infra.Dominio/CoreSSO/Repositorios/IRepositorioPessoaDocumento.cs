using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dominio.CoreSSO.Repositorios
{
    public interface IRepositorioPessoaDocumento : IRepositorioBaseCoreSSO<PessoaDocumento>
    {
        Task InserirPessoaDocumentoCustomizado(string numero, Guid pessoaId, Guid tipoDocumentoId);
    }
}
