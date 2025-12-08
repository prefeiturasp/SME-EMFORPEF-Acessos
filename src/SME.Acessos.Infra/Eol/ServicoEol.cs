namespace SME.Acessos.Infra.Servicos.Eol
{
    public class ServicoEol(HttpClient httpClient) : IServicoEol
    {
        public async Task<bool> VerificarFuncionarioAtivo(string registroFuncional)
        {
            var resposta = await httpClient.GetAsync(string.Format(ServicoEolConstantes.VERIFICAR_FUNCIONARIO_ATIVO, registroFuncional));

            if (!resposta.IsSuccessStatusCode)
                throw new Exception("Erro ao verificar funcionário ativo no eol");

            var retorno = await resposta.Content.ReadAsStringAsync();
            return bool.Parse(retorno);
        }
    }
}