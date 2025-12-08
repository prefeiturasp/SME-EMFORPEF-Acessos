
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Infra.Dominio.Acessos.Repositorios;

public interface IRepositorioUsuarioValidacaoToken
{
    Task<UsuarioValidacaoToken> ObterUsuarioPorLoginSistemaTipoAcao(string login, long sistema, TipoAcao tipoAcao = TipoAcao.RecuperacaoSenha);
    Task<UsuarioValidacaoToken> ObterUsuarioPorTokenSistemaTipoAcao(Guid token, long sistema, TipoAcao tipoAcao = TipoAcao.RecuperacaoSenha);
    Task Salvar(UsuarioValidacaoToken usuarioValidacaoToken);
}