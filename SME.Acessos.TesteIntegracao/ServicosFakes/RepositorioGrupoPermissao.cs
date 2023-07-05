using Dapper;
using SME.Acessos.Infra.Dados;
using SME.Acessos.Infra.Dados.Repositorios.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.TesteIntegracao.Constantes;

namespace SME.Acessos.TesteIntegracao.ServicosFakes
{
    public class RepositorioGrupoPermissaoCoreSSOFake : RepositorioBaseCoreSSO<GrupoPermissao>, IRepositorioGrupoPermissao
    {
        public RepositorioGrupoPermissaoCoreSSOFake(IConexaoCoreSSO conexao) : base(conexao)
        {
        }

        public async Task<IList<GrupoPermissao>> ObterModulosPorPerfilSistema(Guid perfilId, int sistemaId)
        {
            return new List<GrupoPermissao>()
            {
                new GrupoPermissao()
                {
                    Id = new Guid(ConstantesTestes.GRUPO_EXTERNO_GUID),
                    SistemaId = ConstantesTestes.SISTEMA_98,
                    ModuloId = ConstantesTestes.MODULO_OPERACOES_8_ID,
                    EhAlteracao = true,
                    EhConsulta = true,
                    EhExclusao = true,
                    EhInsercao = true
                },
                new GrupoPermissao()
                {
                    Id = new Guid(ConstantesTestes.GRUPO_EXTERNO_GUID),
                    SistemaId = ConstantesTestes.SISTEMA_98,
                    ModuloId = ConstantesTestes.MODULO_SOLICITACOES_10_ID,
                    EhAlteracao = true,
                    EhConsulta = true,
                    EhExclusao = true,
                    EhInsercao = true
                },
            };
        }
    }
}
