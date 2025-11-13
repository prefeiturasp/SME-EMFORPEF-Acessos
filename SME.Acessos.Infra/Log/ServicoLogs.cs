using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Registry;
using SME.Acessos.Infra.Dominio.Enumeradores;
using SME.Acessos.Infra.Polly;
using System.Text;

namespace SME.Acessos.Infra.Servicos
{
    public class ServicoLogs(IConexoesRabbitLogs conexoesRabbit, IReadOnlyPolicyRegistry<string> registry, ILogger<ServicoLogs> logger) : IServicoLogs
    {
        private readonly IConexoesRabbitLogs conexoesRabbit = conexoesRabbit ?? throw new ArgumentNullException(nameof(conexoesRabbit));
        private readonly IAsyncPolicy policy = registry.Get<IAsyncPolicy>(PoliticaPolly.PublicaFila);

        public async Task Enviar(string mensagem, LogContexto contexto = LogContexto.Geral, LogNivel nivel = LogNivel.Critico, string observacao = "", string rastreamento = "", Exception? exception = null)
        {
            if (exception != null)
            {
                logger.LogError(exception, "{mensagem}", mensagem);
            }
            else
            {
                logger.LogInformation("{mensagem}", mensagem);
            }
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
