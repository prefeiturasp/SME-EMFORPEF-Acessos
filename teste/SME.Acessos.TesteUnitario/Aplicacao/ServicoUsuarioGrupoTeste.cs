using Bogus;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using SME.Acessos.Aplicacao.Servicos;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SME.Acessos.TesteUnitario.Aplicacao
{
    public class ServicoUsuarioGrupoTeste
    {
        private readonly Mock<IRepositorioUsuarioGrupo> _repositorioUsuarioGrupoMock;
        private readonly Mock<IRepositorioUsuario> _repositorioUsuarioMock;
        private readonly ServicoUsuarioGrupo _sut;
        private readonly Faker _faker;

        public ServicoUsuarioGrupoTeste()
        {
            var mocker = new AutoMocker();

            _repositorioUsuarioGrupoMock = mocker.GetMock<IRepositorioUsuarioGrupo>();
            _repositorioUsuarioMock = mocker.GetMock<IRepositorioUsuario>();

            _sut = mocker.CreateInstance<ServicoUsuarioGrupo>();
            _faker = new Faker("pt_BR");
        }

        private Usuario GerarUsuarioValido()
        {
            return new Usuario
            {
                Id = _faker.Random.Guid(),
                Login = _faker.Internet.UserName(),
                Email = _faker.Internet.Email(),
                Senha = _faker.Internet.Password()
            };
        }

        [Fact]
        public async Task DadoUsuarioEPerfilValidosJaVinculados_QuandoVincularPerfil_EntaoDeveAtivarVinculoERetornarVerdadeiro()
        {
            // Arrange
            var usuario = GerarUsuarioValido();
            var perfilId = _faker.Random.Guid();

            _repositorioUsuarioMock
                .Setup(r => r.ObterPorLogin(usuario.Login, true))
                .ReturnsAsync(usuario);

            _repositorioUsuarioGrupoMock
                .Setup(r => r.PerfilJaVinculado(usuario.Id, perfilId))
                .ReturnsAsync(true);

            // Act
            var resultado = await _sut.VincularPerfil(usuario.Login, perfilId);

            // Assert
            resultado.Should().BeTrue();

            _repositorioUsuarioMock.Verify(r => r.ObterPorLogin(usuario.Login, true), Times.Once);
            _repositorioUsuarioGrupoMock.Verify(r => r.PerfilJaVinculado(usuario.Id, perfilId), Times.Once);
            _repositorioUsuarioGrupoMock.Verify(r => r.AtivarVinculo(usuario.Id, perfilId), Times.Once);
            _repositorioUsuarioGrupoMock.Verify(r => r.InserirUsuarioGrupoCustomizado(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task DadoUsuarioEPerfilValidosNaoVinculados_QuandoVincularPerfil_EntaoDeveInserirVinculoERetornarVerdadeiro()
        {
            // Arrange
            var usuario = GerarUsuarioValido();
            var perfilId = _faker.Random.Guid();

            _repositorioUsuarioMock
                .Setup(r => r.ObterPorLogin(usuario.Login, true))
                .ReturnsAsync(usuario);

            _repositorioUsuarioGrupoMock
                .Setup(r => r.PerfilJaVinculado(usuario.Id, perfilId))
                .ReturnsAsync(false);

            // Act
            var resultado = await _sut.VincularPerfil(usuario.Login, perfilId);

            // Assert
            resultado.Should().BeTrue();

            _repositorioUsuarioMock.Verify(r => r.ObterPorLogin(usuario.Login, true), Times.Once);
            _repositorioUsuarioGrupoMock.Verify(r => r.PerfilJaVinculado(usuario.Id, perfilId), Times.Once);
            _repositorioUsuarioGrupoMock.Verify(r => r.InserirUsuarioGrupoCustomizado(usuario.Id, perfilId), Times.Once);
            _repositorioUsuarioGrupoMock.Verify(r => r.AtivarVinculo(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task DadoErroDeInfraestrutura_QuandoVincularPerfil_EntaoDeveRetornarFalso()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var perfilId = _faker.Random.Guid();

            _repositorioUsuarioMock
                .Setup(r => r.ObterPorLogin(login, true))
                .ThrowsAsync(new Exception("Falha de conexão com banco de dados"));

            // Act
            var resultado = await _sut.VincularPerfil(login, perfilId);

            // Assert
            resultado.Should().BeFalse();

            _repositorioUsuarioMock.Verify(r => r.ObterPorLogin(login, true), Times.Once);
            _repositorioUsuarioGrupoMock.Verify(r => r.PerfilJaVinculado(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task DadoUsuarioEPerfilValidos_QuandoDesvincularPerfil_EntaoDeveDeletarVinculoERetornarVerdadeiro()
        {
            // Arrange
            var usuario = GerarUsuarioValido();
            var perfilId = _faker.Random.Guid();

            _repositorioUsuarioMock
                .Setup(r => r.ObterPorLogin(usuario.Login, false))
                .ReturnsAsync(usuario);

            _repositorioUsuarioGrupoMock
                .Setup(r => r.DeletarUsuarioGrupoCustomizado(usuario.Id, perfilId))
                .ReturnsAsync(true);

            // Act
            var resultado = await _sut.DesvincularPerfil(usuario.Login, perfilId);

            // Assert
            resultado.Should().BeTrue();

            _repositorioUsuarioMock.Verify(r => r.ObterPorLogin(usuario.Login, false), Times.Once);
            _repositorioUsuarioGrupoMock.Verify(r => r.DeletarUsuarioGrupoCustomizado(usuario.Id, perfilId), Times.Once);
        }

        [Fact]
        public async Task DadoFalhaNaExclusaoDoRepositorio_QuandoDesvincularPerfil_EntaoDeveRetornarFalso()
        {
            // Arrange
            var usuario = GerarUsuarioValido();
            var perfilId = _faker.Random.Guid();

            _repositorioUsuarioMock
                .Setup(r => r.ObterPorLogin(usuario.Login, false))
                .ReturnsAsync(usuario);

            _repositorioUsuarioGrupoMock
                .Setup(r => r.DeletarUsuarioGrupoCustomizado(usuario.Id, perfilId))
                .ReturnsAsync(false);

            // Act
            var resultado = await _sut.DesvincularPerfil(usuario.Login, perfilId);

            // Assert
            resultado.Should().BeFalse();

            _repositorioUsuarioMock.Verify(r => r.ObterPorLogin(usuario.Login, false), Times.Once);
            _repositorioUsuarioGrupoMock.Verify(r => r.DeletarUsuarioGrupoCustomizado(usuario.Id, perfilId), Times.Once);
        }

        [Fact]
        public async Task DadoErroDeInfraestrutura_QuandoDesvincularPerfil_EntaoDeveRetornarFalso()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var perfilId = _faker.Random.Guid();

            _repositorioUsuarioMock
                .Setup(r => r.ObterPorLogin(login, false))
                .ThrowsAsync(new Exception("Falha de conexão com banco de dados"));

            // Act
            var resultado = await _sut.DesvincularPerfil(login, perfilId);

            // Assert
            resultado.Should().BeFalse();

            _repositorioUsuarioMock.Verify(r => r.ObterPorLogin(login, false), Times.Once);
            _repositorioUsuarioGrupoMock.Verify(r => r.DeletarUsuarioGrupoCustomizado(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Never);
        }
    }
}
