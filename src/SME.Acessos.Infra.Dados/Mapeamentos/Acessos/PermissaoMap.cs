using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.Acessos
{
    public class PermissaoMap : DommelEntityMap<Permissao>
    {
        public PermissaoMap()
        {
            ToTable("permissoes");
            Map(a => a.Id).ToColumn("idgrupo");
            Map(a => a.ModuloId).ToColumn("idmodulo");
        }
    }
}
