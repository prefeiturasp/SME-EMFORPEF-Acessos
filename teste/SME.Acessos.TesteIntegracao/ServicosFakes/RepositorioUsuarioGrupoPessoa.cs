using System.Collections;
using Dapper;
using SME.Acessos.Infra.Dados;
using SME.Acessos.Infra.Dados.Repositorios;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.TesteIntegracao.Constantes;

namespace SME.Acessos.TesteIntegracao.ServicosFakes
{
    public class RepositorioUsuarioGrupoPessoaCoreSSOFake : RepositorioBaseCoreSSO<UsuarioGrupoPessoa>, IRepositorioUsuarioGrupoPessoa
    {
        public RepositorioUsuarioGrupoPessoaCoreSSOFake(IConexaoCoreSSO conexao) : base(conexao)
        {
        }

        public Task<IList<UsuarioGrupoPessoa>> ObterPerfisUsuario(string login, int sistemaId)
        {
            IList<UsuarioGrupoPessoa> usuariosGruposPessoas = new List<UsuarioGrupoPessoa>()
            {
                new ()
                {
                    UsuarioId = Guid.NewGuid(),
                    UsuarioEmail = ConstantesTestes.EMAIL_99999999998,
                    PessoaNome = ConstantesTestes.NOME_99999999998,
                    GrupoId = new Guid(ConstantesTestes.GRUPO_EXTERNO_GUID),
                    GrupoNome = ConstantesTestes.GRUPO_EXTERNO_NOME,
                    Login = ConstantesTestes.LOGIN_99999999998
                },
                new ()
                {
                    UsuarioId = Guid.NewGuid(),
                    UsuarioEmail = ConstantesTestes.EMAIL_ADMIN_GERAL_1000,
                    PessoaNome = ConstantesTestes.NOME_ADMIN_GERAL_1000,
                    GrupoId = new Guid(ConstantesTestes.GRUPO_ADMIN_GERAL_GUID),
                    GrupoNome = ConstantesTestes.GRUPO_ADMIN_GERAL_NOME,
                    Login = ConstantesTestes.LOGIN_ADMIN_GERAL_1000
                }
            };

            return Task.FromResult(usuariosGruposPessoas.Where(f => f.Login.Equals(login)).ToList() as IList<UsuarioGrupoPessoa>);
        }

        public Task<IList<UsuarioGrupoPessoa>> ObterUsuariosPerfilPareceristasConecta()
        {
            throw new NotImplementedException();
        }
    }
}
