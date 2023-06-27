using AutoMapper;
using SME.Acesos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.Aplicacao
{
    public class ServicoUsuarios : IServicoUsuarios
    {
        private readonly IRepositorioUsuario repositorioUsuario;
        private readonly IRepositorioPessoa repositorioPessoa;
        private readonly IMapper mapper;

        public ServicoUsuarios(IRepositorioUsuario repositorioUsuario, IMapper mapper,IRepositorioPessoa repositorioPessoa)
        {
            this.repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
            this.repositorioPessoa = repositorioPessoa ?? throw new ArgumentNullException(nameof(repositorioPessoa));
            this.mapper = mapper;
        }

        public async Task<IList<DadosUsuarioDTO>> ObterTodosUsuarios()
        {
            var usuarios = await repositorioUsuario.ObterTodos();
            return mapper.Map<IList<DadosUsuarioDTO>>(usuarios);
        }

        public async Task<DadosUsuarioDTO> ObterUsuarioPorId(Guid id)
            => mapper.Map<DadosUsuarioDTO>(await repositorioUsuario.ObterPorId(id));

        public async Task<DadosUsuarioDTO> ObterUsuarioPorLogin(string login)
            => mapper.Map<DadosUsuarioDTO>(await repositorioUsuario.ObterPorLogin(login));

        public async Task<bool> UsuarioCadastradoCoreSSO(string login)
        {
            return await repositorioUsuario.UsuarioCadastradoCoreSSO(login);
        }

        public async Task<bool> Cadastrar(UsuarioDTO usuarioDto)
        {
            try
            {
                var pessoa = await repositorioPessoa.InserirPessoaCustomizado(usuarioDto.Nome);
                if (pessoa == null)
                    return false;
                
                await repositorioUsuario.InserirUsuarioCustomizado(usuarioDto.Login, usuarioDto.Email, 
                    CriptografiaExtensions.CriptografarSenhaTripleDES(usuarioDto.Senha),pessoa,
                    new Guid(Constantes.ConstCoreSSO.ENTIDADE_SME));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<string> RecuperarSenha(string login, int sistema)
        {
            var sistemaRecuperacao = await sistemaRecuperacaoSenhaRepository.ObterSistema(sistema);
            if (sistemaRecuperacao is null)
                throw new NegocioException("O sistema informado não foi identificado na base de integração do EOL");

            var usuarioCore = await autenticacaoSGPService.CarregarDadosDoUsuarioAsync(login);

            if (usuarioCore == null)
                throw new NegocioException("Usuário ou RF não encontrado");

            var usuario = await usuarioService.ObterUsuarioOuAdiciona(login);

            usuario.IniciarRecuperacaoDeSenha(usuarioCore.Email);
            await usuarioService.Salvar(usuario);

            EnviarEmailRecuperacao(usuarioCore, usuario.TokenRecuperacaoSenha.Value, sistemaRecuperacao, login);
            return usuarioCore.Email;
        }

        private void EnviarEmailRecuperacao(DadosUsuarioDTO usuario, Guid tokenRecuperacaoSenha, SistemaRecuperacaoSenha sistema, string login)
        {
            string caminho = $"{Directory.GetCurrentDirectory()}/wwwroot/ModelosEmail/RecuperacaoSenha.txt";
            var textoArquivo = File.ReadAllText(caminho);
            var textoEmail = textoArquivo
                .Replace("#NOME", usuario.Nome)
                .Replace("#RF", login)
                .Replace("#LINK", $"{sistema.PaginaRecuperacaoSenha}{tokenRecuperacaoSenha}");

            emailService.Enviar(usuario.Email, $"Recuperação de senha do(a) {sistema.NomeSistema}", textoEmail);
        }
        
        public Task<bool> ValidarTokenRecuperacaoSenha(Guid token, int sistema)
        {
            return repositorioUsuario.ValidarTokenRecuperacaoSenha(token, sistema);
        }

        public Task<RetornoAlteracaoSenhaDto> AlterarSenhaPorToken(AlterarSenhaPorTokenDto alterarSenha)
        {
            throw new NotImplementedException();
        }
    }
}
