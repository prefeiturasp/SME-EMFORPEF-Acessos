using AutoMapper;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.CoreSSO;

namespace SME.Acessos.Aplicacao
{
    public class ServicoUsuarios : IServicoUsuarios
    {
        private readonly IRepositorioUsuario repositorioUsuario;
        private readonly IMapper mapper;

        public ServicoUsuarios(IRepositorioUsuario repositorioUsuario, IMapper mapper)
        {
            this.repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
            this.mapper = mapper;
        }

        public async Task<IList<DadosUsuarioDTO>> ObterTodosUsuarios()
        {
            throw new Exception("Deu pau!");
            var usuarios = await repositorioUsuario.ObterTodos();
            return mapper.Map<IList<DadosUsuarioDTO>>(usuarios);
        }

        public async Task<DadosUsuarioDTO> ObterUsuarioPorId(Guid id)
            => mapper.Map<DadosUsuarioDTO>(await repositorioUsuario.ObterPorId(id));

        public async Task<DadosUsuarioDTO> ObterUsuarioPorLogin(string login)
            => mapper.Map<DadosUsuarioDTO>(await repositorioUsuario.ObterPorLogin(login));
    }
}
