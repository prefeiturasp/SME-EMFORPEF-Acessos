using MailKit.Net.Smtp;
using MimeKit;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.TesteIntegracao.ServicosFakes
{
    public class ServicoEmailFake : IServicoEmail
    {
        private readonly IRepositorioConfiguracaoEmail repositorioConfiguracaoEmail;

        public ServicoEmailFake(IRepositorioConfiguracaoEmail repositorioConfiguracaoEmail)
        {
            this.repositorioConfiguracaoEmail = repositorioConfiguracaoEmail ?? throw new ArgumentNullException(nameof(repositorioConfiguracaoEmail));
        }

        public Task Enviar(string nomeDestinatario, string emailDestinatario, string assunto, string mensagemHtml, long sistemaId)
        {
            return Task.CompletedTask;
        }
    }
}
