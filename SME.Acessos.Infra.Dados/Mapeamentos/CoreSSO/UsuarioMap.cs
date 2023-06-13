using Dapper.FluentMap.Dommel.Mapping;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dados.Mapeamentos.CoreSSO
{
    public class UsuarioMap : DommelEntityMap<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("SYS_Usuario");
            Map(a => a.Id).ToColumn("usu_id");
            Map(a => a.Login).ToColumn("usu_login");
            Map(a => a.Email).ToColumn("usu_email");
            Map(a => a.Senha).ToColumn("usu_senha");
            Map(a => a.Dominio).ToColumn("usu_dominio");
            Map(a => a.Criptografia).ToColumn("usu_criptografia");
            Map(a => a.Situacao).ToColumn("usu_situacao");
            Map(a => a.DataCriacao).ToColumn("usu_dataCriacao");
            Map(a => a.DataAlteracao).ToColumn("usu_dataAlteracao");
            Map(a => a.PessoaId).ToColumn("pes_id");
            Map(a => a.Integridade).ToColumn("usu_integridade");
            Map(a => a.IntegracaoAD).ToColumn("usu_integridadeAD");
            Map(a => a.IntegracaoExterna).ToColumn("usu_integridadeExterna");
            Map(a => a.DataAlteracaoSenha).ToColumn("usu_dataAlteracao");
        }
    }
}
