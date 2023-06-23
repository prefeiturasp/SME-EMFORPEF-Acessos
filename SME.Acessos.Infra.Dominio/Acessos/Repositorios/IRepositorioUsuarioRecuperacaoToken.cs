
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dominio.Acessos.Repositorios;

public interface IRepositorioUsuarioRecuperacaoToken
{
    Task<Usuario> ObterUsuarioPorLogin(string login);
    Task<Usuario> ObterUsuarioPorTokenRecuperacaoSenha(Guid token);
}