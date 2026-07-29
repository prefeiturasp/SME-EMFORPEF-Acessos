using Bogus;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using SME.Acessos.Api.Controllers;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Enumerados;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.TesteUnitario.Api.Controllers
{
    public class UsuariosControllerTeste
    {
        private readonly AutoMocker _mocker;
        private readonly UsuariosController _controller;
        private readonly Faker _faker;

        public UsuariosControllerTeste()
        {
            _mocker = new AutoMocker();
            _controller = _mocker.CreateInstance<UsuariosController>();
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task DadoUsuarioDtoValidoQuandoCadastrarEntaoDeveRetornarOkComVerdadeiro()
        {
            // Arrange
            var usuarioDto = new Faker<UsuarioDTO>("pt_BR")
                .RuleFor(u => u.Login, f => f.Internet.UserName())
                .RuleFor(u => u.Nome, f => f.Person.FullName)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Senha, f => f.Internet.Password())
                .Generate();

            var servicoMock = _mocker.GetMock<IServicoUsuarios>();
            servicoMock.Setup(s => s.Cadastrar(usuarioDto)).ReturnsAsync(true);

            // Act
            var resultado = await _controller.CadastrarUsuarioCoreSSO(usuarioDto, servicoMock.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var valorRetornado = Assert.IsType<bool>(okResult.Value);

            Assert.True(valorRetornado);
            servicoMock.Verify(s => s.Cadastrar(usuarioDto), Times.Once);
        }

        [Fact]
        public async Task DadoLoginValidoQuandoObterMeusDadosEntaoDeveRetornarOkComDadosUsuarioDto()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var dadosUsuario = new Faker<DadosUsuarioDto>("pt_BR")
                .RuleFor(d => d.Login, _ => login)
                .RuleFor(d => d.Nome, f => f.Person.FullName)
                .RuleFor(d => d.Cpf, f => f.Random.Replace("###########"))
                .RuleFor(d => d.Email, f => f.Internet.Email())
                .Generate();

            var servicoMock = _mocker.GetMock<IServicoUsuarios>();
            servicoMock.Setup(s => s.ObterMeusDados(login)).ReturnsAsync(dadosUsuario);

            // Act
            var resultado = await _controller.MeusDados(login, servicoMock.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var valorRetornado = Assert.IsType<DadosUsuarioDto>(okResult.Value);

            Assert.Equal(login, valorRetornado.Login);
            servicoMock.Verify(s => s.ObterMeusDados(login), Times.Once);
        }

        [Fact]
        public async Task DadoLoginEPerfilIdQuandoVincularPerfilEntaoDeveRetornarOk()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var perfilId = _faker.Random.Guid();

            var servicoMock = _mocker.GetMock<IServicoUsuarioGrupo>();
            servicoMock.Setup(s => s.VincularPerfil(login, perfilId)).ReturnsAsync(true);

            // Act
            var resultado = await _controller.VincularPerfil(login, perfilId, servicoMock.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.True((bool)okResult.Value!);
            servicoMock.Verify(s => s.VincularPerfil(login, perfilId), Times.Once);
        }

        [Theory]
        [InlineData(AlterarSenhaStatus.TokenExpirado, typeof(UnauthorizedObjectResult), MensagemNegocio.TOKEN_INVALIDO_OU_EXPIRADO)]
        [InlineData(AlterarSenhaStatus.ForaPadrao, typeof(UnauthorizedObjectResult), MensagemNegocio.SENHA_FORA_DO_PADRAO)]
        [InlineData(AlterarSenhaStatus.NaoEncontrado, typeof(UnauthorizedObjectResult), MensagemNegocio.USUARIO_OU_SENHA_INCORRETOS)]
        [InlineData(AlterarSenhaStatus.OK, typeof(OkObjectResult), "login.teste")]
        [InlineData(AlterarSenhaStatus.NoHistorico, typeof(UnauthorizedObjectResult), MensagemNegocio.A_SENHA_NAO_PODE_SER_UMA_DAS_ULTIMAS_5_ANTERIORES)]
        [InlineData(AlterarSenhaStatus.SenhaPadrao, typeof(UnauthorizedObjectResult), MensagemNegocio.A_NOVA_SENHA_NAO_PODE_SER_UMA_SENHA_PADRAO)]
        public async Task DadoVariacoesDoStatusDaSenhaQuandoAlterarSenhaComTokenEntaoDeveRetornarActionCorrespondente(
            AlterarSenhaStatus status, Type tipoResultadoEsperado, string valorEsperado)
        {
            // Arrange
            var sistemaId = _faker.Random.Long(1, 100);
            var alterarSenhaDto = new Faker<AlterarSenhaPorTokenDto>()
                .RuleFor(a => a.Token, f => f.Random.Hash())
                .RuleFor(a => a.Senha, f => f.Internet.Password())
                .Generate();

            var retornoDto = new RetornoAlteracaoSenhaDto(status, "login.teste");

            var servicoMock = _mocker.GetMock<IServicoUsuarios>();
            servicoMock.Setup(s => s.AlterarSenhaPorToken(sistemaId, alterarSenhaDto)).ReturnsAsync(retornoDto);

            // Act
            var resultado = await _controller.AlterarSenhaComTokenRecuperacao(sistemaId, alterarSenhaDto, servicoMock.Object);

            // Assert
            Assert.IsType(tipoResultadoEsperado, resultado);

            var objectResult = resultado as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(valorEsperado, objectResult.Value);
        }

        [Fact]
        public async Task DadoTokenESistemaIdQuandoValidarEmailTokenEntaoDeveRetornarOkComStringDeSucesso()
        {
            // Arrange
            var token = _faker.Random.Guid();
            var sistemaId = _faker.Random.Long(1, 100);
            var tipoAcao = TipoAcao.ValidacaoEmail;
            var stringRetornoEsperada = "Email validado com sucesso";

            var servicoMock = _mocker.GetMock<IServicoUsuarios>();
            servicoMock.Setup(s => s.ValidarTokenEmail(token, sistemaId, tipoAcao)).ReturnsAsync(stringRetornoEsperada);

            // Act
            var resultado = await _controller.ValidarEmailToken(token, sistemaId, tipoAcao, servicoMock.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.Equal(stringRetornoEsperada, okResult.Value);
            servicoMock.Verify(s => s.ValidarTokenEmail(token, sistemaId, tipoAcao), Times.Once);
        }
    }
}
