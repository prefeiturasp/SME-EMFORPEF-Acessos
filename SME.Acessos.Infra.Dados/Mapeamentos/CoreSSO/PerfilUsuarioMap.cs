using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.CoreSSO
{
    public class PerfilUsuarioMap : DommelEntityMap<PerfilUsuario>
    {
        public PerfilUsuarioMap()
        {
            ToTable("SYS_Grupo");
            Map(a => a.Id).ToColumn("gru_id");
            Map(a => a.Nome).ToColumn("gru_nome");
        }
    }
}
