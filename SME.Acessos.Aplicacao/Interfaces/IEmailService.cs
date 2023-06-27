
namespace SME.Acessos.Aplicacao.Interfaces
{
    public interface IServicoEmail
    {
        Task Enviar(string destinatario, string assunto, string mensagemHtml);
    }
}
