using AutoMapper;
using MailKit.Net.Smtp;
using MimeKit;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.Aplicacao
{
    public class ServicoEmail : IServicoEmail
    {
        private readonly IRepositorioConfiguracaoEmail repositorioConfiguracaoEmail;
        private readonly IMapper mapper;

        public ServicoEmail(IRepositorioConfiguracaoEmail repositorioConfiguracaoEmail, IMapper mapper)
        {
            this.repositorioConfiguracaoEmail = repositorioConfiguracaoEmail ?? throw new ArgumentNullException(nameof(repositorioConfiguracaoEmail));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

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
            if (configuracoes == null)
                throw new NegocioException(MensagemNegocio.NAO_LOCALIZADO_CONFIGURACAO_EMAIL);

            return mapper.Map<ConfiguracaoEmailDTO>(configuracoes);
        }
    }
}
