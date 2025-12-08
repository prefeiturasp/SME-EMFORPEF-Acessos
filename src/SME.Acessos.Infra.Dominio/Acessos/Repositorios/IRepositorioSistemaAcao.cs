
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Infra.Dominio.Acessos.Repositorios;

public interface IRepositorioSistemaAcao
{
    Task<SistemaAcao> ObterSistemaAcaoPorAcaoESistema(long sistemaId, TipoAcao tipoAcao = TipoAcao.RecuperacaoSenha);
}