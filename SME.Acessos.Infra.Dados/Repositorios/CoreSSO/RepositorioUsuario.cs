using Dapper;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioUsuario : RepositorioBaseCoreSSO<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(IConexaoCoreSSO conexao) : base(conexao)
        {
        }

        public Task<Usuario> ObterPorLogin(string login)
            => conexao.Obter()
            .QueryFirstOrDefaultAsync<Usuario>(@"select usu_id id, 
                                                            usu_login login, 
                                                            p.pes_nome as nome,
                                                            usu_email email, 
                                                            usu_senha senha
                                                    from SYS_Usuario u 
                                                    join pes_pessoa p on u.pes_id = p.pes_id
                                                    where usu_login = @login", new { login });
    }
}
