using System.Collections;
using System.Text;
using Dapper;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Infra.Dados.Constantes;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioUsuarioGrupoPessoa : RepositorioBaseCoreSSO<UsuarioGrupoPessoa>, IRepositorioUsuarioGrupoPessoa
    {
        public RepositorioUsuarioGrupoPessoa(IConexaoCoreSSO conexao) : base(conexao)
        {
        }

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
                            and g.sis_id = @sistemaId ";
            
            var usuariosGrupos = await conexao.Obter().QueryAsync<UsuarioGrupoPessoa>(query,new { login, sistemaId, tipoDocumentoCpf = ConstantesDados.TIPO_DOCUMENTO_CPF });
            return usuariosGrupos.ToList();
        }

        public async Task<IList<UsuarioGrupoPessoa>> ObterUsuariosPerfilPareceristasConecta(string login, string nome)
        {
            const int sistemaIdConecta = 1007;
            var query = new StringBuilder($@" select
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
                        where g.gru_id = @perfilParecerista ");
            if (!string.IsNullOrEmpty(login))
                query.AppendLine(" and u.usu_login like @login ");
            if (!string.IsNullOrEmpty(nome))
                query.AppendLine("  and p.pes_nome like @nome ");
            
            query.AppendLine(" and g.sis_id = @sistemaIdConecta; ");       
            var usuariosGrupos = await conexao.Obter().QueryAsync<UsuarioGrupoPessoa>(query.ToString(),new { login, nome,sistemaIdConecta, tipoDocumentoCpf = ConstantesDados.TIPO_DOCUMENTO_CPF,perfilParecerista = ConstantesCoreSSO.PERFIL_PARECERISTA });
            return usuariosGrupos.ToList();
        }
    }
}
