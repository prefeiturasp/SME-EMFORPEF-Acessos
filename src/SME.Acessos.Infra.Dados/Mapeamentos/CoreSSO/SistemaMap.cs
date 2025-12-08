using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.CoreSSO
{
    public class SistemaMap : DommelEntityMap<Sistema>
    {
        public SistemaMap()
        {
            ToTable("SYS_Sistema");
            Map(a => a.SistemaId).ToColumn("sis_id");
            Map(a => a.Nome).ToColumn("sis_nome");
            Map(a => a.Descricao).ToColumn("sis_descricao");
            Map(a => a.Caminho).ToColumn("sis_caminho");
            Map(a => a.UrlImagem).ToColumn("sis_urlImagem");
            Map(a => a.UrlLogoCabecalho).ToColumn("sis_urlLogoCabecalho");
            Map(a => a.TipoAutenticacao).ToColumn("sis_tipoAutenticacao");
            Map(a => a.UrlIntegracao).ToColumn("sis_urlIntegracao");
            Map(a => a.Situacao).ToColumn("sis_situacao");
            Map(a => a.CaminhoLogout).ToColumn("sis_caminhoLogout");
            Map(a => a.OcultaLogo).ToColumn("sis_ocultarLogo");
        }
    }
}
