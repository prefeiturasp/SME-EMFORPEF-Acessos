using Dommel;
using SME.Acessos.Infra.Dominio;
using System.Diagnostics.CodeAnalysis;

namespace SME.Acessos.Infra.Dados.Repositorios
{
    [ExcludeFromCodeCoverage]
    public abstract class RepositorioBase<TEntidade, TChave> : IRepositorioBase<TEntidade, TChave>
        where TEntidade : EntidadeBase<TChave>
        where TChave : struct
    {
        protected IConexaoBase conexao;

        public async Task<TEntidade> Atualizar(TEntidade entidade)
        {
            await conexao.Obter().UpdateAsync(entidade);

            return entidade;
        }

        public async Task<TChave> Inserir(TEntidade entidade)
        {
            entidade.Id = (TChave)await conexao.Obter().InsertAsync(entidade);

            return entidade.Id;
        }

        public Task<TEntidade> ObterPorId(TChave id)
            => conexao.Obter().GetAsync<TEntidade>(id);

        public async Task<IList<TEntidade>> ObterTodos()
            => (await conexao.Obter().GetAllAsync<TEntidade>())
                .ToList();
    }
}
