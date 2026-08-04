using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dominio.CoreSSO.Repositorios
{
    public interface IRepositorioPessoa : IRepositorioBaseCoreSSO<Pessoa>
    {
        Task<Guid> InserirPessoaCustomizado(string nome);
    }
}
