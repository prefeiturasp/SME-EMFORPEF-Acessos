using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using SME.Acessos.Aplicacao;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dados;
using SME.Acessos.Infra.Dados.Repositorios.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.IoC;
using SME.Acessos.Infra.Polly;
using SME.Acessos.Infra.Servicos;

namespace SME.Acessos.IoC
{
    public class RegistradorDeDependencias
    {
        private readonly IServiceCollection services;
        private readonly IConfiguration configuration;

        public RegistradorDeDependencias(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            this.services = serviceCollection;
            this.configuration = configuration;
        }

        public virtual void Registrar()
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

        protected virtual void RegistrarLogs()
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

        protected virtual void RegistrarProfiles()
        {
            services.AddAutoMapper(typeof(DominioParaDTOProfile));
        }

        protected virtual void RegistrarServicos()
        {
            services.AddScoped<IServicoUsuarios, ServicoUsuarios>();
        }

        protected virtual void RegistrarRepositorios()
        {
            services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
        }

        protected virtual void RegistrarTelemetria()
        {
            services.ConfigurarTelemetria(configuration);
        }

        protected virtual void RegistrarConexao()
        {
            services.AddScoped<IConexaoAcessos, ConexaoAcessos>(_ => new ConexaoAcessos(configuration.GetConnectionString("Acessos") ?? throw new ArgumentNullException("String de Conexão para base Acessos Nula")));
            services.AddScoped<IConexaoCoreSSO, ConexaoCoreSSO>(_ => new ConexaoCoreSSO(configuration.GetConnectionString("CoreSSO") ?? throw new ArgumentNullException("String de Conexão para base CoreSSO Nula")));
            //serviceCollection.AddScoped<ITransacao, Transacao>();
        }

        protected virtual void RegistrarPolly()
        {
            services.ConfigurarPolly();
        }

    }
}