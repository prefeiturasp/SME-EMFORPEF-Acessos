using AutoMapper;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Enumeradores;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.Aplicacao
{
    public class ServicoUsuarios : IServicoUsuarios
    {
        private readonly IRepositorioUsuario repositorioUsuario;
        private readonly IRepositorioDadosUsuario repositorioDadosUsuario;
        private readonly IRepositorioPessoa repositorioPessoa;
        private readonly IRepositorioPessoaDocumento repositorioPessoaDocumento;
        private readonly IMapper mapper;

        public ServicoUsuarios(IRepositorioUsuario repositorioUsuario, IMapper mapper,IRepositorioPessoa repositorioPessoa,
            IRepositorioPessoaDocumento repositorioPessoaDocumento,IRepositorioDadosUsuario repositorioDadosUsuario)
        {
            this.repositorioDadosUsuario = repositorioDadosUsuario ?? throw new ArgumentNullException(nameof(repositorioDadosUsuario));
            this.repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
            this.repositorioPessoa = repositorioPessoa ?? throw new ArgumentNullException(nameof(repositorioPessoa));
            this.repositorioPessoaDocumento = repositorioPessoaDocumento ?? throw new ArgumentNullException(nameof(repositorioPessoaDocumento));
            this.mapper = mapper;
        }

        public async Task<IList<LoginEmailDTO>> ObterTodosUsuarios()
        {
            var usuarios = await repositorioUsuario.ObterTodos();
            return mapper.Map<IList<LoginEmailDTO>>(usuarios);
        }

        public async Task<LoginEmailDTO> ObterUsuarioPorId(Guid id)
            => mapper.Map<LoginEmailDTO>(await repositorioUsuario.ObterPorId(id));

        public async Task<LoginEmailDTO> ObterUsuarioPorLogin(string login)
            => mapper.Map<LoginEmailDTO>(await repositorioUsuario.ObterPorLogin(login));

        public async Task<bool> UsuarioCadastradoCoreSSO(string login)
        {
            return await repositorioUsuario.UsuarioCadastradoCoreSSO(login);
        }

        public async Task<bool> Cadastrar(UsuarioDTO usuarioDto)
        {
            try
            {
                var pessoa = await repositorioPessoa.InserirPessoaCustomizado(usuarioDto.Nome);
                if (!pessoa.HasValue)
                    return false;
                
                await repositorioPessoaDocumento.InserirPessoaDocumentoCustomizado(usuarioDto.Login, pessoa.Value, new Guid(Constantes.ConstantesCoreSSO.TIPO_DOCUMENTACAO_CPF));
                
                await repositorioUsuario.InserirUsuarioCustomizado(usuarioDto.Login, usuarioDto.Email, 
                    CriptografiaExtensions.CriptografarSenhaTripleDES(usuarioDto.Senha),pessoa.Value,
                    new Guid(Constantes.ConstantesCoreSSO.ENTIDADE_SME));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AlterarSenha(AlterarUsuarioDTO alterarUsuarioDto)
        {
           var usuario = await repositorioUsuario.ObterPorLogin(alterarUsuarioDto.Login);
           
           var senhaAtualCorreta = await repositorioUsuario.ValidarSenhaAtual(usuario.Id, CriptografiaExtensions.CriptografarSenha(alterarUsuarioDto.SenhaAtual,TipoCriptografia.TripleDES));
           if (!senhaAtualCorreta)
               return false;
           
           await repositorioUsuario.AlterarSenha(usuario.Id, CriptografiaExtensions.CriptografarSenha(alterarUsuarioDto.SenhaNova,TipoCriptografia.TripleDES));
           await repositorioUsuario.InserirHistoricoSenha(usuario.Id, CriptografiaExtensions.CriptografarSenha(alterarUsuarioDto.SenhaNova,TipoCriptografia.TripleDES),TipoCriptografia.TripleDES);
           return true;
        }

        public async Task<DadosUsuarioDTO?> ObterMeusDados(string login)
        {
            var usuarios = await repositorioDadosUsuario.ObterMeusDados(login);
            if (usuarios == null)
                throw new NegocioException(MensagemNegocio.USUARIO_NAO_ENCONTRADO);
                
            return mapper.Map<DadosUsuarioDTO>(usuarios);
        }
    }
}
