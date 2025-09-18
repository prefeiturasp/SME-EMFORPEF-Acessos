using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Aplicacao
{
    public class ServicoUsuarioGrupo : IServicoUsuarioGrupo
    {
        private readonly IRepositorioUsuarioGrupo repositorioUsuarioGrupo;
        private readonly IRepositorioUsuario repositorioUsuario;

        public ServicoUsuarioGrupo(IRepositorioUsuarioGrupo repositorioUsuarioGrupo,IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuarioGrupo = repositorioUsuarioGrupo ?? throw new ArgumentNullException(nameof(repositorioUsuarioGrupo));
            this.repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public async Task<bool> VincularPerfil(string login, Guid perfilId)
        {
            try
            {
                var usuario = await repositorioUsuario.ObterPorLogin(login, true);

                var perfilJaVinculado = await repositorioUsuarioGrupo.PerfilJaVinculado(usuario.Id, perfilId);
                if (perfilJaVinculado)
                    await repositorioUsuarioGrupo.AtivarVinculo(usuario.Id, perfilId);
                else
                    await repositorioUsuarioGrupo.InserirUsuarioGrupoCustomizado(usuario.Id, perfilId);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DesvincularPerfil(string login, Guid perfilId)
        {
            try
            {
                var usuario = await repositorioUsuario.ObterPorLogin(login);
                var retorno = await repositorioUsuarioGrupo.DeletarUsuarioGrupoCustomizado(usuario.Id, perfilId);
                return retorno;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
