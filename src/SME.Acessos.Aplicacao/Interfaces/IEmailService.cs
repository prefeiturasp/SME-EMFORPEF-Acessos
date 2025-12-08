using SME.Acessos.Aplicacao.DTO;

namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoEmail
    {
        Task Enviar(string nomeDestinatario, string emailDestinatario, string assunto, string mensagemHtml, long sistemaId);
        Task<ConfiguracaoEmailDTO> ObterConfiguracaoEmail(long sistemaId);
    }
}
