using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.Acessos
{
    public class UsuarioRecuperacaoTokenMap : DommelEntityMap<UsuarioRecuperacaoToken>
    {
        public UsuarioRecuperacaoTokenMap()
        {
            ToTable("usuario_recuperacao_token");
            Map(a => a.Id);
            Map(a => a.Login).ToColumn("email");
            Map(a => a.ExpiracaoRecuperacaoSenha).ToColumn("expiracao_recuperacao_senha");
            Map(a => a.TokenRecuperacaoSenha).ToColumn("token_recuperacao_senha");
        }
    }
}
