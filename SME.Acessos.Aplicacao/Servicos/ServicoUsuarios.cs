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

        public async Task<IList<DadosUsuarioDto>> ObterTodosUsuarios()
        {
            var usuarios = await repositorioUsuario.ObterTodos();
            return mapper.Map<IList<DadosUsuarioDto>>(usuarios);
        }

        public async Task<DadosUsuarioDto> ObterUsuarioPorId(Guid id)
            => mapper.Map<DadosUsuarioDto>(await repositorioUsuario.ObterPorId(id));

        public async Task<DadosUsuarioDto> ObterUsuarioPorLogin(string login)
            => mapper.Map<DadosUsuarioDto>(await repositorioUsuario.ObterPorLogin(login));
    }
}
