using Dapper;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Infra.Dados.Constantes;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    [ExcludeFromCodeCoverage]
    public class RepositorioUsuarioGrupoPessoa(IConexaoCoreSSO conexao) : RepositorioBaseCoreSSO<UsuarioGrupoPessoa>(conexao), IRepositorioUsuarioGrupoPessoa
    {
        public async Task<IList<UsuarioGrupoPessoa>> ObterPerfisUsuario(string login, int sistemaId)
        {
            var query = @"select
                            ug.usu_id UsuarioId,                        
                            u.usu_email UsuarioEmail,
                            p.pes_nome PessoaNome,
                            ug.gru_id GrupoId,
                            g.gru_nome GrupoNome,
                            pd.psd_numero as cpf 
                        from sys_usuario u
                            inner join sys_usuariogrupo ug on u.usu_id = ug.usu_id
                            inner join sys_grupo g on g.gru_id = ug.gru_id
                            inner join pes_pessoa p on p.pes_id = u.pes_id
                             left join pes_pessoadocumento pd on p.pes_id = pd.pes_id
                             and pd.tdo_id = @tipoDocumentoCpf
                        where
                            u.usu_login = @login
                            and g.sis_id = @sistemaId and u.usu_situacao = 1";

            var usuariosGrupos = await conexao.Obter().QueryAsync<UsuarioGrupoPessoa>(query, new { login, sistemaId, tipoDocumentoCpf = ConstantesDados.TIPO_DOCUMENTO_CPF });
            return [.. usuariosGrupos];
        }

        public async Task<IList<UsuarioGrupoPessoa>> ObterUsuariosPerfilPareceristasConecta()
        {
            const int sistemaIdConecta = 1007;
            var query = new StringBuilder($@" select
                            ug.usu_id UsuarioId,                        
                            u.usu_email UsuarioEmail,
                            p.pes_nome PessoaNome,
                            ug.gru_id GrupoId,
                            g.gru_nome GrupoNome,
                            pd.psd_numero as cpf,
                            u.usu_login as login
                        from sys_usuario u
                            inner join sys_usuariogrupo ug on u.usu_id = ug.usu_id
                            inner join sys_grupo g on g.gru_id = ug.gru_id
                            inner join pes_pessoa p on p.pes_id = u.pes_id
                             left join pes_pessoadocumento pd on p.pes_id = pd.pes_id
                             and pd.tdo_id = @tipoDocumentoCpf
                        where g.gru_id = @perfilParecerista ");

            query.AppendLine(" and g.sis_id = @sistemaIdConecta; ");
            var usuariosGrupos = await conexao.Obter().QueryAsync<UsuarioGrupoPessoa>(query.ToString(), new { sistemaIdConecta, tipoDocumentoCpf = ConstantesDados.TIPO_DOCUMENTO_CPF, perfilParecerista = ConstantesCoreSSO.PERFIL_PARECERISTA });
            return [.. usuariosGrupos];
        }
    }
}
