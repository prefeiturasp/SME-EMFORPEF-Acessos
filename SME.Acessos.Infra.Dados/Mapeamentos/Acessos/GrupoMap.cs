using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.Acessos
{
    public class GrupoMap : DommelEntityMap<Grupo>
    {
        public GrupoMap()
        {
            ToTable("grupos");
            Map(a => a.Id).ToColumn("id");
            Map(a => a.Perfil).ToColumn("guidPerfil");
            Map(a => a.Nome).ToColumn("nome");
            Map(a => a.IdAbrangencia).ToColumn("idAbrangencia");
        }
    }
}
