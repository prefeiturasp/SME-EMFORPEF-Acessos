using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dommel;
using SME.Acessos.Infra.Dominio;

namespace SME.Acessos.Infra.Dados
{
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
