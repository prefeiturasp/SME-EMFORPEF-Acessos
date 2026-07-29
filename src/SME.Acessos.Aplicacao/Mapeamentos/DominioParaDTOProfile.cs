using AutoMapper;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using Grupo = SME.Acessos.Infra.Dominio.CoreSSO.Entidades.Grupo;

namespace SME.Acessos.Aplicacao
{
    public class DominioParaDTOProfile : Profile
    {
        public DominioParaDTOProfile()
        {
            CreateMap<LoginEmailDTO, Usuario>().ReverseMap();
            CreateMap<DadosUsuarioDto, DadosUsuario>().ReverseMap();
            CreateMap<Grupo, GrupoDTO>();
            CreateMap<ConfiguracaoEmail, ConfiguracaoEmailDTO>();
        }
    }
}
