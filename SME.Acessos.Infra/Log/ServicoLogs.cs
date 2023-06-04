using Newtonsoft.Json;
using Polly;
using Polly.Registry;
using SME.Acessos.Infra.Dominio;
using SME.Acessos.Infra.Polly;
using System.Text;

namespace SME.Acessos.Infra.Servicos
{
    public class ServicoLogs : IServicoLogs
    {
        private readonly IConexoesRabbitLogs conexoesRabbit;
        private readonly IAsyncPolicy policy;

        public ServicoLogs(IConexoesRabbitLogs conexoesRabbit, IReadOnlyPolicyRegistry<string> registry)
        {
            this.conexoesRabbit = conexoesRabbit ?? throw new ArgumentNullException(nameof(conexoesRabbit));
            this.policy = registry.Get<IAsyncPolicy>(PoliticaPolly.PublicaFila);
        }

        public async Task Enviar(string mensagem, LogContexto contexto = LogContexto.Geral, LogNivel nivel = LogNivel.Critico, string observacao = "", string rastreamento = "")
        {
            var logMensagem = JsonConvert.SerializeObject(new LogMensagem(mensagem, contexto.ToString(), nivel.ToString(), observacao, rastreamento),
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            var body = Encoding.UTF8.GetBytes(logMensagem);

            await policy.ExecuteAsync(async () 
                => await PublicarMensagem(body));
        }

        private Task PublicarMensagem(byte[] body)
        {
            var channel = conexoesRabbit.Get();
            try
            {
                var props = channel.CreateBasicProperties();
                props.Persistent = true;

                channel.BasicPublish(ExchangeRabbit.Logs, RotasRabbitLogs.RotaLogs, true, props, body);
            }
            finally
            {
                conexoesRabbit.Return(channel);
            }

            return Task.CompletedTask;
        }
    }
}
