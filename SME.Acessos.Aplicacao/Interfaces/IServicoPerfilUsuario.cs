using SME.Acessos.Aplicacao.DTO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoPerfilUsuario
    {
        Task<RetornoPerfilUsuarioDTO> ObterPerfisToken(string login, int sistemaId, Guid? perfilUsuarioId = null);
        Task<RetornoPerfilUsuarioDTO> Revalidar(string token);
        Task<IEnumerable<RetornoUsuriosPareceristasDTO>> ObterUsuariosPerfilPareceristasConecta(string login, string nome);
    }
}
