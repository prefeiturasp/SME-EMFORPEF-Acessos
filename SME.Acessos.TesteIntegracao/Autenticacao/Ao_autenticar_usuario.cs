using Shouldly;
using SME.Acessos.Infra.Dominio.Extensions;
using SME.Acessos.TesteIntegracao.Constantes;
using SME.Acessos.TesteIntegracao.Setup;
using Xunit;

namespace SME.Acessos.TesteIntegracao.Autenticacao
{
    public class Ao_autenticar_usuario : TesteBase
    {
        public Ao_autenticar_usuario(CollectionFixture collectionFixture) : base(collectionFixture)
        { }

        //[Fact(DisplayName = "Autenticação - Autenticando usuário válido")]
        //public async Task Autenticacao_usuario_valido()
        //{
        //    var retorno = await GetServicoAutenticacao().Autenticar(ConstantesTestes.LOGIN_99999999998, ConstantesTestes.SENHA_99999999998);
        //    retorno.Login.Equals(ConstantesTestes.LOGIN_99999999998).ShouldBeTrue();
        //    retorno.Nome.Equals(ConstantesTestes.NOME_99999999998).ShouldBeTrue();
        //    retorno.Email.Equals(ConstantesTestes.EMAIL_99999999998).ShouldBeTrue();
        //}

        [Fact(DisplayName = "Autenticação - Não deve permitir autenticação sem preenchimento de login e senha")]
        public async Task Nao_deve_permitir_autenticacao_sem_preenchimento_de_login_e_senha()
        {
            await GetServicoAutenticacao().Autenticar(string.Empty, string.Empty).ShouldThrowAsync<NegocioException>();
        }
        
        [Fact(DisplayName = "Autenticação - Não deve permitir autenticação com login que não existe")]
        public async Task Nao_deve_permitir_autenticacao_com_login_que_nao_existe()
        {
            await GetServicoAutenticacao().Autenticar(ConstantesTestes.LOGIN_00000000000, ConstantesTestes.SENHA_99999999998).ShouldThrowAsync<NegocioException>();
        }
        
        [Fact(DisplayName = "Autenticação - Não deve permitir autenticação com senha atual incorreta")]
        public async Task Nao_deve_permitir_autenticacao_com_senha_atual_incorreta()
        {
            await GetServicoAutenticacao().Autenticar(ConstantesTestes.LOGIN_99999999998, ConstantesTestes.SENHA_99999999999).ShouldThrowAsync<NegocioException>();
        }
    }
}