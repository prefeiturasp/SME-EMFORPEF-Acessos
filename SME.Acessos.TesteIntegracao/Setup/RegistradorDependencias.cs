using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using SME.Acessos.Aplicacao;
using SME.Acessos.Infra.Dados;
using SME.Acessos.Infra.IoC;
using SME.Acessos.Infra.Polly;
using SME.Acessos.Infra.Servicos;
using SME.Acessos.IoC;

namespace SME.CDEP.TesteIntegracao.Setup
{
    public class RegistradorDependencias : RegistradorDeDependencias
    {
        private readonly IServiceCollection services;
        private readonly IConfiguration configuration;

        public RegistradorDependencias(IServiceCollection services, IConfiguration configuration) : base(services, configuration)
        {}

        public override void Registrar()
        {
            RegistrarTelemetria();
            RegistrarConexao();
            RegistrarRepositorios();
            RegistrarServicos();
            RegistrarProfiles();
            RegistrarLogs();
            RegistrarPolly();

            RegistrarMapeamentos.Registrar();
        }

        protected override void RegistrarLogs()
        {
            services.AddOptions<ConfiguracaoRabbitLogsOptions>()
                .Bind(configuration.GetSection(ConfiguracaoRabbitLogsOptions.Secao), c => c.BindNonPublicProperties = true);

            services.AddSingleton<ConfiguracaoRabbitLogsOptions>();
            services.AddSingleton<IConexoesRabbitLogs>(serviceProvider =>
            {
                var options = serviceProvider.GetService<IOptions<ConfiguracaoRabbitLogsOptions>>().Value;
                var provider = serviceProvider.GetService<IOptions<DefaultObjectPoolProvider>>().Value;
                return new ConexoesRabbitLogs(options, provider);
            });

            services.AddSingleton<IServicoLogs, ServicoLogs>();
        }

        protected override void RegistrarProfiles()
        {
            services.AddAutoMapper(typeof(DominioParaDTOProfile));
        }

        protected override void RegistrarServicos()
        {}

        protected override void RegistrarRepositorios()
        {}

        protected override void RegistrarTelemetria()
        {
            services.ConfigurarTelemetria(configuration);
        }

        protected override void RegistrarConexao()
        {
            services.AddScoped<IConexaoAcessos, ConexaoAcessos>();
            services.AddScoped<IConexaoCoreSSO, ConexaoCoreSSO>();
            //serviceCollection.AddScoped<ITransacao, Transacao>();
        }

        protected override void RegistrarPolly()
        {
            services.ConfigurarPolly();
        }
    }
}
