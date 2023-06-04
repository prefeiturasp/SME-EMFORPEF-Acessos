using Dapper;
using SME.Acessos.Infra.Dominio.CoreSSO;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioUsuario : RepositorioBaseCoreSSO<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(IConexaoCoreSSO conexao) : base(conexao)
        {
        }

        public Task<Usuario> ObterPorLogin(string login)
            => conexao.Obter()
            .QueryFirstOrDefaultAsync<Usuario>("select usu_id, usu_login, usu_email, usu_senha from SYS_Usuario where usu_login = @login", new { login });
    }
}
