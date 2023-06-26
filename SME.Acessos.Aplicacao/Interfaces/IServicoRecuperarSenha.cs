using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Infra.Dominio.CoreSSO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoRecuperarSenha
    {
        Task<string> RecuperarSenha(string login, int sistema);
    }
}
