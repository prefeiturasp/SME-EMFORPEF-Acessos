using AutoMapper;
using SME.Acesos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Enumerados;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.Aplicacao
{
    public class ServicoUsuarios : IServicoUsuarios
    {
        private readonly IRepositorioUsuario repositorioUsuarioCoreSSO;
        private readonly IRepositorioPessoa repositorioPessoaCoreSSO;
        private readonly IMapper mapper;
        private readonly IServicoEmail servicoEmail;

        public ServicoUsuarios(IRepositorioUsuario repositorioUsuarioCoreSSO, IMapper mapper,IRepositorioPessoa repositorioPessoaCoreSSO,IServicoEmail servicoEmail)
        {
            this.repositorioUsuarioCoreSSO = repositorioUsuarioCoreSSO ?? throw new ArgumentNullException(nameof(repositorioUsuarioCoreSSO));
            this.repositorioPessoaCoreSSO = repositorioPessoaCoreSSO ?? throw new ArgumentNullException(nameof(repositorioPessoaCoreSSO));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.servicoEmail = servicoEmail ?? throw new ArgumentNullException(nameof(servicoEmail));
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

        public async Task<bool> UsuarioCadastradoCoreSSO(string login)
        {
            return await repositorioUsuarioCoreSSO.UsuarioCadastradoCoreSSO(login);
        }

        public async Task<bool> Cadastrar(UsuarioDTO usuarioDto)
        {
            try
            {
                var pessoa = await repositorioPessoaCoreSSO.InserirPessoaCustomizado(usuarioDto.Nome);
                if (pessoa == null)
                    return false;
                
                await repositorioUsuarioCoreSSO.InserirUsuarioCustomizado(usuarioDto.Login, usuarioDto.Email, 
                    CriptografiaExtensions.CriptografarSenhaTripleDES(usuarioDto.Senha),pessoa,
                    new Guid(Constantes.ConstCoreSSO.ENTIDADE_SME));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> RecuperarSenha(string login, int sistema)
        {
            var sistemaRecuperacao = await sistemaRecuperacaoSenhaRepository.ObterSistema(sistema);
            if (sistemaRecuperacao is null)
                throw new NegocioException(MensagemNegocio.O_SISTEMA_INFORMADO_NAO_FOI_IDENTIFICADO);

            var usuarioCore = await repositorioUsuarioCoreSSO.ObterPorLogin(login);

            if (usuarioCore == null)
                throw new NegocioException(MensagemNegocio.USUARIO_NAO_ENCONTRADO);

            var usuario = await repositorioUsuarioRecuperacaoSenha.ObterUsuarioOuAdiciona(login);

            usuario.IniciarRecuperacaoDeSenha(usuarioCore.Email);
            await repositorioUsuarioRecuperacaoSenha.Salvar(usuario);

            EnviarEmailRecuperacao(usuarioCore, usuario.TokenRecuperacaoSenha.Value, sistemaRecuperacao, login);
            return usuarioCore.Email;
        }

        private void EnviarEmailRecuperacao(Usuario usuario, Guid tokenRecuperacaoSenha, SistemaRecuperacaoSenha sistema, string login)
        {
            string caminho = $"{Directory.GetCurrentDirectory()}/wwwroot/ModelosEmail/RecuperacaoSenha.txt";
            var textoArquivo = File.ReadAllText(caminho);
            var textoEmail = textoArquivo
                .Replace("#NOME", usuario.Pessoa.Nome)
                .Replace("#RF", login)
                .Replace("#LINK", $"{sistema.PaginaRecuperacaoSenha}{tokenRecuperacaoSenha}");

            servicoEmail.Enviar(usuario.Email, $"Recuperação de senha do(a) {sistema.NomeSistema}", textoEmail);
        }
        
        public Task<bool> ValidarTokenRecuperacaoSenha(Guid token, int sistema)
        {
            return repositorioUsuarioCoreSSO.ValidarTokenRecuperacaoSenha(token, sistema);
        }

        public async Task<RetornoAlteracaoSenhaDto> AlterarSenhaPorToken(AlterarSenhaPorTokenDto alterarSenha)
        {
            Usuario usuario = await ServicoUsuarios.ObterUsuarioPorTokenRecuperacaoSenha(new Guid(alterarSenha.Token));

            if (usuario == null)
                return new RetornoAlteracaoSenhaDto(AlterarSenhaStatus.NaoEncontrado);

            if (!usuario.TokenRecuperacaoSenhaValido())
                return new RetornoAlteracaoSenhaDto(AlterarSenhaStatus.TokenExpirado);

            try
            {
                usuario.ValidarSenha(alterarSenha.Senha);
            }
            catch (NegocioException e)
            {
                return new RetornoAlteracaoSenhaDto(AlterarSenhaStatus.ForaPadrao);
            }

            var retornoAlteracaoSenha = await AlterarSenha(usuario.Login, alterarSenha.Senha);

            if (retornoAlteracaoSenha == AlterarSenhaStatus.OK)
            {
                usuario.FinalizarRecuperacaoSenha();
                await usuarioService.Salvar(usuario);
            }

            return new RetornoAlteracaoSenhaDto(retornoAlteracaoSenha, usuario.Login);
        }
        
        private Task<AlterarSenhaStatus> AlterarSenha(string login, string senha)
            => //chamar AlterarSenha que foi criado - autenticacaoSGPService.AlterarSenhaAsync(new AlterarSenhaDto() { Usuario = login, Senha = senha });
    }
}
