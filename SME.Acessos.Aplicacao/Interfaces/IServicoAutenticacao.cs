using SME.Acessos.Aplicacao.DTO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoAutenticacao
    {
        Task<RetornoUsuarioCdepDto> Autenticar(string login, string senha);
    }
}
