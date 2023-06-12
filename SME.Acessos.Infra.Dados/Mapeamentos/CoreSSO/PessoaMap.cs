using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.CoreSSO
{
    public class PessoaMap : DommelEntityMap<Pessoa>
    {
        public PessoaMap()
        {
            ToTable("SYS_Pessoa");
            Map(a => a.Id).ToColumn("pes_id");
            Map(a => a.Nome).ToColumn("pes_nome");
            Map(a => a.NomeAbreviado).ToColumn("pes_nome_abreviado");
            Map(a => a.IdNacionalidade).ToColumn("pai_idNacionalidade");
            Map(a => a.Naturalizado).ToColumn("pes_naturalizado");
            Map(a => a.IdNaturalidade).ToColumn("cid_idNaturalidade");
            Map(a => a.Situacao).ToColumn("usu_situacao");
            Map(a => a.DataNascimento).ToColumn("pes_dataNascimento");
            Map(a => a.EstadoCivil).ToColumn("pes_estadoCivil");
            Map(a => a.RacaCor).ToColumn("pes_racaCor");
            Map(a => a.Sexo).ToColumn("pes_sexo");
            Map(a => a.IdFiliacaoPai).ToColumn("pes_idFiliacaoPai");
            Map(a => a.IdFiliacaoMae).ToColumn("pes_idFiliacaoMae");
            Map(a => a.DataCriacao).ToColumn("pes_dataCriacao");
            Map(a => a.DataAlteracao).ToColumn("pes_dataAlteracao");
            Map(a => a.Integridade).ToColumn("pes_integridade");
            Map(a => a.IdFoto).ToColumn("pes_idFoto");
            Map(a => a.NomeSocial).ToColumn("pes_nomeSocial");
        }
    }
}
