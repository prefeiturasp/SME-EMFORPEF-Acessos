using Shouldly;
using SME.Acessos.Infra.Dominio.Extensions;
using SME.Acessos.TesteIntegracao.Constantes;
using SME.Acessos.TesteIntegracao.Setup;
using SME.CDEP.TesteIntegracao;
using Xunit;

namespace SME.Acessos.TesteIntegracao.Autenticacao
{
    public class Ao_obter_perfis_login : TesteBase
    {
        public Ao_obter_perfis_login(CollectionFixture collectionFixture) : base(collectionFixture)
        { }

        [Fact(DisplayName = "Autenticação - Deve obter perfis com token")]
        public async Task Deve_obter_perfis_com_token()
        {
            var retorno = await GetServicoPerfilUsuario().ObterPerfisToken(ConstantesTestes.LOGIN_99999999998, ConstantesTestes.SISTEMA_98);
            retorno.ShouldNotBeNull();
            // retorno.Login.Equals(ConstantesTestes.LOGIN_99999999998).ShouldBeTrue();
            // retorno.Nome.Equals(ConstantesTestes.NOME_99999999998).ShouldBeTrue();
            // retorno.Email.Equals(ConstantesTestes.EMAIL_99999999998).ShouldBeTrue();
        }
    }
}