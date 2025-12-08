using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.Acessos
{
    public class SistemaAcaoMap : DommelEntityMap<SistemaAcao>
    {
        public SistemaAcaoMap()
        {
            ToTable("sistema_acao");
            Map(a => a.Id).ToColumn("id");
            Map(a => a.CodigoSistema).ToColumn("codigo_sistema");
            Map(a => a.NomeSistema).ToColumn("nome_sistema");
            Map(a => a.Endereco).ToColumn("endereco");
            Map(a => a.TipoAcao).ToColumn("tipo");
        }
    }
}
