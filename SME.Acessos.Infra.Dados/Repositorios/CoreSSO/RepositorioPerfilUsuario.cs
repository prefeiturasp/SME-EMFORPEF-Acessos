using System.Collections;
using Dapper;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioPerfilUsuario : RepositorioBaseCoreSSO<PerfilUsuario>, IRepositorioPerfilUsuario
    {
        public RepositorioPerfilUsuario(IConexaoCoreSSO conexao) : base(conexao)
        {
        }

        public async Task<IList<PerfilUsuario>> ObterPerfisUsuario(string login, int sistemaId)
        {
            var query = @"select            
                            g.gru_id as Id,
                            g.gru_nome as Nome
                        from sys_usuario u
                            inner join sys_usuariogrupo ug on u.usu_id = ug.usu_id
                            inner join sys_grupo g on g.gru_id = ug.gru_id
                        where
                            u.usu_login = @login
                            and g.sis_id = @sistemaId ";
            
            return (await conexao.Obter().QueryAsync<PerfilUsuario>(query, new { login, sistemaId })).ToList();
        }
    }
}
