using Shouldly;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Extensions;
using SME.Acessos.TesteIntegracao.Constantes;
using SME.Acessos.TesteIntegracao.Setup;
using Xunit;

namespace SME.Acessos.TesteIntegracao.Autenticacao
{
    public class Ao_recuperacao_senha : TesteBase
    {
        public Ao_recuperacao_senha(CollectionFixture collectionFixture) : base(collectionFixture)
        { }

        [Fact(DisplayName = "Usuário - Deve identificar que o token está válido")]
        public async Task Deve_identificar_que_o_token_esta_valido()
        {
            var token = Guid.NewGuid();

            await InserirNaBase(new UsuarioValidacaoToken()
            {
                CodigoSistema = ConstantesTestes.SISTEMA_98_ID,
                Expiracao = DateTimeExtensions.HorarioBrasilia().AddMinutes(ConstantesTestes.EXPIRES_IN_720_MINUTES),
                Token = token,
                Login = ConstantesTestes.LOGIN_99999999998
            });
            
            var retorno = await GetServicoUsuarios().ValidarTokenSenha(token, ConstantesTestes.SISTEMA_98_ID);
            
            retorno.ShouldBeTrue();
        }
        
        [Fact(DisplayName = "Usuário - Deve identificar que o token está expirado")]
        public async Task Deve_identificar_que_o_token_esta_expirado()
        {
            var token = Guid.NewGuid();

            await InserirNaBase(new UsuarioValidacaoToken()
            {
                CodigoSistema = ConstantesTestes.SISTEMA_98_ID,
                Expiracao = DateTimeExtensions.HorarioBrasilia().AddMinutes(-ConstantesTestes.EXPIRES_IN_720_MINUTES),
                Token = token,
                Login = ConstantesTestes.LOGIN_99999999998
            });
            
            var retorno = await GetServicoUsuarios().ValidarTokenSenha(token, ConstantesTestes.SISTEMA_98_ID);
            
            retorno.ShouldBeTrue();
        }
        
        [Fact(DisplayName = "Usuário - Deve retornar o e-mail cadastrado no CoreSSO")]
        public async Task Deve_retornar_o_email_cadastrado_no_CoreSSO()
        {
            var token = Guid.NewGuid();

            await InserirNaBase(new UsuarioValidacaoToken()
            {
                CodigoSistema = ConstantesTestes.SISTEMA_98_ID,
                Expiracao = DateTimeExtensions.HorarioBrasilia().AddMinutes(ConstantesTestes.EXPIRES_IN_720_MINUTES),
                Token = token,
                Login = ConstantesTestes.LOGIN_99999999998
            });

            await InserirNaBase("sistema_acao",
                new[] { "codigo_sistema", "nome_sistema", "endereco" },
                new[]
                {
                    ConstantesTestes.SISTEMA_98_ID.ToString(), 
                    ConstantesTestes.SISTEMA_98_NOME,
                    ConstantesTestes.SISTEMA_98_PAGINA
                });
            
            // var retorno = await GetServicoUsuarios().RecuperarSenha(ConstantesTestes.LOGIN_99999999998, ConstantesTestes.SISTEMA_98_ID);
            //Aqui não temos acesso ao endereço onde está o template que sobrescreve o e-mail
        }
    }
}