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

namespace SME.Acessos.Aplicacao
{
    public class ServicoUsuarios : IServicoUsuarios
    {
        private readonly IRepositorioUsuario repositorioUsuarioCoreSSO;
        private readonly IRepositorioPessoa repositorioPessoaCoreSSO;
        private readonly IRepositorioDadosUsuario repositorioDadosUsuario;
        private readonly IMapper mapper;
        private readonly IServicoEmail servicoEmail;
        private readonly IRepositorioUsuarioValidacaoToken repositorioUsuarioValidacaoToken;
        private readonly IRepositorioSistemaAcao repositorioSistemaAcao;
        private readonly IRepositorioPessoaDocumento repositorioPessoaDocumento;

        public ServicoUsuarios(IRepositorioUsuario repositorioUsuarioCoreSSO, IMapper mapper,IRepositorioPessoa repositorioPessoaCoreSSO,
            IServicoEmail servicoEmail,IRepositorioDadosUsuario repositorioDadosUsuario,IRepositorioUsuarioValidacaoToken repositorioUsuarioValidacaoToken,
            IRepositorioSistemaAcao repositorioSistemaAcao,IRepositorioPessoaDocumento repositorioPessoaDocumento)
        {
            this.repositorioUsuarioCoreSSO = repositorioUsuarioCoreSSO ?? throw new ArgumentNullException(nameof(repositorioUsuarioCoreSSO));
            this.repositorioPessoaCoreSSO = repositorioPessoaCoreSSO ?? throw new ArgumentNullException(nameof(repositorioPessoaCoreSSO));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.servicoEmail = servicoEmail ?? throw new ArgumentNullException(nameof(servicoEmail));
            this.repositorioUsuarioValidacaoToken = repositorioUsuarioValidacaoToken ?? throw new ArgumentNullException(nameof(repositorioUsuarioValidacaoToken));
            this.repositorioDadosUsuario = repositorioDadosUsuario ?? throw new ArgumentNullException(nameof(repositorioDadosUsuario));
            this.repositorioSistemaAcao = repositorioSistemaAcao ?? throw new ArgumentNullException(nameof(repositorioSistemaAcao));
            this.repositorioPessoaDocumento = repositorioPessoaDocumento ?? throw new ArgumentNullException(nameof(repositorioPessoaDocumento));
        }

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
            return await repositorioUsuarioCoreSSO.UsuarioCadastradoCoreSSO(login);
        }

