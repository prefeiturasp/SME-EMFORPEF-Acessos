using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.CoreSSO
{
    public class PessoaDocumentoMap : DommelEntityMap<PessoaDocumento>
    {
        public PessoaDocumentoMap()
        {
            ToTable("PES_PessoaDocumento");
            Map(a => a.Numero).ToColumn("psd_numero");
        }
    }
}
