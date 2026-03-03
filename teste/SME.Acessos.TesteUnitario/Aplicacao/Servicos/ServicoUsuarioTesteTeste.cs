using FluentAssertions;
using Moq;
using Moq.AutoMock;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Aplicacao.Servicos;

namespace SME.Acessos.TesteUnitario.Aplicacao.Servicos
{
    public class ServicoUsuarioTesteTeste
    {
        private readonly Mock<IServicoUsuarios> _servicoUsuariosMock;
        private readonly Mock<IServicoUsuarioGrupo> _servicoUsuarioGrupoMock;
        private readonly ServicoUsuarioTeste _sut;

        public ServicoUsuarioTesteTeste()
        {
            var mocker = new AutoMocker();
            _servicoUsuariosMock = mocker.GetMock<IServicoUsuarios>();
            _servicoUsuarioGrupoMock = mocker.GetMock<IServicoUsuarioGrupo>();
            _sut = mocker.CreateInstance<ServicoUsuarioTeste>();
        }

        [Fact]
        public async Task DadoQuantidadeValida_QuandoCadastrarUsuariosEmMassa_EntaoDeveRetornarListaDeUsuariosCadastrados()
        {
            // Arrange
            int quantidade = 10;
            _servicoUsuariosMock.Setup(s => s.Cadastrar(It.IsAny<UsuarioDTO>())).ReturnsAsync(true);
            _servicoUsuarioGrupoMock.Setup(s => s.VincularPerfil(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(true);

            // Act
            var result = await _sut.CadastrarUsuariosEmMassaAsync(quantidade, null);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(quantidade);
            _servicoUsuariosMock.Verify(s => s.Cadastrar(It.IsAny<UsuarioDTO>()), Times.Exactly(quantidade));
            _servicoUsuarioGrupoMock.Verify(s => s.VincularPerfil(It.IsAny<string>(), new Guid("7EDA4540-A16C-4FE5-8322-9F75B3414E27")), Times.Exactly(quantidade));
        }

        [Fact]
        public async Task DadoFalhaNoCadastro_QuandoCadastrarUsuariosEmMassa_EntaoDeveRetornarListaVazia()
        {
            // Arrange
            int quantidade = 10;
            Guid perfilId = Guid.NewGuid();
            _servicoUsuariosMock.Setup(s => s.Cadastrar(It.IsAny<UsuarioDTO>())).ReturnsAsync(false);

            // Act
            var result = await _sut.CadastrarUsuariosEmMassaAsync(quantidade, perfilId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
            _servicoUsuariosMock.Verify(s => s.Cadastrar(It.IsAny<UsuarioDTO>()), Times.Exactly(quantidade));
            _servicoUsuarioGrupoMock.Verify(s => s.VincularPerfil(It.IsAny<string>(), It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task DadoFalhaNoVinculoDePerfil_QuandoCadastrarUsuariosEmMassa_EntaoDeveRetornarListaVazia()
        {
            // Arrange
            int quantidade = 10;
            Guid perfilId = Guid.NewGuid();
            _servicoUsuariosMock.Setup(s => s.Cadastrar(It.IsAny<UsuarioDTO>())).ReturnsAsync(true);
            _servicoUsuarioGrupoMock.Setup(s => s.VincularPerfil(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(false);
            // Act
            var result = await _sut.CadastrarUsuariosEmMassaAsync(quantidade, perfilId);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
            _servicoUsuariosMock.Verify(s => s.Cadastrar(It.IsAny<UsuarioDTO>()), Times.Exactly(quantidade));
            _servicoUsuarioGrupoMock.Verify(s => s.VincularPerfil(It.IsAny<string>(), perfilId), Times.Exactly(quantidade));
        }
    }
}
