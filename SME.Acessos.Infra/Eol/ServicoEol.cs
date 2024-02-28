namespace SME.Acessos.Infra.Servicos.Eol
{
    public class ServicoEol : IServicoEol
    {
        private readonly HttpClient _httpClient;

        public ServicoEol(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<bool> VerificarFuncionarioAtivo(string registroFuncional)
        {
            var resposta = await _httpClient.GetAsync(string.Format(ServicoEolConstantes.VERIFICAR_FUNCIONARIO_ATIVO, registroFuncional));

            if (!resposta.IsSuccessStatusCode)
                throw new Exception("Erro ao verificar funcionário ativo no eol");

            var retorno = await resposta.Content.ReadAsStringAsync();
            return Boolean.Parse(retorno);
        }
    }
}
