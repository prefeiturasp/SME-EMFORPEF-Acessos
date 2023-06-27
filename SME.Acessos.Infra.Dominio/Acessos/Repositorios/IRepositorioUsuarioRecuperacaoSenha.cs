
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dominio.Acessos.Repositorios;

public interface IRepositorioUsuarioRecuperacaoSenha
{
    Task<UsuarioRecuperacaoSenha> ObterUsuarioPorLogin(string login);
    Task<UsuarioRecuperacaoSenha> ObterUsuarioPorTokenRecuperacaoSenha(Guid token);
}