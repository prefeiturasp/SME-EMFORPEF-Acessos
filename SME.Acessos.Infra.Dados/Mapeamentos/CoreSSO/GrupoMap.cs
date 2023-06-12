using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.CoreSSO
{
    public class GrupoMap : DommelEntityMap<Grupo>
    {
        public GrupoMap()
        {
            ToTable("SYS_Grupo");
            Map(a => a.Id).ToColumn("gru_id");
            Map(a => a.Nome).ToColumn("gru_nome");
            Map(a => a.Situacao).ToColumn("gru_situacao");
            Map(a => a.DataCriacao).ToColumn("gru_dataCriacao");
            Map(a => a.DataAlteracao).ToColumn("gru_dataAlteracao");
            Map(a => a.VisaoId).ToColumn("vis_id");
            Map(a => a.SistemaId).ToColumn("sis_id");
            Map(a => a.Integridade).ToColumn("gru_integridade");
        }
    }
}
