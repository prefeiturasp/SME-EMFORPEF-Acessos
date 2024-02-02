using Dapper;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Infra.Dados.Acessos;

public class RepositorioUsuarioValidacaoToken : RepositorioBaseAcessos<UsuarioValidacaoToken>, IRepositorioUsuarioValidacaoToken
{
    public RepositorioUsuarioValidacaoToken(IConexaoAcessos conexao) : base(conexao)
    {
    }

    public async Task<UsuarioValidacaoToken> ObterUsuarioPorLoginSistemaTipoAcao(string login, long sistema, TipoAcao tipoAcao = TipoAcao.RecuperacaoSenha)
    {
        var query = @"select id, 
                             login,
                             Expiracao, 
                             Token, 
                             codigo_sistema,
                             tipo
                      from usuario_validacao_token 
                      where login = @login 
                        and codigo_sistema = @sistema
                        and tipo = @tipoAcao";

        return await conexao.Obter().QueryFirstOrDefaultAsync<UsuarioValidacaoToken>(query, new { login, sistema, tipoAcao });
    }

    public async Task<UsuarioValidacaoToken> ObterUsuarioPorTokenSistemaTipoAcao(Guid token, long sistema, TipoAcao tipoAcao = TipoAcao.RecuperacaoSenha)
    {
        var query = @"select id, 
                             login,
                             Expiracao, 
                             Token, 
                             codigo_sistema,
                             tipo
                      from usuario_validacao_token 
                      where token = @token 
                        and codigo_sistema = @sistema
                        and tipo = @tipoAcao";

        return await conexao.Obter().QueryFirstOrDefaultAsync<UsuarioValidacaoToken>(query, new { token, sistema, tipoAcao });
    }

    public async Task Salvar(UsuarioValidacaoToken usuarioValidacaoToken)
    {
        if (usuarioValidacaoToken.Id > 0)
            await Atualizar(usuarioValidacaoToken);
        else    
            await Inserir(usuarioValidacaoToken);
    }
}