using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SME.Acessos.Aplicacao;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Aplicacao.Servicos;
using SME.Acessos.Aplicacao.Settings;
using SME.Acessos.Infra.Dados;
using SME.Acessos.Infra.Dados.Acessos;
using SME.Acessos.Infra.Dados.Repositorios.CoreSSO;
using SME.Acessos.Infra.Dominio;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
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
            RegistrarJwtSettings();

            RegistrarMapeamentos.Registrar();
        }

        protected virtual void RegistrarJwtSettings()
        {
            var jwtConfiguration = configuration.GetSection(nameof(JwtTokenSettings));
            services.Configure<JwtTokenSettings>(jwtConfiguration);
            
            var jwtTokenSettings = jwtConfiguration.Get<JwtTokenSettings>();
            
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidAudience = jwtTokenSettings.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenSettings.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtTokenSettings.IssuerSigningKey))
                };
            });
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
            services.AddScoped<IServicoUsuarioGrupo, ServicoUsuarioGrupo>();
            services.AddScoped<IServicoAutenticacao, ServicoAutenticacao>();
            services.AddScoped<IServicoPerfilUsuario, ServicoPerfilUsuario>();
            services.AddScoped<IServicoTokenJwt, ServicoTokenJwt>();
        }

        protected virtual void RegistrarRepositorios()
        {
            services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
            services.AddScoped<IRepositorioUsuarioGrupoPessoa, RepositorioUsuarioGrupoPessoa>();
            services.AddScoped<IRepositorioUsuarioGrupo, RepositorioUsuarioGrupo>();
            services.AddScoped<IRepositorioGrupoPermissao, RepositorioGrupoPermissao>();
            services.AddScoped<IRepositorioPermissao, RepositorioPermissao>();
            services.AddScoped<IRepositorioSistemaRecuperacaoSenha, RepositorioSistemaRecuperacaoSenha>();
            services.AddScoped<IRepositorioUsuarioRecuperacaoSenha, RepositorioUsuarioRecuperacaoSenha>();
            services.AddScoped<IRepositorioConfiguracaoEmail, RepositorioConfiguracaoEmail>();
            services.AddScoped<IRepositorioPessoa, RepositorioPessoa>();
        }

        protected virtual void RegistrarTelemetria()
        {
            services.ConfigurarTelemetria(configuration);
        }

        protected virtual void RegistrarConexao()
        {
            services.AddScoped<IConexaoAcessos, ConexaoAcessos>(_ => new ConexaoAcessos(configuration.GetConnectionString("Acessos")));
            services.AddScoped<IConexaoCoreSSO, ConexaoCoreSSO>(_ =>new ConexaoCoreSSO(configuration.GetConnectionString("CoreSSO")));
            
            //serviceCollection.AddScoped<ITransacao, Transacao>();
        }

        protected virtual void RegistrarPolly()
        {
            services.ConfigurarPolly();
        }

    }
}