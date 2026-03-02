using Bogus;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoUsuarioTeste(IServicoUsuarios servicoUsuarios, IServicoUsuarioGrupo servicoUsuarioGrupo) : IServicoUsuarioTeste
    {
        private static readonly Faker _faker = new("pt_BR");
        public async Task<IEnumerable<DadosPessoaUsuarioDto>> CadastrarUsuariosEmMassaAsync(int quantidade, Guid? perfilId)
        {
            perfilId ??= new Guid("7EDA4540-A16C-4FE5-8322-9F75B3414E27");

            var usuariosCadastrados = new List<DadosPessoaUsuarioDto>(quantidade);

            for (int i = 0; i < quantidade; i++)
            {
                var usuario = GerarUsuarioDeTeste();
                if (!await servicoUsuarios.Cadastrar(usuario)) continue;
                if (!await servicoUsuarioGrupo.VincularPerfil(usuario.Login, perfilId.Value)) continue;
                usuariosCadastrados.Add(new DadosPessoaUsuarioDto()
                {
                    Login = usuario.Login,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Senha = usuario.Senha,
                    PerfilId = perfilId.Value
                });
            }
            return usuariosCadastrados;
        }
        private static UsuarioDTO GerarUsuarioDeTeste()
        {
            return new UsuarioDTO()
            {
                Login = _faker.Internet.UserName(),
                Nome = _faker.Name.FullName(),
                Email = _faker.Internet.Email(),
                Senha = _faker.Internet.Password()
            };
        }
    }
}