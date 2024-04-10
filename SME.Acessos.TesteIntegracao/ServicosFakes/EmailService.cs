using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;

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

        public Task<ConfiguracaoEmailDTO> ObterConfiguracaoEmail(long sistemaId)
        {
            return Task.FromResult(new ConfiguracaoEmailDTO());
        }
    }
}
