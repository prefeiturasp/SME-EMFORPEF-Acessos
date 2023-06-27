using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.Aplicacao
{
    public class ServicoEmail : IServicoEmail
    {
        private readonly IConfiguracaoEmailRepository configuracaoEmailRepository;

        public ServicoEmail(IConfiguracaoEmailRepository configuracaoEmailRepository)
        {
            this.configuracaoEmailRepository = configuracaoEmailRepository ?? throw new ArgumentNullException(nameof(configuracaoEmailRepository));
        }

        public async Task Enviar(string destinatario, string assunto, string mensagemHtml)
        {
            var configuracaoEmail = await ObterConfiguracaoEmail();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(configuracaoEmail.NomeRemetente, configuracaoEmail.EmailRemetente));
            message.To.Add(new MailboxAddress(destinatario));
            message.Subject = assunto;

            message.Body = new TextPart("html")
            {
                Text = mensagemHtml
            };

            using (var client = new SmtpClient())
            {
                client.Connect(configuracaoEmail.ServidorSmtp, configuracaoEmail.Porta, configuracaoEmail.UsarTls);

                client.Authenticate(configuracaoEmail.Usuario, configuracaoEmail.Senha);

                client.Send(message);
                client.Disconnect(true);
            }
        }

        private async Task<ConfiguracaoEmail> ObterConfiguracaoEmail()
        {
            var configuracoes = await configuracaoEmailRepository.ObterTodos();

            if (configuracoes == null || !configuracoes.Any())
                throw new NegocioException(MensagemNegocio.NAO_LOCALIZADO_CONFIGURACAO_EMAIL);

            return configuracoes.First();
        }
    }
}
