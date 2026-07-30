using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace SME.Acessos.Api.Middlewares
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    [ExcludeFromCodeCoverage]
    public class ChaveIntegracaoApiAttribute : Attribute, IAsyncActionFilter
    {
        private const string ChaveIntegracaoHeader = "x-api-acessos-key";
        private const string ChaveIntegracaoEnvironmentVariableName = "ChaveIntegracaoApi";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var chaveApi = Environment.GetEnvironmentVariable(ChaveIntegracaoEnvironmentVariableName);

            if (!context.HttpContext.Request.Headers.TryGetValue(ChaveIntegracaoHeader, out var chaveRecebida) ||
                !chaveRecebida.Equals(chaveApi))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
