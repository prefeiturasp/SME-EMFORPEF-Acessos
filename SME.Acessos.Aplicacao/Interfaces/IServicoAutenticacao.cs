using SME.Acessos.Aplicacao.DTO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoAutenticacao
    {
        Task<RetornoAutenticacaoDTO> Autenticar(string login, string senha);
    }
}
