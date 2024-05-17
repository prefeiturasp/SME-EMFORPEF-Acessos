using Dapper;
using SME.Acessos.Infra.Dados.Constantes;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioUsuario : RepositorioBaseCoreSSO<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(IConexaoCoreSSO conexao) : base(conexao)
        {
        }

        public async Task<Usuario> ObterPorLogin(string login, bool somenteAtivos = false)
        {
            var query = @" select usu_id, 
                                 usu_login, 
                                 usu_email, 
                                 usu_senha,
                                 usu_criptografia,                                 
                                 p.pes_id,
                                 p.pes_nome,
                                 pd.psd_numero
                         from SYS_Usuario u 
                         join pes_pessoa p on u.pes_id = p.pes_id
                         left join pes_pessoadocumento pd on p.pes_id = pd.pes_id
                         and pd.tdo_id = @tipoDocumentoCpf
                        where u.usu_login = @login ";

            if (somenteAtivos)
                query += " and u.usu_situacao = 1";

            var usuarios = await conexao.Obter().QueryAsync<Usuario, Pessoa, PessoaDocumento, Usuario>(query,
                (usuario, pessoa, pessoaDocumento) =>
                {
                    usuario.AdicionarPessoa(pessoa);
                    usuario.AdicionarPessoaDocumento(pessoaDocumento);
                    return usuario;
                }, new { login, tipoDocumentoCpf = ConstantesDados.TIPO_DOCUMENTO_CPF, }, splitOn: "pes_id,psd_numero");
            return usuarios.FirstOrDefault();
        }

        public async Task<bool> UsuarioCadastradoCoreSSO(string login)
        {
            var query = @"select 1
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

        public async Task<bool> ValidarSenhaAtual(Guid usuarioId, string senhaAtual)
        {
            var query = @"select 1
                          from SYS_Usuario 
                          where usu_id = @usuarioId 
                            and usu_senha = @senhaAtual ";

            var usuarios = await conexao.Obter().QueryAsync<int>(query, new { usuarioId, senhaAtual });

            return usuarios.Any();
        }

        public async Task AlterarSenha(Guid usuarioId, string senhaNova, TipoCriptografia criptografia)
        {
            var atualizarSenha = @"update SYS_Usuario 
                                        set usu_senha = @senhaNova, 
                                            usu_criptografia = @criptografia,
                                            usu_dataalteracao = getdate(), 
                                            usu_dataalteracaosenha = getdate() 
                                   where usu_id = @usuarioId ";
            await conexao.Obter().ExecuteAsync(atualizarSenha, new { usuarioId, senhaNova, criptografia });
        }

        public async Task InserirHistoricoSenha(Guid usuarioId, string senha, TipoCriptografia criptografia)
        {
            var inserirHistorico = @"INSERT INTO SYS_UsuarioSenhaHistorico
                                        (usu_id ,ush_senha ,ush_criptografia ,ush_id ,ush_data)
                                    VALUES
                                        (@usuarioId ,@senha ,@tipoCriptografia ,NEWID() ,GETDATE())";

            await conexao.Obter().ExecuteAsync(inserirHistorico, new { usuarioId, senha, tipoCriptografia = (int)criptografia });
        }

        public async Task AlterarEmail(Guid usuarioId, string email)
        {
            var alterarEmail = @"update SYS_Usuario 
                                        set usu_email = @email, 
                                            usu_dataalteracao = getdate()
                                   where usu_id = @usuarioId ";
            await conexao.Obter().ExecuteAsync(alterarEmail, new { usuarioId, email });
        }

        public async Task AlterarUsuario(Guid usuarioId, string senhaNova, TipoCriptografia criptografia, string email)
        {
            var situacao = 1;
            var atualizarUsuario = @"update SYS_Usuario 
                                        set usu_senha = @senhaNova, 
                                            usu_criptografia = @criptografia,
                                            usu_email = @email,
                                            usu_situacao = @situacao,
                                            usu_dataalteracao = getdate(), 
                                            usu_dataalteracaosenha = getdate() 
                                   where usu_id = @usuarioId ";

            await conexao.Obter().ExecuteAsync(atualizarUsuario, new { usuarioId, senhaNova, email, criptografia, situacao });
        }

        public async Task<IEnumerable<string>> ObterDresPorLoginEPerfil(string login, Guid? perfil)
        {
            var query = $@"select distinct ua.uad_codigo 
                          from SYS_UsuarioGrupoUA ug
                            join SYS_UnidadeAdministrativa ua on ua.uad_id = ug.uad_id 
                            join SYS_Usuario su on su.usu_id = ug.usu_id 
                          where su.usu_login = @login and ug.gru_id = @perfil";

            var usuarios = await conexao.Obter().QueryAsync<string>(query, new { login, perfil });

            return usuarios;
        }

        public async Task<IEnumerable<DadosUsuario>> ObterUsuariosComPerfisResponsavel(Guid[] perfis, long sistemaId)
        {
            var query = @"select distinct su.usu_login as login, p.pes_nome as nome  
                          from SYS_UsuarioGrupo sug
                            join SYS_Usuario su on su.usu_id = sug.usu_id
                            join SYS_Grupo sg on sg.gru_id = sug.gru_id
                            join pes_pessoa p on p.pes_id = su.pes_id 
                          where sg.sis_id = @sistemaId 
                            and sg.gru_id in @perfis
                          order by p.pes_nome";

            return await conexao.Obter().QueryAsync<DadosUsuario>(query, new { perfis, sistemaId });
        }

        public async Task<string> ObterLoginUsuarioPorCpfCadastradoCoreSSO(string login)
        {
            var query = @"select top 1 su.usu_login
                        from SYS_Usuario su
                        join PES_Pessoa pp on pp.pes_id = su.pes_id
                        join PES_PessoaDocumento ppd on ppd.pes_id = pp.pes_id
                        join SYS_TipoDocumentacao std on std.tdo_id = ppd.tdo_id
                        where
	                        ppd.psd_numero = @login
	                        and tdo_sigla = 'CPF'";

            return await conexao.Obter().QueryFirstOrDefaultAsync<string>(query, new { login });
        }

        public async Task AlterarNome(Guid usuarioId, string nome)
        {
            var alterarNome = @"UPDATE PES_Pessoa
                                 	SET pes_nome = @nome,
                                 	    pes_dataalteracao = getdate()
                                 FROM SYS_Usuario su
                                 JOIN PES_Pessoa pp ON pp.pes_id = su.pes_id
                                 WHERE su.usu_id = @usuarioId ";

            await conexao.Obter().ExecuteAsync(alterarNome, new { usuarioId, nome });
        }

        public async Task Inativar(Guid usuarioId)
        {
            var query = @"update SYS_Usuario 
                                        set usu_situacao = 3,
                                            usu_dataalteracao = getdate()
                                   where usu_id = @usuarioId ";
            await conexao.Obter().ExecuteAsync(query, new { usuarioId });
        }
    }
}
