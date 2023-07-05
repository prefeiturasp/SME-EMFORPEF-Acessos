using Shouldly;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Extensions;
using SME.Acessos.TesteIntegracao.Constantes;
using SME.Acessos.TesteIntegracao.Setup;
using Xunit;

namespace SME.Acessos.TesteIntegracao.Autenticacao
{
    public class Ao_obter_perfis_login : TesteBase
    {
        public Ao_obter_perfis_login(CollectionFixture collectionFixture) : base(collectionFixture)
        { }

        [Fact(DisplayName = "Autenticação - Deve obter perfil externo com token ")]
        public async Task Deve_obter_perfil_externo_com_token()
        {
            await CriarAcoesGrupoPermissoes();
            
            var retorno = await GetServicoPerfilUsuario().ObterPerfisToken(ConstantesTestes.LOGIN_99999999998, ConstantesTestes.SISTEMA_98);
            
            retorno.ShouldNotBeNull();
            retorno.Token.ShouldNotBeEmpty();
            retorno.Autenticado.ShouldBeTrue();
            retorno.Email.ShouldBeEquivalentTo(ConstantesTestes.EMAIL_99999999998);
            retorno.UsuarioLogin.ShouldBeEquivalentTo(ConstantesTestes.LOGIN_99999999998);
            retorno.UsuarioNome.ShouldBeEquivalentTo(ConstantesTestes.NOME_99999999998);
            retorno.DataHoraExpiracao.ShouldBeGreaterThan(DateTimeExtensions.HorarioBrasilia());
            retorno.PerfilUsuario.Any().ShouldBeTrue();
            retorno.PerfilUsuario.Any(a=> a.Perfil == new Guid(ConstantesTestes.GRUPO_EXTERNO_GUID)).ShouldBeTrue();
        }
        
        [Fact(DisplayName = "Autenticação - Deve obter perfil Admin Geral com token")]
        public async Task Deve_obter_perfil_admin_geral_com_token()
        {
            await CriarAcoesGrupoPermissoes();
            
            var retorno = await GetServicoPerfilUsuario().ObterPerfisToken(ConstantesTestes.LOGIN_ADMIN_GERAL_1000, ConstantesTestes.SISTEMA_98);
            
            retorno.ShouldNotBeNull();
            retorno.Token.ShouldNotBeEmpty();
            retorno.Autenticado.ShouldBeTrue();
            retorno.Email.ShouldBeEquivalentTo(ConstantesTestes.EMAIL_ADMIN_GERAL_1000);
            retorno.UsuarioLogin.ShouldBeEquivalentTo(ConstantesTestes.LOGIN_ADMIN_GERAL_1000);
            retorno.UsuarioNome.ShouldBeEquivalentTo(ConstantesTestes.NOME_ADMIN_GERAL_1000);
            retorno.DataHoraExpiracao.ShouldBeGreaterThan(DateTimeExtensions.HorarioBrasilia());
            retorno.PerfilUsuario.Any().ShouldBeTrue();
            retorno.PerfilUsuario.Any(a=> a.Perfil == new Guid(ConstantesTestes.GRUPO_ADMIN_GERAL_GUID)).ShouldBeTrue();
        }
    }
}