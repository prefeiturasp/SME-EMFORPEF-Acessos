using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.Acessos
{
    public class UsuarioValidacaoTokenMap : DommelEntityMap<UsuarioValidacaoToken>
    {
        public UsuarioValidacaoTokenMap()
        {
            ToTable("usuario_validacao_token");
            Map(a => a.Id).ToColumn("id").IsIdentity().IsKey();;
            Map(a => a.Login).ToColumn("login");
            Map(a => a.Expiracao).ToColumn("expiracao");
            Map(a => a.Token).ToColumn("token");
            Map(a => a.CodigoSistema).ToColumn("codigo_sistema");
            Map(a => a.TipoAcao).ToColumn("tipo");
        }
    }
}
