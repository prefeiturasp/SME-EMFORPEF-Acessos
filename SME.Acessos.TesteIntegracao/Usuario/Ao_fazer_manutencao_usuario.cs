using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.TesteIntegracao.Setup;
using SME.CDEP.TesteIntegracao;
using Xunit;

namespace SME.CDE.TesteIntegracao.Usuario
{
    public class Ao_fazer_manutencao_usuario : TesteBase
    {
        public Ao_fazer_manutencao_usuario(CollectionFixture collectionFixture) : base(collectionFixture)
        {
        }

        // [Fact(DisplayName = "Usuário - Obter todos os usuarios")]
        public async Task ObterTodosUsuarios()
        {
            IServicoUsuarios servicoUsuario = GetServicoUsuario();
            servicoUsuario.ShouldNotBeNull();
        }

        // [Fact(DisplayName = "Usuário - Obter por id")]
        public async Task ObterUsuarioPorId()
        {
            IServicoUsuarios servicoUsuario = GetServicoUsuario();
            servicoUsuario.ShouldNotBeNull();
        }

        private IServicoUsuarios GetServicoUsuario()
        {
            return ServiceProvider.GetService<IServicoUsuarios>();
        }
    }
}