using SME.Acessos.Infra.Dominio;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dados
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
