using AutoMapper;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoGrupos(IMapper mapper, IRepositorioGrupo repositorioGrupo) : IServicoGrupos
    {
        public async Task<IEnumerable<GrupoDTO>> ObterGruposPorSistemaId(long sistemaId)
        {
            var grupos = await repositorioGrupo.ObterPorSistemaId(sistemaId);
            return mapper.Map<List<GrupoDTO>>(grupos);
        }
        public async Task<GrupoDTO> ObterGrupoPorIdSistemaId(long sistemaId, Guid grupoId)
        {
            var grupo = await repositorioGrupo.ObterGrupoPorIdSistemaId(sistemaId,grupoId);
            return mapper.Map<GrupoDTO>(grupo);
        }
    }
}
