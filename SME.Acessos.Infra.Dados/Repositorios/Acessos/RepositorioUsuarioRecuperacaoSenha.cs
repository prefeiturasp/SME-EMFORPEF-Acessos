using Dapper;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;

namespace SME.Acessos.Infra.Dados.Acessos;

public class RepositorioUsuarioRecuperacaoSenha : RepositorioBaseAcessos<UsuarioRecuperacaoSenha>, IRepositorioUsuarioRecuperacaoSenha
{
    public RepositorioUsuarioRecuperacaoSenha(IConexaoAcessos conexao) : base(conexao)
    {
    }

    public async Task<UsuarioRecuperacaoSenha> ObterUsuarioPorLoginSistema(string login, long sistema)
    {
        var query = "select id, login, expiracao, token from usuario_recuperacao_senha where login = @login and codigo_sistema = @sistema";

        return (await conexao.Obter().QueryAsync<UsuarioRecuperacaoSenha>(query, new { login, sistema }))
            .ToList()
            .FirstOrDefault();
    }

    public async Task<UsuarioRecuperacaoSenha> ObterUsuarioPorTokenRecuperacaoSenha(Guid token, long sistema)
    {
        var query = "select id, login, expiracao, token from usuario_recuperacao_senha where token = @token and codigo_sistema = @sistema";

        return (await conexao.Obter().QueryAsync<UsuarioRecuperacaoSenha>(query, new { token, sistema }))
            .ToList()
            .FirstOrDefault();
    }

    public async Task Salvar(UsuarioRecuperacaoSenha usuarioRecuperacaoSenha)
    {
        if (usuarioRecuperacaoSenha.Id > 0)
            await Atualizar(usuarioRecuperacaoSenha);
        else    
            await Inserir(usuarioRecuperacaoSenha);
    }
}