        public async Task<bool> Cadastrar(UsuarioDTO usuarioDto)
        {
            try
            {
                var pessoa = await repositorioPessoaCoreSSO.InserirPessoaCustomizado(usuarioDto.Nome);
                if (!pessoa.HasValue)
                    return false;
                
                await repositorioPessoaDocumento.InserirPessoaDocumentoCustomizado(usuarioDto.Login, pessoa.Value, new Guid(ConstantesCoreSSO.TIPO_DOCUMENTACAO_CPF));
                
                await repositorioUsuarioCoreSSO.InserirUsuarioCustomizado(usuarioDto.Login, usuarioDto.Email, 
                    CriptografiaExtensions.CriptografarSenhaTripleDES(usuarioDto.Senha),pessoa.Value,
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
           var usuario = await repositorioUsuarioCoreSSO.ObterPorLogin(login);
           
           var senhaAtualCorreta = await repositorioUsuarioCoreSSO.ValidarSenhaAtual(usuario.Id, CriptografiaExtensions.CriptografarSenha(alterarSenhaUsuarioDto.SenhaAtual,TipoCriptografia.TripleDES));
           if (!senhaAtualCorreta)
               return false;
           
           await AlterarSenhaRegistrarHistorico(alterarSenhaUsuarioDto.SenhaNova, usuario.Id);
           return true;
        }

        private async Task AlterarSenhaRegistrarHistorico(string senhaNova, Guid usuarioId)
        {
            await repositorioUsuarioCoreSSO.AlterarSenha(usuarioId, CriptografiaExtensions.CriptografarSenha(senhaNova, TipoCriptografia.TripleDES));
            await repositorioUsuarioCoreSSO.InserirHistoricoSenha(usuarioId,senhaNova, TipoCriptografia.TripleDES);
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
            var usuario = await ValidarLogin(login);
           
            await repositorioUsuarioCoreSSO.AlterarEmail(usuario.Id, alterarEmailUsuarioDto.Email);
            return true;
        }

        public async Task<string> SolicitarRecuperacaoSenha(string login, long sistemaId)
        {
            var sistemaAcao = await ObterSistemaAcaoPorAcaoESistema(sistemaId);

            var usuarioCore = await ValidarLogin(login);

            var token = await ObterOuCriarMovimentacaoTokenUsuario(login, sistemaId, sistemaAcao, usuarioCore);
            
            await EnviarEmailRecuperacaoSenha(usuarioCore, token, sistemaAcao, login);
            
            return usuarioCore.Email;
        }
        
        public async Task<bool> EnviarEmailValidacaoCadastro(string login, long sistemaId)
        {
            var sistemaAcao = await ObterSistemaAcaoPorAcaoESistema(sistemaId, TipoAcao.ValidacaoEmail);

            var usuarioCore = await ValidarLogin(login);

            var token = await ObterOuCriarMovimentacaoTokenUsuario(login, sistemaId, sistemaAcao, usuarioCore, TipoAcao.ValidacaoEmail);

            await EnviarEmailParaValidacaoEmail(usuarioCore, token, sistemaAcao, login);
            
            return true;
        }

        private async Task<Guid> ObterOuCriarMovimentacaoTokenUsuario(string login, long sistemaId, SistemaAcao? sistemaRecuperacao, Usuario? usuarioCore, TipoAcao tipoAcao = TipoAcao.RecuperacaoSenha)
        {
            var usuarioRecuperacaoSenha = await repositorioUsuarioValidacaoToken.ObterUsuarioPorLoginSistemaTipoAcao(login, sistemaRecuperacao.CodigoSistema, tipoAcao);
            
            if (usuarioRecuperacaoSenha.EhNulo())
                usuarioRecuperacaoSenha = new UsuarioValidacaoToken() { Login = usuarioCore.Login, CodigoSistema = sistemaId, TipoAcao = tipoAcao};
            
            usuarioRecuperacaoSenha.IniciarMovimentacaoTokenUsuario(usuarioCore.Email);
            await repositorioUsuarioValidacaoToken.Salvar(usuarioRecuperacaoSenha);
            
            return usuarioRecuperacaoSenha.Token.Value;
        }

        private async Task<SistemaAcao?> ObterSistemaAcaoPorAcaoESistema(long sistemaId, TipoAcao tipoAcao = TipoAcao.RecuperacaoSenha)
        {
            var sistemaRecuperacao = await repositorioSistemaAcao.ObterSistemaAcaoPorAcaoESistema(sistemaId, tipoAcao);
            
            if (sistemaRecuperacao.EhNulo())
                throw new NegocioException(MensagemNegocio.O_SISTEMA_INFORMADO_NAO_FOI_IDENTIFICADO);
            
            return sistemaRecuperacao;
        }

        private async Task<Usuario?> ValidarLogin(string login)
        {
            var usuarioCore = await repositorioUsuarioCoreSSO.ObterPorLogin(login);

            if (usuarioCore == null)
                throw new NegocioException(MensagemNegocio.USUARIO_NAO_ENCONTRADO);
            return usuarioCore;
        }

        private async Task EnviarEmailRecuperacaoSenha(Usuario usuario, Guid token, SistemaAcao sistema, string login)
        {
            string caminho = $"{Directory.GetCurrentDirectory()}/wwwroot/ModelosEmail/RecuperacaoSenha.txt";
            var textoArquivo = File.ReadAllText(caminho);
            var textoEmail = textoArquivo
                .Replace("#NOME", usuario.Pessoa.Nome)
                .Replace("#RF", login)
                .Replace("#LINK", string.Format(sistema.Endereco,token));

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
                case ConstantesAcessos.SISTEMA_CONECTA :
                    return TratarTextoAssuntoConectaFormacao(sistema.NomeSistema, usuario.Pessoa.Nome, sistema.Endereco, token);
            }
            return default;
        }

        private ConteudoEAssuntoEmailDTO TratarTextoAssuntoConectaFormacao(string nomeSistema, string nomeUsuario, string endereco, Guid token)
        {
            var caminho = $"{Directory.GetCurrentDirectory()}/wwwroot/ModelosEmail/ValidacaoEmail_Conecta.txt";
            
            var textoArquivo = File.ReadAllText(caminho);
            
            var textoEmail = textoArquivo
                .Replace("#NOME", nomeUsuario)
                .Replace("#SISTEMA", nomeSistema)
                .Replace("#LINK", string.Format(endereco,token));
            
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
            return usuarioValidacaoToken.NaoEhNulo() ? usuarioValidacaoToken.Login : string.Empty;
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
                usuarioRecuperacaoSenha.ValidarSenha(alterarSenha.Senha);
            }
            catch (NegocioException e)
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
            var usuarioCore = await ValidarLogin(login);
            
            await AlterarSenhaRegistrarHistorico(senha, usuarioCore.Id);
            
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
    }
}
