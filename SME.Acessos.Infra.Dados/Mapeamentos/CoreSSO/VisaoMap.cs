using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.CoreSSO
{
    public class VisaoMap : DommelEntityMap<Visao>
    {
        public VisaoMap()
        {
            ToTable("SYS_Visao");
            Map(a => a.VisaoId).ToColumn("vis_id");
            Map(a => a.Nome).ToColumn("gru_nome");
        }
    }
}
