using AutoMapper;
using Bogus;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Servicos;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.TesteUnitario.Aplicacao
{
    public class ServicoEmailTeste
    {
        private readonly Mock<IRepositorioConfiguracaoEmail> _repositorioConfiguracaoEmailMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ServicoEmail _sut;

        public ServicoEmailTeste()
        {
            var mocker = new AutoMocker();
            _repositorioConfiguracaoEmailMock = mocker.GetMock<IRepositorioConfiguracaoEmail>();
            _mapperMock = mocker.GetMock<IMapper>();
            _sut = mocker.CreateInstance<ServicoEmail>();
        }

        [Fact]
        public async Task DadoSistemaIdValido_QuandoObterConfiguracaoEmail_EntaoRetornaConfiguracaoEmailDto()
        {
            // Arrange
            var faker = new Faker("pt_BR");
            var sistemaId = faker.Random.Long(1, 1000);

            var configuracaoEmail = new ConfiguracaoEmail
            {
                Email = faker.Internet.Email(),
                Nome = faker.Person.FullName,
                Smtp = faker.Internet.DomainName(),
                Usuario = faker.Internet.UserName(),
                Senha = faker.Internet.Password(),
                Porta = faker.Random.Int(1000, 9999),
                TLS = faker.Random.Bool()
            };

            var configuracaoEmailDto = new ConfiguracaoEmailDTO
            {
                Email = configuracaoEmail.Email,
                Nome = configuracaoEmail.Nome,
                Smtp = configuracaoEmail.Smtp,
                Usuario = configuracaoEmail.Usuario,
                Senha = configuracaoEmail.Senha,
                Porta = configuracaoEmail.Porta,
                TLS = configuracaoEmail.TLS
            };

            _repositorioConfiguracaoEmailMock
                .Setup(r => r.ObterConfiguracaoEmailPorSistema(sistemaId))
                .ReturnsAsync(configuracaoEmail);

            _mapperMock
                .Setup(m => m.Map<ConfiguracaoEmailDTO>(configuracaoEmail))
                .Returns(configuracaoEmailDto);

            // Act
            var resultado = await _sut.ObterConfiguracaoEmail(sistemaId);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().BeEquivalentTo(configuracaoEmailDto);

            _repositorioConfiguracaoEmailMock.Verify(r => r.ObterConfiguracaoEmailPorSistema(sistemaId), Times.Once);
            _mapperMock.Verify(m => m.Map<ConfiguracaoEmailDTO>(configuracaoEmail), Times.Once);
        }

        [Fact]
        public async Task DadoSistemaIdInvalido_QuandoObterConfiguracaoEmail_EntaoLancaNegocioException()
        {
            // Arrange
            var faker = new Faker();
            var sistemaId = faker.Random.Long(1, 1000);

            _repositorioConfiguracaoEmailMock
                .Setup(r => r.ObterConfiguracaoEmailPorSistema(sistemaId))
                .ReturnsAsync((ConfiguracaoEmail)null!);

            // Act
            var acao = async () => await _sut.ObterConfiguracaoEmail(sistemaId);

            // Assert
            await acao.Should().ThrowAsync<NegocioException>()
                .WithMessage(MensagemNegocio.NAO_LOCALIZADO_CONFIGURACAO_EMAIL);

            _repositorioConfiguracaoEmailMock.Verify(r => r.ObterConfiguracaoEmailPorSistema(sistemaId), Times.Once);
            _mapperMock.Verify(m => m.Map<ConfiguracaoEmailDTO>(It.IsAny<ConfiguracaoEmail>()), Times.Never);
        }
    }
}
