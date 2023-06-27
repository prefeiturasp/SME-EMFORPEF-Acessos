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

        public async Task<bool> UsuarioCadastradoCoreSSO(string login)
        {
            var query = @"select 1
                          from SYS_Usuario su 
                           join PES_Pessoa pp on pp.pes_id = su.pes_id 
                           join PES_PessoaDocumento ppd on ppd.pes_id = pp.pes_id 
                           join SYS_TipoDocumentacao std on std.tdo_id = ppd.tdo_id 
                          where ppd.psd_numero = @login and tdo_sigla = 'CPF'
                          union 
                          select 1
                          from SYS_Usuario su
                          where su.usu_login = @login ";
            
            var usuarios = await conexao.Obter().QueryAsync(query, new { login });
            
            return usuarios.Any();
        }

        public async Task InserirUsuarioCustomizado(string login, string email, string senha, Guid pessoa, Guid entidade)
        {
            var sql = $@"insert into [SYS_Usuario] ([usu_login],[usu_email],[usu_senha],[pes_id],[ent_id]) values ('{login}','{email}','{senha}','{pessoa}','{entidade}'); ";

            await conexao.Obter().ExecuteScalarAsync(sql);
        }

        public Task<Usuario> ValidarTokenRecuperacaoSenha(Guid token, int sistema)
        {
            var query = "select * from usuario where token_recuperacao_senha = @token";

            return conexao.Obter().ExecuteScalarAsync(query, new { token });
        }
    }
}
