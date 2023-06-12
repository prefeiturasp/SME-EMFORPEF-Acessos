using SME.Acessos.Aplicacao.DTO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoAutenticacaoCdep
    {
        Task<RetornoUsuarioCdepDTO> ObterPerfisToken(RetornoAutenticacaoDTO retornoAutenticacao);
    }
}
