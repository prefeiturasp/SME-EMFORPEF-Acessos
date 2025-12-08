using AutoMapper;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoGrupos : IServicoGrupos
    {
        private readonly IMapper _mapper;
        private readonly IRepositorioGrupo _repositorioGrupo;

        public ServicoGrupos(IMapper mapper, IRepositorioGrupo repositorioGrupo)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repositorioGrupo = repositorioGrupo ?? throw new ArgumentNullException(nameof(repositorioGrupo));
        }

        public async Task<IEnumerable<GrupoDTO>> ObterGruposPorSistemaId(long sistemaId)
        {
            var grupos = await _repositorioGrupo.ObterPorSistemaId(sistemaId);
            return _mapper.Map<List<GrupoDTO>>(grupos);
        }
        public async Task<GrupoDTO> ObterGrupoPorIdSistemaId(long sistemaId, Guid grupoId)
        {
            var grupo = await _repositorioGrupo.ObterGrupoPorIdSistemaId(sistemaId,grupoId);
            return _mapper.Map<GrupoDTO>(grupo);
        }
    }
}
