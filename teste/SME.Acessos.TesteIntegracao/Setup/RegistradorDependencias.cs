using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using SME.Acessos.Aplicacao;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Aplicacao.Servicos;
using SME.Acessos.Aplicacao.Settings;
using SME.Acessos.Infra.Dados;
using SME.Acessos.Infra.Dados.Repositorios.Acessos;
using SME.Acessos.Infra.Dados.Repositorios.CoreSSO;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.IoC;
using SME.Acessos.Infra.Polly;
using SME.Acessos.Infra.Servicos;
using SME.Acessos.Infra.Servicos.Log;
using SME.Acessos.IoC;
using SME.Acessos.TesteIntegracao.Constantes;
using SME.Acessos.TesteIntegracao.ServicosFakes;

namespace SME.Acessos.TesteIntegracao.Setup
{
    public class RegistradorDependencias(IServiceCollection services, IConfiguration configuration) : RegistradorDeDependencias(services, configuration)
    {
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
            services.AddOptions<ConfiguracaoRabbitLogsOptions>()
                .Bind(configuration.GetSection(ConfiguracaoRabbitLogsOptions.Secao), c => c.BindNonPublicProperties = true);

            services.AddSingleton<ConfiguracaoRabbitLogsOptions>();
            services.AddSingleton<IConexoesRabbitLogs>(serviceProvider =>
            {
                var options = serviceProvider.GetService<IOptions<ConfiguracaoRabbitLogsOptions>>()!.Value;
                var provider = serviceProvider.GetService<IOptions<DefaultObjectPoolProvider>>()!.Value;
                return new ConexoesRabbitLogs(options, provider);
            });

            services.AddSingleton<IServicoTokenJwt>(serviceProvider =>
            {
                var options = serviceProvider.GetService<IOptions<JwtTokenSettings>>()!;

                options.Value.Audience = ConstantesTestes.TOKEN_AUDIENCE;
                options.Value.Issuer = ConstantesTestes.TOKEN_ISSUER;
                options.Value.IssuerSigningKey = ConstantesTestes.TOKEN_ISSUER_SIGNING_KEY;
                options.Value.ExpiresInMinutes = ConstantesTestes.EXPIRES_IN_720_MINUTES;

                return new ServicoTokenJwt(options);
            });

            services.AddSingleton<IServicoLogs, ServicoLogs>();
        }

        protected override void RegistrarProfiles()
        {
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(DominioParaDtoProfile).Assembly));
        }

        protected override void RegistrarRepositorios()
        {
            services.AddScoped<IRepositorioUsuario, RepositorioUsuarioCoreSSOFake>();
            services.AddScoped<IRepositorioDadosUsuario, RepositorioDadosUsuario>();
            services.AddScoped<IRepositorioUsuarioGrupoPessoa, RepositorioUsuarioGrupoPessoaCoreSSOFake>();
            services.AddScoped<IRepositorioUsuarioGrupo, RepositorioUsuarioGrupo>();
            services.AddScoped<IRepositorioGrupoPermissao, RepositorioGrupoPermissaoCoreSSOFake>();
            services.AddScoped<IRepositorioPermissao, RepositorioPermissao>();
            services.AddScoped<IRepositorioSistemaAcao, RepositorioSistemaAcao>();
            services.AddScoped<IRepositorioUsuarioValidacaoToken, RepositorioUsuarioValidacaoToken>();
            services.AddScoped<IRepositorioConfiguracaoEmail, RepositorioConfiguracaoEmail>();
            services.AddScoped<IRepositorioPessoa, RepositorioPessoa>();
            services.AddScoped<IRepositorioPessoaDocumento, RepositorioPessoaDocumento>();
        }

        protected override void RegistrarServicos()
        {
            services.AddScoped<IServicoUsuarios, ServicoUsuarios>();
            services.AddScoped<IServicoEmail, ServicoEmailFake>();
            services.AddScoped<IServicoUsuarioGrupo, ServicoUsuarioGrupo>();
            services.AddScoped<IServicoAutenticacao, ServicoAutenticacao>();
            services.AddScoped<IServicoPerfilUsuario, ServicoPerfilUsuario>();
            services.AddScoped<IServicoTokenJwt, ServicoTokenJwt>();
        }

        protected override void RegistrarTelemetria()
        {
            services.ConfigurarTelemetria(configuration);
        }

        protected override void RegistrarConexao()
        {
            var retorno = services.FirstOrDefault(f => f.ServiceType == typeof(System.Data.IDbConnection));
            var conexao = ((CollectionFixture)retorno.ImplementationFactory.Target).Database.Conexao;
            services.AddScoped<IConexaoAcessos, ConexaoAcessos>(_ => new ConexaoAcessos(conexao.ConnectionString));
            services.AddScoped<IConexaoCoreSSO, ConexaoCoreSSOFake>(_ => new ConexaoCoreSSOFake(string.Empty));
        }

        protected override void RegistrarPolly()
        {
            services.ConfigurarPolly();
        }
    }
}
