using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.Acessos;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.CoreSSO
{
    public class GrupoPermissaoMap : DommelEntityMap<GrupoPermissao>
    {
        public GrupoPermissaoMap()
        {
            ToTable("SYS_GrupoPermissao");
            Map(a => a.Id).ToColumn("gru_id");
            Map(a => a.SistemaId).ToColumn("sis_id");
            Map(a => a.ModuloId).ToColumn("mod_id");
            Map(a => a.EhConsulta).ToColumn("grp_consultar");
            Map(a => a.EhInsercao).ToColumn("grp_inserir");
            Map(a => a.EhAlteracao).ToColumn("grp_alterar");
            Map(a => a.EhExclusao).ToColumn("grp_excluir");
        }
    }
}
