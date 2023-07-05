using Shouldly;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Extensions;
using SME.Acessos.TesteIntegracao.Constantes;
using SME.Acessos.TesteIntegracao.Setup;
using Xunit;

namespace SME.Acessos.TesteIntegracao.Autenticacao
{
    public class Ao_validar_token_recuperacao_senha : TesteBase
    {
        public Ao_validar_token_recuperacao_senha(CollectionFixture collectionFixture) : base(collectionFixture)
        { }

        [Fact(DisplayName = "Usuário - Deve identificar que o token está válido")]
        public async Task Deve_identificar_que_o_token_esta_valido()
        {
            var token = Guid.NewGuid();

            await InserirNaBase(new UsuarioRecuperacaoSenha()
            {
                CodigoSistema = ConstantesTestes.SISTEMA_98,
                Expiracao = DateTimeExtensions.HorarioBrasilia().AddMinutes(ConstantesTestes.EXPIRES_IN_720_MINUTES),
                Token = token,
                Login = ConstantesTestes.LOGIN_99999999998
            });
            
            var retorno = await GetServicoUsuarios().ValidarTokenRecuperacaoSenha(token, ConstantesTestes.SISTEMA_98);
            
            retorno.ShouldBeTrue();
        }
        
        [Fact(DisplayName = "Usuário - Deve identificar que o token está expirado")]
        public async Task Deve_identificar_que_o_token_esta_expirado()
        {
            var token = Guid.NewGuid();

            await InserirNaBase(new UsuarioRecuperacaoSenha()
            {
                CodigoSistema = ConstantesTestes.SISTEMA_98,
                Expiracao = DateTimeExtensions.HorarioBrasilia().AddMinutes(-ConstantesTestes.EXPIRES_IN_720_MINUTES),
                Token = token,
                Login = ConstantesTestes.LOGIN_99999999998
            });
            
            var retorno = await GetServicoUsuarios().ValidarTokenRecuperacaoSenha(token, ConstantesTestes.SISTEMA_98);
            
            retorno.ShouldBeFalse();
        }
    }
}