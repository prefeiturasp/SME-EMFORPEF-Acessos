using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.Acessos;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using Modulo = SME.Acessos.Infra.Dominio.CoreSSO.Entidades.Modulo;

namespace SME.Acessos.Infra.Dados.Mapeamentos.CoreSSO
{
    public class ModuloMap : DommelEntityMap<Modulo>
    {
        public ModuloMap()
        {
            ToTable("SYS_Modulo");
            Map(a => a.SistemaId).ToColumn("sis_id");
            Map(a => a.ModuloId).ToColumn("mod_id");
            Map(a => a.Nome).ToColumn("mod_nome");
            Map(a => a.Descricao).ToColumn("mod_descricao");
            Map(a => a.IdPai).ToColumn("mod_idPai");
            Map(a => a.Auditoria).ToColumn("mod_auditoria");
            Map(a => a.Situacao).ToColumn("mod_situacao");
            Map(a => a.DataCriacao).ToColumn("mod_dataCriacao");
            Map(a => a.DataAlteracao).ToColumn("mod_dataAlteracao");
        }
    }
}
