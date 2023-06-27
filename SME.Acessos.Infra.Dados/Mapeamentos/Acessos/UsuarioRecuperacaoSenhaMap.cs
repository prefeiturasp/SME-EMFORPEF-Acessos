using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.Acessos
{
    public class UsuarioRecuperacaoSenhaMap : DommelEntityMap<UsuarioRecuperacaoSenha>
    {
        public UsuarioRecuperacaoSenhaMap()
        {
            ToTable("usuario_recuperacao_senha");
            Map(a => a.Id).ToColumn("id");
            Map(a => a.Login).ToColumn("login");
            Map(a => a.Expiracao).ToColumn("expiracao");
            Map(a => a.Token).ToColumn("token");
            Map(a => a.CodigoSistema).ToColumn("codigo_sistema");
        }
    }
}
