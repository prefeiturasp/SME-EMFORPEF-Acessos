using AutoMapper;
using Bogus;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Servicos;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.TesteUnitario.Aplicacao
{
    public class ServicoGruposTeste
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepositorioGrupo> _repositorioGrupoMock;
        private readonly ServicoGrupos _sut;

        public ServicoGruposTeste()
        {
            var mocker = new AutoMocker();
            _mapperMock = mocker.GetMock<IMapper>();
            _repositorioGrupoMock = mocker.GetMock<IRepositorioGrupo>();
            _sut = mocker.CreateInstance<ServicoGrupos>();
        }

        [Fact]
        public async Task DadoSistemaIdValido_QuandoObterGruposPorSistemaId_EntaoRetornaListaGruposDto()
        {
            // Arrange
            var faker = new Faker("pt_BR");
            var sistemaId = faker.Random.Long(1, 100);

            var grupos = new Faker<Grupo>("pt_BR")
                .RuleFor(g => g.Nome, f => f.Company.CompanyName())
                .RuleFor(g => g.VisaoId, f => f.Random.Int(1, 10))
                .Generate(3);

            var gruposDto = grupos.Select(g => new GrupoDTO
            {
                Nome = g.Nome,
                VisaoId = g.VisaoId
            }).ToList();

            _repositorioGrupoMock
                .Setup(r => r.ObterPorSistemaId(sistemaId))
                .ReturnsAsync(grupos);

            _mapperMock
                .Setup(m => m.Map<List<GrupoDTO>>(grupos))
                .Returns(gruposDto);

            // Act
            var resultado = await _sut.ObterGruposPorSistemaId(sistemaId);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().BeEquivalentTo(gruposDto);
            resultado.Should().HaveCount(3);

            _repositorioGrupoMock.Verify(r => r.ObterPorSistemaId(sistemaId), Times.Once);
            _mapperMock.Verify(m => m.Map<List<GrupoDTO>>(grupos), Times.Once);
        }

        [Fact]
        public async Task DadoSistemaIdSemGrupos_QuandoObterGruposPorSistemaId_EntaoRetornaListaVazia()
        {
            // Arrange
            var faker = new Faker();
            var sistemaId = faker.Random.Long(1, 100);
            var grupos = Enumerable.Empty<Grupo>();
            var gruposDto = new List<GrupoDTO>();

            _repositorioGrupoMock
                .Setup(r => r.ObterPorSistemaId(sistemaId))
                .ReturnsAsync(grupos);

            _mapperMock
                .Setup(m => m.Map<List<GrupoDTO>>(grupos))
                .Returns(gruposDto);

            // Act
            var resultado = await _sut.ObterGruposPorSistemaId(sistemaId);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().BeEmpty();

            _repositorioGrupoMock.Verify(r => r.ObterPorSistemaId(sistemaId), Times.Once);
            _mapperMock.Verify(m => m.Map<List<GrupoDTO>>(grupos), Times.Once);
        }

        [Fact]
        public async Task DadoSistemaIdEGrupoIdValidos_QuandoObterGrupoPorIdSistemaId_EntaoRetornaGrupoDto()
        {
            // Arrange
            var faker = new Faker("pt_BR");
            var sistemaId = faker.Random.Long(1, 100);
            var grupoId = faker.Random.Guid();

            var grupo = new Faker<Grupo>("pt_BR")
                .RuleFor(g => g.Nome, f => f.Company.CompanyName())
                .RuleFor(g => g.VisaoId, f => f.Random.Int(1, 10))
                .Generate();

            var grupoDto = new GrupoDTO
            {
                Id = grupoId,
                Nome = grupo.Nome,
                VisaoId = grupo.VisaoId
            };

            _repositorioGrupoMock
                .Setup(r => r.ObterGrupoPorIdSistemaId(sistemaId, grupoId))
                .ReturnsAsync(grupo);

            _mapperMock
                .Setup(m => m.Map<GrupoDTO>(grupo))
                .Returns(grupoDto);

            // Act
            var resultado = await _sut.ObterGrupoPorIdSistemaId(sistemaId, grupoId);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().BeEquivalentTo(grupoDto);

            _repositorioGrupoMock.Verify(r => r.ObterGrupoPorIdSistemaId(sistemaId, grupoId), Times.Once);
            _mapperMock.Verify(m => m.Map<GrupoDTO>(grupo), Times.Once);
        }

        [Fact]
        public async Task DadoGrupoInexistente_QuandoObterGrupoPorIdSistemaId_EntaoRetornaNulo()
        {
            // Arrange
            var faker = new Faker();
            var sistemaId = faker.Random.Long(1, 100);
            var grupoId = faker.Random.Guid();

            // Act
            var resultado = await _sut.ObterGrupoPorIdSistemaId(sistemaId, grupoId);

            // Assert
            resultado.Should().BeNull();

            _repositorioGrupoMock.Verify(r => r.ObterGrupoPorIdSistemaId(sistemaId, grupoId), Times.Once);
            _mapperMock.Verify(m => m.Map<GrupoDTO>(It.IsAny<Grupo>()), Times.Once);
        }
    }
}
