using Bogus;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using SME.Acessos.Api.Controllers;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;

namespace SME.Acessos.TesteUnitario.Api.Controllers
{
    public class AutenticacaoControllerTeste
    {
        private readonly AutoMocker _mocker;
        private readonly AutenticacaoController _controller;
        private readonly Faker _faker;

        public AutenticacaoControllerTeste()
        {
            _mocker = new AutoMocker();
            _controller = _mocker.CreateInstance<AutenticacaoController>();
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task DadoDtoValido_QuandoAutenticar_EntaoDeveRetornarOkComRetornoAutenticacaoDto()
        {
            // Arrange
            var autenticacaoDto = new Faker<AutenticacaoDTO>("pt_BR")
                .RuleFor(x => x.Login, f => f.Internet.UserName())
                .RuleFor(x => x.Senha, f => f.Internet.Password())
                .Generate();

            var retornoEsperado = new Faker<RetornoAutenticacaoDTO>("pt_BR")
                .CustomInstantiator(f => new RetornoAutenticacaoDTO
                {
                    Nome = f.Person.FullName,
                    Login = autenticacaoDto.Login,
                    Email = f.Internet.Email(),
                    Cpf = f.Random.Replace("###########")
                })
                .Generate();

            var servicoMock = _mocker.GetMock<IServicoAutenticacao>();
            servicoMock.Setup(s => s.Autenticar(autenticacaoDto.Login, autenticacaoDto.Senha))
                       .ReturnsAsync(retornoEsperado);

            // Act
            var resultado = await _controller.Autenticar(servicoMock.Object, autenticacaoDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var valorRetornado = Assert.IsType<RetornoAutenticacaoDTO>(okResult.Value);

            Assert.Equal(retornoEsperado.Login, valorRetornado.Login);
            servicoMock.Verify(s => s.Autenticar(autenticacaoDto.Login, autenticacaoDto.Senha), Times.Once);
        }

        [Fact]
        public async Task DadoTokenValido_QuandoObterDadosToken_EntaoDeveRetornarOkComRetornoPerfilUsuarioDto()
        {
            // Arrange
            var revalidarDto = new Faker<AutenticacaoRevalidarDTO>("pt_BR")
                .RuleFor(x => x.Token, f => f.Random.AlphaNumeric(32))
                .Generate();

            var retornoEsperado = new Faker<RetornoPerfilUsuarioDTO>("pt_BR")
                .RuleFor(x => x.Token, _ => revalidarDto.Token)
                .RuleFor(x => x.UsuarioLogin, f => f.Internet.UserName())
                .RuleFor(x => x.Autenticado, _ => true)
                .Generate();

            var servicoMock = _mocker.GetMock<IServicoPerfilUsuario>();
            servicoMock.Setup(s => s.Revalidar(revalidarDto.Token!))
                       .ReturnsAsync(retornoEsperado);

            // Act
            var resultado = await _controller.ObterDadosToken(servicoMock.Object, revalidarDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var valorRetornado = Assert.IsType<RetornoPerfilUsuarioDTO>(okResult.Value);

            Assert.Equal(retornoEsperado.Token, valorRetornado.Token);
            servicoMock.Verify(s => s.Revalidar(revalidarDto.Token!), Times.Once);
        }

        [Fact]
        public async Task DadoLoginESistemaIdValidos_QuandoListarPerfisUsuario_EntaoDeveRetornarOkComRetornoPerfilUsuarioDto()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var sistemaId = _faker.Random.Int(1, 100);

            var retornoEsperado = new Faker<RetornoPerfilUsuarioDTO>("pt_BR")
                .RuleFor(x => x.UsuarioLogin, _ => login)
                .RuleFor(x => x.Autenticado, _ => true)
                .Generate();

            var servicoMock = _mocker.GetMock<IServicoPerfilUsuario>();
            servicoMock.Setup(s => s.ObterPerfisToken(login, sistemaId, null))
                       .ReturnsAsync(retornoEsperado);

            // Act
            var resultado = await _controller.ListarPerfisUsuario(servicoMock.Object, login, sistemaId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var valorRetornado = Assert.IsType<RetornoPerfilUsuarioDTO>(okResult.Value);

            Assert.Equal(retornoEsperado.UsuarioLogin, valorRetornado.UsuarioLogin);
            servicoMock.Verify(s => s.ObterPerfisToken(login, sistemaId, null), Times.Once);
        }

        [Fact]
        public async Task DadoParametrosValidos_QuandoListarPerfisUsuarioComPerfilId_EntaoDeveRetornarOkComRetornoPerfilUsuarioDto()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var sistemaId = _faker.Random.Int(1, 100);
            var perfilId = _faker.Random.Guid();

            var retornoEsperado = new Faker<RetornoPerfilUsuarioDTO>("pt_BR")
                .RuleFor(x => x.UsuarioLogin, _ => login)
                .RuleFor(x => x.Autenticado, _ => true)
                .Generate();

            var servicoMock = _mocker.GetMock<IServicoPerfilUsuario>();
            servicoMock.Setup(s => s.ObterPerfisToken(login, sistemaId, perfilId))
                       .ReturnsAsync(retornoEsperado);

            // Act
            var resultado = await _controller.ListarPerfisUsuario(servicoMock.Object, login, sistemaId, perfilId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var valorRetornado = Assert.IsType<RetornoPerfilUsuarioDTO>(okResult.Value);

            Assert.Equal(retornoEsperado.UsuarioLogin, valorRetornado.UsuarioLogin);
            servicoMock.Verify(s => s.ObterPerfisToken(login, sistemaId, perfilId), Times.Once);
        }
    }
}
