using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.CoreSSO;

namespace SME.Acessos.Infra.Dados.Mapeamentos.CoreSSO
{
    public class UsuarioMap : DommelEntityMap<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("SYS_Usuario");
            Map(a => a.Id).ToColumn("usu_id");
            Map(a => a.Login).ToColumn("usu_login");
            Map(a => a.Email).ToColumn("usu_email");
            Map(a => a.Senha).ToColumn("usu_senha");
        }
    }
}
