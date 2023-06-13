using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.Acessos;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.CoreSSO
{
    public class UsuarioGrupoMap : DommelEntityMap<UsuarioGrupo>
    {
        public UsuarioGrupoMap()
        {
            ToTable("SYS_UsuarioGrupo");
            Map(a => a.Id).ToColumn("usu_id");
            Map(a => a.GrupoId).ToColumn("gru_id");
        }
    }
}
