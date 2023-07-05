using Microsoft.Extensions.DependencyInjection;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.TesteIntegracao.Constantes;
using SME.Acessos.TesteIntegracao.Setup;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace SME.Acessos.TesteIntegracao
{
    [Collection("TesteIntegradoAcessos")]
    public class TesteBase : IClassFixture<TestFixture>
    {
        protected readonly CollectionFixture _collectionFixture;

        public ServiceProvider ServiceProvider => _collectionFixture.ServiceProvider;

        public TesteBase(CollectionFixture collectionFixture)
        {
            _collectionFixture = collectionFixture;
            _collectionFixture.Database.LimparBase();
            _collectionFixture.IniciarServicos();

            RegistrarFakes(_collectionFixture.Services);
            _collectionFixture.BuildServiceProvider();
        }

        protected virtual void RegistrarFakes(IServiceCollection services)
        {
            RegistrarCommandFakes(services);
            RegistrarQueryFakes(services);        
        }

        protected virtual void RegistrarCommandFakes(IServiceCollection services)
        {
            //services.Replace(new ServiceDescriptor(typeof(IRequestHandler<PublicarFilaSgpCommand, bool>),typeof(PublicarFilaSgpCommandHandlerFake), ServiceLifetime.Scoped));
        }

        protected virtual void RegistrarQueryFakes(IServiceCollection services)
        {
            //services.Replace(new ServiceDescriptor(typeof(IRequestHandler<ObterTurmaEOLParaSyncEstruturaInstitucionalPorTurmaIdQuery, TurmaParaSyncInstitucionalDto>),
            //    typeof(ObterTurmaEOLParaSyncEstruturaInstitucionalPorTurmaIdQueryHandlerFake), ServiceLifetime.Scoped));
        }

        public Task InserirNaBase<T>(IEnumerable<T> objetos) where T : class, new()
        {
            _collectionFixture.Database.Inserir(objetos);
            return Task.CompletedTask;
        }

        public Task InserirNaBase<T>(T objeto) where T : class, new()
        {
            _collectionFixture.Database.Inserir(objeto);
            return Task.CompletedTask;
        }
        
        public Task AtualizarNaBase<T>(T objeto) where T : class, new()
        {
            _collectionFixture.Database.Atualizar(objeto);
            return Task.CompletedTask;
        }

        public Task InserirNaBase(string nomeTabela, params string[] campos)
        {
            _collectionFixture.Database.Inserir(nomeTabela, campos);
            return Task.CompletedTask;
        }
        
        public Task InserirNaBase(string nomeTabela, string[] campos, string[] valores)
        {
            _collectionFixture.Database.Inserir(nomeTabela, campos, valores);
            return Task.CompletedTask;
        }

        public List<T> ObterTodos<T>() where T : class, new()
        {
            return _collectionFixture.Database.ObterTodos<T>();
        }

        public T ObterPorId<T, K>(K id)
            where T : class, new()
            where K : struct
        {
            return _collectionFixture.Database.ObterPorId<T, K>(id);
        }
        
        protected IServicoAutenticacao GetServicoAutenticacao()
        {
            return ObterServico<IServicoAutenticacao>();
        }
        
        protected IServicoPerfilUsuario GetServicoPerfilUsuario()
        {
            return ObterServico<IServicoPerfilUsuario>();
        }
        
        protected IServicoUsuarios GetServicoUsuarios()
        {
            return ObterServico<IServicoUsuarios>();
        }
        
        public T ObterServico<T>()
        {
            return ServiceProvider.GetService<T>() ?? throw new Exception($"Servi�o {typeof(T).Name} n�o registrado!");
        }

        protected async Task CriarAcoesGrupoPermissoes()
        {
            await CriarAcoes();
            await CriarGrupos();
            await CriarModulos();
            await CriarPermissoesAdminGeral();
            await CriarPermissoesAdminBiblioteca();
            await CriarPermissoesAdminMemoria();
            await CriarPermissoesAdminMemorial();
            await CriarPermissoesBasico();
            await CriarPermissoesExterno();
        }
        
        private async Task CriarPermissoesExterno()
        {
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_EXTERNO_ID, ModuloId = ConstantesTestes.SOLICITACOES_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_EXTERNO_ID, ModuloId = ConstantesTestes.SOLICITACOES_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_EXTERNO_ID, ModuloId = ConstantesTestes.SOLICITACOES_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_EXTERNO_ID, ModuloId = ConstantesTestes.SOLICITACOES_ALTERACAO_ID});            
        }

        private async Task CriarPermissoesBasico()
        {
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_BASICO_ID, ModuloId = ConstantesTestes.CREDITO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_BASICO_ID, ModuloId = ConstantesTestes.AUTOR_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_BASICO_ID, ModuloId = ConstantesTestes.EDITORA_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_BASICO_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_BASICO_ID, ModuloId = ConstantesTestes.ASSUNTO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_BASICO_ID, ModuloId = ConstantesTestes.ACERVO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_BASICO_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_BASICO_ID, ModuloId = ConstantesTestes.SOLICITACOES_CONSULTA_ID});
        }
        private async Task CriarPermissoesAdminMemorial()
        {
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.CREDITO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.CREDITO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.CREDITO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.CREDITO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.AUTOR_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.AUTOR_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.AUTOR_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.AUTOR_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.EDITORA_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.EDITORA_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.EDITORA_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.EDITORA_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.ASSUNTO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.ASSUNTO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.ASSUNTO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.ASSUNTO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.ACERVO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.ACERVO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.ACERVO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.ACERVO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.SOLICITACOES_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.SOLICITACOES_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.SOLICITACOES_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, ModuloId = ConstantesTestes.SOLICITACOES_ALTERACAO_ID});            
        }
        
        private async Task CriarPermissoesAdminMemoria()
        {
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.CREDITO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.CREDITO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.CREDITO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.CREDITO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.AUTOR_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.AUTOR_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.AUTOR_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.AUTOR_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.EDITORA_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.EDITORA_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.EDITORA_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.EDITORA_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.ASSUNTO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.ASSUNTO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.ASSUNTO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.ASSUNTO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.ACERVO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.ACERVO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.ACERVO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.ACERVO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.SOLICITACOES_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.SOLICITACOES_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.SOLICITACOES_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, ModuloId = ConstantesTestes.SOLICITACOES_ALTERACAO_ID});            
        }
        
        private async Task CriarPermissoesAdminBiblioteca()
        {
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.CREDITO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.CREDITO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.CREDITO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.CREDITO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.AUTOR_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.AUTOR_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.AUTOR_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.AUTOR_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.EDITORA_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.EDITORA_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.EDITORA_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.EDITORA_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.ASSUNTO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.ASSUNTO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.ASSUNTO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.ASSUNTO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.ACERVO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.ACERVO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.ACERVO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.ACERVO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.SOLICITACOES_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.SOLICITACOES_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.SOLICITACOES_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, ModuloId = ConstantesTestes.SOLICITACOES_ALTERACAO_ID});            
        }
        
        private async Task CriarPermissoesAdminGeral()
        {
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.CREDITO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.CREDITO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.CREDITO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.CREDITO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.AUTOR_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.AUTOR_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.AUTOR_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.AUTOR_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.EDITORA_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.EDITORA_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.EDITORA_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.EDITORA_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.SERIE_COLECAO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.ASSUNTO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.ASSUNTO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.ASSUNTO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.ASSUNTO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.ACERVO_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.ACERVO_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.ACERVO_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.ACERVO_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.ATENDIMENTO_SOLICITACOES_ALTERACAO_ID});
            
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.SOLICITACOES_CONSULTA_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.SOLICITACOES_INCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.SOLICITACOES_EXCLUSAO_ID});
            await InserirNaBase(new Permissao() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, ModuloId = ConstantesTestes.SOLICITACOES_ALTERACAO_ID});            
        }

        private async Task CriarModulos()
        {
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.CREDITO_CONSULTA_ID, Descricao = ConstantesTestes.CREDITO_CONSULTA_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_CREDITO_2_ID, AcaoId = ConstantesTestes.ACAO_CONSULTA_1_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.CREDITO_INCLUSAO_ID, Descricao = ConstantesTestes.CREDITO_INCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_CREDITO_2_ID, AcaoId = ConstantesTestes.ACAO_INCLUSAO_2_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.CREDITO_EXCLUSAO_ID, Descricao = ConstantesTestes.CREDITO_EXCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_CREDITO_2_ID, AcaoId = ConstantesTestes.ACAO_EXCLUSAO_3_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.CREDITO_ALTERACAO_ID, Descricao = ConstantesTestes.CREDITO_ALTERACAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_CREDITO_2_ID, AcaoId = ConstantesTestes.ACAO_ALTERACAO_4_ID});
            
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.AUTOR_CONSULTA_ID, Descricao = ConstantesTestes.AUTOR_CONSULTA_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_AUTOR_3_ID, AcaoId = ConstantesTestes.ACAO_CONSULTA_1_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.AUTOR_INCLUSAO_ID, Descricao = ConstantesTestes.AUTOR_INCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_AUTOR_3_ID, AcaoId = ConstantesTestes.ACAO_INCLUSAO_2_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.AUTOR_EXCLUSAO_ID, Descricao = ConstantesTestes.AUTOR_EXCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_AUTOR_3_ID, AcaoId = ConstantesTestes.ACAO_EXCLUSAO_3_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.AUTOR_ALTERACAO_ID, Descricao = ConstantesTestes.AUTOR_ALTERACAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_AUTOR_3_ID, AcaoId = ConstantesTestes.ACAO_ALTERACAO_4_ID});
            
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.EDITORA_CONSULTA_ID, Descricao = ConstantesTestes.EDITORA_CONSULTA_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_EDITORA_4_ID, AcaoId = ConstantesTestes.ACAO_CONSULTA_1_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.EDITORA_INCLUSAO_ID, Descricao = ConstantesTestes.EDITORA_INCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_EDITORA_4_ID, AcaoId = ConstantesTestes.ACAO_INCLUSAO_2_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.EDITORA_EXCLUSAO_ID, Descricao = ConstantesTestes.EDITORA_EXCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_EDITORA_4_ID, AcaoId = ConstantesTestes.ACAO_EXCLUSAO_3_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.EDITORA_ALTERACAO_ID, Descricao = ConstantesTestes.EDITORA_ALTERACAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_EDITORA_4_ID, AcaoId = ConstantesTestes.ACAO_ALTERACAO_4_ID});
            
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.SERIE_COLECAO_CONSULTA_ID, Descricao = ConstantesTestes.SERIE_COLECAO_CONSULTA_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_SERIE_COLECAO_5_ID, AcaoId = ConstantesTestes.ACAO_CONSULTA_1_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.SERIE_COLECAO_INCLUSAO_ID, Descricao = ConstantesTestes.SERIE_COLECAO_INCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_SERIE_COLECAO_5_ID, AcaoId = ConstantesTestes.ACAO_INCLUSAO_2_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.SERIE_COLECAO_EXCLUSAO_ID, Descricao = ConstantesTestes.SERIE_COLECAO_EXCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_SERIE_COLECAO_5_ID, AcaoId = ConstantesTestes.ACAO_EXCLUSAO_3_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.SERIE_COLECAO_ALTERACAO_ID, Descricao = ConstantesTestes.SERIE_COLECAO_ALTERACAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_SERIE_COLECAO_5_ID, AcaoId = ConstantesTestes.ACAO_ALTERACAO_4_ID});
            
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.ASSUNTO_CONSULTA_ID, Descricao = ConstantesTestes.ASSUNTO_CONSULTA_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_ASSUNTO_6_ID, AcaoId = ConstantesTestes.ACAO_CONSULTA_1_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.ASSUNTO_INCLUSAO_ID, Descricao = ConstantesTestes.ASSUNTO_INCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_ASSUNTO_6_ID, AcaoId = ConstantesTestes.ACAO_INCLUSAO_2_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.ASSUNTO_EXCLUSAO_ID, Descricao = ConstantesTestes.ASSUNTO_EXCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_ASSUNTO_6_ID, AcaoId = ConstantesTestes.ACAO_EXCLUSAO_3_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.ASSUNTO_ALTERACAO_ID, Descricao = ConstantesTestes.ASSUNTO_ALTERACAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_ASSUNTO_6_ID, AcaoId = ConstantesTestes.ACAO_ALTERACAO_4_ID});
            
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.ACERVO_CONSULTA_ID, Descricao = ConstantesTestes.ACERVO_CONSULTA_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_ACERVO_7_ID, AcaoId = ConstantesTestes.ACAO_CONSULTA_1_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.ACERVO_INCLUSAO_ID, Descricao = ConstantesTestes.ACERVO_INCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_ACERVO_7_ID, AcaoId = ConstantesTestes.ACAO_INCLUSAO_2_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.ACERVO_EXCLUSAO_ID, Descricao = ConstantesTestes.ACERVO_EXCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_ACERVO_7_ID, AcaoId = ConstantesTestes.ACAO_EXCLUSAO_3_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.ACERVO_ALTERACAO_ID, Descricao = ConstantesTestes.ACERVO_ALTERACAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_ACERVO_7_ID, AcaoId = ConstantesTestes.ACAO_ALTERACAO_4_ID});
            
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.ATENDIMENTO_SOLICITACOES_CONSULTA_ID, Descricao = ConstantesTestes.ATENDIMENTO_SOLICITACOES_CONSULTA_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_ATENDIMENTO_SOLICITACOES_9_ID, AcaoId = ConstantesTestes.ACAO_CONSULTA_1_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.ATENDIMENTO_SOLICITACOES_INCLUSAO_ID, Descricao = ConstantesTestes.ATENDIMENTO_SOLICITACOES_INCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_ATENDIMENTO_SOLICITACOES_9_ID, AcaoId = ConstantesTestes.ACAO_INCLUSAO_2_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.ATENDIMENTO_SOLICITACOES_EXCLUSAO_ID, Descricao = ConstantesTestes.ATENDIMENTO_SOLICITACOES_EXCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_ATENDIMENTO_SOLICITACOES_9_ID, AcaoId = ConstantesTestes.ACAO_EXCLUSAO_3_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.ATENDIMENTO_SOLICITACOES_ALTERACAO_ID, Descricao = ConstantesTestes.ATENDIMENTO_SOLICITACOES_ALTERACAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_ATENDIMENTO_SOLICITACOES_9_ID, AcaoId = ConstantesTestes.ACAO_ALTERACAO_4_ID});
            
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.SOLICITACOES_CONSULTA_ID, Descricao = ConstantesTestes.SOLICITACOES_CONSULTA_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_SOLICITACOES_10_ID, AcaoId = ConstantesTestes.ACAO_CONSULTA_1_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.SOLICITACOES_INCLUSAO_ID, Descricao = ConstantesTestes.SOLICITACOES_INCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_SOLICITACOES_10_ID, AcaoId = ConstantesTestes.ACAO_INCLUSAO_2_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.SOLICITACOES_EXCLUSAO_ID, Descricao = ConstantesTestes.SOLICITACOES_EXCLUSAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_SOLICITACOES_10_ID, AcaoId = ConstantesTestes.ACAO_EXCLUSAO_3_ID});
            await InserirNaBase(new Modulo() { Id = ConstantesTestes.SOLICITACOES_ALTERACAO_ID, Descricao = ConstantesTestes.SOLICITACOES_ALTERACAO_NOME, ModuloCoreSSOId = ConstantesTestes.MODULO_SOLICITACOES_10_ID, AcaoId = ConstantesTestes.ACAO_ALTERACAO_4_ID});
            //
            // 1	Cadastros
            // 2	Crédito
            // 3	Autor
            // 4	Editora
            // 5	Série/Coleção
            // 6	Assunto
            // 7	Acervo
            // 8	Operações
            // 9	Atendimento de solicitações
            // 10	Solicitações
        }

        private async Task CriarGrupos()
        {
            await InserirNaBase(new Grupo() { Id = ConstantesTestes.GRUPO_ADMIN_GERAL_ID, Perfil = new Guid(ConstantesTestes.GRUPO_ADMIN_GERAL_GUID), Nome = ConstantesTestes.GRUPO_ADMIN_GERAL_NOME,IdAbrangencia = ConstantesTestes.ABRANGENCIA_1});
            await InserirNaBase(new Grupo() { Id = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_ID, Perfil = new Guid(ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_GUID), Nome = ConstantesTestes.GRUPO_ADMIN_BIBLIOTECA_NOME,IdAbrangencia = ConstantesTestes.ABRANGENCIA_1});
            await InserirNaBase(new Grupo() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIA_ID, Perfil = new Guid(ConstantesTestes.GRUPO_ADMIN_MEMORIA_GUID), Nome = ConstantesTestes.GRUPO_ADMIN_MEMORIA_NOME,IdAbrangencia = ConstantesTestes.ABRANGENCIA_1});
            await InserirNaBase(new Grupo() { Id = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_ID, Perfil = new Guid(ConstantesTestes.GRUPO_ADMIN_MEMORIAL_GUID), Nome = ConstantesTestes.GRUPO_ADMIN_MEMORIAL_NOME,IdAbrangencia = ConstantesTestes.ABRANGENCIA_1});
            await InserirNaBase(new Grupo() { Id = ConstantesTestes.GRUPO_BASICO_ID, Perfil = new Guid(ConstantesTestes.GRUPO_BASICO_GUID), Nome = ConstantesTestes.GRUPO_BASICO_NOME,IdAbrangencia = ConstantesTestes.ABRANGENCIA_1});
            await InserirNaBase(new Grupo() { Id = ConstantesTestes.GRUPO_EXTERNO_ID, Perfil = new Guid(ConstantesTestes.GRUPO_EXTERNO_GUID), Nome = ConstantesTestes.GRUPO_EXTERNO_NOME,IdAbrangencia = ConstantesTestes.ABRANGENCIA_1});
        }

        private async Task CriarAcoes()
        {
            await InserirNaBase(new Acao { Id = 1, Descricao = ConstantesTestes.ACAO_CONSULTA_1_NOME });
            await InserirNaBase(new Acao { Id = 2, Descricao = ConstantesTestes.ACAO_INCLUSAO_2_NOME });
            await InserirNaBase(new Acao { Id = 3, Descricao = ConstantesTestes.ACAO_EXCLUSAO_3_NOME });
            await InserirNaBase(new Acao { Id = 4, Descricao = ConstantesTestes.ACAO_ALTERACAO_4_NOME });
        }
    }
}
