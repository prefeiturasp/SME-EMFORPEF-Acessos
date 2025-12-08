using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoUsuarioGrupo(IRepositorioUsuarioGrupo repositorioUsuarioGrupo, IRepositorioUsuario repositorioUsuario) : IServicoUsuarioGrupo
    {
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
