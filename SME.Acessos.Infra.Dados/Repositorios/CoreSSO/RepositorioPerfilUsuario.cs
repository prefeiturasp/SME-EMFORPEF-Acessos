using System.Collections;
using Dapper;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioPerfilUsuario : RepositorioBaseCoreSSO<Grupo>, IRepositorioPerfilUsuario
    {
        public RepositorioPerfilUsuario(IConexaoCoreSSO conexao) : base(conexao)
        {
        }

        public async Task<IList<Grupo>> ObterPerfisUsuario(string login, int sistemaId)
        {
            var query = @"select            
                            g.gru_id,
                            g.gru_nome
                        from sys_usuario u
                            inner join sys_usuariogrupo ug on u.usu_id = ug.usu_id
                            inner join sys_grupo g on g.gru_id = ug.gru_id
                        where
                            u.usu_login = @login
                            and g.sis_id = @sistemaId ";
            
            return (await conexao.Obter().QueryAsync<Grupo>(query, new { login, sistemaId })).ToList();
        }
    }
}
