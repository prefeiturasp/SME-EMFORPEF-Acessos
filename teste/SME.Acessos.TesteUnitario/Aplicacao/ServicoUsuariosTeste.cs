using Bogus;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Enumerados;
using SME.Acessos.Aplicacao.Servicos;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Enumeradores;
using SME.Acessos.Infra.Servicos.Eol;

namespace SME.Acessos.TesteUnitario.Aplicacao
{
    public class ServicoUsuariosTeste
    {
        private readonly AutoMocker _mocker;
        private readonly ServicoUsuarios _sut;
        private readonly Faker _faker;

        public ServicoUsuariosTeste()
        {
            _mocker = new AutoMocker();
            _sut = _mocker.CreateInstance<ServicoUsuarios>();
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task DadoLoginCadastradoNoCoreSSO_QuandoExisteUsuarioCadastradoCoreSSO_EntaoRetornaVerdadeiro()
        {
            // Arrange
            var login = _faker.Internet.UserName();

            _mocker.GetMock<IRepositorioUsuario>()
                   .Setup(r => r.UsuarioCadastradoCoreSSO(login))
                   .ReturnsAsync(true);

            // Act
            var resultado = await _sut.ExisteUsuarioCadastradoCoreSSO(login);

            // Assert
            resultado.Should().BeTrue();
            _mocker.GetMock<IRepositorioUsuario>().Verify(r => r.UsuarioCadastradoCoreSSO(login), Times.Once);
            _mocker.GetMock<IServicoEol>().Verify(s => s.VerificarFuncionarioAtivo(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task DadoLoginNaoCadastradoMasAtivoNoEol_QuandoExisteUsuarioCadastradoCoreSSO_EntaoRetornaVerdadeiro()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var loginPorCpf = _faker.Random.Replace("###########");

            _mocker.GetMock<IRepositorioUsuario>()
                   .Setup(r => r.UsuarioCadastradoCoreSSO(login))
                   .ReturnsAsync(false);

            _mocker.GetMock<IRepositorioUsuario>()
                   .Setup(r => r.ObterLoginUsuarioPorCpfCadastradoCoreSSO(login))
                   .ReturnsAsync(loginPorCpf);

            _mocker.GetMock<IServicoEol>()
                   .Setup(s => s.VerificarFuncionarioAtivo(loginPorCpf))
                   .ReturnsAsync(true);

            // Act
            var resultado = await _sut.ExisteUsuarioCadastradoCoreSSO(login);

            // Assert
            resultado.Should().BeTrue();
            _mocker.GetMock<IServicoEol>().Verify(s => s.VerificarFuncionarioAtivo(loginPorCpf), Times.Once);
        }

        [Fact]
        public async Task DadoUsuarioDtoValido_QuandoCadastrar_EntaoRetornaVerdadeiroEExecutaInsercoes()
        {
            // Arrange
            var usuarioDto = new Faker<UsuarioDTO>("pt_BR")
                .RuleFor(u => u.Login, f => f.Internet.UserName())
                .RuleFor(u => u.Nome, f => f.Person.FullName)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Senha, f => f.Internet.Password())
                .Generate();

            var pessoaId = Guid.NewGuid();

            _mocker.GetMock<IRepositorioPessoa>()
                   .Setup(r => r.InserirPessoaCustomizado(usuarioDto.Nome))
                   .ReturnsAsync(pessoaId);

            // Act
            var resultado = await _sut.Cadastrar(usuarioDto);

            // Assert
            resultado.Should().BeTrue();

            _mocker.GetMock<IRepositorioPessoaDocumento>()
                   .Verify(r => r.InserirPessoaDocumentoCustomizado(
                       usuarioDto.Login,
                       pessoaId,
                       new Guid(ConstantesCoreSSO.TIPO_DOCUMENTACAO_CPF)), Times.Once);

            _mocker.GetMock<IRepositorioUsuario>()
                   .Verify(r => r.InserirUsuarioCustomizado(
                       usuarioDto.Login,
                       usuarioDto.Email,
                       It.IsAny<string>(),
                       pessoaId,
                       new Guid(ConstantesCoreSSO.ENTIDADE_SME)), Times.Once);
        }

        [Fact]
        public async Task DadoLoginESenhaCorreta_QuandoAlterarSenha_EntaoRetornaVerdadeiroERegistraHistorico()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var alterarSenhaDto = new AlterarSenhaUsuarioDTO
            {
                SenhaAtual = "Senha@Atual123",
                SenhaNova = "Nova@Senha123"
            };

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Login = login,
                Email = _faker.Internet.Email(),
                Senha = "Hash"
            };

            _mocker.GetMock<IRepositorioUsuario>()
                   .Setup(r => r.ObterPorLogin(login, false))
                   .ReturnsAsync(usuario);

            _mocker.GetMock<IRepositorioUsuario>()
                   .Setup(r => r.ValidarSenhaAtual(usuario.Id, It.IsAny<string>()))
                   .ReturnsAsync(true);

            // Act
            var resultado = await _sut.AlterarSenha(login, alterarSenhaDto);

            // Assert
            resultado.Should().BeTrue();

            _mocker.GetMock<IRepositorioUsuario>()
                   .Verify(r => r.AlterarSenha(usuario.Id, It.IsAny<string>(), TipoCriptografia.TripleDES), Times.Once);

            _mocker.GetMock<IRepositorioUsuario>()
                   .Verify(r => r.InserirHistoricoSenha(usuario.Id, It.IsAny<string>(), TipoCriptografia.TripleDES), Times.Once);
        }

        [Fact]
        public async Task DadoTokenExpirado_QuandoAlterarSenhaPorToken_EntaoRetornaStatusTokenExpirado()
        {
            // Arrange
            var alterarSenhaDto = new AlterarSenhaPorTokenDto
            {
                Token = Guid.NewGuid().ToString(),
                Senha = "Nova@Senha123"
            };
            var sistemaId = _faker.Random.Long(1, 100);

            var usuarioValidacaoToken = new UsuarioValidacaoToken
            {
                Token = Guid.NewGuid(),
                Expiracao = DateTime.UtcNow.AddHours(-10),
                Login = _faker.Internet.UserName()
            };

            _mocker.GetMock<IRepositorioUsuarioValidacaoToken>()
                   .Setup(r => r.ObterUsuarioPorTokenSistemaTipoAcao(It.IsAny<Guid>(), sistemaId, TipoAcao.RecuperacaoSenha))
                   .ReturnsAsync(usuarioValidacaoToken);

            // Act
            var resultado = await _sut.AlterarSenhaPorToken(sistemaId, alterarSenhaDto);

            // Assert
            resultado.Status.Should().Be(AlterarSenhaStatus.TokenExpirado);
            _mocker.GetMock<IRepositorioUsuario>().Verify(r => r.AlterarSenha(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<TipoCriptografia>()), Times.Never);
        }

        [Fact]
        public async Task DadoEmailInvalido_QuandoAlterarEmail_EntaoLancaExcecao()
        {
            // Arrange
            var login = _faker.Internet.UserName();
            var alterarEmailDto = new AlterarEmailUsuarioDTO { Email = "email_invalido" };

            // Act
            Func<Task> acao = async () => await _sut.AlterarEmail(login, alterarEmailDto);

            // Assert
            await acao.Should().ThrowAsync<Exception>()
                      .WithMessage(MensagemNegocio.USUARIO_COM_EMAIL_INVALIDO);
        }
    }
}
