using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.Acessos
{
    public class AcaoMap : DommelEntityMap<Acao>
    {
        public AcaoMap()
        {
            ToTable("acoes");
            Map(a => a.Id).ToColumn("id");
            Map(a => a.Descricao).ToColumn("descricao");
        }
    }
}
