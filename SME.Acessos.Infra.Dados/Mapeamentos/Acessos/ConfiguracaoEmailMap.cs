using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.Acessos
{
    public class ConfiguracaoEmailMap : DommelEntityMap<ConfiguracaoEmail>
    {
        public ConfiguracaoEmailMap()
        {
            ToTable("configuracao_email");
            Map(a => a.Id);
            Map(a => a.Email).ToColumn("email");
            Map(a => a.CodigoSistema).ToColumn("codigo_sistema");
            Map(a => a.Nome).ToColumn("nome");
            Map(a => a.smtp).ToColumn("smtp");
            Map(a => a.Usuario).ToColumn("usuario");
            Map(a => a.Senha).ToColumn("senha");
            Map(a => a.Porta).ToColumn("porta");
            Map(a => a.TLS).ToColumn("tls");
        }
    }
}
