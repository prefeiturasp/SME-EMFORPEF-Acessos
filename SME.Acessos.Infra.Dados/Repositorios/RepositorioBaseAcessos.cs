using SME.Acessos.Infra.Dominio;

namespace SME.Acessos.Infra.Dados
{
    public abstract class RepositorioBaseAcessos<TEntidade> : RepositorioBase<TEntidade, long>, IRepositorioBaseAcessos<TEntidade>
        where TEntidade : EntidadeBaseAcessos
    {
        public RepositorioBaseAcessos(IConexaoAcessos conexao)
        {
            this.conexao = conexao;
        }
    }
}
