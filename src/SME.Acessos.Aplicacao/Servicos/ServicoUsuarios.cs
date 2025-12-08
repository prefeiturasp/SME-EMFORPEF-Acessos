using AutoMapper;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Enumerados;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Enumeradores;
using SME.Acessos.Infra.Dominio.Extensions;
using SME.Acessos.Infra.Dominio.Extensoes;
using SME.Acessos.Infra.Servicos.Eol;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoUsuarios(IRepositorioUsuario repositorioUsuarioCoreSSO, IMapper mapper, IRepositorioPessoa repositorioPessoaCoreSSO,
        IServicoEmail servicoEmail, IRepositorioDadosUsuario repositorioDadosUsuario, IRepositorioUsuarioValidacaoToken repositorioUsuarioValidacaoToken,
        IRepositorioSistemaAcao repositorioSistemaAcao, IRepositorioPessoaDocumento repositorioPessoaDocumento, IServicoEol servicoEol) : IServicoUsuarios
    {
        private readonly IServicoEmail servicoEmail = servicoEmail ?? throw new ArgumentNullException(nameof(servicoEmail));

        public async Task<IList<DadosUsuarioDTO>> ObterTodosUsuarios()
        {
            var usuarios = await repositorioUsuarioCoreSSO.ObterTodos();
            return mapper.Map<IList<DadosUsuarioDTO>>(usuarios);
        }

        public async Task<DadosUsuarioDTO> ObterUsuarioPorId(Guid id)
            => mapper.Map<DadosUsuarioDTO>(await repositorioUsuarioCoreSSO.ObterPorId(id));

        public async Task<DadosUsuarioDTO> ObterUsuarioPorLogin(string login)
            => mapper.Map<DadosUsuarioDTO>(await repositorioUsuarioCoreSSO.ObterPorLogin(login));

        public async Task<bool> ExisteUsuarioCadastradoCoreSSO(string login)
        {
            var existeUsuarioCoresso = await repositorioUsuarioCoreSSO.UsuarioCadastradoCoreSSO(login);
            if (existeUsuarioCoresso)
                return true;

            var loginUsuarioPorCpf = await repositorioUsuarioCoreSSO.ObterLoginUsuarioPorCpfCadastradoCoreSSO(login);
            if (loginUsuarioPorCpf.IsNull())
                return false;

            var RfEstaAtivoNoEol = await servicoEol.VerificarFuncionarioAtivo(loginUsuarioPorCpf);
            return RfEstaAtivoNoEol;
        }

        public async Task<bool> Cadastrar(UsuarioDTO usuarioDto)
        {
            try
            {
                var pessoaId = await repositorioPessoaCoreSSO.InserirPessoaCustomizado(usuarioDto.Nome);

                await repositorioPessoaDocumento.InserirPessoaDocumentoCustomizado(usuarioDto.Login, pessoaId, new Guid(ConstantesCoreSSO.TIPO_DOCUMENTACAO_CPF));

                await repositorioUsuarioCoreSSO.InserirUsuarioCustomizado(usuarioDto.Login, usuarioDto.Email,
                    CriptografiaExtensions.CriptografarSenhaTripleDES(usuarioDto.Senha), pessoaId,
                    new Guid(ConstantesCoreSSO.ENTIDADE_SME));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AlterarSenha(string login, AlterarSenhaUsuarioDTO alterarSenhaUsuarioDto)
        {
            var usuario = await ValidarLogin(login);

            var senhaAtualCorreta = await repositorioUsuarioCoreSSO.ValidarSenhaAtual(usuario.Id, CriptografiaExtensions.CriptografarSenha(alterarSenhaUsuarioDto.SenhaAtual, TipoCriptografia.TripleDES));
            if (!senhaAtualCorreta)
                return false;

            await AlterarSenhaRegistrarHistorico(alterarSenhaUsuarioDto.SenhaNova, usuario.Id);
            return true;
        }

        private async Task AlterarSenhaRegistrarHistorico(string senhaNova, Guid usuarioId)
        {
            var criptografia = TipoCriptografia.TripleDES;
            var senhaCriptografada = CriptografiaExtensions.CriptografarSenha(senhaNova, criptografia);

            await repositorioUsuarioCoreSSO.AlterarSenha(usuarioId, senhaCriptografada, criptografia);
            await repositorioUsuarioCoreSSO.InserirHistoricoSenha(usuarioId, senhaCriptografada, criptografia);
        }

        public async Task<DadosUsuarioDTO?> ObterMeusDados(string login)
        {
            var usuarios = await repositorioDadosUsuario.ObterMeusDados(login);
            if (usuarios == null)
                throw new NegocioException(MensagemNegocio.USUARIO_NAO_ENCONTRADO);

            return mapper.Map<DadosUsuarioDTO>(usuarios);
        }

        public async Task<bool> AlterarEmail(string login, AlterarEmailUsuarioDTO alterarEmailUsuarioDto)
        {
            if (!alterarEmailUsuarioDto.Email.EmailEhValido())
                throw new NegocioException(MensagemNegocio.USUARIO_COM_EMAIL_INVALIDO);

            var usuario = await ValidarLogin(login);

            await repositorioUsuarioCoreSSO.AlterarEmail(usuario.Id, alterarEmailUsuarioDto.Email);
            return true;
        }

        public async Task<string> SolicitarRecuperacaoSenha(string login, long sistemaId)
        {
            var sistemaAcao = await ObterSistemaAcaoPorAcaoESistema(sistemaId);

            var usuarioCore = await ValidarLogin(login, true);

            var token = await ObterOuCriarMovimentacaoTokenUsuario(login, sistemaId, sistemaAcao, usuarioCore);

            await EnviarEmailRecuperacaoSenha(usuarioCore, token, sistemaAcao, login);

            return usuarioCore.Email;
        }

        public async Task<bool> EnviarEmailValidacaoCadastro(string login, long sistemaId)
        {
            var sistemaAcao = await ObterSistemaAcaoPorAcaoESistema(sistemaId, TipoAcao.ValidacaoEmail);

            var usuarioCore = await ValidarLogin(login, true);

            var token = await ObterOuCriarMovimentacaoTokenUsuario(login, sistemaId, sistemaAcao, usuarioCore, TipoAcao.ValidacaoEmail);

            await EnviarEmailParaValidacaoEmail(usuarioCore, token, sistemaAcao, login);

            return true;
        }

        private async Task<Guid> ObterOuCriarMovimentacaoTokenUsuario(string login, long sistemaId, SistemaAcao sistemaRecuperacao, Usuario usuarioCore, TipoAcao tipoAcao = TipoAcao.RecuperacaoSenha)
        {
            var usuarioRecuperacaoSenha = await repositorioUsuarioValidacaoToken.ObterUsuarioPorLoginSistemaTipoAcao(login, sistemaRecuperacao.CodigoSistema, tipoAcao);

            usuarioRecuperacaoSenha ??= new UsuarioValidacaoToken() { Login = usuarioCore.Login, CodigoSistema = sistemaId, TipoAcao = tipoAcao };

            usuarioRecuperacaoSenha.IniciarMovimentacaoTokenUsuario(usuarioCore.Email);
            await repositorioUsuarioValidacaoToken.Salvar(usuarioRecuperacaoSenha);

            return usuarioRecuperacaoSenha.Token.Value;
        }

        private async Task<SistemaAcao> ObterSistemaAcaoPorAcaoESistema(long sistemaId, TipoAcao tipoAcao = TipoAcao.RecuperacaoSenha)
        {
            var sistemaRecuperacao = await repositorioSistemaAcao.ObterSistemaAcaoPorAcaoESistema(sistemaId, tipoAcao);

            if (sistemaRecuperacao is null)
                throw new NegocioException(MensagemNegocio.O_SISTEMA_INFORMADO_NAO_FOI_IDENTIFICADO);

            return sistemaRecuperacao;
        }

        private async Task<Usuario> ValidarLogin(string login, bool validarEmail = false)
        {
            var usuarioCore = await repositorioUsuarioCoreSSO.ObterPorLogin(login) ??
                throw new NegocioException(MensagemNegocio.USUARIO_NAO_ENCONTRADO);

            if (validarEmail)
            {

                if (usuarioCore.Email.EhNulo())
                    throw new NegocioException(MensagemNegocio.USUARIO_NAO_POSSUI_EMAIL);
                if (!usuarioCore.Email.EmailEhValido())
                    throw new NegocioException(MensagemNegocio.USUARIO_COM_EMAIL_INVALIDO);
            }
            return usuarioCore;
        }

        private async Task EnviarEmailRecuperacaoSenha(Usuario usuario, Guid token, SistemaAcao sistema, string login)
        {
            string caminho = $"{Directory.GetCurrentDirectory()}/wwwroot/ModelosEmail/RecuperacaoSenha.txt";
            var textoArquivo = File.ReadAllText(caminho);
            var textoEmail = textoArquivo
                .Replace("#NOME", usuario.Pessoa.Nome)
                .Replace("#RF", login)
                .Replace("#LINK", string.Format(sistema.Endereco, token));

            await servicoEmail.Enviar(usuario.Pessoa.Nome, usuario.Email, $"Recuperação de senha do(a) {sistema.NomeSistema}", textoEmail, sistema.CodigoSistema);
        }

        private async Task EnviarEmailParaValidacaoEmail(Usuario usuario, Guid tokenRecuperacaoSenha, SistemaAcao sistema, string login)
        {
            var conteudoEAssuntoEmail = ObterTextoEmailPorSistema(usuario, tokenRecuperacaoSenha, sistema);

            await servicoEmail.Enviar(usuario.Pessoa.Nome, usuario.Email, conteudoEAssuntoEmail.Assunto, conteudoEAssuntoEmail.Conteudo, sistema.CodigoSistema);
        }

        private ConteudoEAssuntoEmailDTO ObterTextoEmailPorSistema(Usuario usuario, Guid token, SistemaAcao sistema)
        {
            switch (sistema.CodigoSistema)
            {
                case ConstantesAcessos.SISTEMA_CONECTA:
                    return TratarTextoAssuntoConectaFormacao(sistema.NomeSistema, usuario.Pessoa.Nome, sistema.Endereco, token);
            }
            return default!;
        }

        private static ConteudoEAssuntoEmailDTO TratarTextoAssuntoConectaFormacao(string nomeSistema, string nomeUsuario, string endereco, Guid token)
        {
            var caminho = $"{Directory.GetCurrentDirectory()}/wwwroot/ModelosEmail/ValidacaoEmail_Conecta.txt";

            var textoArquivo = File.ReadAllText(caminho);

            var textoEmail = textoArquivo
                .Replace("#NOME", nomeUsuario)
                .Replace("#SISTEMA", nomeSistema)
                .Replace("#LINK", string.Format(endereco, token));

            return new ConteudoEAssuntoEmailDTO()
            {
                Assunto = $"Validação do e-mail do {nomeSistema}",
                Conteudo = textoEmail
            };
        }

        public async Task<bool> ValidarTokenSenha(Guid token, long sistemaId, TipoAcao tipoAcao)
        {
            var usuarioValidacaoToken = await repositorioUsuarioValidacaoToken.ObterUsuarioPorTokenSistemaTipoAcao(token, sistemaId, tipoAcao);
            return usuarioValidacaoToken?.TokenValido() ?? false;
        }

        public async Task<string> ValidarTokenEmail(Guid token, long sistemaId, TipoAcao tipoAcao)
        {
            var usuarioValidacaoToken = await repositorioUsuarioValidacaoToken.ObterUsuarioPorTokenSistemaTipoAcao(token, sistemaId, tipoAcao);
            return usuarioValidacaoToken.NaoEhNulo() && usuarioValidacaoToken.TokenValido() ? usuarioValidacaoToken.Login : string.Empty;
        }

        public async Task<RetornoAlteracaoSenhaDto> AlterarSenhaPorToken(long sistemaId, AlterarSenhaPorTokenDto alterarSenha)
        {
            var usuarioRecuperacaoSenha = await repositorioUsuarioValidacaoToken.ObterUsuarioPorTokenSistemaTipoAcao(new Guid(alterarSenha.Token), sistemaId);

            if (usuarioRecuperacaoSenha == null)
                return new RetornoAlteracaoSenhaDto(AlterarSenhaStatus.NaoEncontrado);

            if (!usuarioRecuperacaoSenha.TokenValido())
                return new RetornoAlteracaoSenhaDto(AlterarSenhaStatus.TokenExpirado);

            try
            {
                UsuarioValidacaoToken.ValidarSenha(alterarSenha.Senha);
            }
            catch
            {
                return new RetornoAlteracaoSenhaDto(AlterarSenhaStatus.ForaPadrao);
            }

            var retornoAlteracaoSenha = await AlterarSenha(usuarioRecuperacaoSenha.Login, alterarSenha.Senha);

            if (retornoAlteracaoSenha == AlterarSenhaStatus.OK)
            {
                usuarioRecuperacaoSenha.FinalizarRecuperacaoSenha();
                await repositorioUsuarioValidacaoToken.Salvar(usuarioRecuperacaoSenha);
            }

            return new RetornoAlteracaoSenhaDto(retornoAlteracaoSenha, usuarioRecuperacaoSenha.Login);
        }

        private async Task<AlterarSenhaStatus> AlterarSenha(string login, string senha)
        {
            var usuario = await ValidarLogin(login);

            await AlterarSenhaRegistrarHistorico(senha, usuario.Id);

            return AlterarSenhaStatus.OK;
        }

        public async Task<IEnumerable<ResponsavelDTO>> ObterUsuariosComPerfisResponsavel(Guid[] perfis, long sistemaId)
        {
            var rfsResponsaveis = await repositorioUsuarioCoreSSO.ObterUsuariosComPerfisResponsavel(perfis, sistemaId);

            if (rfsResponsaveis.NaoPossuiElementos())
                throw new NegocioException(MensagemNegocio.USUARIO_NAO_ENCONTRADO);

            return rfsResponsaveis.Select(s => new ResponsavelDTO()
            {
                Login = s.Login,
                Nome = s.Nome
            });
        }

        public async Task<bool> AlterarNome(string login, string nome)
        {
            if (nome.IsNull())
                throw new NegocioException(MensagemNegocio.USUARIO_NOME_NAO_PREENCHIDO);

            var usuario = await ValidarLogin(login);

            await repositorioUsuarioCoreSSO.AlterarNome(usuario.Id, nome);
            return true;
        }

        public async Task<bool> Alterar(string login, UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO.Email.EhNulo())
                throw new NegocioException(MensagemNegocio.USUARIO_NAO_POSSUI_EMAIL);
            if (!usuarioDTO.Email.EmailEhValido())
                throw new NegocioException(MensagemNegocio.USUARIO_COM_EMAIL_INVALIDO);
            if (usuarioDTO.Nome.IsNull())
                throw new NegocioException(MensagemNegocio.USUARIO_NOME_NAO_PREENCHIDO);

            var usuario = await ValidarLogin(login);

            var criptografia = TipoCriptografia.TripleDES;
            var senhaCriptografada = string.IsNullOrEmpty(usuarioDTO.Senha) ? usuario.Senha : CriptografiaExtensions.CriptografarSenha(usuarioDTO.Senha, criptografia);

            await repositorioUsuarioCoreSSO.AlterarNome(usuario.Id, usuarioDTO.Nome);
            await repositorioUsuarioCoreSSO.AlterarUsuario(usuario.Id, senhaCriptografada, criptografia, usuarioDTO.Email);

            if (!string.IsNullOrEmpty(usuarioDTO.Senha))
                await repositorioUsuarioCoreSSO.InserirHistoricoSenha(usuario.Id, senhaCriptografada, criptografia);

            return true;
        }

        public async Task<bool> Inativar(string login)
        {
            var usuario = await ValidarLogin(login);

            await repositorioUsuarioCoreSSO.Inativar(usuario.Id);
            return true;
        }
    }
}