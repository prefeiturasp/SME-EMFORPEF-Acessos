
namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoEmail
    {
        Task Enviar(string nomeDestinatario, string emailDestinatario, string assunto, string mensagemHtml);
    }
}
