using SME.Acessos.Aplicacao.DTO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoGrupos
    {
        Task<IEnumerable<GrupoDTO>> ObterGruposPorSistemaId(long sistemaId);
    }
}
