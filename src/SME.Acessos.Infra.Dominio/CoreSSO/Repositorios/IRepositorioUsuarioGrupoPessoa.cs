
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dominio.CoreSSO.Repositorios
{
    public interface IRepositorioUsuarioGrupoPessoa
    {
        Task<IList<UsuarioGrupoPessoa>> ObterPerfisUsuario(string login, int sistemaId);
        Task<IList<UsuarioGrupoPessoa>> ObterUsuariosPerfilPareceristasConecta();
    }
}
