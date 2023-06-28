using AutoMapper;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Aplicacao
{
    public class DominioParaDTOProfile : Profile
    {
        public DominioParaDTOProfile()
        {
            CreateMap<LoginEmailDTO, Usuario>().ReverseMap();
            CreateMap<DadosUsuarioDTO, DadosUsuario>().ReverseMap();
        }
    }
}
