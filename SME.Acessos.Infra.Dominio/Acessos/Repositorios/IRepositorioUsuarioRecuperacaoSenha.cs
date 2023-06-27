
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;

namespace SME.Acessos.Infra.Dominio.Acessos.Repositorios;

public interface IRepositorioUsuarioRecuperacaoSenha
{
    Task<UsuarioRecuperacaoSenha> ObterUsuarioPorLoginSistema(string login, long sistema);
    Task<UsuarioRecuperacaoSenha> ObterUsuarioPorTokenRecuperacaoSenha(Guid token, long sistema);
    Task Salvar(UsuarioRecuperacaoSenha usuarioRecuperacaoSenha);
}