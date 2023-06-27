using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.Acessos
{
    public class UsuarioRecuperacaoSenhaMap : DommelEntityMap<UsuarioRecuperacaoSenha>
    {
        public UsuarioRecuperacaoSenhaMap()
        {
            ToTable("usuario_recuperacao_senha");
            Map(a => a.Id);
            Map(a => a.Login);
            Map(a => a.Expiracao);
            Map(a => a.Token);
            Map(a => a.CodigoSistema).ToColumn("codigo_sistema");
        }
    }
}
