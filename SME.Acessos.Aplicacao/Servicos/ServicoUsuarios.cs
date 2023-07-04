using AutoMapper;
using SME.Acessos.Aplicacao.Constantes;
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
        private readonly IRepositorioUsuarioRecuperacaoSenha repositorioUsuarioRecuperacaoSenha;
        private readonly IRepositorioSistemaRecuperacaoSenha repositorioSistemaRecuperacaoSenha;

        public ServicoUsuarios(IRepositorioUsuario repositorioUsuarioCoreSSO, IMapper mapper,IRepositorioPessoa repositorioPessoaCoreSSO,
            IServicoEmail servicoEmail,IRepositorioDadosUsuario repositorioDadosUsuario,IRepositorioUsuarioRecuperacaoSenha repositorioUsuarioRecuperacaoSenha,
            IRepositorioSistemaRecuperacaoSenha repositorioSistemaRecuperacaoSenha)
        {
            this.repositorioUsuarioCoreSSO = repositorioUsuarioCoreSSO ?? throw new ArgumentNullException(nameof(repositorioUsuarioCoreSSO));
            this.repositorioPessoaCoreSSO = repositorioPessoaCoreSSO ?? throw new ArgumentNullException(nameof(repositorioPessoaCoreSSO));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.servicoEmail = servicoEmail ?? throw new ArgumentNullException(nameof(servicoEmail));
            this.repositorioUsuarioRecuperacaoSenha = repositorioUsuarioRecuperacaoSenha ?? throw new ArgumentNullException(nameof(repositorioUsuarioRecuperacaoSenha));
            this.repositorioDadosUsuario = repositorioDadosUsuario ?? throw new ArgumentNullException(nameof(repositorioDadosUsuario));
            this.repositorioSistemaRecuperacaoSenha = repositorioSistemaRecuperacaoSenha ?? throw new ArgumentNullException(nameof(repositorioSistemaRecuperacaoSenha));
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
                if (!pessoa.HasValue)
                    return false;
                
                await repositorioUsuarioCoreSSO.InserirUsuarioCustomizado(usuarioDto.Login, usuarioDto.Email, 
                    CriptografiaExtensions.CriptografarSenhaTripleDES(usuarioDto.Senha),pessoa.Value,
                    new Guid(Constantes.ConstantesCoreSSO.ENTIDADE_SME));

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

        public async Task<string> RecuperarSenha(string login, long sistemaId)
        {
            var sistemaRecuperacao = await repositorioSistemaRecuperacaoSenha.ObterSistema(sistemaId); 
            if (sistemaRecuperacao is null)
                throw new NegocioException(MensagemNegocio.O_SISTEMA_INFORMADO_NAO_FOI_IDENTIFICADO);

            var usuarioCore = await ValidarLogin(login);

            var usuarioRecuperacaoSenha = await repositorioUsuarioRecuperacaoSenha.ObterUsuarioPorLoginSistema(login, sistemaRecuperacao.CodigoSistema);
            if (usuarioRecuperacaoSenha == null)
                usuarioRecuperacaoSenha = new UsuarioRecuperacaoSenha() { Login = usuarioCore.Login, CodigoSistema = sistemaId};
            
            usuarioRecuperacaoSenha.IniciarRecuperacaoDeSenha(usuarioCore.Email);
            await repositorioUsuarioRecuperacaoSenha.Salvar(usuarioRecuperacaoSenha);

            await EnviarEmailRecuperacao(usuarioCore, usuarioRecuperacaoSenha.Token.Value, sistemaRecuperacao, login);
            return usuarioCore.Email;
        }

        private async Task<Usuario?> ValidarLogin(string login)
        {
            var usuarioCore = await repositorioUsuarioCoreSSO.ObterPorLogin(login);

            if (usuarioCore == null)
                throw new NegocioException(MensagemNegocio.USUARIO_NAO_ENCONTRADO);
            return usuarioCore;
        }

        private async Task EnviarEmailRecuperacao(Usuario usuario, Guid tokenRecuperacaoSenha, SistemaRecuperacaoSenha sistema, string login)
        {
            string caminho = $"{Directory.GetCurrentDirectory()}/wwwroot/ModelosEmail/RecuperacaoSenha.txt";
            var textoArquivo = File.ReadAllText(caminho);
            var textoEmail = textoArquivo
                .Replace("#NOME", usuario.Pessoa.Nome)
                .Replace("#RF", login)
                .Replace("#LINK", $"{sistema.PaginaRecuperacaoSenha}{tokenRecuperacaoSenha}");

            await servicoEmail.Enviar(usuario.Pessoa.Nome, usuario.Email, $"Recuperação de senha do(a) {sistema.NomeSistema}", textoEmail, sistema.CodigoSistema);
        }
        
        public async Task<bool> ValidarTokenRecuperacaoSenha(Guid token, long sistemaId)
        {
            var usuarioPorTokenRecuperacaoSenha = await repositorioUsuarioRecuperacaoSenha.ObterUsuarioPorTokenRecuperacaoSenha(token, sistemaId);
            return usuarioPorTokenRecuperacaoSenha?.TokenRecuperacaoSenhaValido() ?? false;
        }

        public async Task<RetornoAlteracaoSenhaDto> AlterarSenhaPorToken(long sistemaId, AlterarSenhaPorTokenDto alterarSenha)
        {
            var usuarioRecuperacaoSenha = await repositorioUsuarioRecuperacaoSenha.ObterUsuarioPorTokenRecuperacaoSenha(new Guid(alterarSenha.Token), sistemaId);

            if (usuarioRecuperacaoSenha == null)
                return new RetornoAlteracaoSenhaDto(AlterarSenhaStatus.NaoEncontrado);

            if (!usuarioRecuperacaoSenha.TokenRecuperacaoSenhaValido())
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
                await repositorioUsuarioRecuperacaoSenha.Salvar(usuarioRecuperacaoSenha);
            }

            return new RetornoAlteracaoSenhaDto(retornoAlteracaoSenha, usuarioRecuperacaoSenha.Login);
        }

        private async Task<AlterarSenhaStatus> AlterarSenha(string login, string senha)
        {
            var usuarioCore = await ValidarLogin(login);
            
            await AlterarSenhaRegistrarHistorico(senha, usuarioCore.Id);
            
            return AlterarSenhaStatus.OK;
        }
    }
}
