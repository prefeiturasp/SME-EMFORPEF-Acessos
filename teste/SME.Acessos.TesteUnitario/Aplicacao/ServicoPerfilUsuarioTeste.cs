using Bogus;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Aplicacao.Servicos;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.TesteUnitario.Aplicacao
{
    public class ServicoPerfilUsuarioTeste
    {
        private readonly ServicoPerfilUsuario _sut;
        private readonly Faker _faker;
        private readonly Mock<IRepositorioUsuarioGrupoPessoa> _repositorioUsuarioGrupoPessoaMock;
        private readonly Mock<IServicoTokenJwt> _servicoTokenJwtMock;
        private readonly Mock<IRepositorioUsuario> _repositorioUsuarioMock;

        public ServicoPerfilUsuarioTeste()
        {
            var mocker = new AutoMocker();
            _sut = mocker.CreateInstance<ServicoPerfilUsuario>();
            _faker = new Faker("pt_BR");

            _repositorioUsuarioGrupoPessoaMock = mocker.GetMock<IRepositorioUsuarioGrupoPessoa>();
            _servicoTokenJwtMock = mocker.GetMock<IServicoTokenJwt>();
            _repositorioUsuarioMock = mocker.GetMock<IRepositorioUsuario>();
        }

        [Fact]
        public async Task DadoUsuarioComPerfisEPerfilIdValido_QuandoObterPerfisToken_EntaoRetornaRetornoPerfilUsuarioDTO()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var sistemaId = _faker.Random.Int(1, 100);
            var perfilId = Guid.NewGuid();
            var tokenEsperado = _faker.Random.AlphaNumeric(32);
            var dataExpiracaoEsperada = DateTime.Now.AddHours(2);

            var perfisUsuario = new List<UsuarioGrupoPessoa>
            {
                new()
                {
                    GrupoId = perfilId,
                    PessoaNome = _faker.Person.FullName,
                    UsuarioEmail = _faker.Internet.Email(),
                    Cpf = _faker.Random.Replace("###########"),
                    GrupoNome = "Administrador"
                }
            };

            _repositorioUsuarioGrupoPessoaMock
                .Setup(r => r.ObterPerfisUsuario(login, sistemaId))
                .ReturnsAsync(perfisUsuario);

            _servicoTokenJwtMock
                .Setup(s => s.GerarToken(It.IsAny<ClaimsTokenDto>()))
                .Returns(tokenEsperado);

            _servicoTokenJwtMock
                .Setup(s => s.ObterDataHoraExpiracao())
                .Returns(dataExpiracaoEsperada);

            // Act
            var resultado = await _sut.ObterPerfisToken(login, sistemaId, perfilId);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Token.Should().Be(tokenEsperado);
            resultado.UsuarioNome.Should().Be(perfisUsuario.First().PessoaNome);
            resultado.Autenticado.Should().BeTrue();
            resultado.DataHoraExpiracao.Should().Be(dataExpiracaoEsperada);
            _repositorioUsuarioMock.Verify(r => r.ObterPorLogin(It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
        }

        [Fact]
        public async Task DadoUsuarioComPerfisEPerfilIdInvalido_QuandoObterPerfisToken_EntaoLancaExcecaoNegocio()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var sistemaId = _faker.Random.Int(1, 100);
            var perfilIdInvalido = Guid.NewGuid();

            var perfisUsuario = new List<UsuarioGrupoPessoa>
            {
                new() { GrupoId = Guid.NewGuid() }
            };

            _repositorioUsuarioGrupoPessoaMock
                .Setup(r => r.ObterPerfisUsuario(login, sistemaId))
                .ReturnsAsync(perfisUsuario);

            // Act
            Func<Task> acao = async () => await _sut.ObterPerfisToken(login, sistemaId, perfilIdInvalido);

            // Assert
            await acao.Should().ThrowAsync<Exception>()
                .WithMessage($"Perfil {perfilIdInvalido} não encontrado para o usuário {login}");
        }

        [Fact]
        public async Task DadoUsuarioSemPerfisMasCadastradoCoreSSO_QuandoObterPerfisToken_EntaoRetornaRetornoPerfilUsuarioDTO()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var sistemaId = _faker.Random.Int(1, 100);
            var tokenEsperado = _faker.Random.AlphaNumeric(32);

            var usuarioCoreSso = new Usuario
            {
                Login = login,
                Email = _faker.Internet.Email(),
                Senha = "hash",
                Pessoa = new Pessoa { Nome = _faker.Person.FullName },
                Documento = new PessoaDocumento { Numero = _faker.Random.Replace("###########") }
            };

            _repositorioUsuarioGrupoPessoaMock
                .Setup(r => r.ObterPerfisUsuario(login, sistemaId))
                .ReturnsAsync(new List<UsuarioGrupoPessoa>());

            _repositorioUsuarioMock
                .Setup(r => r.ObterPorLogin(login, true))
                .ReturnsAsync(usuarioCoreSso);

            _servicoTokenJwtMock
                .Setup(s => s.GerarToken(It.IsAny<ClaimsTokenDto>()))
                .Returns(tokenEsperado);

            // Act
            var resultado = await _sut.ObterPerfisToken(login, sistemaId);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Token.Should().Be(tokenEsperado);
            resultado.UsuarioNome.Should().Be(usuarioCoreSso.Pessoa.Nome);
            _repositorioUsuarioMock.Verify(r => r.ObterPorLogin(login, true), Times.Once);
        }

        [Fact]
        public async Task DadoUsuarioNaoEncontradoCoreSSO_QuandoObterPerfisToken_EntaoLancaExcecaoNegocio()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var sistemaId = _faker.Random.Int(1, 100);

            _repositorioUsuarioGrupoPessoaMock
                .Setup(r => r.ObterPerfisUsuario(login, sistemaId))
                .ReturnsAsync(new List<UsuarioGrupoPessoa>());

            _repositorioUsuarioMock
                .Setup(r => r.ObterPorLogin(login, true))
                .ReturnsAsync((Usuario)null!);

            // Act
            Func<Task> acao = async () => await _sut.ObterPerfisToken(login, sistemaId);

            // Assert
            await acao.Should().ThrowAsync<Exception>()
                .WithMessage(MensagemNegocio.USUARIO_NAO_ENCONTRADO);
        }

        [Fact]
        public async Task DadoTokenValido_QuandoRevalidar_EntaoRetornaRetornoPerfilUsuarioDTO()
        {
            // Arrange
            var token = _faker.Random.AlphaNumeric(32);
            var dadosToken = new DadosUsuarioTokenDTO
            {
                Login = _faker.Internet.UserName(),
                Sistema = _faker.Random.Int(1, 100),
                Perfil = Guid.NewGuid()
            };

            var perfisUsuario = new List<UsuarioGrupoPessoa>
            {
                new()
                {
                    GrupoId = dadosToken.Perfil.Value,
                    PessoaNome = _faker.Person.FullName,
                    UsuarioEmail = _faker.Internet.Email(),
                    Cpf = _faker.Random.Replace("###########")
                }
            };

            _servicoTokenJwtMock
                .Setup(s => s.ObterDadosToken(token))
                .Returns(dadosToken);

            _repositorioUsuarioGrupoPessoaMock
                .Setup(r => r.ObterPerfisUsuario(dadosToken.Login, dadosToken.Sistema))
                .ReturnsAsync(perfisUsuario);

            _servicoTokenJwtMock
                .Setup(s => s.GerarToken(It.IsAny<ClaimsTokenDto>()))
                .Returns(token);

            // Act
            var resultado = await _sut.Revalidar(token);

            // Assert
            resultado.Should().NotBeNull();
            resultado.UsuarioLogin.Should().Be(dadosToken.Login);
            resultado.Token.Should().Be(token);
            _servicoTokenJwtMock.Verify(s => s.ObterDadosToken(token), Times.Once);
        }

        [Fact]
        public async Task DadoExistemPareceristas_QuandoObterUsuariosPerfilPareceristasConecta_EntaoRetornaListaPreenchida()
        {
            // Arrange
            var pareceristas = new List<UsuarioGrupoPessoa>
            {
                new() { PessoaNome = _faker.Person.FullName, Login = _faker.Internet.UserName(), NomeSocial = _faker.Person.FirstName },
                new() { PessoaNome = _faker.Person.FullName, Login = _faker.Internet.UserName() }
            };

            _repositorioUsuarioGrupoPessoaMock
                .Setup(r => r.ObterUsuariosPerfilPareceristasConecta())
                .ReturnsAsync(pareceristas);

            // Act
            var resultado = await _sut.ObterUsuariosPerfilPareceristasConecta();

            // Assert
            resultado.Should().NotBeNullOrEmpty();
            resultado.Should().HaveCount(2);
            resultado!.First().Nome.Should().Be(pareceristas[0].PessoaNome);
            resultado!.First().Login.Should().Be(pareceristas[0].Login);
        }

        [Fact]
        public async Task DadoNaoExistemPareceristas_QuandoObterUsuariosPerfilPareceristasConecta_EntaoRetornaListaVazia()
        {
            // Arrange
            _repositorioUsuarioGrupoPessoaMock
                .Setup(r => r.ObterUsuariosPerfilPareceristasConecta())
                .ReturnsAsync(new List<UsuarioGrupoPessoa>());

            // Act
            var resultado = await _sut.ObterUsuariosPerfilPareceristasConecta();

            // Assert
            resultado.Should().BeEmpty();
        }
    }
}