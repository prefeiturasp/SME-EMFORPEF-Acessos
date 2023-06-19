using AutoMapper;
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
    }
}
