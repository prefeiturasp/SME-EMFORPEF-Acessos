using Dapper;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Infra.Dados.Repositorios.CoreSSO
{
    public class RepositorioGrupoPermissao : RepositorioBaseCoreSSO<GrupoPermissao>, IRepositorioGrupoPermissao
    {
        public RepositorioGrupoPermissao(IConexaoCoreSSO conexao) : base(conexao)
        {
        }

        public async Task<IList<GrupoPermissao>> ObterModulosPorPerfilSistema(Guid perfilId, int sistemaId)
        {
            var query = @"SELECT                     
	                            gp.sis_id,		                    
                                gp.gru_id,			                    
			                    gp.mod_id,
			                    gp.grp_consultar, 
                                gp.grp_inserir, 
                                gp.grp_alterar, 
                                gp.grp_excluir,                                                                
                                m.sis_id,
			                    m.mod_id,                                
                                m.mod_nome                                
                           FROM sys_grupo g 
                           INNER JOIN sys_visao v ON v.vis_id = g.vis_id 
                           INNER JOIN sys_visaomodulo vm ON vm.vis_id = v.vis_id AND vm.sis_id = g.sis_id 
                           INNER JOIN sys_grupopermissao gp ON g.gru_id = gp.gru_id AND g.sis_id = gp.sis_id 
                           INNER JOIN SYS_Modulo  m ON m.mod_id = gp.mod_id AND m.mod_id = vm.mod_id AND m.sis_id = g.sis_id
                            WHERE g.gru_id = @perfilId  
                                AND g.gru_situacao = 1 
                                AND m.mod_idPai IS NULL 
                                AND g.sis_id = @sistemaId";

            var grupoPermissao = await conexao.Obter().QueryAsync<GrupoPermissao, Modulo, GrupoPermissao>(query, 
                (grupoPermissao, modulo) =>
                {
                    grupoPermissao.AdicionarModulo(modulo);
                    return grupoPermissao;
                }, new { perfilId, sistemaId}, splitOn: "sis_id");
            return grupoPermissao.ToList();
        }
    }
}
