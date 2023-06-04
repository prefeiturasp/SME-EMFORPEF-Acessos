using AutoMapper;
using SME.Acessos.Infra.Dominio.CoreSSO;

namespace SME.Acessos.Aplicacao
{
    public class DominioParaDTOProfile : Profile
    {
        public DominioParaDTOProfile()
        {
            CreateMap<DadosUsuarioDTO, Usuario>().ReverseMap();
        }
    }
}
