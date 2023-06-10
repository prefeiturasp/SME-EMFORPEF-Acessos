using Dapper;
using SME.Acessos.Infra.Dominio.Acessos;
using SME.Acessos.Infra.Dominio.CoreSSO;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioModuloGrupoPermissaoGrupoPermissao : RepositorioQuery, IRepositorioModuloGrupoPermissao
    {
        public RepositorioModuloGrupoPermissaoGrupoPermissao(IConexaoCoreSSO conexao) : base(conexao)
        {
        }

        public async Task<IList<ModuloGrupoPermissao>> ObterModulosPorPerfilSistema(Guid perfilId, int sistemaId)
        {
            var query = @"SELECT                     
			                    m.mod_id IdModCoreSSO,
                                m.mod_nome Descricao,
                                gp.grp_consultar AS EhConsulta, 
                                gp.grp_inserir AS EhInsercao, 
                                gp.grp_alterar AS EhAlteracao, 
                                gp.grp_excluir AS EhExclusao
                           FROM sys_grupo g 
                           INNER JOIN sys_visao v ON v.vis_id = g.vis_id 
                           INNER JOIN sys_visaomodulo vm ON vm.vis_id = v.vis_id AND vm.sis_id = g.sis_id 
                           INNER JOIN sys_grupopermissao gp ON g.gru_id = gp.gru_id AND g.sis_id = gp.sis_id 
                           INNER JOIN SYS_Modulo  m ON m.mod_id = gp.mod_id AND m.mod_id = vm.mod_id AND m.sis_id = g.sis_id
                            WHERE g.gru_id = @perfilId  
                                AND g.gru_situacao = 1 
                                AND m.mod_idPai IS NULL 
                                AND g.sis_id = @sistemaId";
            
            return (await conexao.Obter().QueryAsync<ModuloGrupoPermissao>(query, new { perfilId, sistemaId })).ToList();
        }
    }
}
