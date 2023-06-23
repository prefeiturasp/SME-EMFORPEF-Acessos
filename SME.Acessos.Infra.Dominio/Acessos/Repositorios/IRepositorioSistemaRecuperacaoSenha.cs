
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dominio.Acessos.Repositorios;

public interface IRepositorioSistemaRecuperacaoSenha
{
    Task<SistemaRecuperacaoSenha> ObterSistema(int sistemaId);
}