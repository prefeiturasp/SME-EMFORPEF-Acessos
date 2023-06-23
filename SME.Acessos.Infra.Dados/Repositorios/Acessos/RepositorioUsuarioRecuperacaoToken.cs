using System.Text;
using Dapper;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.Enumeradores;
using Modulo = SME.Acessos.Infra.Dominio.Acessos.Entidades.Modulo;

namespace SME.Acessos.Infra.Dados.Acessos;

public class RepositorioUsuarioRecuperacaoToken : RepositorioBaseAcessos<UsuarioRecuperacaoToken>, IRepositorioUsuarioRecuperacaoToken
{
    public RepositorioUsuarioRecuperacaoToken(IConexaoAcessos conexao) : base(conexao)
    {
    }

    public async Task<Usuario> ObterUsuarioPorLogin(string login)
    {
        var query = "select * from usuario where login = @login";

        return (await conexao.Obter().QueryAsync<Usuario>(query, new { login }))
            .ToList()
            .FirstOrDefault();
    }

    public async Task<Usuario> ObterUsuarioPorTokenRecuperacaoSenha(System.Guid token)
    {
        var query = "select * from usuario where token_recuperacao_senha = @token";

        return (await conexao.Obter().QueryAsync<Usuario>(query, new { token }))
            .ToList()
            .FirstOrDefault();
    }
}