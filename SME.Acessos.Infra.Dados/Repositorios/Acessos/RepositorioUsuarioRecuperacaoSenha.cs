using Dapper;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;

namespace SME.Acessos.Infra.Dados.Acessos;

public class RepositorioUsuarioRecuperacaoSenha : RepositorioBaseAcessos<UsuarioRecuperacaoSenha>, IRepositorioUsuarioRecuperacaoSenha
{
    public RepositorioUsuarioRecuperacaoSenha(IConexaoAcessos conexao) : base(conexao)
    {
    }

    public async Task<UsuarioRecuperacaoSenha> ObterUsuarioPorLogin(string login)
    {
        var query = "select * from usuario_recuperacao_senha where login = @login";

        return (await conexao.Obter().QueryAsync<UsuarioRecuperacaoSenha>(query, new { login }))
            .ToList()
            .FirstOrDefault();
    }

    public async Task<UsuarioRecuperacaoSenha> ObterUsuarioPorTokenRecuperacaoSenha(System.Guid token)
    {
        var query = "select * from usuario_recuperacao_senha where token_recuperacao_senha = @token";

        return (await conexao.Obter().QueryAsync<UsuarioRecuperacaoSenha>(query, new { token }))
            .ToList()
            .FirstOrDefault();
    }
}