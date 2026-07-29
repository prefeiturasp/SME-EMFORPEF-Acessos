using Bogus;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Servicos;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Enumeradores;
using SME.Acessos.Infra.Dominio.Extensions;
using SME.Acessos.Infra.Dominio.Extensoes;

namespace SME.Acessos.TesteUnitario.Aplicacao
{
    public class ServicoAutenticacaoTeste
    {
        private readonly AutoMocker _mocker;
        private readonly ServicoAutenticacao _sut;
        private readonly Faker _faker;

        public ServicoAutenticacaoTeste()
        {
            _mocker = new AutoMocker();
            _sut = _mocker.CreateInstance<ServicoAutenticacao>();
            _faker = new Faker("pt_BR");
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData(null, "")]
        [InlineData("", null)]
        public async Task DadoLoginESenhaVazios_QuandoAutenticar_EntaoDeveLancarExcecaoNegocio(string? login, string? senha)
        {
            // Arrange

            // Act
            Func<Task> acao = async () => await _sut.Autenticar(login, senha);

            // Assert
            await acao.Should().ThrowAsync<NegocioException>()
                .WithMessage(MensagemNegocio.LOGIN_SENHA_SAO_OBRIGATORIOS);
        }

        [Fact]
        public async Task DadoUsuarioInexistente_QuandoAutenticar_EntaoDeveLancarExcecaoNegocio()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var senha = _faker.Internet.Password();

            _mocker.GetMock<IRepositorioUsuario>()
                   .Setup(r => r.ObterPorLogin(login, true))
                   .ReturnsAsync((Usuario)null!);

            // Act
            Func<Task> acao = async () => await _sut.Autenticar(login, senha);

            // Assert
            await acao.Should().ThrowAsync<NegocioException>()
                .WithMessage(MensagemNegocio.USUARIO_NAO_ENCONTRADO);

            _mocker.GetMock<IRepositorioUsuario>().Verify(r => r.ObterPorLogin(login, true), Times.Once);
        }

        [Fact]
        public async Task DadoSenhaInvalida_QuandoAutenticar_EntaoDeveLancarExcecaoNegocio()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var senhaFornecida = "SenhaFornecida123";

            var usuarioDb = new Usuario
            {
                Login = login,
                Email = _faker.Internet.Email(),
                Senha = "HashSenhaCompletamenteDiferente",
                Criptografia = TipoCriptografia.TripleDES
            };

            _mocker.GetMock<IRepositorioUsuario>()
                   .Setup(r => r.ObterPorLogin(login, true))
                   .ReturnsAsync(usuarioDb);

            // Act
            Func<Task> acao = async () => await _sut.Autenticar(login, senhaFornecida);

            // Assert
            await acao.Should().ThrowAsync<NegocioException>()
                .WithMessage(MensagemNegocio.USUARIO_OU_SENHA_INCORRETOS);
        }

        [Fact]
        public async Task DadoCredenciaisValidas_QuandoAutenticar_EntaoDeveRetornarRetornoAutenticacaoDto()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var senha = "SenhaCorreta123";

            var usuarioDb = new Usuario
            {
                Login = login,
                Email = _faker.Internet.Email(),
                Senha = CriptografiaExtensions.CriptografarSenhaTripleDES(senha),
                Criptografia = TipoCriptografia.TripleDES,
                Pessoa = new Pessoa
                {
                    Nome = _faker.Person.FullName,
                    NomeSocial = _faker.Person.FirstName
                },
                Documento = new PessoaDocumento
                {
                    Numero = _faker.Random.Replace("###########")
                }
            };

            _mocker.GetMock<IRepositorioUsuario>()
                   .Setup(r => r.ObterPorLogin(login, true))
                   .ReturnsAsync(usuarioDb);

            // Act
            var resultado = await _sut.Autenticar(login, senha);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().BeOfType<RetornoAutenticacaoDTO>();
            resultado.Login.Should().Be(usuarioDb.Login);
            resultado.Nome.Should().Be(usuarioDb.Pessoa.Nome);
            resultado.NomeSocial.Should().Be(usuarioDb.Pessoa.NomeSocial);
            resultado.Email.Should().Be(usuarioDb.Email);
            resultado.Cpf.Should().Be(usuarioDb.Documento.Numero);

            _mocker.GetMock<IRepositorioUsuario>().Verify(r => r.ObterPorLogin(login, true), Times.Once);
        }
    }
}
