using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.Acessos
{
    public class SistemaRecuperacaoSenhaMap : DommelEntityMap<SistemaRecuperacaoSenha>
    {
        public SistemaRecuperacaoSenhaMap()
        {
            ToTable("sistema_recuperacao_senha");
            Map(a => a.Id);
            Map(a => a.CodigoSistema).ToColumn("codigo_sistema");
            Map(a => a.NomeSistema).ToColumn("nome_sistema");
            Map(a => a.PaginaRecuperacaoSenha).ToColumn("pagina_recuperacao_senha");
        }
    }
}
