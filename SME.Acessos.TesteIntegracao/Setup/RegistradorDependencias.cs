using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SME.Acessos.IoC;

namespace SME.CDEP.TesteIntegracao.Setup
{
    public class RegistradorDependencias : RegistradorDeDependencias
    {
        public RegistradorDependencias(IServiceCollection serviceCollection, IConfiguration configuration) : base(serviceCollection, configuration)
        {
           
        }
    }
}
