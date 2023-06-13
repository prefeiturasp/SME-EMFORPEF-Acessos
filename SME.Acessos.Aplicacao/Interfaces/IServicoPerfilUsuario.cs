using SME.Acessos.Aplicacao.DTO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoPerfilUsuario
    {
        Task<RetornoPerfilUsuarioDTO> ObterPerfisToken(string login, int sistemaId);
    }
}
