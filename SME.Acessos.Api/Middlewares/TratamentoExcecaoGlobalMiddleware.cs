using Newtonsoft.Json;
using SME.Acessos.Infra.Servicos;
using System.Net;
using SME.Acessos.Infra.Dominio.Enumeradores;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.Api
{
    public class TratamentoExcecaoGlobalMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IServicoLogs servicoLogs;

        public TratamentoExcecaoGlobalMiddleware(RequestDelegate next, IServicoLogs servicoLogs)
        {
            this.next = next;
            this.servicoLogs = servicoLogs ?? throw new ArgumentNullException(nameof(servicoLogs));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (NegocioException nex)
            {
                await servicoLogs.Enviar(nex.Message, observacao: nex.Message, rastreamento: nex.StackTrace);
                await TratarExcecao(context, nex.Message);
            }
            catch (Exception ex)
            {
                var mensagem = "Houve um comportamento inesperado do sistema de Acessos. Por favor, contate a SME.";
                await servicoLogs.Enviar(mensagem, observacao: ex.Message, rastreamento: ex.StackTrace);
                await TratarExcecao(context, mensagem);
            }
        }

        private async Task TratarExcecao(HttpContext context, string mensagem, int statusCode = (int)HttpStatusCode.InternalServerError)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(mensagem));
        }
    }

    public static class TratamentoExcecaoGlobalMiddlewareExtensions
    {
        public static IApplicationBuilder UseTratamentoExcecoesGlobalMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TratamentoExcecaoGlobalMiddleware>();
        }
    }

}
