using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using SME.Acessos.Aplicacao;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Aplicacao.Servicos;
using SME.Acessos.Infra.Dados;
using SME.Acessos.Infra.Dados.Acessos;
using SME.Acessos.Infra.Dados.Repositorios.CoreSSO;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.IoC;
using SME.Acessos.Infra.Polly;
using SME.Acessos.Infra.Servicos;
using SME.Acessos.IoC;
using SME.Acessos.TesteIntegracao.ServicosFakes;

namespace SME.CDEP.TesteIntegracao.Setup
{
    public class RegistradorDependencias : RegistradorDeDependencias
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly IConfiguration _configuration;

        public RegistradorDependencias(IServiceCollection services, IConfiguration configuration) : base(services, configuration)
        {
            _serviceCollection = services;
            _configuration = configuration;
        }

        public override void Registrar()
        {
            RegistrarTelemetria();
            RegistrarConexao();
            RegistrarRepositorios();
            RegistrarServicos();
            RegistrarProfiles();
            RegistrarLogs();
            RegistrarPolly();
        }

        protected override void RegistrarLogs()
        {
            _serviceCollection.AddOptions<ConfiguracaoRabbitLogsOptions>()
                .Bind(_configuration.GetSection(ConfiguracaoRabbitLogsOptions.Secao), c => c.BindNonPublicProperties = true);

            _serviceCollection.AddSingleton<ConfiguracaoRabbitLogsOptions>();
            _serviceCollection.AddSingleton<IConexoesRabbitLogs>(serviceProvider =>
            {
                var options = serviceProvider.GetService<IOptions<ConfiguracaoRabbitLogsOptions>>().Value;
                var provider = serviceProvider.GetService<IOptions<DefaultObjectPoolProvider>>().Value;
                return new ConexoesRabbitLogs(options, provider);
            });

            _serviceCollection.AddSingleton<IServicoLogs, ServicoLogs>();
        }

        protected override void RegistrarProfiles()
        {
            _serviceCollection.AddAutoMapper(typeof(DominioParaDTOProfile));
        }
        
        protected virtual void RegistrarRepositorios()
        {
            _serviceCollection.AddScoped<IRepositorioUsuario, RepositorioUsuarioCoreSSOFake>();
            _serviceCollection.AddScoped<IRepositorioDadosUsuario, RepositorioDadosUsuario>();
            _serviceCollection.AddScoped<IRepositorioUsuarioGrupoPessoa, RepositorioUsuarioGrupoPessoa>();
            _serviceCollection.AddScoped<IRepositorioUsuarioGrupo, RepositorioUsuarioGrupo>();
            _serviceCollection.AddScoped<IRepositorioGrupoPermissao, RepositorioGrupoPermissao>();
            _serviceCollection.AddScoped<IRepositorioPermissao, RepositorioPermissao>();
            _serviceCollection.AddScoped<IRepositorioSistemaRecuperacaoSenha, RepositorioSistemaRecuperacaoSenha>();
            _serviceCollection.AddScoped<IRepositorioUsuarioRecuperacaoSenha, RepositorioUsuarioRecuperacaoSenha>();
            _serviceCollection.AddScoped<IRepositorioConfiguracaoEmail, RepositorioConfiguracaoEmail>();
            _serviceCollection.AddScoped<IRepositorioPessoa, RepositorioPessoa>();
            _serviceCollection.AddScoped<IRepositorioPessoaDocumento, RepositorioPessoaDocumento>();
        }

        protected override void RegistrarTelemetria()
        {
            _serviceCollection.ConfigurarTelemetria(_configuration);
        }

        protected override void RegistrarConexao()
        {
            _serviceCollection.AddScoped<IConexaoAcessos, ConexaoAcessos>();
            _serviceCollection.AddScoped<IConexaoCoreSSO, ConexaoCoreSSOFake>(_ =>new ConexaoCoreSSOFake(string.Empty));
        }

        protected override void RegistrarPolly()
        {
            _serviceCollection.ConfigurarPolly();
        }
    }
}
