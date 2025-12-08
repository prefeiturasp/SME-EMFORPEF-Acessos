using SME.Acessos.Infra.Dominio;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;

namespace SME.Acessos.Infra.Dados.Acessos
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
