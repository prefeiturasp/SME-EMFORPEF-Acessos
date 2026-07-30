using AutoMapper;
using MailKit.Net.Smtp;
using MimeKit;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoEmail(IRepositorioConfiguracaoEmail repositorioConfiguracaoEmail, IMapper mapper) : IServicoEmail
    {
        public async Task Enviar(string nomeDestinatario, string emailDestinatario, string assunto, string mensagemHtml, long sistemaId)
        {
            var configuracaoEmail = await ObterConfiguracaoEmail(sistemaId);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(configuracaoEmail.Nome, configuracaoEmail.Email));
            message.To.Add(new MailboxAddress(nomeDestinatario, emailDestinatario));
            message.Subject = assunto;

            message.Body = new TextPart("html")
            {
                Text = mensagemHtml
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(configuracaoEmail.Smtp, configuracaoEmail.Porta, configuracaoEmail.TLS);
            await client.AuthenticateAsync(configuracaoEmail.Usuario, configuracaoEmail.Senha);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        public async Task<ConfiguracaoEmailDTO> ObterConfiguracaoEmail(long sistemaId)
        {
            var configuracoes = await repositorioConfiguracaoEmail.ObterConfiguracaoEmailPorSistema(sistemaId);
            return configuracoes == null
                ? throw new NegocioException(MensagemNegocio.NAO_LOCALIZADO_CONFIGURACAO_EMAIL)
                : mapper.Map<ConfiguracaoEmailDTO>(configuracoes);
        }
    }
}
