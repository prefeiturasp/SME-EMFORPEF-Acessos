using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;

namespace SME.Acessos.TesteIntegracao.ServicosFakes
{
    public class ServicoEmailFake : IServicoEmail
    {
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
