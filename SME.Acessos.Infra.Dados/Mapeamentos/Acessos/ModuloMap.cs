using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.Acessos
{
    public class ModuloMap : DommelEntityMap<Modulo>
    {
        public ModuloMap()
        {
            ToTable("Modulos");
            Map(a => a.Id).ToColumn("id");
            Map(a => a.Descricao).ToColumn("descricao");
            Map(a => a.ModuloCoreSSOId).ToColumn("idmodcoresso");
            Map(a => a.AcaoId).ToColumn("idacao");
        }
    }
}
