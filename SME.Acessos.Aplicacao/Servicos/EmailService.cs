using MailKit.Net.Smtp;
using MimeKit;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.Aplicacao
{
    public class ServicoEmail : IServicoEmail
    {
        private readonly IRepositorioConfiguracaoEmail repositorioConfiguracaoEmail;

        public ServicoEmail(IRepositorioConfiguracaoEmail repositorioConfiguracaoEmail)
        {
            this.repositorioConfiguracaoEmail = repositorioConfiguracaoEmail ?? throw new ArgumentNullException(nameof(repositorioConfiguracaoEmail));
        }

        public async Task Enviar(string nomeDestinatario, string emailDestinatario, string assunto, string mensagemHtml)
        {
            var configuracaoEmail = await ObterConfiguracaoEmail();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(configuracaoEmail.Nome, configuracaoEmail.Email));
            message.To.Add(new MailboxAddress(nomeDestinatario, emailDestinatario));
            message.Subject = assunto;

            message.Body = new TextPart("html")
            {
                Text = mensagemHtml
            };

            using (var client = new SmtpClient())
            {
                client.Connect(configuracaoEmail.Smtp, configuracaoEmail.Porta, configuracaoEmail.TLS);

                client.Authenticate(configuracaoEmail.Usuario, configuracaoEmail.Senha);

                client.Send(message);
                client.Disconnect(true);
            }
        }

        private async Task<ConfiguracaoEmail> ObterConfiguracaoEmail()
        {
            var configuracoes = await repositorioConfiguracaoEmail.ObterTodos();

            if (configuracoes == null || !configuracoes.Any())
                throw new NegocioException(MensagemNegocio.NAO_LOCALIZADO_CONFIGURACAO_EMAIL);

            return configuracoes.FirstOrDefault();
        }
    }
}
