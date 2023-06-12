using System.Data.SqlClient;
using Dapper;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioUsuario : RepositorioBaseCoreSSO<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(IConexaoCoreSSO conexao) : base(conexao)
        {
        }

        public async Task<Usuario> ObterPorLogin(string login)
        {
            var query = @"select usu_id, 
                                 usu_login, 
                                 usu_email, 
                                 usu_senha,
                                 p.pes_id,
                                 p.pes_nome
                         from SYS_Usuario u 
                         join pes_pessoa p on u.pes_id = p.pes_id
                         where usu_login = @login ";
            
            var usuarios = await conexao.Obter().QueryAsync<Usuario, Pessoa, Usuario>(query, 
                (usuario, pessoa) =>
                {
                    usuario.AdicionarPessoa(pessoa);
                    return usuario;
                }, new { login }, splitOn: "pes_id");
            return usuarios.FirstOrDefault();
        }
    }
}
