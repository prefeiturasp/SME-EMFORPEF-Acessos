using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SME.Acessos.Infra.Options;

namespace SME.Acessos.Infra.IoC
{
    public static class RegistrarTelemetria
    {
        public static void ConfigurarTelemetria(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null)
                return;

            services.AddOptions<TelemetriaOptions>()
                .Bind(configuration.GetSection(TelemetriaOptions.Secao), c => c.BindNonPublicProperties = true);

            services.AddSingleton<TelemetriaOptions>();
            services.AddSingleton<IServicoTelemetria, ServicoTelemetria>();
        }
    }
}
