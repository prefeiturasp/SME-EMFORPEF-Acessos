using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Infra.Dados.Repositorios
{
    public class RepositorioBaseCoreSSO<TEntidade> : RepositorioBase<TEntidade, Guid>, IRepositorioBaseCoreSSO<TEntidade>
        where TEntidade : EntidadeBaseCoreSSO
    {
        public RepositorioBaseCoreSSO(IConexaoCoreSSO conexao)
        {
            this.conexao = conexao;
        }
    }
}
