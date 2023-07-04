using System.Data.SqlClient;
using Dapper;
using SME.Acessos.Infra.Dados;
using SME.Acessos.Infra.Dados.Repositorios.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Enumeradores;
using SME.Acessos.Infra.Dominio.Extensions;
using SME.Acessos.TesteIntegracao.Constantes;

namespace SME.Acessos.TesteIntegracao.ServicosFakes
{
    public class RepositorioUsuarioCoreSSOFake : RepositorioBaseCoreSSO<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuarioCoreSSOFake(IConexaoCoreSSO conexao) : base(conexao)
        {
        }

        public async Task<Usuario> ObterPorLogin(string login)
        {
            switch (login)
            {
                case ConstantesTestes.LOGIN_99999999998:
                    return new Usuario()
                    {
                        Id = new Guid(ConstantesTestes.ID_99999999998),
                        Login = ConstantesTestes.LOGIN_99999999998,
                        Email = ConstantesTestes.EMAIL_99999999998,
                        Senha = CriptografiaExtensions.CriptografarSenhaTripleDES(ConstantesTestes.SENHA_99999999998),
                        PessoaId = new Guid(ConstantesTestes.ID_99999999998),
                        Pessoa = new Pessoa()
                        {
                            Id = new Guid(ConstantesTestes.ID_99999999998),
                            Nome = ConstantesTestes.NOME_99999999998
                        }
                    };
                default:
                    return new Usuario()
                    {
                        Id = new Guid(ConstantesTestes.ID_99999999999),
                        Login = ConstantesTestes.LOGIN_99999999999,
                        Email = ConstantesTestes.EMAIL_99999999999,
                        Senha = CriptografiaExtensions.CriptografarSenhaTripleDES(ConstantesTestes.SENHA_99999999998),
                        PessoaId = new Guid(ConstantesTestes.ID_99999999999),
                        Pessoa = new Pessoa()
                        {
                            Id = new Guid(ConstantesTestes.ID_99999999999),
                            Nome = ConstantesTestes.NOME_99999999999
                        }
                    };
            }
        }

        public async Task<bool> UsuarioCadastradoCoreSSO(string login)
        {
            return login.Equals(ConstantesTestes.LOGIN_99999999998);
        }

        public Task InserirUsuarioCustomizado(string login, string email, string senha, Guid pessoa, Guid entidade)
        {
            return Task.CompletedTask;
        }

        public Task<bool> ValidarSenhaAtual(Guid usuarioId, string senhaAtual)
        {
            return Task.FromResult(true);
        }

        public Task AlterarSenha(Guid usuarioId, string senhaNova)
        {
            return Task.CompletedTask;
        }

        public Task InserirHistoricoSenha(Guid usuarioId, string senha, TipoCriptografia criptografia)
        {
            return Task.CompletedTask;
        }

        public Task AlterarEmail(Guid usuarioId, string email)
        {
            return Task.CompletedTask;
        }
    }
}
