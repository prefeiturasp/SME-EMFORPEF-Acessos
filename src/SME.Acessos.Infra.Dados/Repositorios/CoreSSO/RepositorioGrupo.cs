using Dapper;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioGrupo(IConexaoCoreSSO conexao) : IRepositorioGrupo
    {
        public Task<IEnumerable<Grupo>> ObterPorSistemaId(long sistemaId)
        {
            var query = @"SELECT gru_id, gru_nome ,vis_id                       
                          FROM sys_grupo 
                          WHERE sis_id = @sistemaId
                          ORDER BY gru_nome ";

            return conexao.Obter().QueryAsync<Grupo>(query, new { sistemaId });
        }
        
        public Task<Grupo> ObterGrupoPorIdSistemaId(long sistemaId, Guid grupoId)
        {
            var query = @"SELECT gru_id, gru_nome ,vis_id                       
                          FROM sys_grupo 
                          WHERE sis_id = @sistemaId
                          and gru_id = @grupoId
                          ORDER BY gru_nome ";

            return conexao.Obter().QueryFirstOrDefaultAsync<Grupo>(query, new { sistemaId,grupoId });
        }
    }
}
