using AutoMapper;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using System.Diagnostics.CodeAnalysis;
using Grupo = SME.Acessos.Infra.Dominio.CoreSSO.Entidades.Grupo;

namespace SME.Acessos.Aplicacao
{
    [ExcludeFromCodeCoverage]
    public class DominioParaDtoProfile : Profile
    {
        public DominioParaDtoProfile()
        {
            CreateMap<LoginEmailDTO, Usuario>().ReverseMap();
            CreateMap<DadosUsuarioDto, DadosUsuario>().ReverseMap();
            CreateMap<Grupo, GrupoDTO>();
            CreateMap<ConfiguracaoEmail, ConfiguracaoEmailDTO>();
        }
    }
}
