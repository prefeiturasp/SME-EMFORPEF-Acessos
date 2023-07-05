using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using SME.Acessos.Aplicacao;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Aplicacao.Servicos;
using SME.Acessos.Aplicacao.Settings;
using SME.Acessos.Infra.Dados;
using SME.Acessos.Infra.Dados.Acessos;
using SME.Acessos.Infra.Dados.Mapeamentos.Acessos;
using SME.Acessos.Infra.Dados.Mapeamentos.CoreSSO;
using SME.Acessos.Infra.Dados.Repositorios.CoreSSO;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Extensions;
using SME.Acessos.Infra.IoC;
using SME.Acessos.Infra.Polly;
using SME.Acessos.Infra.Servicos;
using SME.Acessos.IoC;
using SME.Acessos.TesteIntegracao.Constantes;
using SME.Acessos.TesteIntegracao.ServicosFakes;
using SME.Acessos.TesteIntegracao.Setup;
using GrupoMap = SME.Acessos.Infra.Dados.Mapeamentos.Acessos.GrupoMap;
using ModuloMap = SME.Acessos.Infra.Dados.Mapeamentos.Acessos.ModuloMap;

namespace SME.Acessos.TesteIntegracao.Setup
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
            RegistrarMapeamentos();
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
            
            _serviceCollection.AddSingleton<IServicoTokenJwt>(serviceProvider =>
            {
                var options = serviceProvider.GetService<IOptions<JwtTokenSettings>>();
                
                options.Value.Audience = ConstantesTestes.TOKEN_AUDIENCE;
                options.Value.Issuer = ConstantesTestes.TOKEN_ISSUER;
                options.Value.IssuerSigningKey = ConstantesTestes.TOKEN_ISSUER_SIGNING_KEY;
                options.Value.ExpiresInMinutes = ConstantesTestes.EXPIRES_IN_720_MINUTES;
                
                return new ServicoTokenJwt(options);
            });

            _serviceCollection.AddSingleton<IServicoLogs, ServicoLogs>();
        }

        protected override void RegistrarProfiles()
        {
            _serviceCollection.AddAutoMapper(typeof(DominioParaDTOProfile));
        }
        
        protected override void RegistrarRepositorios()
        {
            _serviceCollection.AddScoped<IRepositorioUsuario, RepositorioUsuarioCoreSSOFake>();
            _serviceCollection.AddScoped<IRepositorioDadosUsuario, RepositorioDadosUsuario>();
            _serviceCollection.AddScoped<IRepositorioUsuarioGrupoPessoa, RepositorioUsuarioGrupoPessoaCoreSSOFake>();
            _serviceCollection.AddScoped<IRepositorioUsuarioGrupo, RepositorioUsuarioGrupo>();
            _serviceCollection.AddScoped<IRepositorioGrupoPermissao, RepositorioGrupoPermissaoCoreSSOFake>();
            _serviceCollection.AddScoped<IRepositorioPermissao, RepositorioPermissao>();
            _serviceCollection.AddScoped<IRepositorioSistemaRecuperacaoSenha, RepositorioSistemaRecuperacaoSenha>();
            _serviceCollection.AddScoped<IRepositorioUsuarioRecuperacaoSenha, RepositorioUsuarioRecuperacaoSenha>();
            _serviceCollection.AddScoped<IRepositorioConfiguracaoEmail, RepositorioConfiguracaoEmail>();
            _serviceCollection.AddScoped<IRepositorioPessoa, RepositorioPessoa>();
            _serviceCollection.AddScoped<IRepositorioPessoaDocumento, RepositorioPessoaDocumento>();
        }
        
        protected override void RegistrarServicos()
        {
            _serviceCollection.AddScoped<IServicoUsuarios, ServicoUsuarios>();
            _serviceCollection.AddScoped<IServicoEmail, ServicoEmailFake>();
            _serviceCollection.AddScoped<IServicoUsuarioGrupo, ServicoUsuarioGrupo>();
            _serviceCollection.AddScoped<IServicoAutenticacao, ServicoAutenticacao>();
            _serviceCollection.AddScoped<IServicoPerfilUsuario, ServicoPerfilUsuario>();
            _serviceCollection.AddScoped<IServicoTokenJwt, ServicoTokenJwt>();
        }

        protected override void RegistrarTelemetria()
        {
            _serviceCollection.ConfigurarTelemetria(_configuration);
        }

        protected override void RegistrarConexao()
        {
            var retorno = _serviceCollection.FirstOrDefault(f => f.ServiceType == typeof(System.Data.IDbConnection));
            var conexao = ((CollectionFixture)retorno.ImplementationFactory.Target).Database.Conexao;
            _serviceCollection.AddScoped<IConexaoAcessos, ConexaoAcessos>(_ =>new ConexaoAcessos(conexao.ConnectionString));
            _serviceCollection.AddScoped<IConexaoCoreSSO, ConexaoCoreSSOFake>(_ =>new ConexaoCoreSSOFake(string.Empty));
        }

        protected override void RegistrarPolly()
        {
            _serviceCollection.ConfigurarPolly();
        }
    }
}
