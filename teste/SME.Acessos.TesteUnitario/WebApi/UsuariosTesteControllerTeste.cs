using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Moq;
using Moq.AutoMock;
using SME.Acessos.Api.Controllers;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;

namespace SME.Acessos.TesteUnitario.WebApi
{
    public class UsuariosTesteControllerTeste
    {
        private readonly Mock<IServicoUsuarioTeste> _servicoUsuarioTesteMock;
        private readonly Mock<IWebHostEnvironment> _environmentMock;
        private readonly UsuariosTesteController _sut;

        public UsuariosTesteControllerTeste()
        {
            var mocker = new AutoMocker();
            _servicoUsuarioTesteMock = mocker.GetMock<IServicoUsuarioTeste>();
            _environmentMock = mocker.GetMock<IWebHostEnvironment>();
            _sut = mocker.CreateInstance<UsuariosTesteController>();
        }

        [Fact]
        public async Task DadoAmbienteProducao_QuandoCadastrarUsuariosEmMassa_EntaoDeveRetornarNotFound()
        {
            // Arrange
            _environmentMock.Setup(env => env.EnvironmentName).Returns(Environments.Production);

            // Act
            var result = await _sut.CadastrarUsuariosEmMassa(10, null);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            _servicoUsuarioTesteMock.Verify(s => s.CadastrarUsuariosEmMassaAsync(It.IsAny<int>(), It.IsAny<Guid?>()), Times.Never);
        }

        [Fact]
        public async Task DadaQuantidadeExcessiva_QuandoCadastrarUsuariosEmMassa_EntaoDeveRetornarBadRequest()
        {
            // Arrange
            _environmentMock.Setup(env => env.EnvironmentName).Returns(Environments.Development);

            // Act
            var result = await _sut.CadastrarUsuariosEmMassa(600, null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            _servicoUsuarioTesteMock.Verify(s => s.CadastrarUsuariosEmMassaAsync(It.IsAny<int>(), It.IsAny<Guid?>()), Times.Never);
        }

        [Fact]
        public async Task DadoAmbienteValidoEQuantidadeValida_QuandoCadastrarUsuariosEmMassa_EntaoDeveRetornarOk()
        {
            // Arrange
            _environmentMock.Setup(env => env.EnvironmentName).Returns(Environments.Development);
            var usuariosCadastrados = new List<DadosPessoaUsuarioDto>
            {
                new() { Nome = "usuario1", Login = "login1", Senha = "senha1" },
                new() { Nome = "usuario2", Login = "login2", Senha = "senha2" }
            };

            _servicoUsuarioTesteMock
                .Setup(s => s.CadastrarUsuariosEmMassaAsync(10, null))
                .ReturnsAsync(usuariosCadastrados);

            // Act
            var result = await _sut.CadastrarUsuariosEmMassa(10, null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(usuariosCadastrados, okResult.Value);
            _servicoUsuarioTesteMock.Verify(s => s.CadastrarUsuariosEmMassaAsync(10, null), Times.Once);
        }
    }
}
